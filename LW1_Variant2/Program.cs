using System;
using System.IO;

namespace LW1_Variant2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Считывание открытого текста
            var path = @"D:\HEI\BLOCK 4C\Ignatiev\LW1_Variant2\LW1_Variant2\PlainText.TXT";
            using var sr = new StreamReader(path);
            var result = sr.ReadToEnd();

            // Шифрование текста с ключом
            var encryptedPath = @"D:\HEI\BLOCK 4C\Ignatiev\LW1_Variant2\LW1_Variant2\EncryptedText.TXT";
            const string key = "PROCRASTINATION";
            using (var swEncrypted = new BinaryWriter(File.Open(encryptedPath, FileMode.Create)))
            {
                for (var index = 0; index < result.Length; index++)
                {
                    var s = result[index];
                    var pI = (byte)result[index];
                    var kI = (byte)key[index % key.Length];
                    var cI = (byte)(pI + kI);
                    swEncrypted.Write(cI);
                }
            }

            //Считывание зашифрованного текста
            using var srDecrypted = new BinaryReader(File.Open(encryptedPath, FileMode.Open));
            var bytes = srDecrypted.ReadBytes(result.Length);

            // Расшифрование зашифрованного текста
            var decryptedPath = @"D:\HEI\BLOCK 4C\Ignatiev\LW1_Variant2\LW1_Variant2\DecryptedText.TXT";
            using var swEncryptedText = new StreamWriter(File.Open(decryptedPath, FileMode.Create));
            for (int index = 0; index < bytes.Length; index++)
            {
                var cI = bytes[index];
                var kI = (byte)key[index % key.Length];
                var pI = (byte)(cI - kI);
                var @char = (char)pI;
                swEncryptedText.Write(pI);
            }

        }
    }
}
