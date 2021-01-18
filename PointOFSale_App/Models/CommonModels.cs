using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PointOFSale_App.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required]
        [StringLength(50,ErrorMessage ="maximum size exceeded",MinimumLength =3)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        public virtual ICollection<Brand> Brand { get; set; }
    }

    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "maximum size exceeded", MinimumLength = 3)]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
        [ForeignKey("ProductId")]
        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<Category> Category { get; set; }
    }


    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "maximum size exceeded", MinimumLength = 3)]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [ForeignKey("BrandId")]
        [Required]
        public int BrandId { get; set; }


        //[Display(Name = "Picture")]
        //public byte ProuctImage { get; set; }
        //[Display(Name = "Image Path")]
        //public String ProuctImagePath { get; set; }



        public virtual ICollection<ProductEntry> ProductEntry { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrder { get; set; }
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
        public virtual Brand Brand { get; set; }
    }




    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "maximum size exceeded", MinimumLength = 3)]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }
        public virtual ICollection<District> District { get; set; }
    }

    public class District
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DistrictId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "maximum size exceeded", MinimumLength = 3)]
        [Display(Name = "District Name")]
        public string DistrictName { get; set; }
        [ForeignKey("CountryId")]
        [Required]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Thana> Thana { get; set; }
    }


    public class Thana
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ThanaId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "maximum size exceeded", MinimumLength = 3)]
        [Display(Name = "Thana Name")]
        public string ThanaName { get; set; }
        [ForeignKey("DistrictId")]
        [Required]
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }

    public class Gender
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenderId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "maximum size exceeded", MinimumLength = 3)]
        [Display(Name = "Gender Name")]
        public string GenderName { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }

    public class Religion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReligionId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "maximum size exceeded", MinimumLength = 3)]
        [Display(Name = "Religion Name")]
        public string ReligionName { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }



    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "maximum size exceeded", MinimumLength = 3)]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [StringLength(200, ErrorMessage = "maximum size exceeded")]
        public string Address { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "maximum size exceeded", MinimumLength = 11)]
        [Display(Name = "Customer Mobile")]
        public string CustomerMobileNo { get; set; }

        public virtual ICollection<CustomerOrder> CustomerOrder { get; set; }
        public virtual ICollection<CustomerPayment> CustomerPayment { get; set; }
    }


    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "maximum size exceeded", MinimumLength = 3)]
        [Display(Name = "Employee Name")]
        public string EmpName { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "maximum size exceeded", MinimumLength = 3)]
        [Display(Name = "Mobile")]
        public string EmpMobileNo { get; set; }
        [StringLength(200, ErrorMessage = "maximum size exceeded", MinimumLength = 3)]
        public string EmpAddress { get; set; }
        [ForeignKey("ThanaId")]
        public int ThanaId { get; set; }
        [ForeignKey("ReligionId")]
        public int ReligionId { get; set; }
        [ForeignKey("GenderId")]
        public int GenderId { get; set; }

        //[Display(Name = "Photo")]
        //public string Emp_ImagePath { get; set; }
        //[Display(Name = "Picture")]
        //public byte Emp_ByteImage { get; set; }

        public virtual ICollection<ProductEntry> ProductEntry { get; set; }
        public virtual ICollection<Product_Sell> Product_Sell { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
        public virtual Thana Thana { get; set; }
        public virtual Religion Religion { get; set; }
        public virtual Gender Gender { get; set; }


    }
}
