using System.ComponentModel.DataAnnotations;

namespace SimpleWebPositionApp.Models {
    public record SearchBar {
        [Key]
        public string Code { get; init; }
    }
}
