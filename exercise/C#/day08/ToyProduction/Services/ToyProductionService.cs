using LanguageExt;
using static LanguageExt.Prelude;
using ToyProduction.Domain;

namespace ToyProduction.Services
{
    public class ToyProductionService(IToyRepository repository)
    {
        public Option<Toy> StartProduction(string toyName) =>
            from toy in repository.FindByName(toyName)
            from inProduction in toy.StartProduction()
                .Do(repository.Save)
            select inProduction;
    }
}