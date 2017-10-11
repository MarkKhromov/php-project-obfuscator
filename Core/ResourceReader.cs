using System;
using System.IO;
using System.Reflection;

namespace PHPProjectObfuscator.Core {
    static class ResourceReader {
        public static T Read<T>(string resourceName) {
            using(var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            using(var streamReader = new StreamReader(stream)) {
                return (T)Convert.ChangeType(streamReader.ReadToEnd(), typeof(T));
            }
        }
    }
}