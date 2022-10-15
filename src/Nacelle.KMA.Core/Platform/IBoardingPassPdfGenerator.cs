using System.Collections.Generic;
using Nacelle.KMA.Core.Models.Items;

namespace Nacelle.KMA.Core.Platform
{
    public interface IBoardingPassPdfGenerator
    {
        string CreateBoardingpassPdf(List<BoardingPassItem> boardingPasses);
    }    
}
