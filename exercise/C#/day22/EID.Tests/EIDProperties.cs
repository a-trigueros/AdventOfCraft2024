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
            const int StartIndex = 0;
            const int EIDMaxIndex = 7;
            
            private static readonly Mutator RemoveRandomCharacterMutator = new(
                "Mutator that removes a random character",
                eid =>
                    from indexToRemove in Gen.Choose(StartIndex, EIDMaxIndex)
                    let eidChars = eid.ToString().ToImmutableList()
                    let newChars = eidChars.RemoveAt(indexToRemove).ToArray()
                    let newString = new string(newChars)
                    select newString);

            private static readonly Mutator AddRandomCharacterMutator = new("Mutator that adds a random character",
                eid =>
                    from indexToInsert in Gen.Choose(StartIndex, EIDMaxIndex)
                    from newChar in Arb.Default.Char().Generator
                    let eidChars = eid.ToString().ToImmutableList()
                    let newChars = eidChars.Insert(indexToInsert, newChar).ToArray()
                    let newString = new string(newChars)
                    select newString);

            private static readonly Mutator ChangeACharacterMutator = new("Mutator that change a random character",
                eid =>
                    from indexToChange in Gen.Choose(StartIndex, EIDMaxIndex)
                    from newChar in Arb.Default.Char().Generator.Where(c => !char.IsDigit(c))
                    let eidChars = eid.ToString().ToImmutableList()
                    let newChars = eidChars.SetItem(indexToChange, newChar).ToArray()
                    let newString = new string(newChars)
                    select newString);

            private static readonly char[] ValidSexes = ['1', '2', '3'];

            private static readonly Mutator ReplaceSexByInvalidValueMutator = new(
                "Mutator that changes the sex to an invalid value",
                eid =>
                    from newSex in Arb.Default.Char().Generator.Where(c => !ValidSexes.Contains(c))
                    select $"{newSex}{eid.ToString().Substring(1)}");
            
            private static readonly Mutator ReplaceSerialNumberByInvalidValue = new(
                "Mutator that changes the serial number to an invalid value",
                eid =>
                    from newSerialNumber in Gen.Constant("000")
                    let eidString = eid.ToString()
                    select $"{eidString.Substring(0, 3)}{newSerialNumber}{eidString.Substring(6)}");
            
            private static readonly Mutator MakeControlKeyInvalidMutator = new("Mutator that incremnt control key",
                    eid =>
                    from newControlKey in Gen.Constant(int.Parse(eid.ToString().Substring(6)) + 1)
                    let eidString = eid.ToString()
                    select $"{eidString.Substring(0, 6)}{newControlKey:D2}");

            [SuppressMessage("FSCheck", "UnusedMember.Local", Justification = "Used by FSCheck")]
            public static Arbitrary<Mutator> Mutator() =>
                Gen.Elements(
                        RemoveRandomCharacterMutator,
                        AddRandomCharacterMutator,
                        ChangeACharacterMutator,
                        ReplaceSexByInvalidValueMutator,
                        ReplaceSerialNumberByInvalidValue,
                        MakeControlKeyInvalidMutator
                    )
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