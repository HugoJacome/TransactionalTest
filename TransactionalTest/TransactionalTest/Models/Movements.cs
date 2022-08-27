using System.ComponentModel.DataAnnotations;

namespace TransactionalTest.Models
{
    public class Movements
    {
        [Key]
        public Guid MovementId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime MovementDate { get; set; }
        [Required]
        public string MovementType { get; set; }
        [Required]
        public string Value { get; set; }
        public string Balance { get; set; }
        public Account account { get; set; }
    }
}
