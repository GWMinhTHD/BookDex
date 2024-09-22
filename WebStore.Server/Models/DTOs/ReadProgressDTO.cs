namespace WebStore.Server.Models.DTOs
{
    public class ReadProgressDTO
    {
        public int BookId { get; set; }
        public string Name { get; set; }

        public string FileLocation { get; set; }
        public int PageNum { get; set; } = 1;
    }
}
