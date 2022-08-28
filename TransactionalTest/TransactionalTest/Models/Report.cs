namespace TransactionalTest.Models
{
    public class ReportRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ClientIdentification { get; set; }
    }
}
