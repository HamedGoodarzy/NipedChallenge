using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NipedWebApi.Data.Model;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace NipedWebApi.Data
{
    public class NipedDbContext : DbContext
    {
        /// <summary>
        /// Update DN command: 
        /// EntityFrameworkCore\add-migration 'MIGRATION-NAME' -Project NipedWebApi -StartupProject NipedWebApi -verbose
        /// EntityFrameworkCore\update-database -Project NipedWebApi -StartupProject NipedWebApi -verbose
        /// EntityFrameworkCore\update-database 0 -Project NipedWebApi -StartupProject NipedWebApi -verbose    --  REVERTS TO EMPTY DB
        /// EntityFrameworkCore\remove-migration -Project NipedWebApi -StartupProject NipedWebApi -verbose  -- REMOVE THE LAST (NOT APPLIED) MIGRATION
        /// 
        /// </summary>
        public NipedDbContext(DbContextOptions<NipedDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var modelAss = typeof(NipedDbContext).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(modelAss);
        }
        public DbSet<Guideline> Guidelines { get; set; }
        //public DbSet<CholesterolGuideline> CholesterolGuidelines { get; set; }
        public DbSet<ValueGuideline> ValueGuidelines { get; set; }
        //public DbSet<TextGuideline> TextGuidelines { get; set; }
        //public DbSet<BloodPressureGuideline> BloodPressureGuidelines { get; set; }
        //public DbSet<BloodPressureThreshold> BloodPressureThresholds { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Bloodwork> Bloodworks { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
    }
}
