using System.Reflection;
using System.Runtime.CompilerServices;
using StupidParserTest.Attributes;
using StupidParserTest.Attributes.Values;

namespace StupidParserTest
{
    public static class ReportParser
    {
        public static T Parse<T>(byte[] data) where T : TabletReportBase, new()
        {
            T report = new T();
            Type t = typeof(T);

            PropertyInfo[] props = t.GetProperties(BindingFlags.Public | BindingFlags.NonPublic);

            Dictionary<PropertyInfo, uint> reportComponents = new Dictionary<PropertyInfo, uint>();

            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    if (attr is ReportPositionAttribute p) //all report components have a position
                    {
                        reportComponents.Add(prop, p.Position);
                    }
                }
            }

            SortedDictionary<uint, bool> barrelButtons = new SortedDictionary<uint, bool>();
            
            SortedDictionary<uint, bool> buttons = new SortedDictionary<uint, bool>();
            
            foreach (var component in reportComponents)
            {
                IEnumerable<Attribute> attr = component.Key.GetCustomAttributes();
                var attributes = attr as Attribute[] ?? attr.ToArray();
                if (attributes.Has(out ReportIdAttribute _))
                {
                    report.ReportId = data[component.Value];
                }

                if (attributes.Has(out OnReportIdAttribute id))
                {
                    if (attributes.Has(out PenPositionAttribute pos) && id.Position == report.ReportId)
                    {
                        switch (pos.Component)
                        {
                            case VectorComponent.X:
                            {
                                report.PenX = Unsafe.ReadUnaligned<ushort>(ref data[component.Value]);
                                break;
                            }
                            case VectorComponent.Y:
                            {
                                report.PenY = Unsafe.ReadUnaligned<ushort>(ref data[component.Value]);
                                break;
                            }
                        }
                    }

                    if (attributes.Has(out TipPressureAttribute _) && id.Position == report.ReportId)
                    {
                        report.Pressure = Unsafe.ReadUnaligned<ushort>(ref data[component.Value]);
                    }

                    if (attributes.Has(out ButtonAttribute b))
                    {
                        bool but;
                        if (attributes.Has(out BitAttribute bit))
                        {
                            but = data[component.Value].IsBitSet(bit.Bit) && id.Position == report.ReportId;
                        }
                        else
                        {
                            but = data[component.Value].IsBitSet(0) && id.Position == report.ReportId;
                        }

                        switch (b.Type)
                        {
                            case ButtonType.Barrel:
                                barrelButtons.Add(b.ButtonIndex, but);
                                break;
                            case ButtonType.Other:
                            case ButtonType.Unknown:
                                buttons.Add(b.ButtonIndex, but);
                                break;
                        }
                    }
                }
            }
            
            report.BarrelButtons = new bool[barrelButtons.Count];
            report.Buttons = new bool[buttons.Count];
            for(int i = 0; i < report.BarrelButtons.Length; i++)
            {
                report.BarrelButtons[i] = barrelButtons.ElementAt(i).Value;
            }
            
            for(int i = 0; i < report.Buttons.Length; i++)
            {
                report.Buttons[i] = buttons.ElementAt(i).Value;
            }
            
            return report;
        }
        
        public static bool Has<T>(this IEnumerable<Attribute> a, out T var) where T : Attribute
        {
            foreach (var attr in a)
            {
                if (attr is T t)
                {
                    var = t;
                    return true;
                }
            }

            var = null;
            return false;
        }
        
        public static bool IsBitSet(this byte a, int bit)
        {
            return (a & (1 << bit)) != 0;
        }
    }
}