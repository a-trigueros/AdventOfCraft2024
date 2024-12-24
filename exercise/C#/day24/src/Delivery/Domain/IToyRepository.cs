using Delivery.Domain.Core;
using LanguageExt;

namespace Delivery.Domain
{
    public interface IToyRepository : IRepository<Toy>
    {
        Toy? FindByName(string n);
    }
}