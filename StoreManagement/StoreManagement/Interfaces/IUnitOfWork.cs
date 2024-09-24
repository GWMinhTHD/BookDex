namespace StoreManagement.Interfaces
{
    public interface IUnitOfWork
    {
        IBook Book { get; }
        ICategory Category { get; }
        IAuthor Author { get; }
        ICart Cart { get; }
        IOrder Order { get; }
        void Save();
        void AddRange(IEnumerable<Object> objects);
    }
}
