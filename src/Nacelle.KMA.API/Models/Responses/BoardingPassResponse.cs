using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class BoardingPassResponse : BaseResponse<Result>
    {
        [JsonProperty("boardingPasses")]
        public List<BoardingPassElement> BoardingPasses { get; set; }
    }

    public class FormattedBoardingPass
    {
    }    

    public class BoardingPassSeat
    {
        [JsonProperty("column")]
        public string Column { get; set; }

        [JsonProperty("row")]
        public string Row { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }    
}
