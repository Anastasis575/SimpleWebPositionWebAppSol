using Microsoft.AspNetCore.Mvc;
using System.Text;
using ExcelDataReader;
using SimpleWebPositionApp.Data;
using System.Data;
using SimpleWebPositionApp.Models;

namespace SimpleWebPositionApp.Controllers {
    public class ProductFilesController : Controller {
        private readonly ProductDbContext _context;
        private readonly ILogger<ProductFilesController> _logger;

        public ProductFilesController(ProductDbContext context, ILogger<ProductFilesController> logger) {
            _context = context;
            _logger = logger;
        }

        // GET: ProductFiles
        public IActionResult Index() => View();

        [HttpGet("[action]")]
        public IActionResult Upload() {
            return View();
        }

        // POST: ProductFiles/Upload
        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadAsync(UploadFile uf) {
            if (uf == null) {
                return RedirectToAction("error", "productfiles", new { errorType = "Δεν επιλέγθηκε αρχείο." });
            }

            IFormFile formFile = uf.ExcelFile;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (MemoryStream stream = new()) {
                await formFile.CopyToAsync(stream);

                ExcelReaderConfiguration conf = new()
                {
                    FallbackEncoding = Encoding.UTF8
                };

                using (var reader = ExcelReaderFactory.CreateReader(stream, conf).AsDataSet(new() { ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true } })) {
                    DataTable? datarows = reader.Tables["395"];
                    if (datarows == null) return RedirectToAction("error", "productfiles", new { errorType = "Δεν βρέθηκε ο πίνακας 395." });
                    HashSet<CodeItem> codes = new();
                    foreach (DataRow row in datarows.Rows) {
                        if (row[0] == null || row[1] == null) continue;
                        if (codes.Any(v => v.Barcode == row[0].ToString())) continue;
                        codes.Add(new()
                        {
                            Barcode = row[0].ToString(),
                            TopCode = row[1].ToString()
                        });
                    }
                    datarows = reader.Tables["ΤΡΟΦΟΔΟΣΙΑ"];
                    if (datarows != null) {
                        if (uf.SelWarehouse == "64") {
                        HashSet<Product64> products = new();
                        int i;
                            foreach (DataRow row in datarows.Rows) {
                                if (row["GXCode"] == null) continue;
                                try {
                                    products.Add(new()
                                    {
                                        TopCode = row["GXCode"].ToString(),
                                        Description = row["GXDESCRIPTION"].ToString(),
                                        Position68 = row["ΘΕΣΗ ΑΛΚΜ 68"].ToString(),
                                        PositionCentral = row["ΘΕΣΗ ΚΕΝΤΡΙΚΟΥ"].ToString(),
                                        Reserved68 = Int32.TryParse(row["ΔΕΣΜ.ΤΡΟΦΟΔΟΣΙΑΣ"].ToString(), out i) ? Int32.Parse(row["ΔΕΣΜ.ΤΡΟΦΟΔΟΣΙΑΣ"].ToString()) : 0,
                                        Balance68 = Int32.TryParse(row["ΥΠΟΛΟΙΠΟ ΤΡΟΦΟΔΟΣΙΑΣ"].ToString(), out i) ? Int32.Parse(row["ΥΠΟΛΟΙΠΟ ΤΡΟΦΟΔΟΣΙΑΣ"].ToString()) : 0,
                                        BalanceCentral = Int32.TryParse(row["ΥΠΟΛΟΙΠΟ ΚΕΝΤΡΙΚΟΥ"].ToString(), out i) ? Int32.Parse(row["ΥΠΟΛΟΙΠΟ ΚΕΝΤΡΙΚΟΥ"].ToString()) : 0,
                                        CapacityCentral = Int32.TryParse(row["ΧΩΡΗΤΙΚΟΤΗΤΑ KENT."].ToString(), out i) ? Int32.Parse(row["ΧΩΡΗΤΙΚΟΤΗΤΑ KENT."].ToString()) : 0,
                                        Monthly = Int32.TryParse(row["ΠΟΣ.ΜΗΝ.ΠΩΛ. ΣΥΝΟΛΟ"].ToString(), out i) ? Int32.Parse(row["ΠΟΣ.ΜΗΝ.ΠΩΛ. ΣΥΝΟΛΟ"].ToString()) : 0,
                                        TransactionLine = Int32.TryParse(row["ΓΡΑΜΜΕΣ ΠΩΛΗΣΕΩΝ"].ToString(), out i) ? Int32.Parse(row["ΓΡΑΜΜΕΣ ΠΩΛΗΣΕΩΝ"].ToString()) : 0
                                    }); ;
                                }
                                catch (Exception) {
                                    _logger.LogError("Format Error");
                                }
                            }
                            _context.Codes.RemoveRange(_context.Codes);
                            _context.Products64.RemoveRange(_context.Products64);
                            await _context.Codes.AddRangeAsync(codes);
                            await _context.Products64.AddRangeAsync(products);
                            _context.SaveChanges();
                        }
                        else {
                            HashSet<Product68> products = new();
                            int i;
                            foreach (DataRow row in datarows.Rows) {
                                if (row["GXCode"] == null) continue;
                                try {
                                    products.Add(new()
                                    {
                                        TopCode = row["GXCode"].ToString(),
                                        Description = row["GXDESCRIPTION"].ToString(),
                                        Position68 = row["ΘΕΣΗ ΑΛΚΜ 68"].ToString(),
                                        PositionCentral = row["ΘΕΣΗ ΚΕΝΤΡΙΚΟΥ"].ToString(),
                                        Reserved68 = Int32.TryParse(row["ΔΕΣΜ.ΤΡΟΦΟΔΟΣΙΑΣ"].ToString(), out i) ? Int32.Parse(row["ΔΕΣΜ.ΤΡΟΦΟΔΟΣΙΑΣ"].ToString()) : 0,
                                        Balance68 = Int32.TryParse(row["ΥΠΟΛΟΙΠΟ ΤΡΟΦΟΔΟΣΙΑΣ"].ToString(), out i) ? Int32.Parse(row["ΥΠΟΛΟΙΠΟ ΤΡΟΦΟΔΟΣΙΑΣ"].ToString()) : 0,
                                        BalanceCentral = Int32.TryParse(row["ΥΠΟΛΟΙΠΟ ΚΕΝΤΡΙΚΟΥ"].ToString(), out i) ? Int32.Parse(row["ΥΠΟΛΟΙΠΟ ΚΕΝΤΡΙΚΟΥ"].ToString()) : 0,
                                        CapacityCentral = Int32.TryParse(row["ΧΩΡΗΤΙΚΟΤΗΤΑ KENT."].ToString(), out i) ? Int32.Parse(row["ΧΩΡΗΤΙΚΟΤΗΤΑ KENT."].ToString()) : 0,
                                        Monthly = Int32.TryParse(row["ΠΟΣ.ΜΗΝ.ΠΩΛ. ΣΥΝΟΛΟ"].ToString(), out i) ? Int32.Parse(row["ΠΟΣ.ΜΗΝ.ΠΩΛ. ΣΥΝΟΛΟ"].ToString()) : 0,
                                        TransactionLine = Int32.TryParse(row["ΓΡΑΜΜΕΣ ΠΩΛΗΣΕΩΝ"].ToString(), out i) ? Int32.Parse(row["ΓΡΑΜΜΕΣ ΠΩΛΗΣΕΩΝ"].ToString()) : 0
                                    }); ;
                                }
                                catch (Exception) {
                                    _logger.LogError("Format Error");
                                }
                            }
                            _context.Codes.RemoveRange(_context.Codes);
                            _context.Products68.RemoveRange(_context.Products68);
                            await _context.Codes.AddRangeAsync(codes);
                            await _context.Products68.AddRangeAsync(products);
                            _context.SaveChanges();
                        }
                    
                    }
                    else {
                        return RedirectToAction("error", "productfiles", new { errorType = "Δεν Βρέθηκε ο πίνακας Τροφοδοσία." });
                    }
                }
            }

            return RedirectToAction("index", "productfiles");

        }

       
        [HttpGet("[action]")]
        public IActionResult Login() => View();

        [HttpPost("/Login")]
        public IActionResult Login(PasswordDTO dto) {
            return _context.Login.Where(login => dto.Pass == login.Pass).SingleOrDefault() == null ? RedirectToAction("error", "productfiles", new { errorType = "Δεν μπορέσατε να ταυτοποιηθείτε." }) : RedirectToAction("upload", "productfiles");
        }


        [HttpGet()]
        public IActionResult Error([FromQuery(Name = "errorType")] string type) => View(new ErrorClass { Message = type });

    }
}

