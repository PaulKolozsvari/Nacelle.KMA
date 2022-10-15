using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Nacelle.KMA.API.Exceptions.Factories;
using Nacelle.KMA.API.Models.Responses;

namespace Nacelle.KMA.API.Exceptions.Extensions
{
    public static class ResultsResponseExtensions
    {
        public static GeneralException ToException<T>(this List<T> results) where T : Result
        {
            var status = results.FirstOrDefault().Status.FirstOrDefault();
            Debug.WriteLine($"General Exception: Status Code = {status}, Status Type = {status.Type}.");
            if (status != null)
            {
                switch (status.Code)
                {
                    case "ERR.DCCI.BUSINESS_ERROR.FLIGHT_NOT_INITIALIZED":
                        return ExceptionFactory.FindBooking.FlightNotInitialized(status.Code, status.Type);

                    case "ERR.DCCI.RESERVATION_NOT_FOUND":
                        return ExceptionFactory.FindBooking.NotFound(status.Code, status.Type);

                    case "BOARDING_PASS_INHIBITED_TIME_RESTRICTION":
                        return ExceptionFactory.BoardingPass.InhibitedTimeRestriction(status.Code, status.Type);

                    case "BOARDING_PASS_REPRINT_FAILURE":
                        {
                            if (status.Message.Equals("!FLIGHT LEG RESTRICTED")) //TODO: swollow error based on Cordova App??
                            {
                                break;
                            }
                            return ExceptionFactory.General.TechnicalDifficulties();
                        }

                    case "ERR.DCCI.REQUEST_VALIDATION":
                        return ExceptionFactory.CheckIn.ReservationNotEligible();
                    case "ERR.DCCI.APP.BUSINESS_ERROR":
                       if (status.Message.StartsWith("Cannot find cached object for sessionID=", System.StringComparison.InvariantCulture))
                        {
                            return ExceptionFactory.CheckIn.ExpiredConversation();
                        }
                        return ExceptionFactory.General.TechnicalDifficulties();

                }
            }

            return ExceptionFactory.General.SomethingWentWrong();
        }

        public static bool MustThrowExceptionIfAny<T>(this List<T> results) where T : Result => results != null && results.SelectMany(x => x.Status).Any(x => x.Type == "ERROR");
        public static bool MustThrowExceptionIfEmpty(this List<Result> results) => results == null || !results.Any();
    }
}
