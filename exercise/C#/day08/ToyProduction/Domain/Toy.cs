namespace ToyProduction.Domain
{
    public class Toy(string name, State state)
    {
        public string Name { get; } = name;
        private State State { get; set; } = state;

        public bool CanStartProduction() => State == State.Unassigned;

        public bool TryStartProduction()
        {
            if (!CanStartProduction()) return false;
            State = State.InProduction;
            return true;
        }
    }

    public enum State
    {
        Unassigned,
        InProduction,
        Completed
    }
}