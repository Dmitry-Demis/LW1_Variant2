using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LW1_Variant2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ThirdLaboratory();
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

        static void ThirdLaboratory()
        {
            // Считывание открытого текста
            const string path = @"PlainText.TXT";
            var key = "B";
            BitArray bitArray = new BitArray(new bool[] { true, false, true, true, false });
           // BitArray bitArray = new BitArray(Encoding.ASCII.GetBytes(key));
            Console.Write($"{new string(' ', 8)}");
            foreach (bool b in bitArray)
                Console.Write(b ? 1 : 0);
            Console.WriteLine();
            bitArray = ShiftLeftCycled(bitArray, 2);           
            
            using (var sw = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                var result = sw.ReadBytes((int)new FileInfo(path).Length);

            }
            //BitArray bitArray = new BitArray()
            //string[] keys = new string[11];
            //for (int i = 1; i < keys.Length; i++)
            //{
            //    keys[i] = (key[i-1]<<<3)
            //}
            static BitArray ShiftLeftCycled(BitArray bitArray, int bitCount)
            {
                var leftShift = (BitArray)bitArray.Clone();
                var rightShift = (BitArray)bitArray.Clone();
                (leftShift).LeftShift(bitCount);
                //rightShift.RightShift(bitArray.Length - bitCount);
                //Console.Write($"LEFT_ {bitCount} ");
                foreach (bool b in leftShift)
                    Console.Write(b ? 1 : 0);
                //Console.WriteLine();                
                //Console.Write($"RIGHT {bitCount} ");
                //foreach (bool b in rightShift)
                //    Console.Write(b ? 1 : 0);
                //Console.WriteLine();
                //var result =  leftShift.Or(rightShift);
                //Console.Write($"RESULT{bitCount} ");
                //foreach (bool b in result)
                //    Console.Write(b ? 1 : 0);
                //Console.WriteLine();
                return null;
               
            }
        }

    }
}
