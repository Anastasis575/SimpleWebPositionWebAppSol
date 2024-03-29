﻿using Microsoft.AspNetCore.Mvc;
using System.Text;
using ExcelDataReader;
using SimpleWebPositionApp.Data;
using System.Data;
using SimpleWebPositionApp.Models;
using SimpleWebPositionApp.Models.Dto;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace SimpleWebPositionApp.Controllers {
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductFilesController : Controller {
        private readonly ProductDbContext _context;
        private readonly ILogger<ProductFilesController> _logger;

        public ProductFilesController(ProductDbContext context, ILogger<ProductFilesController> logger) {
            _context = context;
            _logger = logger;
        }

        // GET: ProductFiles
        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Upload() {
            return View();
        }

        // POST: ProductFiles/Upload
        [HttpPost]
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
                    _logger.LogInformation(message: uf.SelWarehouse);
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
                                        Position68 = row["ΘΕΣΗ ΑΛΚΜ 64"].ToString(),
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
            string controller = uf.SelWarehouse == "68" ? "product68" : "product64";
            return RedirectToAction("index", controller);

        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login([FromForm] PasswordDTO dto) {
            _logger.LogInformation(message: $"{_context.Login.Where(login => dto.Pass == login.Pass).SingleOrDefault()}");
            _logger.LogInformation(dto.Pass);
            return _context.Login.Where(login => dto.Pass == login.Pass).SingleOrDefault() == null ? RedirectToAction("error", "productfiles", new { errorType = "Δεν μπορέσατε να ταυτοποιηθείτε." }) : RedirectToAction("upload", "productfiles");
        }


        [HttpGet]
        public IActionResult Error([FromQuery(Name = "errorType")] string type) => View(new ErrorClass { Message = type });


        [HttpPost]
        public async Task<ActionResult<Boolean>> Census(List<CensusItem> excelDTO) {
            await _context.Census.AddRangeAsync(excelDTO);
            _context.SaveChanges();
            return Ok(true);
        }


        [HttpGet]
        public async Task<IActionResult> Census() {
            ISheet sheet;
            using (MemoryStream stream = new()) {
                stream.Position =0;

                XSSFWorkbook wordkbook = new XSSFWorkbook(stream);
                sheet = wordkbook.GetSheetAt(0);
                IRow header = sheet.CreateRow(0);
                header.CreateCell(0).SetCellValue("Κωδικός Top");
                header.CreateCell(1).SetCellValue("Περιγραφή");
                header.CreateCell(2).SetCellValue("Απογραφή");
                header.CreateCell(3).SetCellValue("Λογιστικό Υπόλοιπο");
                header.CreateCell(4).SetCellValue("Διαφορά");
                header.CreateCell(5).SetCellValue("PDA Λειτουργίας");
                header.CreateCell(6).SetCellValue("Αποθήκης");
                int rowCount = 1;
                foreach (CensusItem item in _context.Census) {
                    IRow row = sheet.CreateRow(rowCount);
                    row.CreateCell(0).SetCellValue(item.TopCode);
                    row.CreateCell(1).SetCellValue(item.Description);
                    row.CreateCell(2).SetCellValue(item.Scanned);
                    row.CreateCell(3).SetCellValue(item.Logistics);
                    row.CreateCell(4).SetCellValue(item.Logistics);
                    row.CreateCell(5).SetCellValue(item.Device);
                    row.CreateCell(6).SetCellValue(item.Warehouse);
                    rowCount++;
                }
                wordkbook.Write(stream);
                return File(stream, "application/vnd.ms-excel");
            }
        }


    }
}

