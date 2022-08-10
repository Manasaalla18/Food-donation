using Microsoft.EntityFrameworkCore;
using FoodDonation.Models;

namespace FoodDonation.Models
{
    public class FDContext : DbContext
    {
        public FDContext()
        {

        }
        public FDContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("dbConn"));
        }

        public DbSet<UserMaster> Users { get; set; }
        public DbSet<Donate> Donation { get; set; }
        public DbSet<FoodRequest> Requests { get; set; }
        public DbSet<LogisticModel> Logistics { get; set; }
        public DbSet<AdminMsg> AdminMsgs { get; set; }
        public DbSet<FoodDonation.Models.LoginModel>? LoginModel { get; set; }
        public DbSet<ResetPassword> ResetPassword { get; set; }
        public DbSet<FeedBack> FeedBack { get; set; }


    }
}
