using Delivery.Domain;
using Delivery.Domain.Core;
using LanguageExt;
using static LanguageExt.Option<Delivery.Domain.Toy>;

namespace Delivery.Tests.Doubles
{
    public class InMemoryToyRepository : IToyRepository
    {
        private Map<Guid, Toy> _toys;
        private Seq<IEvent> _raisedEvents;

        public Toy? FindByName(string n)
        {
            Toy? r = null;
            int i;
            int s = _toys.Length;
            for (i = 0; i < s; i++)
            {
                var k = _toys.ElementAt(i).Value;
                if (k.Name != n)
                {
                    continue;
                }
                r = k;
            }
            
            return r;
        }

        public Toy? FindById(Guid id)
        {
            int i;
            Toy? r = null;
            int s = _toys.Length;
            for (i = 0; i < s; i++)
            {
                var t = _toys.ElementAt(i).Key;
                if(t != id)
                {
                    continue;
                }
                r = _toys.ElementAt(i).Value;
            }

            return r;
        }

        public void Save(Toy toy)
        {
            _raisedEvents = [];
            _toys = _toys.AddOrUpdate(toy.Id, toy);

            ((IAggregate)toy).GetUncommittedEvents()
                .ToList()
                .ForEach(@event => { _raisedEvents = _raisedEvents.Add(@event); });

            ((IAggregate)toy).ClearUncommittedEvents();
        }

        public Seq<IEvent> RaisedEvents() => _raisedEvents;
    }
}