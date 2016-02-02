using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Enums
{
    public sealed class TimeZones
    {
        private static readonly Dictionary<string, TimeZones> instance = new Dictionary<string, TimeZones>();

        private readonly String name;
        private readonly int value;

        public static readonly TimeZones AST = new TimeZones(1, "Atlantic Standard Time (AST)");
        public static readonly TimeZones EST = new TimeZones(2, "Eastern Standard Time (EST)");
        public static readonly TimeZones CST = new TimeZones(3, "Central Standard Time (CST)");
        public static readonly TimeZones MST = new TimeZones(4, "Mountain Standard Time(MST)");
        public static readonly TimeZones PST = new TimeZones(5, "Pacific Standard Time (PST)");
        public static readonly TimeZones AKST = new TimeZones(6, "Alaskan Standard Time (AKST)");
        public static readonly TimeZones HST = new TimeZones(7, "Hawaii-Aleutian Standard Time(HST)");
        public static readonly TimeZones SST = new TimeZones(8, "Samoa Standard Time (UTC-11)");
        public static readonly TimeZones CHST = new TimeZones(9, "Chamorro Standard Time(UTC+10)");

        private TimeZones(int value, String name)
        {
            this.name = name;
            this.value = value;
            instance[name] = this;
        }

        public override String ToString()
        {
            return name;
        }

        public static Dictionary<string, string> getDictionary()
        {
            Dictionary<string, string> output = new Dictionary<string, string>();

            foreach (KeyValuePair<string, TimeZones> entry in instance)
            {
                output.Add(entry.Value.value.ToString(), entry.Value.ToString());
            }
            return output;
        }

        public static explicit operator TimeZones(string str)
        {
            TimeZones result;
            if (instance.TryGetValue(str, out result))
                return result;
            else
                throw new InvalidCastException(str + " is an unrecognized timezone");
        }
    }
}
