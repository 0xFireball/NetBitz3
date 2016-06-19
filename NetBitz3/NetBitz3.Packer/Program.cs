/*
 */

using System;
using System.IO;
using System.Linq;
using dnlib.DotNet;
using NBytzHyperKube;

namespace NetBitz3.Packer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("NetBitz3 - Nickel Hydrogen Aluminum Research < />");
            Console.WriteLine("(c) 2016, 0xFireball. All rights reserved.");
            if (args.Length == 0)
            {
                Console.WriteLine("Usage:\nNetBitzPack <input file> [options]");
                return;
            }
	        var options = new string[0];
	        if (args.Length > 1)
	        {
		        options = args.Skip(1).ToArray();
	        }
            var inputExe = args[0];
            var asmCubeBytes = File.ReadAllBytes(inputExe);
	        Console.WriteLine("Input file loaded.");
            var keyBytes = new byte[asmCubeBytes.Length];
	        Console.Write("Encrypting...");
            GenerateRandomBytes(ref keyBytes);
            ByteMonster.Shifto(asmCubeBytes, keyBytes);
	        Console.WriteLine("Done.");
	        var inputExeAsm = AssemblyDef.Load(inputExe);
            var inputExeMod = inputExeAsm.Modules[0];
            var inputExeType = inputExeMod.Kind;
            var cube = new nKubeImporter();
            var cubeHost = AssemblyDef.Load("NBytzHyperKube.exe"); //Load NBCube
            cubeHost.Name = "NBHyperKube";
            var nbCubeMod = cubeHost.Modules[0];
	        if (options.Contains("-updatemetadata"))
	        {
		        //Update metadata of nboutput to match input
		        cubeHost.Name = inputExeAsm.Name;
		        cubeHost.Version = inputExeAsm.Version;
		        nbCubeMod.Assembly.Name = inputExeMod.Assembly.Name;
		        nbCubeMod.Assembly.Version = inputExeMod.Assembly.Version;
		        foreach (var attr in inputExeMod.Assembly.CustomAttributes)
		        {
			        nbCubeMod.Assembly.CustomAttributes.Add(attr);
		        }
		        Console.WriteLine("Updated metadata.");
	        }
	        Console.Write("Creating and writing new assembly...");
            nbCubeMod.Kind = inputExeType;
            nbCubeMod.Resources.Add(new EmbeddedResource("nbPackedAssembly", asmCubeBytes));
            nbCubeMod.Resources.Add(new EmbeddedResource("squirrel", keyBytes));
            nbCubeMod.Write(Path.GetFileNameWithoutExtension(inputExe) + ".NetBitz.exe");
	        Console.WriteLine("Done.");
        }

        public static void GenerateRandomBytes(ref byte[] buffy)
        {
            var r = new Random();
            r.NextBytes(buffy);
        }
    }
}