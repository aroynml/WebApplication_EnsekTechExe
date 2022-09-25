using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApi.Models
{
    /// <summary>
    /// Database for EnsekMeterReading 
    /// </summary>
    public class EnsekMeterReading
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime UploadReadTime { get; set; }
        public int UploadReadValue { get; set; }
        public string? UploadReadRemark { get; set; }
        //public EnsekAccounts EnsekAccounts { get; set; }
    }
}
