using System.ComponentModel.DataAnnotations;

namespace SimpleWebPositionApp.Models {
    public record Login {

        [Key]
        [Display(Name = "Α/Α")]
        public int Login_id { get; init; }

        public string Login_Name { get; init; }

        public string Pass { get; init; }

        public Mode mode { get; init; }
    }

    public enum Mode {
        Basic,Root
    }
}
