/*
 */

using System;
using System.IO;
using dnlib.DotNet;
using NBytzHyperKube;

namespace NetBitz3.Packer
{
    internal class Program
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
            var inputExe = args[0];
            var asmCubeBytes = File.ReadAllBytes(inputExe);
            var keyBytes = new byte[asmCubeBytes.Length];
            GenerateRandomBytes(ref keyBytes);
            ByteMonster.Shifto(asmCubeBytes, keyBytes);
            var inputExeMod = AssemblyDef.Load(inputExe).Modules[0];
            var inputExeType = inputExeMod.Kind;
            var cube = new nKubeImporter();
            var cubeHost = AssemblyDef.Load("NBytzHyperKube.exe"); //Load NBCube
            cubeHost.Name = "NBHyperKube";
            var nbCubeMod = cubeHost.Modules[0];
            nbCubeMod.Kind = inputExeType;
            nbCubeMod.Resources.Add(new EmbeddedResource("nbPackedAssembly", asmCubeBytes));
            nbCubeMod.Resources.Add(new EmbeddedResource("squirrel", keyBytes));
            nbCubeMod.Write(Path.GetFileNameWithoutExtension(inputExe) + ".NetBitz.exe");
        }

        public static void GenerateRandomBytes(ref byte[] buffy)
        {
            var r = new Random();
            r.NextBytes(buffy);
        }
    }
}