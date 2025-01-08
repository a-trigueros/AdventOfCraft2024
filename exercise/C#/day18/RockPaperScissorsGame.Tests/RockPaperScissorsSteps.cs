using FluentAssertions;
using TechTalk.SpecFlow;

namespace RockPaperScissorsGame.Tests
{
    [Binding]
    public class RockPaperScissorsSteps
    {
        private Result? _result;
        private Choice _player1Choice;
        private Choice _player2Choice;

        [Given(@"Player (\d+) chooses (.*)")]
        public void GivenPlayerChooses(int player, string choice)
        {
            var parsedChoice = ParseChoice(choice);

            if (player == 1) _player1Choice = parsedChoice;
            else _player2Choice = parsedChoice;
        }

        [When(@"they play")]
        public void WhenTheyPlay() => _result = RockPaperScissors.Play(_player1Choice, _player2Choice);

        [Then(@"the winner should be (.*) because (.*)")]
        public void ThenTheWinnerShouldBeBecause(string expectedWinner, string expectedReason)
        {
            _result!.Winner.Should().Be(
                ParseWinner(expectedWinner)
            );
            _result.Reason.Should().Be(expectedReason);
        }

        private static Choice ParseChoice(string choice)
            => choice switch
            {
                "🪨" => Choice.Rock,
                "📄" => Choice.Paper,
                "✂️" => Choice.Scissors,
                "🦎" => Choice.Lizard,
                "🖖" => Choice.Spock,
                _ => throw new ArgumentException("Invalid choice")
            };

        private static Winner ParseWinner(string winner)
            => winner switch
            {
                "Player 1" => Winner.Player1,
                "Player 2" => Winner.Player2,
                "Draw" => Winner.Draw,
                _ => throw new ArgumentException("Invalid winner")
            };

        [GivenAttribute(@"Players play using matrix are ensured to have the expected results")]
        public void GivenPlayersPlayUsingMatrixAreEnsuredToHaveTheExpectedResults(Table table)
        {
            foreach (var row in table.Rows)
            {
                var player1Choice = ParseChoice(row["Player 1"]);
                var player2Choice = ParseChoice(row["Player 2"]);
                var result = RockPaperScissors.Play(player1Choice, player2Choice);
                result.Winner.Should().Be(ParseWinner(row["Winner"]));
                result.Reason.Should().Be(row["Reason"]);
            }
        }

    }
}