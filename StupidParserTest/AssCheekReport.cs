using System.Numerics;
using StupidParserTest.Attributes;
using StupidParserTest.Attributes.Values;

namespace StupidParserTest
{
    public class AssCheekReport : TabletReportBase
    {
        [ReportId]
        [ReportPosition(0)]
        public override byte ReportId { get; set; }
        
        [PenPosition(VectorComponent.X)]
        [OnReportId(0), ReportPosition(1)]
        public override ushort PenX { get; set; }
        
        [PenPosition(VectorComponent.Y)]
        [OnReportId(0), ReportPosition(3)]
        public override ushort PenY { get; set; }

        [TipPressure]
        [OnReportId(0), ReportPosition(5)]
        public override ushort Pressure { get; set; }
        
        [Button(0, ButtonType.Barrel)]
        [OnReportId(0), ReportPosition(6)]
        public bool BarrelButton0 { get; set; }
        
        [Button(1, ButtonType.Barrel)]
        [OnReportId(0), ReportPosition(6), Bit(1)]
        public bool BarrelButton1 { get; set; }
        
        [Button(0, ButtonType.Other)]
        [OnReportId(1), ReportPosition(7)]
        public bool Button0 { get; set; }
        
        [Button(1, ButtonType.Other)]
        [OnReportId(1), ReportPosition(7), Bit(1)]
        public bool Button1 { get; set; }
    }
}