namespace StupidParserTest.Attributes.Values
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PenPositionAttribute : Attribute
    {
        public VectorComponent Component { get; }
        
        public PenPositionAttribute(VectorComponent component)
        {
            
        }
    }
}