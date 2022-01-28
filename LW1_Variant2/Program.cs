using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LW1_Variant2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SecondLaboratory();
        }
        static void FirstLaboratory()
        {
            // Считывание открытого текста
            var path = @"PlainText.TXT";
            using var sr = new StreamReader(path);
            var result = sr.ReadToEnd();

            // Шифрование текста с ключом
            var encryptedPath = @"EncryptedText.TXT";
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
            var decryptedPath = @"DecryptedText.TXT";
            using var swEncryptedText = new StreamWriter(File.Open(decryptedPath, FileMode.Create));
            for (int index = 0; index < bytes.Length; index++)
            {
                var cI = bytes[index];
                var kI = (byte)key[index % key.Length];
                var pI = (byte)(cI - kI);
                var @char = (char)pI;
                swEncryptedText.Write(@char);
            }

        }
        static void SecondLaboratory()
        {
            Dictionary<char, uint> dictionary = new();
            uint size = 32;           
            for (int i = 0; i < size; i++)
            {
                dictionary[(char)('А' + i)] = Convert.ToUInt32(Convert.ToString(i, toBase: 2));          
            }
           
            var path = @"LW2.TXT";
            using var sr = new StreamReader(path);
            var text = sr.ReadToEnd();
            text = text.Replace('Ё', 'Е');
            string synonyms = string.Empty;
         
            for (int i = 0; i < text.Length; i++)
            {
                synonyms += Convert.ToString(dictionary[text[i]]).PadLeft(5, '0');
            }
            int remain = synonyms.Length % 8;
            synonyms += Convert.ToString('0').PadRight(8 - remain);

            // Шифрование текста с ключом
            var encryptedPath = @"EncryptedText.TXT";
            using (var swEncrypted = new BinaryWriter(File.Open(encryptedPath, FileMode.Create)))
            {
                for (int i = 0; i < synonyms.Length; i+=8)
                {
                    var s = synonyms.Substring(i, 8);                  
                    byte b = 0;
                    for (int j = 0; j < s.Length; j++) 
                    { 
                        if (s[j]=='1')
                        {
                            b += (byte)(1 << (s.Length-1 - j));
                        }
                    }
                    swEncrypted.Write(b);
                }
            }
        }
    }
}
