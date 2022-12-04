using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleWebPositionApp.Data;
using SimpleWebPositionApp.Models;

namespace SimpleWebPositionApp.Controllers
{
    public class Product64Controller : Controller
    {
        private readonly ProductDbContext _context;
        private readonly ILogger<Product64Controller> _logger;

        public Product64Controller(ProductDbContext context, ILogger<Product64Controller> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index() {
            return View();
        }

        // GET: ProductFiles/Search
        [HttpPost("[action]")]
        public async Task<IActionResult> Search(SearchBar bar) {
            var top_code = bar.Code;
            if (top_code == null) {
                return RedirectToAction("error", "productfiles", new { errorType = "Δεν δόθηκε κωδικός προς αναζήτηση." });
            }

            var code = await _context.Codes
                .FirstOrDefaultAsync(m => m.TopCode == top_code || m.Barcode == top_code || ((top_code.Length == 3) && m.TopCode == "02.026.0" + top_code) || (top_code.Length == 9 && m.TopCode == modify(top_code)));
            if (code == null)
                return RedirectToAction("error", "productfiles", new { errorType = "Δεν βρέθηκε εγγραφή." });
            var productFile = await _context.Products64.SingleOrDefaultAsync(m => m.TopCode == code.TopCode);

            return productFile == null ? RedirectToAction("error", "productfiles", new { errorType = "Δεν Βρέθηκε εγγραφή." }) : View(productFile);
        }

        public static string modify(string code) {
            StringBuilder builder1 = new();
            for (int i = 0; i < code.Length; i++) {
                builder1.Append(code[i]);
                if (i == 1 || i == 4) builder1.Append('.');
            }
            return builder1.ToString();
        }

        [HttpGet()]
        public IActionResult Error([FromQuery(Name = "errorType")] string type) => View(new ErrorClass { Message = type });


    }
}
