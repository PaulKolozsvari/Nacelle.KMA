using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Nacelle.KMA.Core.Models.Items;
using Nacelle.KMA.Core.Platform;
using Nacelle.KMA.UI.Plugins;
using SkiaSharp;
using Xamarin.Essentials;

namespace Nacelle.KMA.UI.Services
{
    public class BoardingPassPdfGenerator : IBoardingPassPdfGenerator
    {
        private readonly IBarcodeService _barcodeService;

        public BoardingPassPdfGenerator(IBarcodeService barcodeService)
        {
            _barcodeService = barcodeService;
        }

        public string CreateBoardingpassPdf(List<BoardingPassItem> boardingPasses)
        {
            var fileName = $"{boardingPasses.FirstOrDefault().PassengerName}-{boardingPasses.FirstOrDefault().FlightNumber}.pdf";
            var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var stream = new SKFileWStream(filePath))
            {
                using (var document = SKDocument.CreatePdf(stream))
                {
                    using (var paint = new SKPaint())
                    {
                        paint.IsAntialias = true;
                        paint.TextAlign = SKTextAlign.Left;

                        const float width = 231;
                        const float height = 421;

                        const float bpStartX = width / 2 - width / 2; //boarding pass starting X
                        const float bpStartY = height / 2 - height / 2; //boarding pass starting Y
                        const float bpCenterX = bpStartX + (width / 2); //boarding pass center X

                        const float cornerRadius = 6; //corner radius

                        const float sidePadding = 13;

                        const float sectionsTopPadding = 17;
                        const float sectionsValPadding = 15;

                        //section 1 measurements
                        const float s1H = 72.5f; //section 1 height
                        const float s1Offset = 30; // section 1 offset
                        const float planeW = 21;
                        const float planeH = 21;
                        const float departureCenterX = bpStartX + ((bpCenterX - (planeW / 2) - bpStartX) / 2);
                        const float arrivalCenterX = bpStartX + width - ((bpCenterX - (planeW / 2) - bpStartX) / 2);

                        const float section1TopPadding = 35;
                        const float section1NamePadding = 25;

                        //section 2 measurements
                        const float s2H = 41; //section 2 height
                        const float s2Start = bpStartY + s1H; //section 2 start

                        //section 3 measurements
                        const float s3H = 41; //section 3 height
                        const float s3Start = s2Start + s2H; //section 3 start
                        const float s3MiddlePadding = 25; //section 3 padding between the differnt things

                        //section 4 measurements
                        const float s4H = 41; //section 4 height
                        const float s4Start = s3Start + s3H; //section 4 start
                        const float s4MiddlePadding = 25; //section 4 padding between the differnt things

                        //section 5 measurements
                        const float s5H = height - s1H - s2H - s3H - s4H; //section 5 height
                        const float s5Start = s4Start + s4H; //section 5 start
                        const float s5Offset = 30; // section 5 offset
                        const float qrMiddle = bpStartY + height - s5H / 2;
                        const int qrSide = 200;

                        foreach (var boardingPass in boardingPasses)
                        {
                            // draw page 1
                            using (var pdfCanvas = document.BeginPage(width, height))
                            {
                                const string resourceID = "Nacelle.KMA.UI.Resources.Images.icon-plane-white-large.svg";
                                var assembly = typeof(App).GetTypeInfo().Assembly;

                                var planeSVG = new SkiaSharp.Extended.Svg.SKSvg();

                                using (var resourceStream = assembly.GetManifestResourceStream(resourceID))
                                {
                                    planeSVG.Load(resourceStream);
                                }

                                paint.Color = new SKColor(139, 198, 62);
                                paint.Style = SKPaintStyle.StrokeAndFill;

                                pdfCanvas.DrawRoundRect(bpStartX, bpStartY, width, s1H, cornerRadius, cornerRadius, paint);
                                pdfCanvas.DrawRect(bpStartX, bpStartY + s1Offset, width, s1H - s1Offset, paint);

                                paint.Color = SKColors.White;
                                paint.TextSize = 24;
                                paint.Typeface = SKTypeface.FromFamilyName(
                                    "OpenSans-Bold",
                                    SKFontStyleWeight.Normal,
                                    SKFontStyleWidth.Normal,
                                    SKFontStyleSlant.Upright);

                                var departAirportCodeTextWidth = paint.MeasureText(boardingPass.DepartureAirportCode);
                                var arriveAirportCodeTextWidth = paint.MeasureText(boardingPass.ArrivalAirportCode);

                                pdfCanvas.DrawText(boardingPass.DepartureAirportCode, departureCenterX - (departAirportCodeTextWidth / 2), bpStartY + section1TopPadding, paint);
                                pdfCanvas.DrawText(boardingPass.ArrivalAirportCode, arrivalCenterX - (arriveAirportCodeTextWidth / 2), bpStartY + section1TopPadding, paint);

                                paint.TextSize = 10;
                                paint.Typeface = SKTypeface.FromFamilyName(
                                    "OpenSans-Semibold",
                                    SKFontStyleWeight.Normal,
                                    SKFontStyleWidth.Normal,
                                    SKFontStyleSlant.Upright);

                                var departAirportTextWidth = paint.MeasureText(boardingPass.DepartureAirport);
                                var arriveAirportTextWidth = paint.MeasureText(boardingPass.ArrivalAirport);

                                pdfCanvas.DrawText(boardingPass.DepartureAirport, departureCenterX - (departAirportTextWidth / 2), bpStartY + section1NamePadding + 24, paint);
                                pdfCanvas.DrawText(boardingPass.ArrivalAirport, arrivalCenterX - (arriveAirportTextWidth / 2), bpStartY + section1NamePadding + 24, paint);

                                var svgMax = Math.Max(planeSVG.Picture.CullRect.Width, planeSVG.Picture.CullRect.Height);
                                var scale = 21 / svgMax;
                                var scaleMatrix = SKMatrix.MakeScale(scale, scale);
                                var translationMatrix = SKMatrix.MakeTranslation(bpCenterX - (planeW / 2), bpStartY + (s1H / 2) - (planeH / 2));
                                SKMatrix.PostConcat(ref scaleMatrix, translationMatrix);
                                pdfCanvas.DrawPicture(planeSVG.Picture, ref scaleMatrix);

                                // SECTION 2
                                paint.Color = SKColors.White;
                                pdfCanvas.DrawRect(bpStartX, s2Start, width, s2H, paint);

                                paint.TextSize = 10;
                                paint.Typeface = SKTypeface.FromFamilyName(
                                    "OpenSans-Bold",
                                    SKFontStyleWeight.Normal,
                                    SKFontStyleWidth.Normal,
                                    SKFontStyleSlant.Upright);
                                paint.Color = new SKColor(187, 187, 187);
                                var s2RightStart = MaxWidth(paint, "Seat", boardingPass.Seat);

                                pdfCanvas.DrawText("Name", bpStartX + sidePadding, s2Start + sectionsTopPadding, paint);
                                pdfCanvas.DrawText("Seat", bpStartX + width - s2RightStart - sidePadding, s2Start + sectionsTopPadding, paint);

                                paint.TextSize = 12;
                                paint.Color = SKColors.Black;
                                pdfCanvas.DrawText(boardingPass.PassengerName, bpStartX + sidePadding, s2Start + sectionsTopPadding + sectionsValPadding, paint);
                                pdfCanvas.DrawText(boardingPass.Seat, bpStartX + width - s2RightStart - sidePadding, s2Start + sectionsTopPadding + sectionsValPadding, paint);

                                // SECTION 3
                                paint.Color = SKColors.White;
                                pdfCanvas.DrawRect(bpStartX, s3Start, width, s3H, paint);


                                var s3RightLargest = MaxWidth(paint, "Zone", string.IsNullOrWhiteSpace(boardingPass.Zone) ? "Zone" : boardingPass.Zone);
                                var s3LeftLargest = MaxWidth(paint, "Gate", string.IsNullOrWhiteSpace(boardingPass.Gate) ? "Gate" : boardingPass.Gate);
                                var s3ThirdLargest = MaxWidth(paint, "Board", boardingPass.BoardingTime.ToString("HH:mm"));

                                var s3LeftEnd = bpStartX + sidePadding + s3LeftLargest;
                                var s3RightStart = bpStartX + width - s3RightLargest - sidePadding;

                                paint.TextSize = 10;
                                paint.Color = new SKColor(187, 187, 187);

                                var gateX = bpStartX + sidePadding;
                                var gateY = s3Start + sectionsTopPadding;
                                pdfCanvas.DrawText("Gate", gateX, gateY, paint);

                                var terminalX = s3LeftEnd + s3MiddlePadding;
                                pdfCanvas.DrawText("Terminal", terminalX, gateY, paint);

                                var boardX = s3RightStart - s3MiddlePadding - s3ThirdLargest;
                                pdfCanvas.DrawText("Board", boardX, gateY, paint);

                                var zoneX = bpStartX + width - s3RightLargest - sidePadding;
                                pdfCanvas.DrawText("Zone", zoneX, gateY, paint);

                                paint.TextSize = 12;
                                paint.Color = SKColors.Black;
                                pdfCanvas.DrawText(boardingPass.Gate, bpStartX + sidePadding, s3Start + sectionsTopPadding + sectionsValPadding, paint);
                                pdfCanvas.DrawText(boardingPass.Terminal, s3LeftEnd + s3MiddlePadding, s3Start + sectionsTopPadding + sectionsValPadding, paint);
                                pdfCanvas.DrawText(boardingPass.BoardingTime.ToString("HH:mm"), s3RightStart - s3MiddlePadding - s3ThirdLargest, s3Start + sectionsTopPadding + sectionsValPadding, paint);
                                pdfCanvas.DrawText(boardingPass.Zone, bpStartX + width - s3RightLargest - sidePadding, s3Start + sectionsTopPadding + sectionsValPadding, paint);

                                // SECTION 4
                                paint.Color = SKColors.White;
                                pdfCanvas.DrawRect(bpStartX, s4Start, width, s4H, paint);

                                paint.TextSize = 10;
                                paint.Color = new SKColor(187, 187, 187);
                                var s4RightLargest = MaxWidth(paint, "Arrive", boardingPass.ArrivalDateTime.ToString("HH:mm"));
                                var s4LeftLargest = MaxWidth(paint, "Flight", boardingPass.FlightNumber);
                                var s4MiddleLargest = MaxWidth(paint, "Depart", boardingPass.DepartureDateTime.ToString("HH:mm"));

                                var s4LeftEnd = bpStartX + sidePadding + s4LeftLargest;
                                var s4RightStart = bpStartX + width - s4RightLargest - sidePadding;

                                pdfCanvas.DrawText("Flight", bpStartX + sidePadding, s4Start + sectionsTopPadding, paint);
                                pdfCanvas.DrawText("Date", s4LeftEnd + s4MiddlePadding, s4Start + sectionsTopPadding, paint);
                                pdfCanvas.DrawText("Depart", s4RightStart - s4MiddlePadding - s4MiddleLargest, s4Start + sectionsTopPadding, paint);
                                pdfCanvas.DrawText("Arrive", bpStartX + width - s4RightLargest - sidePadding, s4Start + sectionsTopPadding, paint);

                                paint.TextSize = 12;
                                paint.Color = SKColors.Black;
                                pdfCanvas.DrawText(boardingPass.FlightNumber, bpStartX + sidePadding, s4Start + sectionsTopPadding + sectionsValPadding, paint);
                                pdfCanvas.DrawText(boardingPass.DepartureDateTime.ToString("dd/MM"), s4LeftEnd + s4MiddlePadding, s4Start + sectionsTopPadding + sectionsValPadding, paint);
                                pdfCanvas.DrawText(boardingPass.DepartureDateTime.ToString("HH:mm"), s4RightStart - s4MiddlePadding - s4MiddleLargest, s4Start + sectionsTopPadding + sectionsValPadding, paint);
                                pdfCanvas.DrawText(boardingPass.ArrivalDateTime.ToString("HH:mm"), bpStartX + width - s4RightLargest - sidePadding, s4Start + sectionsTopPadding + sectionsValPadding, paint);

                                // SECTION 5
                                paint.Color = SKColors.White;
                                pdfCanvas.DrawRect(bpStartX, s5Start, width, s5H - s5Offset, paint);
                                pdfCanvas.DrawRoundRect(bpStartX, s5Start, width, s5H, cornerRadius, cornerRadius, paint);

                                SKBitmap qrBitmap = null;
                                using (var barcodeStream = _barcodeService.CreateBarcode(boardingPass.QrCode, qrSide, qrSide))
                                {
                                    qrBitmap = SKBitmap.Decode(barcodeStream);
                                }

                                pdfCanvas.DrawBitmap(qrBitmap, bpCenterX - (qrSide / 2), qrMiddle - qrSide / 2);

                                paint.Color = new SKColor(187, 187, 187);
                                paint.StrokeWidth = 0.75f;
                                pdfCanvas.DrawLine(bpStartX, s3Start, bpStartX + width, s3Start, paint);
                                pdfCanvas.DrawLine(bpStartX, s4Start, bpStartX + width, s4Start, paint);
                                pdfCanvas.DrawLine(bpStartX, s5Start, bpStartX + width, s5Start, paint);

                                document.EndPage();
                            }
                        }
                    }

                    // end the doc
                    document.Close();
                }
            }

            return filePath;
        }

        private static float MaxWidth(SKPaint paint, string val1, string val2)
        {
            paint.TextSize = 10;
            var w1 = paint.MeasureText(val1);

            paint.TextSize = 12;
            var w2 = paint.MeasureText(val2);

            return Math.Max(w1, w2);
        }        
    }
}
