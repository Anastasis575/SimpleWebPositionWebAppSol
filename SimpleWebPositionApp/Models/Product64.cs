using System.ComponentModel.DataAnnotations;

namespace SimpleWebPositionApp.Models {
    public class Product64 {
        [Key]
        [Display(Name = "Κωδικός Είδους")]
        public string TopCode { get; init; }
        [Display(Name = "Περιγραφή")]
        public string Description { get; init; }
        [Display(Name = "Υπόλοιπο 64")]
        public decimal Balance68 { get; init; }
        [Display(Name = "Θέση 64")]
        public string Position68 { get; init; }
        [Display(Name = "Υπόλοιπο Κεντρικό")]
        public decimal BalanceCentral { get; init; }
        [Display(Name = "Θέση Κεντρικό")]
        public string PositionCentral { get; init; }
        [Display(Name = "Δεσμευμένα 64")]
        public decimal Reserved68 { get; init; }
        [Display(Name = "Χωριτικότητα Κεντρικό")]
        public decimal CapacityCentral { get; init; }
        [Display(Name = "Μηνιαίες Πωλήσεις")]
        public decimal Monthly { get; init; }

        [Display(Name = "Γραμμές Πωλήσεων")]
        public int TransactionLine { get; init; }

        [Display(Name = "Μπορούμε να φέρουμε στο κεντρικό")]
        public decimal Transferrable {
            get {
                if (Balance68 - Reserved68 < CapacityCentral - BalanceCentral) {
                    return Math.Max(Balance68 - Reserved68,0);
                }
                else {
                    return Math.Max(CapacityCentral - BalanceCentral,0);
                }
            }
        }
    }
}
