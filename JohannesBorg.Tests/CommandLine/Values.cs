using System;
using NUnit.Framework;

namespace JohannesBorg.Tests.CommandLine
{
    public static class Values
    {
        public static Context Context = ParseCommandLineOrDefault(Keys.TestContext, defaultEnum: Context.Local);

        private static T ParseCommandLineOrDefault<T>(Keys key, T defaultEnum)
        {
            return ParseOrDefault(TestContext.Parameters[key.ToString()], defaultEnum);
        }

        private static T ParseOrDefault<T>(string value, T defaultEnum)
        {
            T enumeration = defaultEnum;
            if (value != null)
            {
                enumeration = (T)Enum.Parse(typeof(T), value);
            }
            return enumeration;
        }
    }
}