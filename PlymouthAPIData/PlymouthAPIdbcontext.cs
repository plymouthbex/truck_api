using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlymouthAPIData
{
    public class PlymouthAPIdbcontext : DbContext
    {
        public PlymouthAPIdbcontext(DbContextOptions<PlymouthAPIdbcontext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var configVI_TRUCK = modelBuilder.Entity<tblTruck>();
            configVI_TRUCK.ToTable("TRUCK");

            var configVI_ROUTE = modelBuilder.Entity<tblRoute>();
            configVI_ROUTE.ToTable("ROUTE");

            var configVI_DRIVER = modelBuilder.Entity<tblDriver>();
            configVI_DRIVER.ToTable("DRIVER");

            var ConfigComp = modelBuilder.Entity<tblCompany>();
            ConfigComp.ToTable("COMPANYMASTER");

            var configVI_LROUTE = modelBuilder.Entity<tblRoute>();
            configVI_LROUTE.ToTable("VI_ROUTE");

            var config_DOOR = modelBuilder.Entity<tblDoor>();
            config_DOOR.ToTable("DOOR");

            var config_truckheader = modelBuilder.Entity<tblTruckHeader>();
            config_truckheader.ToTable("TRUCKHEADER");

            var config_VI_TRUCKLISTVIEW = modelBuilder.Entity<tblTruckDoorDriverRecord>();
            config_VI_TRUCKLISTVIEW.ToTable("VI_TRUCKLISTVIEW");

            var config_VI_TRUCKDETAIL = modelBuilder.Entity<tblEnquiytList>();
            config_VI_TRUCKDETAIL.ToTable("VI_TRUCKDETAIL");

            var config_TRUCKDETAIL = modelBuilder.Entity<tblTruckDetail>();
            config_TRUCKDETAIL.ToTable("TRUCKDETAIL");

        }
        public DbSet<tblTruck> dtTruck { get; set; }
        public DbSet<tblDriver> dtDriver { get; set; }
        public DbSet<tblRoute> dtRoute { get; set; }
        public DbSet<tblCompany> dtComp { get; set; }
        public DbSet<tblDoor> dtDoor { get; set; }
        public DbSet<tblTruckDoorDriverRecord> dtTruckListView { get; set; }
        public DbSet<tblEnquiytList> dtEnquiryList { get; set; }
        public DbSet<tblTruckHeader> dttruckHeaders { get; set; }
        public DbSet<tblTruckDetail> dtTruckDetail{ get; set; }
        //public DbSet<TruckDetail> TruckDetails { get; set; }

    }

}
