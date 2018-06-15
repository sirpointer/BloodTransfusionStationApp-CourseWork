namespace BloodTransfusionStationApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class UsersModel : DbContext
    {
        public UsersModel()
            : base("name=UsersModel")
        {
        }

        public virtual DbSet<DataOfUser> DataOfUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
