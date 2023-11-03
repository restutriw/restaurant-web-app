using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantWebApp.Models;

namespace RestaurantWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionsContext _context;

        public TransactionsController(TransactionsContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transactions>>> GetTransactions()
        {
            var transactions = await _context.Transactions.Include(t => t.Customer).Include(t => t.Food).ToListAsync();
            var transactionList = transactions.Select(t => new
            {
                transactionId = t.transactionId,
                customerId = t.customerId,
                customer = new
                {
                    customerId = t.Customer.customerId,
                    customerName = t.Customer.customerName,
                    email = t.Customer.email,
                    phone = t.Customer.phone
                },
                foodId = t.foodId,
                food = new
                {
                    foodId = t.Food.foodId,
                    foodName = t.Food.foodName,
                    stock = t.Food.stock,
                    price = t.Food.price
                },
                totalPrice = t.totalPrice,
                transactionDate = t.transactionDate
            });

            return Ok(transactionList);
            //return await _context.Transactions.ToListAsync();
        }

        // GET: api/Transactions/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Transactions>> GetTransactions(int id)
        {
            var transactions = await _context.Transactions
                .Include(t => t.Customer)
                .Include(t => t.Food)
                .FirstOrDefaultAsync(t => t.transactionId == id);


            if (transactions == null)
            {
                return NotFound();
            }

            var transaction = new
            {
                transactionId = transactions.transactionId,
                customerId = transactions.customerId,
                customer = new
                {
                    customerId = transactions.Customer.customerId,
                    customerName = transactions.Customer.customerName,
                    email = transactions.Customer.email,
                    phone = transactions.Customer.phone
                },
                foodId = transactions.foodId,
                food = new
                {
                    foodId = transactions.Food.foodId,
                    foodName = transactions.Food.foodName,
                    stock = transactions.Food.stock,
                    price = transactions.Food.price
                },
                totalPrice = transactions.totalPrice,
                transactionDate = transactions.transactionDate
            };
            return Ok(transaction);
        }

        // PUT: api/Transactions/1
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactions(int id, Transactions transactions)
        {
            if (id != transactions.transactionId)
            {
                return BadRequest();
            }

            _context.Entry(transactions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionsExists(id))
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

        // POST: api/Transactions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Transactions>> PostTransactions(Transactions transactions)
        {
            _context.Transactions.Add(transactions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransactions", new { id = transactions.transactionId }, transactions);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Transactions>> DeleteTransactions(int id)
        {
            var transactions = await _context.Transactions.FindAsync(id);
            if (transactions == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transactions);
            await _context.SaveChangesAsync();

            return transactions;
        }

        private bool TransactionsExists(int id)
        {
            return _context.Transactions.Any(e => e.transactionId == id);
        }
    }
}