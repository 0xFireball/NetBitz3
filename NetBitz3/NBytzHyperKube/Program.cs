/*
 */

using System;
using System.IO;
using System.Reflection;

namespace NBytzHyperKube
{
    public static class Program
    {
        private static Stream LoadResourceText(string resName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resName);
        }

        public static byte[] StrmToBytes(Stream input)
        {
            var buffer = new byte[16*1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        [STAThread]
        private static int Main(string[] args)
        {
            var inKqingo = StrmToBytes(LoadResourceText("nbPackedAssembly")); //input packed assembly, but XORed
            var nuQuLo = StrmToBytes(LoadResourceText("squirrel")); //the XOR key
            ByteMonster.Shifto(inKqingo, nuQuLo);
            var asm = Assembly.Load(inKqingo);
            var main = asm.EntryPoint;
            var inpArgs = new object[main.GetParameters().Length];
            if (inpArgs.Length != 0)
            {
                inpArgs[0] = args;
            }
            var obj = main.Invoke(null, inpArgs);
            if (obj is int)
            {
                return (int) obj;
            }
            return 0;
            //var defaultParameters = main.GetParameters().Select(p => GetDefaultValue(p.ParameterType)).ToArray();
            //main.Invoke(null, defaultParameters);
        }
    }
}