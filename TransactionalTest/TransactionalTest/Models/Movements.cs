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
    public class MovementInfoEqualityComparer : IEqualityComparer<Movements>
    {
        public bool Equals(Movements mv1, Movements mv2)
        {
            if (mv1 == null && mv2 == null) return true;
            else if (mv1 == null || mv2 == null) return false;
            //TODO: Review key value comparison expression
            else if (mv1.Balance == mv2.Balance && mv1.MovementDate == mv2.MovementDate
                && mv1.MovementType == mv2.MovementType && mv1.Value == mv2.Value )
                return true;
            else
                return false;
        }
        public int GetHashCode(Movements cli)
        {
            return cli.GetHashCode();
        }
    }
}
