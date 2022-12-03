using System.ComponentModel.DataAnnotations;

namespace SimpleWebPositionApp.Models {
    public record CodeItem {
        [Key]
        public string Barcode { get; init; }
        public string TopCode { get; init; }

    }
}
