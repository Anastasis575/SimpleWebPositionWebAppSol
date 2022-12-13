using System.ComponentModel.DataAnnotations;

namespace SimpleWebPositionApp.Models.Dto {
    public record CensusItem {
        [Required]
        public string TopCode { get; init; }
        [Required]
        public int Device { get; init; }
        public string Description{ get; init; }
        public int Scanned{ get; init; }
        public int Logistics{ get; init; }
        public int Diff{ get; init; }
        
    }
}
