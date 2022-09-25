using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using WebApi.Helper;
using System.Data;
using System.IO;


namespace WebApi.Controllers
{
    //[Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EnsekAccountsController : ControllerBase
    {
        private readonly AccountMeterDbContext _context;


        public EnsekAccountsController(AccountMeterDbContext context)
        {
            _context = context;
        }

        // GET: api/EnsekAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnsekAccounts>>> GetEnsekAccounts()
        {
            return await _context.EnsekAccounts.ToListAsync();
        }

        
        /// <summary>
        /// Upload CSV file into Meter Reading
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<BaseModel.meterreadingupload_result>), 200)]
        [Route("/meter-reading-uploads")]
        public async Task<IActionResult> UploadPointfile(IFormFile file)
        {
            BaseModel.meterreadingupload_result _rtn = new BaseModel.meterreadingupload_result();
            ValidateHelper _helper = new ValidateHelper();

            //Upload File Directory
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "public", "upload", "meterreading");
            string path = "";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            try
            {
                // Check if File Valid
                string fileext = System.IO.Path.GetExtension(file.FileName).ToLower();
                if (file.Length > 0 && _helper.ValidateFile(fileext))
                {
                    path = SetReadingUploadFilePath(folder, System.IO.Path.GetExtension(file.FileName));

                    //Copy CSV 
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                else
                {
                    return NotFound(_rtn);
                }

                //
                DataTable dt = new DataTable();
                dt.Columns.Add("AccountId");
                dt.Columns.Add("MeterReadingDateTime");
                dt.Columns.Add("MeterReadValue");
                dt.Columns.Add("Remarks");

                string csvdata = System.IO.File.ReadAllText(path);
                int rowCount = System.IO.File.ReadAllLines(path).Count();
                List<string> SuccessList = new List<string>();
                List<string> FailedList = new List<string>();
                int listCount = 0;

                foreach (string row in csvdata.Split("\n"))
                {
                    bool issuccessadd = false;
                    DataRow dr = dt.NewRow();
                    int ColCount = 0;

                    foreach (string cell in row.Split(","))
                    {
                        Console.WriteLine(cell);
                        dr[ColCount] = cell.ToString().Trim('"').Replace("'", "").Replace("\"", "").Replace("\r", "");
                        ColCount++;
                    }

                    if (!string.IsNullOrEmpty(dr[0].ToString()) && !dr[0].ToString().Contains("AccountId") && !string.IsNullOrEmpty(dr[1].ToString()) && !string.IsNullOrEmpty(dr[2].ToString()))
                    {
                        DateTime thisreadtime = _helper.ValidateReadingDate(dr[1].ToString().Replace("\"", ""));
                        int thisreadvalue = _helper.ValidateReadingValue(dr[2].ToString().Replace("\"", ""));
                        bool thisaccount = EnsekAccountIdExists(dr[0].ToString().Replace("\"", ""));

                        if (thisaccount && thisreadtime != DateTime.MinValue && thisreadvalue >= 0)
                        {
                            int accountid = Convert.ToInt32(dr[0].ToString());

                            if (!EnsekMeterReadingExists(accountid, thisreadtime, thisreadvalue))
                            {
                                //Create Read Value
                                EnsekMeterReading ensekMeterReading = new EnsekMeterReading
                                {
                                    AccountId = accountid,
                                    UploadReadTime = thisreadtime,
                                    UploadReadValue = thisreadvalue,
                                    UploadReadRemark = (ColCount > 2 && !string.IsNullOrEmpty(dr[3].ToString())) ? dr[3].ToString().Trim() : null
                                };

                                _context.EnsekMeterReading.Add(ensekMeterReading);
                                _context.SaveChanges();
                                SuccessList.Add(ensekMeterReading.Id.ToString());
                                issuccessadd = true;
                            }
                        }
                    }

                    if (!issuccessadd && listCount != 0)
                    {
                        FailedList.Add(listCount.ToString());
                    }
                    listCount++;
                }

                //
                _rtn.success_readings = SuccessList.Count;
                _rtn.failed_readings = FailedList.Count;

            }
            catch (Exception)
            {
                return BadRequest(_rtn);
            }

            return Ok(_rtn);
        }
        
        

        /// <summary>
        /// Set Path for Uploading File
        /// return string 
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        private string SetReadingUploadFilePath(string folder, string filename)
        {
            DateTime dt = DateTime.Now;
            string _hashedfilename = $@"meterreading_{dt.ToString("yyMMddHHmmss")}{filename}";
            return $@"{folder}\{_hashedfilename}";
        }

        /// <summary>
        /// Check If Accountid is valid account
        /// Return False if invalid or not found
        /// </summary>
        /// <param name="accountid"></param>
        /// <returns></returns>
        private bool EnsekAccountIdExists(string accountid)
        {
            if (int.TryParse(accountid.Trim('"').Replace("'", ""), out int getid0))
            {
                Console.WriteLine(getid0);
                var s = _context.EnsekAccounts.Any(p => p.AccountId == getid0 && p.Status == 1);
            }

            return int.TryParse(accountid.Trim('"').Replace("'",""), out int getid) ? _context.EnsekAccounts.Any(p => p.AccountId == getid && p.Status == 1) : false;
        }


        /// <summary>
        /// Check If the readingvalue and date have been added in record
        /// Return False if invalid or not found
        /// </summary>
        /// <param name="accountid"></param>
        /// <param name="thisreadtime"></param>
        /// <param name="meterreadvalue"></param>
        /// <returns></returns>
        private bool EnsekMeterReadingExists(int accountid, DateTime thisreadtime, int meterreadvalue)
        {
            return _context.EnsekMeterReading.Any(r => r.AccountId == accountid && r.UploadReadTime == thisreadtime && r.UploadReadValue == meterreadvalue);
        }


        private bool EnsekAccountsExists(int id)
        {
            return _context.EnsekAccounts.Any(e => e.Id == id);
        }
    }
}
