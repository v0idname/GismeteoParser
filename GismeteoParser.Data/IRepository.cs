using System.Linq;

namespace GismeteoParser.Data
{
    public interface IRepository<T>
    {
        IQueryable<T> Items { get; }
    }
}
