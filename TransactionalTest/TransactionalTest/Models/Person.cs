using System.ComponentModel.DataAnnotations;

namespace TransactionalTest.Models
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$")]
        public string Name { get; set; }
        public GenderEnum Gender { get; set; }
        public string Age { get; set; }
        public string Identification { get; set; }
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*$")]
        public string Phone { get; set; }
    }
}
