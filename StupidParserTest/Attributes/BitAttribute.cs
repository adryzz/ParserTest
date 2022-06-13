namespace StupidParserTest.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BitAttribute : Attribute
    {
        public ushort Bit { get; }
        
        public BitAttribute(ushort bit = 0)
        {
            Bit = bit;
        }
    }
}