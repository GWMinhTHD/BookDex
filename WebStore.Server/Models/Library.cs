namespace WebStore.Server.Models
{
    public class Library
    {
        public string CusId { get; set; }
        public int BookId { get; set; }
        public int CurrentPage { get; set; } = 0;
        public Book Book { get; set; }
        public ApplicationUser User { get; set; }
    }
}
