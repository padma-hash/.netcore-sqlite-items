using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ItemsAPI.Model;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace ItemsAPI.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Items> Item { get; set; }
        private readonly IConfiguration _config;

        public ApplicationDbContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlite(_config.GetConnectionString("cs"));

            }
        }

    }

}
