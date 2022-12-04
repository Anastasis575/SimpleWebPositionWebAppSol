using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleWebPositionApp.Data;
using SimpleWebPositionApp.Models;
using System.Text;

namespace SimpleWebPositionApp.Controllers {
    public class Product68Controller : Controller {
        private readonly ProductDbContext _context;
        private readonly ILogger<Product68Controller> logger;


        public Product68Controller(ProductDbContext context, ILogger<Product68Controller> _logger) {
            _context = context;
            logger = _logger;
        }
        public IActionResult Index() {
            return View();
        }

        // GET: ProductFiles/Search
        [HttpPost("/Search")]
        public async Task<IActionResult> Search(SearchBar bar) {
            var top_code = bar.Code;
            if (top_code == null) {
                return RedirectToAction("error", "productfiles", new { errorType = "Δεν δόθηκε κωδικός προς αναζήτηση." });
            }

            var code = await _context.Codes
                .FirstOrDefaultAsync(m => m.TopCode == top_code || m.Barcode == top_code || ((top_code.Length == 3) && m.TopCode == "02.026.0" + top_code) || (top_code.Length == 9 && m.TopCode == modify(top_code)));
            if (code == null)
                return RedirectToAction("error", "productfiles", new { errorType = "Δεν βρέθηκε εγγραφή." });
            var productFile = await _context.Products68.SingleOrDefaultAsync(m => m.TopCode == code.TopCode);

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
