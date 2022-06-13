namespace StupidParserTest.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ReportPositionAttribute : Attribute
    {
        public uint Position { get; }
        
        public ReportPositionAttribute(uint position)
        {
            Position = position;
        }
    }
}