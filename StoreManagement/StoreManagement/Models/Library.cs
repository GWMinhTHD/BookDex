namespace StoreManagement.Models
{
    public class Library
    {
        public string CusId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public ApplicationUser User { get; set; }
    }
}
