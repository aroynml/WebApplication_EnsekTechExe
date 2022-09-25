using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Data
{
    public class AccountMeterDbContext : DbContext
    {

        public AccountMeterDbContext(DbContextOptions<AccountMeterDbContext> options) : base(options)
        {
        }
        public DbSet<EnsekAccounts> EnsekAccounts { get; set; }
        public DbSet<EnsekMeterReading> EnsekMeterReading { get; set; }

        public void MarkAsModified(EnsekMeterReading meterreading)
        {
            Entry(meterreading).State = EntityState.Modified;
        }

    }
}
