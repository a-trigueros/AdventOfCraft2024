namespace RockPaperScissorsGame
{
    public enum Choice
    {
        Rock,
        Paper,
        Scissors
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
        const string RockCrushesScissors = "rock crushes scissors";
        const string PaperCoversRock = "paper covers rock";
        const string ScissorsCutsPaper = "scissors cuts paper";
        const string Draw = "same choice";
        public static Result Play(Choice player1, Choice player2) =>
            (player1, player2) switch
            {
                (Choice.Rock, Choice.Scissors) => new Result(Winner.Player1, RockCrushesScissors),
                (Choice.Paper, Choice.Rock) => new Result(Winner.Player1, PaperCoversRock),
                (Choice.Scissors, Choice.Paper) => new Result(Winner.Player1, ScissorsCutsPaper),
                (Choice.Scissors, Choice.Rock) => new Result(Winner.Player2, RockCrushesScissors),
                (Choice.Rock, Choice.Paper) => new Result(Winner.Player2, PaperCoversRock),
                (Choice.Paper, Choice.Scissors) => new Result(Winner.Player2, ScissorsCutsPaper),
                _ => new Result(Winner.Draw, Draw)
            };
    }
}