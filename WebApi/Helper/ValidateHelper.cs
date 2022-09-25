using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Helper
{
    public class ValidateHelper
    {
        /// <summary>
        /// Validate File Format
        /// return false if invalid
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool ValidateFile(string filename)
        {
            string[] validFileTypes = { ".xls", ".xlsx", ".csv" };
            return validFileTypes.Contains(filename);
        }

        /// <summary>
        /// Check If MeterReadingDateTime is valid DateTime
        /// Return Datetime MinValue if invalid
        /// </summary>
        /// <param name="MeterReadingDateTime"></param>
        /// <returns></returns>
        public DateTime ValidateReadingDate(string MeterReadingDateTime)
        {
            DateTime getdate;
            if ((DateTime.TryParse(MeterReadingDateTime, out getdate)))
            {
                return getdate;
            }
            else if (DateTime.TryParseExact(MeterReadingDateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out getdate))
            {
                return getdate;
            }
            else if (DateTime.TryParseExact(MeterReadingDateTime, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out getdate))
            {
                return getdate;
            }
            else if (DateTime.TryParseExact(MeterReadingDateTime, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out getdate))
            {
                return getdate;
            }
            else if (DateTime.TryParseExact(MeterReadingDateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out getdate))
            {
                return getdate;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Check If MeterReadValue is valid int (between 0 and 99999)
        /// Return -1 if invalid
        /// </summary>
        /// <param name="MeterReadValue"></param>
        /// <returns></returns>
        public int ValidateReadingValue(string MeterReadValue)
        {
            if (int.TryParse(MeterReadValue, out int getvalue))
            {
                return (getvalue >= 0 && getvalue <= 99999) ? getvalue : -1;
            }
            return -1;
        }

        private readonly AccountMeterDbContext _context;

        /// <summary>
        /// Check If Accountid is valid account
        /// Return False if invalid or not found
        /// </summary>
        /// <param name="accountid"></param>
        /// <returns></returns>
        public bool ValidateAccountId(string accountid)
        {
            

            if (int.TryParse(accountid.Trim('"').Replace("'", ""), out int getid0))
            {
                Console.WriteLine(getid0);
                var s = _context.EnsekAccounts.Any(p => p.AccountId == getid0 && p.Status == 1);
            }

            return int.TryParse(accountid.Trim('"').Replace("'", ""), out int getid) ? _context.EnsekAccounts.Any(p => p.AccountId == getid && p.Status == 1) : false;
        }
    }
}
