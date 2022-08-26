using System.ComponentModel.DataAnnotations;

namespace TransactionalTest.Models
{
    public class Movements
    {
        [Key]
        public Guid MovementId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime MovementDate { get; set; }
        public string MovementType { get; set; }
        public string Value { get; set; }
        public string Balance { get; set; }
        public Account account { get; set; }
    }
}
