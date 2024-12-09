using LanguageExt;

namespace ToyProduction.Domain
{
    public interface IToyRepository
    {
        Option<Toy> FindByName(string name);
        void Save(Toy toy);
    }
}