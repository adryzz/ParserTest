namespace StupidParserTest.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OnReportIdAttribute : Attribute
    {
        public byte Position { get; }
        
        public OnReportIdAttribute(byte position)
        {
            Position = position;
        }
    }
}