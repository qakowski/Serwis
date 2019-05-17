using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Serwis.Models;

namespace Serwis.Controllers
{
    public class CustomersController : Controller
    {
        Service _context = new Service();
        private readonly IHostingEnvironment _appEnvironment;
        public CustomersController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            _context.Customers.Load();   
        }

        // GET: Customers
        public async Task<IActionResult> Index(string name, string number)
        {
            return View(_context.Customers.Where(x => (x.ClientForeName + " " + x.ClientSureName).Contains(name) || name == null).Where(x => x.SerialNumber.Contains(number) || number == null).ToList());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .SingleOrDefaultAsync(m => m.SerialNumber == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Login()
        {
            return Redirect("Login/Login");
        }



        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SerialNumber,ClientForeName,ClientSureName,AcceptanceDate,IssueDescription,State,Photo")] Models.Customers customers, IFormFile file) 
        {

       

            var filePath = Path.GetTempFileName();
            
            string pathToImages = _appEnvironment.WebRootPath+ "/images/";

            using (var stream = new FileStream(pathToImages, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            customers.Photo = filePath;





            string acceptTmp = customers.AcceptanceDate.ToString();
            if(DateTime.TryParse(acceptTmp, out DateTime tempDate)){
                if (ModelState.IsValid)
                {

                    _context.Add(customers);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));


                }
            }
        else
            {
                if (ModelState.IsValid)
                {
                    customers.AcceptanceDate = DateTime.Now;
                    _context.Add(customers);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));


                }

            }
            return View(customers);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers.SingleOrDefaultAsync(m => m.SerialNumber == id);
            if (customers == null)
            {
                return NotFound();
            }
            return View(customers);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SerialNumber,ClientForeName,ClientSureName,AcceptanceDate,IssueDescription,State,Photo")] Models.Customers customers)
        {
            if (id != customers.SerialNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomersExists(customers.SerialNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customers);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .SingleOrDefaultAsync(m => m.SerialNumber == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customers = await _context.Customers.SingleOrDefaultAsync(m => m.SerialNumber == id);
            _context.Customers.Remove(customers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomersExists(string id)
        {
            return _context.Customers.Any(e => e.SerialNumber == id);
        }
    }
}
