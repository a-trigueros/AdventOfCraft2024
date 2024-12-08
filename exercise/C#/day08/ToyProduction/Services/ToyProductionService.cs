using ToyProduction.Domain;

namespace ToyProduction.Services
{
    public class ToyProductionService(IToyRepository repository)
    {
        public void StartProduction(string toyName)
        {
            var toy = repository.FindByName(toyName);

            if (toy != null && toy.TryStartProduction())
            {
                repository.Save(toy);
            }
        }
    }
}