/*
 */
using System;
using System.IO;
using dnlib.DotNet;

namespace NetBitz3.Packer
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("NetBitz3 - Nickel Hydrogen Aluminum Research < />");
			Console.WriteLine("(c) 2016, Nickel Hydrogen Aluminum Research. All rights reserved.");
			string inputExe = args[0];
			byte[] asmCubeBytes = File.ReadAllBytes(inputExe);
			var cube = new NBytzHyperKube.nKubeImporter();
			AssemblyDef cubeDll = AssemblyDef.Load("NBytzHyperKube.dll"); //Load NBCube
            cubeDll.Name = "NBHyperKube";
            ModuleDef nbCubeMod = cubeDll.Modules[0];
            nbCubeMod.Resources.Add(new EmbeddedResource("nbPackedAssembly", asmCubeBytes));
            nbCubeMod.Write(Path.GetFileNameWithoutExtension(inputExe)+".NetBitz.exe");
		}
	}
}