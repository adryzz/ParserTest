namespace StupidParserTest.Attributes.Values
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ButtonAttribute : Attribute
    {
        public ButtonType Type { get; }
        
        public uint ButtonIndex { get; }
        
        public ButtonAttribute(uint index, ButtonType type = ButtonType.Unknown)
        {
            ButtonIndex = index;
            Type = type;
        }
    }
}