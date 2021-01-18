using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOFSale_App.Models;
using PointOFSale_App.ViewModel;

namespace PointOFSale_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrdersController : ControllerBase
    {
        private readonly PointOfSellDbContext _context;

        public CustomerOrdersController(PointOfSellDbContext context)
        {
            _context = context;
        }



        // GET: api/CustomerOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VmCustomerOrderDetails>>> GetCustomerOrder()
        {
            List<VmCustomerOrderDetails> vmCustomerOrderList = new List<VmCustomerOrderDetails>();
            Category category = new Category();
            Customer customer = new Customer();
             
          var OrderList = await _context.CustomerOrder.ToListAsync();

            foreach (var data in OrderList)
            {
                category = await _context.Category.Where(c => c.CategoryId == data.CategoryId).FirstOrDefaultAsync();
                customer = await _context.Customer.Where(c => c.CustomerId == data.CustomerId).FirstOrDefaultAsync();
                
                VmCustomerOrderDetails vmCustomerOrder = new VmCustomerOrderDetails()
                {
                    CustomerOrderId = data.CustomerId,
                    Quantity =data.Quantity ,
                    DateOfOrder =data.DateOfOrder ,
                    DeliveryDate =data.DeliveryDate ,
                    DeliveryStatus =null,

                    CategoryId = category.CategoryId ,
                    CategoryName = category.CategoryName,

                    CustomerId = customer.CustomerId,
                    CustomerName = customer.CustomerName,
                    Address = customer.Address,
                    CustomerMobileNo = customer.CustomerMobileNo
                };

                vmCustomerOrderList.Add(vmCustomerOrder);
            }

            return vmCustomerOrderList;



        }

        // GET: api/CustomerOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerOrder>> GetCustomerOrder(int id)
        {
            var customerOrder = await _context.CustomerOrder.FindAsync(id);

            if (customerOrder == null)
            {
                return NotFound();
            }

            return customerOrder;
        }

        // PUT: api/CustomerOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerOrder(int id, CustomerOrder customerOrder)
        {
            if (id != customerOrder.CustomerOrderId)
            {
                return BadRequest();
            }

            _context.Entry(customerOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerOrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CustomerOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerOrder>> PostCustomerOrder(CustomerOrder customerOrder)
        {
            _context.CustomerOrder.Add(customerOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerOrder", new { id = customerOrder.CustomerOrderId }, customerOrder);
        }

        // DELETE: api/CustomerOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerOrder(int id)
        {
            var customerOrder = await _context.CustomerOrder.FindAsync(id);
            if (customerOrder == null)
            {
                return NotFound();
            }

            _context.CustomerOrder.Remove(customerOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerOrderExists(int id)
        {
            return _context.CustomerOrder.Any(e => e.CustomerOrderId == id);
        }
    }
}
