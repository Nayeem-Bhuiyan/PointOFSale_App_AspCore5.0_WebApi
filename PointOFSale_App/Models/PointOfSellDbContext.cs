using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PointOFSale_App.Models;


namespace PointOFSale_App.Models
{
    public class PointOfSellDbContext : DbContext
    {
        public PointOfSellDbContext(DbContextOptions<PointOfSellDbContext> options) : base(options)
        {
           
        }

        public DbSet<Product> Product {get;set;}
        public DbSet<Brand> Brand {get;set;}
        public DbSet<Category> Category {get;set;}
        public DbSet<Country> Country { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Thana> Thana { get; set; }
        public DbSet<Gender> Gender  { get; set; }
        public DbSet<Religion> Religion  { get; set; }
        public DbSet<Employee> Employee  { get; set; }
        public DbSet<Customer> Customer  { get; set; }
        public DbSet<ProductEntry> ProductEntry  { get; set; }
        public DbSet<Product_Sell> Product_Sell  { get; set; }
        public DbSet<CustomerPayment> CustomerPayment { get; set; }
        public DbSet<CustomerOrder> CustomerOrder { get; set; }
        public DbSet<ProductOrder> ProductOrder { get; set; }
        public DbSet<Payment> Payment { get; set; }

    }
}
