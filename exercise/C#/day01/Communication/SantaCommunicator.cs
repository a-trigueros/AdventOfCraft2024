using LanguageExt;

namespace Communication
{
    public class SantaCommunicator(RestDays numberOfDaysToRest, ICountDaysUntilChrismas daysBeforeChristmas)
    {
        /// <summary>
        /// I feel this is the best for Santa to call with each of its Reinders and decide wether he should send a message or log an error.
        /// </summary>
        /// <param name="reinder"></param>
        /// <returns></returns>
        public Either<string, string> ComposeMessageSafe(Reinder reinder)
        {
            var daysBeforeReturn = DaysBeforeReturn(reinder.Location.DistanceInDays);
            if (daysBeforeReturn <= 0)
            {
                return Either<string, string>.Left(
                    $"Overdue for {reinder.Name.Value} located {reinder.Location.Name}.");
            }
            
            return Either<string, string>.Right(
                $"Dear {reinder.Name.Value}, please return from {reinder.Location.Name} in {daysBeforeReturn} day(s) to be ready and rest before Christmas.");
            
            
        }
        public string ComposeMessage(Reinder reinder)
        {
            var daysBeforeReturn = DaysBeforeReturn(reinder.Location.DistanceInDays);
            return
                $"Dear {reinder.Name.Value}, please return from {reinder.Location.Name} in {daysBeforeReturn} day(s) to be ready and rest before Christmas.";
        }

        public bool IsOverdue(Reinder reinder, ILogger logger)
        {
            if (DaysBeforeReturn(reinder.Location.DistanceInDays) <= 0)
            {
                logger.Log($"Overdue for {reinder.Name.Value} located {reinder.Location.Name}.");
                return true;
            }

            return false;
        }

        private int DaysBeforeReturn(int numbersOfDaysForComingBack) =>
            daysBeforeChristmas.GetDays() - (numbersOfDaysForComingBack + numberOfDaysToRest.Value);
    }
}