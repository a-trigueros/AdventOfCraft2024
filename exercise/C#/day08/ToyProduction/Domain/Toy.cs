using LanguageExt;

namespace ToyProduction.Domain
{
    public class Toy(string name, State state)
    {
        public string Name { get; } = name;
        private State State { get; set; } = state;

        public bool CanStartProduction() => State == State.Unassigned;

        public Option<Toy> StartProduction()
        {
            if(!CanStartProduction())
                return Option<Toy>.None;
            
            State = State.InProduction;
            return this;
        }
    }

    public enum State
    {
        Unassigned,
        InProduction,
        Completed
    }
}