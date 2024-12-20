namespace RockPaperScissorsGame
{
    public enum Choice
    {
        Rock,
        Paper,
        Scissors,
        Lizard,
        Spock
    }

    public enum Winner
    {
        Player1,
        Player2,
        Draw
    }

    public record Result(Winner Winner, string Reason);

    public static class RockPaperScissors
    {
        private const string RockCrushesScissors = "rock crushes scissors";
        private const string PaperCoversRock = "paper covers rock";
        private const string ScissorsCutsPaper = "scissors cuts paper";
        private const string RockCrushesLizard = "rock crushes lizard";
        private const string LizardEatsPaper = "lizard eats paper";
        private const string ScissorsDecapitatesLizard = "scissors decapitates lizard";
        private const string SpockSmashesScissors = "spock smashes scissors";
        private const string PaperDisprovesSpock = "paper disproves spock";
        private const string SpockVaporizesRock = "spock vaporizes rock";
        private const string Draw = "same choice";
        
        public static Result Play(Choice player1, Choice player2) =>
            (player1, player2) switch
            {
                (Choice.Rock, Choice.Scissors) => new Result(Winner.Player1, RockCrushesScissors),
                (Choice.Paper, Choice.Rock) => new Result(Winner.Player1, PaperCoversRock),
                (Choice.Scissors, Choice.Paper) => new Result(Winner.Player1, ScissorsCutsPaper),
                (Choice.Scissors, Choice.Rock) => new Result(Winner.Player2, RockCrushesScissors),
                (Choice.Rock, Choice.Paper) => new Result(Winner.Player2, PaperCoversRock),
                (Choice.Paper, Choice.Scissors) => new Result(Winner.Player2, ScissorsCutsPaper),
                (Choice.Rock, Choice.Lizard) => new Result(Winner.Player1, RockCrushesLizard),
                (Choice.Lizard, Choice.Rock) => new Result(Winner.Player2, RockCrushesLizard),
                (Choice.Lizard, Choice.Paper) => new Result(Winner.Player1, LizardEatsPaper),
                (Choice.Paper, Choice.Lizard) => new Result(Winner.Player2, LizardEatsPaper),
                (Choice.Scissors, Choice.Lizard) => new Result(Winner.Player1, ScissorsDecapitatesLizard),
                (Choice.Lizard, Choice.Scissors) => new Result(Winner.Player2, ScissorsDecapitatesLizard),
                (Choice.Spock, Choice.Scissors) => new Result(Winner.Player1, SpockSmashesScissors),
                (Choice.Scissors, Choice.Spock) => new Result(Winner.Player2, SpockSmashesScissors),
                (Choice.Paper, Choice.Spock) => new Result(Winner.Player1, PaperDisprovesSpock),
                (Choice.Spock, Choice.Paper) => new Result(Winner.Player2, PaperDisprovesSpock),
                (Choice.Spock, Choice.Rock) => new Result(Winner.Player1, SpockVaporizesRock),
                (Choice.Rock, Choice.Spock) => new Result(Winner.Player2, SpockVaporizesRock),
                _ => new Result(Winner.Draw, Draw)
            };
    }
}