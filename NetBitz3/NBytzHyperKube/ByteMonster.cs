/*
 */
using System;

namespace NBytzHyperKube
{
	/// <summary>
	/// A dinosaur :D
	/// </summary>
	public class ByteMonster
	{
		public static void Shifto(byte[] data, byte[] key)
		{
		    for(int i = 0; i < data.Length; i++)
		    {
		        data[i] = (byte) (data[i] ^ key[i]);
		    }
		}
	}
}
