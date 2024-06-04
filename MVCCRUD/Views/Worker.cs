namespace MVCCRUD.Models
{
    public class Worker
    {
        public int id { get; set; }
        public string? fullName { get; set; }
        public string? lastName { get; set; }
        public string? typeDocument { get; set; }
        public string? numberDocument { get; set; }
        public DateTime birthDate { get; set; }
        public DateTime entryDate { get; set; }
        public int numberChildren { get; set; }
    }
}
