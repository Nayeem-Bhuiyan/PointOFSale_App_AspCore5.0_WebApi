using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PointOFSale_App.Models;
using PointOFSale_App.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace PointOFSale_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommonData : ControllerBase
    {
        private readonly PointOfSellDbContext db;

        public CommonData(PointOfSellDbContext _db)
        {
            this.db = _db;
        }

        

        [HttpGet]
        [ActionName("LoadEmployeeList")]
        public async Task<ActionResult<IEnumerable<Employee>>> LoadEmployeeList()
        {
            List<Employee> ListData = new List<Employee>();
            var responseData = await db.Employee.Select(e => new { e.EmpAddress, e.EmployeeId, e.EmpMobileNo, e.EmpName, e.GenderId, e.ReligionId, e.ThanaId }).ToListAsync();

            foreach (var item in responseData)
            {
                Employee emp = new Employee()
                {
                    EmployeeId=item.EmployeeId,
                    EmpAddress=item.EmpAddress,
                    EmpMobileNo=item.EmpMobileNo,
                    EmpName=item.EmpName,
                    GenderId=item.GenderId,
                    ThanaId=item.ThanaId,
                    ReligionId=item.ReligionId
                };

                ListData.Add(emp);
            }
            if (ListData == null)
            {
                return NotFound();
            }

            return ListData;
        }

        [HttpPost]
        [ActionName("SearchEmployee")]
        public async Task<ActionResult<Employee>> SearchEmployee(Employee empObj)
        {
            var item = await db.Employee.Where(e => e.EmployeeId == empObj.EmployeeId || e.EmpMobileNo == empObj.EmpMobileNo || e.EmpName.ToLower().StartsWith(empObj.EmpName.ToLower()) || e.Religion.ReligionName.ToLower().StartsWith(empObj.Religion.ReligionName.ToLower())).Select(e=>new { e.EmpAddress,e.EmployeeId,e.EmpMobileNo,e.EmpName,e.GenderId,e.ReligionId,e.ThanaId}).FirstOrDefaultAsync();

            Employee emp = new Employee()
                {
                    EmployeeId=item.EmployeeId,
                    EmpAddress=item.EmpAddress,
                    EmpMobileNo=item.EmpMobileNo,
                    EmpName=item.EmpName,
                    GenderId=item.GenderId,
                    ThanaId=item.ThanaId,
                    ReligionId=item.ReligionId
                };
        
            
            if (emp == null)
            {
                return NotFound();
            }

            return emp;
        }

        [HttpPost]
        [ActionName("SearchCustomer")]
        public async Task<ActionResult<Customer>> SearchCustomer(VmSearchParams customer)
        {

            var data = await db.Customer.Where(c => c.CustomerMobileNo == customer.CustomerMobileNo || c.CustomerId==customer.CustomerId).Select(e=>new { e.Address,e.CustomerId,e.CustomerMobileNo,e.CustomerName}).FirstOrDefaultAsync();

            Customer cm = new Customer()
            {
                CustomerId=data.CustomerId,
                Address=data.Address,
                CustomerMobileNo=data.CustomerMobileNo,
                CustomerName=data.CustomerName
            };
            
            if (cm == null)
            {
                return NotFound();
            }

            return cm;
        }

        [HttpGet]
        [ActionName("LoadCustomerList")]
        public async Task<ActionResult<IEnumerable<Customer>>> LoadCustomerList()
        {
            List<Customer> listCustomer = new List<Customer>();
            
            var responseData = await db.Customer.Select(e => new { e.Address, e.CustomerId, e.CustomerMobileNo, e.CustomerName }).ToListAsync();

            foreach (var data in responseData)
            {
                Customer cm = new Customer()
                {
                    CustomerId = data.CustomerId,
                    Address = data.Address,
                    CustomerMobileNo = data.CustomerMobileNo,
                    CustomerName = data.CustomerName
                };

                listCustomer.Add(cm);
            }
            if (listCustomer == null)
            {
                return NotFound();
            }

            return listCustomer;
        }

        [HttpGet]
        [ActionName("LoadReligionList")]
        public async Task<ActionResult<IEnumerable<Religion>>> LoadReligionList()
        {
            return await db.Religion.ToListAsync();
        }

        [HttpGet]
        [ActionName("LoadGenderList")]
        public async Task<ActionResult<IEnumerable<Gender>>> LoadGenderList()
        {
            return await db.Gender.ToListAsync();
        }

        [HttpGet]
        [ActionName("LoadProductList")]
        public async Task<ActionResult<IEnumerable<Product>>> LoadProductList()
        {
            List<Product> proList = new List<Product>();
           var data= await db.Product.Select(s => new { s.ProductId, s.ProductName }).ToListAsync();
            foreach (var item in data)
            {
                Product pd = new Product()
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName
                };
                proList.Add(pd);
            }

            return proList;
        }


        [HttpGet("{id}")]
        [ActionName("LoadBrandListByProductId")]
        public async Task<ActionResult<IEnumerable<Brand>>> LoadBrandListByProductId(int id)
        {
            List<Brand> ListBrandByProductId = new List<Brand>();

            var brands = await db.Brand.Where(b => b.ProductId == id).Select(b => new { b.BrandId, b.BrandName, b.ProductId }).ToListAsync();
            foreach (var item in brands)
            {
                Brand brand = new Brand
                {
                    BrandName = item.BrandName,
                    BrandId = item.BrandId,
                    ProductId = item.ProductId
                };

                ListBrandByProductId.Add(brand);

            }

            if (brands == null)
            {
                return NotFound();
            }

            return ListBrandByProductId;
        }




        [HttpGet("{id}")]
        [ActionName("LoadCategoryListByBrandId")]
        public async Task<ActionResult<IEnumerable<Category>>> LoadCategoryListByBrandId(int id)
        {
            List<Category> ListCategory = new List<Category>();
            
            var categoryListByBrandId = await db.Category.Where(c => c.BrandId == id).Select(c=>new {c.BrandId,c.CategoryId,c.CategoryName }).ToListAsync();

            foreach (var data in categoryListByBrandId)
            {
                Category ct = new Category()
                {
                    CategoryId = data.CategoryId,
                    CategoryName = data.CategoryName,
                    BrandId = data.BrandId
                };
                ListCategory.Add(ct);
            }
            
            if (ListCategory == null)
            {
                return NotFound();
            }
            return ListCategory;
        }

        [HttpGet]
        [ActionName("LoadCountryList")]
        public async Task<ActionResult<IEnumerable<Country>>> LoadCountryList()
        {
            return await db.Country.ToListAsync();
        }

        [HttpGet("{id}")]
        [ActionName("LoadDistrictListByCountryId")]
        public async Task<ActionResult<IEnumerable<District>>> LoadDistrictListByCountryId(int id)
        {
            List<District> ListDistrict = new List<District>();
            var DistrictListByCountryId = await db.District.Where(d => d.CountryId == id).Select(d=>new {d.CountryId,d.DistrictId,d.DistrictName }).ToListAsync();
            foreach (var data in DistrictListByCountryId)
            {
                District dt = new District()
                {
                    DistrictId=data.DistrictId,
                    DistrictName=data.DistrictName,
                    CountryId=data.CountryId
                };
                ListDistrict.Add(dt);
            }
            
            
            if (ListDistrict == null)
            {
                return NotFound();
            }
            return ListDistrict;
        }


        [HttpGet("{id}")]
        [ActionName("LoadThanaListByDistrictId")]
        public async Task<ActionResult<IEnumerable<Thana>>> LoadThanaListByDistrictId(int id)
        {
            List<Thana> ObjThanaList = new List<Thana>();
            
            var ThanaListByDistrictId = await db.Thana.Where(d => d.DistrictId == id).Select(t=>new {t.DistrictId,t.ThanaId,t.ThanaName }).ToListAsync();
            

            foreach (var data in ThanaListByDistrictId)
            {
                Thana ObjOfthana = new Thana()
                {
                    DistrictId = data.DistrictId,
                    ThanaId = data.ThanaId,
                    ThanaName = data.ThanaName
                };

                ObjThanaList.Add(ObjOfthana);
            }

            


            if (ObjThanaList == null)
            {
                return NotFound();
            }
            return ObjThanaList;
        }



    }
}
