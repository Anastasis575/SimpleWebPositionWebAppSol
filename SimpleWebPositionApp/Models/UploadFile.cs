using Microsoft.AspNetCore.Mvc.Rendering;

namespace SimpleWebPositionApp.Models {
    public record UploadFile {
        public IFormFile ExcelFile { get; init; }

        public string SelWarehouse { get; init; }


    }
}
