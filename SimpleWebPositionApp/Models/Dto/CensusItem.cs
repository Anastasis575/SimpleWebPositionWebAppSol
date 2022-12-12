using System.ComponentModel.DataAnnotations;

namespace SimpleWebPositionApp.Models.Dto {
    public record CensusItem {
        [Key]
        public string TopCode { get; init; }
        public string Description{ get; init; }
        public int Scanned{ get; init; }
        public int Logistics{ get; init; }
        public int Diff{ get; init; }
        public int Device{ get; init; }
    }
}
