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
			if (args.Length == 0)
			{
				Console.WriteLine("Usage:\nNetBitzPack <input file> [options]");
				return;
			}
			string inputExe = args[0];
			byte[] asmCubeBytes = File.ReadAllBytes(inputExe);
			byte[] keyBytes = new byte[asmCubeBytes.Length];
			NBytzHyperKube.ByteMonster.Shifto(asmCubeBytes, keyBytes);
			GenerateRandomBytes(ref keyBytes);
			var inputExeMod = AssemblyDef.Load(inputExe).Modules[0];
			var inputExeType = inputExeMod.Kind;
			var cube = new NBytzHyperKube.nKubeImporter();
			AssemblyDef cubeDll = AssemblyDef.Load("NBytzHyperKube.exe"); //Load NBCube
            cubeDll.Name = "NBHyperKube";
            ModuleDef nbCubeMod = cubeDll.Modules[0];
			nbCubeMod.Kind = inputExeType;
            nbCubeMod.Resources.Add(new EmbeddedResource("nbPackedAssembly", asmCubeBytes));
            nbCubeMod.Resources.Add(new EmbeddedResource("squirrel", keyBytes));
            nbCubeMod.Write(Path.GetFileNameWithoutExtension(inputExe)+".NetBitz.exe");
		}
		
		public static void GenerateRandomBytes(ref byte[] buffy)
		{
			var r = new Random();
			r.NextBytes(buffy);
		}
	}
}