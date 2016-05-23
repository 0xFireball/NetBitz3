/*
 */

namespace NBytzHyperKube
{
    /// <summary>
    ///     A dinosaur :D
    /// </summary>
    public class ByteMonster
    {
        public static void Shifto(byte[] data, byte[] key)
        {
            for (var i = 0; i < data.Length; i++)
            {
                data[i] = (byte) (data[i] ^ key[i]);
            }
        }
    }
}