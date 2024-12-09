using FluentAssertions;
using LanguageExt;
using ToyProduction.Domain;

namespace ToyProduction.Tests.Doubles
{
    public class InMemoryToyRepository : IToyRepository
    {
        private readonly List<string> _findByNameCalls = [];
        private readonly List<Toy> _saveCalls = [];
        
        private readonly List<Toy> _toys = [];

        
        public static InMemoryToyRepository Initialize(Action<InMemoryToyRepository> initializer){
            var repository = new InMemoryToyRepository();
            initializer(repository);
            repository._saveCalls.Clear();
            repository._findByNameCalls.Clear();
            return repository;
        }
        
        public Option<Toy> FindByName(string name)
        {
            _findByNameCalls.Add(name);
            return _toys.FirstOrDefault(t => t.Name == name);
        }

        public void Save(Toy toy)
        {
            _saveCalls.Add(toy);
            _toys.Remove(toy);
            _toys.Add(toy);
        }

        public InMemoryToyRepository ShoulHaveSearched(string searchString)
        {
            _findByNameCalls.Should().Contain(searchString);
            return this;
        }

        public InMemoryToyRepository ShouldNotHaveSaved()
        {
            _saveCalls.Should().BeEmpty();
            return this;
        }
        
        public InMemoryToyRepository ShouldHaveSaved(Toy toy)
        {
            _saveCalls.Should().Contain(toy);
            return this;
        }
    }
}