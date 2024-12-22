using System.Collections.Immutable;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using FsCheck;
using Microsoft.VisualBasic.CompilerServices;

namespace EID.Tests
{
    public class EIDProperties
    {
        [FsCheck.Xunit.Property(Arbitrary = [typeof(EIDGenerator)])]
        public Property RoundTripEID(EID eid) =>
            EID.Parse(eid.ToString())
                .Exists(parsedEID => parsedEID == eid)
                .ToProperty();

        public record Mutator(string Name, Func<EID, Gen<string>> Mutate)
        {
            public string Apply(EID eid) => Mutate(eid).Sample(0, 1).Head;
        }

        private static class MutatorGenerator
        {
            private const int StartIndex = 0;
            const int EIDMaxIndex = 7;
            // mutations:
            // - Remove a character
            // - Add a character
            // - Change a character
            // - Swap two characters
            // - replace any character by non-digit
            // - Replace sex by anything but [1,2,3]
            // - Replace SerialNumber by "000"
            // - Change control key (increment)
            
            private static readonly Mutator AMutator = new("A mutator",
                eid => Gen.Elements("Implement this first mutator")
            );
            
            private static readonly Mutator RemoveCharacterMutator = new("Mutator that removes a random character",
                    eid => 
                        from indexToRemove in Gen.Choose(StartIndex, EIDMaxIndex)
                        let eidChars = eid.ToString().ToImmutableList()
                        let newChars = eidChars.RemoveAt(indexToRemove).ToArray()
                        let newString = new string(newChars)
                        select newString);
                        
            [SuppressMessage("FSCheck", "UnusedMember.Local", Justification = "Used by FSCheck")]
            public static Arbitrary<Mutator> Mutator() =>
                Gen.Elements(
                        // AMutator, 
                        RemoveCharacterMutator)
                    .ToArbitrary();
        }

        [FsCheck.Xunit.Property(Arbitrary = [typeof(EIDGenerator), typeof(MutatorGenerator)])]
        public Property InvalidEIDCanNeverBeParsed(EID eid, Mutator mutator) =>
            EID.Parse(mutator.Apply(eid))
                .IsLeft
                .ToProperty()
                .Classify(true, mutator.Name);
    }
}