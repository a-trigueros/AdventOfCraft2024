Feature: Rock Paper Scissors Game Matrix

    Scenario: Play matrix should be right
        Given Player 1 chooses <Player 1>
        And Player 2 chooses <Player 2>
        When they play
        Then the winner should be <Winner> because <Reason>

        Examples:
          | Player 1 | Player 2 | Winner   | Reason                      |
          | 🪨       | ✂️       | Player 1 | rock crushes scissors       |
          | ✂️       | 🪨       | Player 2 | rock crushes scissors       |
          | 📄       | 🪨       | Player 1 | paper covers rock           |
          | 🪨       | 📄       | Player 2 | paper covers rock           |
          | ✂️       | 📄       | Player 1 | scissors cuts paper         |
          | 📄       | ✂️       | Player 2 | scissors cuts paper         |
          | 🪨       | 🦎       | Player 1 | rock crushes lizard         |
          | 🦎       | 🪨       | Player 2 | rock crushes lizard         |
          | 🦎       | 📄       | Player 1 | lizard eats paper           |
          | 📄       | 🦎       | Player 2 | lizard eats paper           |
          | ✂️       | 🦎       | Player 1 | scissors decapitates lizard |
          | 🦎       | ✂️       | Player 2 | scissors decapitates lizard |
          | 🖖       | ✂️       | Player 1 | spock smashes scissors      |
          | ✂️       | 🖖       | Player 2 | spock smashes scissors      |
          | 📄       | 🖖       | Player 1 | paper disproves spock       |
          | 🖖       | 📄       | Player 2 | paper disproves spock       |
          | 🖖       | 🪨       | Player 1 | spock vaporizes rock        |
          | 🪨       | 🖖       | Player 2 | spock vaporizes rock        |
          | 🦎       | 🖖       | Player 1 | lizard poisons spock        |
          | 🖖       | 🦎       | Player 2 | lizard poisons spock        |