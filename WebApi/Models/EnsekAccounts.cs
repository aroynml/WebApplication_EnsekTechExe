using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApi.Models
{
    /// <summary>
    /// Database for EnsekAccount 
    /// </summary>
    public class EnsekAccounts
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public DateTime? LastRead { get; set; }
        public int? LastReadValue { get; set; }
        public byte Status { get; set; }
        public DateTime? Lastupdatetime { get; set; }

    }

}
