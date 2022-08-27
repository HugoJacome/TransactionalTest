using System.ComponentModel.DataAnnotations;

namespace TransactionalTest.Models
{
    public class MovementRequest
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime MovementDate { get; set; }
        [Required]
        public long MovementAccount { get; set; }
        [Required]
        public string MovementType { get; set; }
        [Required]
        public double Value { get; set; }

    }
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
        public double Value { get; set; }
        public double Balance { get; set; }
        public Account account { get; set; }
    }
}
