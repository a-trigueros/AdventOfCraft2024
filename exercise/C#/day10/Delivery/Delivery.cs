// C# TypeAlias
using Instruction = char;

namespace Delivery;

using InstructionComputationStrategy = Func<Instruction, int>;

public static class Building
{
    const Instruction UpInstruction = '(';
    const Instruction DownInstruction = ')';

    private static readonly InstructionComputationStrategy StandardInstructionStrategy =
        instruction => instruction switch
        {
            UpInstruction => 1,
            DownInstruction => -1,
            _ => 0
        };

    private static readonly InstructionComputationStrategy ElfInstructionStrategy =
        instruction => instruction switch
        {
            UpInstruction => -2,
            DownInstruction => 3,
            _ => 0
        };
    
    public static int WhichFloor(string instructions) => 
        instructions.Aggregate(0, SumInstructions(GetInstructionComputationStrategy(instructions)));

    private static InstructionComputationStrategy GetInstructionComputationStrategy(string instructions) =>
        instructions.Contains("🧝") 
            ? ElfInstructionStrategy 
            : StandardInstructionStrategy;


    private static Func<int, Instruction, int> SumInstructions(Func<Instruction, int> computationStrategy) => 
        (acc, val) => acc + computationStrategy(val);
}