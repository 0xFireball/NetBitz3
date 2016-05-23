﻿/*
 */
using System;
using System.Linq;
using System.IO;
using System.Reflection;

namespace NBytzHyperKube
{
	public static class Program
	{
		static Stream LoadResourceText(string resName)
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceStream(resName);
		}
		public static byte[] StrmToBytes(Stream input)
		{
		    byte[] buffer = new byte[16*1024];
		    using (MemoryStream ms = new MemoryStream())
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
		public static void Main(string[] args)
		{
			var asm = Assembly.Load(StrmToBytes(LoadResourceText("nbPackedAssembly")));
			MethodInfo main = asm.EntryPoint;
            var defaultParameters = main.GetParameters().Select(p => GetDefaultValue(p.ParameterType)).ToArray();
            main.Invoke(null, defaultParameters);
		}
	}
}