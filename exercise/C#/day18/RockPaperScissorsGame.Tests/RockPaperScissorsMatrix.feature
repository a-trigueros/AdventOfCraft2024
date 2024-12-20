Feature: Rock Paper Scissors Game Matrix

    Scenario: Play matrix should be right
        Given Players play using matrix are ensured to have the expected results
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