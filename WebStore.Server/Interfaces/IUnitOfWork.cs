namespace WebStore.Server.Interfaces
{
    public interface IUnitOfWork
    {
        IBook Book { get; }
        ICategory Category { get; }
        IAuthor Author { get; }
        ILibrary Library { get; }
        ICart Cart { get; }
        IOrder Order { get; }
        Task Save();
        Task AddRange(IEnumerable<Object> objects);
    }
}
