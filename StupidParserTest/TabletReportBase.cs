using System.Numerics;

namespace StupidParserTest
{
    public abstract class TabletReportBase : ReportBase
    {
        public abstract byte ReportId { get; set; }
        
        public abstract ushort PenX { get; set; }
        
        public abstract ushort PenY { get; set; }

        public Vector2 Position => new Vector2(PenX, PenY);
        
        public abstract ushort Pressure { get; set; }
        
        public bool[] BarrelButtons { get; set; }
        
        public bool[] Buttons { get; set; }
    }
}