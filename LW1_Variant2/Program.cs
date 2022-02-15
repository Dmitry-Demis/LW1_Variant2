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
            for (var index = 0; index < bytes.Length; index++)
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
            for (var i = 0; i < size; i++)
            {
                dictionary[(char)('А' + i)] = Convert.ToUInt32(Convert.ToString(i, toBase: 2));          
            }
           
            var path = @"LW2.TXT";
            using var sr = new StreamReader(path);
            var text = sr.ReadToEnd();
            text = text.Replace('Ё', 'Е');
            var synonyms = string.Empty;
         
            for (var i = 0; i < text.Length; i++)
            {
                synonyms += Convert.ToString(dictionary[text[i]]).PadLeft(5, '0');
            }
            var remain = synonyms.Length % 8;
            synonyms += Convert.ToString('0').PadRight(8 - remain);

            // Шифрование текста с ключом
            var encryptedPath = @"EncryptedText.TXT";
            using (var swEncrypted = new BinaryWriter(File.Open(encryptedPath, FileMode.Create)))
            {
                for (var i = 0; i < synonyms.Length; i+=8)
                {
                    var s = synonyms.Substring(i, 8);                  
                    byte b = 0;
                    for (var j = 0; j < s.Length; j++) 
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
            byte[] eTable =
            {
                1,45,226,147,190,69,21,174,
                120,3,135,164,184,56,207,63,
                8,103,9,148,235,38,168,107,
                189, 24, 52, 27, 187, 191, 114, 247,
                64, 53, 72, 156, 81, 47, 59, 85,
                227, 192, 159, 216, 211, 243, 141, 177,
                255, 167, 62, 220, 134, 119, 215, 166,
                17, 251, 244, 186, 146, 145, 100, 131,
                241, 51, 239, 218, 44, 181, 178, 43,
                136, 209, 153, 203, 140, 132, 29, 20,
                129, 151, 113, 202, 95, 163, 139, 87,
                60, 130, 196, 82, 92, 28, 232, 160,
                4, 180, 133, 74, 246, 19, 84, 182,
                223, 12, 26, 142, 222, 224, 57, 252,
                32, 155, 36, 78, 169, 152, 158, 171,
                242, 96, 208, 108, 234, 250, 199, 217,
                0, 212, 31, 110, 67, 188, 236, 83,
                137, 254, 122, 93, 73, 201, 50, 194,
                249, 154, 248, 109, 22, 219, 89, 150,
                68, 233, 205, 230, 70, 66, 143, 10,
                193, 204, 185, 101, 176, 210, 198, 172,
                30, 65, 98, 41, 46, 14, 116, 80,
                2, 90, 195, 37, 123, 138, 42, 91,
                240, 6, 13, 71, 111, 112, 157, 126,
                16, 206, 18, 39, 213, 76, 79, 214,
                121, 48, 104, 54, 117, 125, 228, 237,
                128, 106, 144, 55, 162, 94, 118, 170,
                197, 127, 61, 175, 165, 229, 25, 97,
                253, 77, 124, 183, 11, 238, 173, 75,
                34, 245, 231, 115, 35, 33, 200, 5,
                225, 102, 221, 179, 88, 105, 99, 86,
                15, 161, 49, 149, 23, 7, 58, 40
            };
            byte[] lTable =
            {
                128, 0, 176, 9, 96, 239, 185, 253,
                16, 18, 159, 228, 105, 186, 173, 248,
                192, 56, 194, 101, 79, 6, 148, 252,
                25, 222, 106, 27, 93, 78, 168, 130,
                112, 237, 232, 236, 114, 179, 21, 195,
                255, 171, 182, 71, 68, 1, 172, 37,
                201, 250, 142, 65, 26, 33, 203, 211,
                13, 110, 254, 38, 88, 218, 50, 15,
                32, 169, 157, 132, 152, 5, 156, 187,
                34, 140, 99, 231, 197, 225, 115, 198,
                175, 36, 91, 135, 102, 39, 247, 87,
                244, 150, 177, 183, 92, 139, 213, 84,
                121, 223, 170, 246, 62, 163, 241, 17,
                202, 245, 209, 23, 123, 147, 131, 188,
                189, 82, 30, 235, 174, 204, 214, 53,
                8, 200, 138, 180, 226, 205, 191, 217,
                208, 80, 89, 63, 77, 98, 52, 10,
                72, 136, 181, 86, 76, 46, 107, 158,
                210, 61, 60, 3, 19, 251, 151, 81,
                117, 74, 145, 113, 35, 190, 118, 42,
                95, 249, 212, 85, 11, 220, 55, 49,
                22, 116, 215, 119, 167, 230, 7, 219,
                164, 47, 70, 243, 97, 69, 103, 227,
                12, 162, 59, 28, 133, 24, 4, 29,
                41, 160, 143, 178, 90, 216, 166, 126,
                238, 141, 83, 75, 161, 154, 193, 14,
                122, 73, 165, 44, 129, 196, 199, 54,
                43, 127, 67, 149, 51, 242, 108, 104,
                109, 240, 2, 40, 206, 221, 155, 234,
                94, 153, 124, 20, 134, 207, 229, 66,
                184, 64, 120, 45, 58, 233, 100, 31,
                146, 144, 125, 57, 111, 224, 137, 48
            };
            var c = new ulong[]
            {
                0x_1673_3b1e_8e70_bd86,
                0x_477e_2456_f177_8846,
                0x_b1ba_a3b7_100a_c537,
                0x_c95a_28ac_64a5_ecab,
                0x_c667_9558_0df8_9af6,
                0x_66dc_053d_d38a_c3d8,
                0x_6ae9_3649_43bf_ebd4,
                0x_9b68_a065_5d57_921f,
                0x_715c_bb22_c1be_7bbc,
                0x_6394_5f2a_61b8_3432,
                0x_fdfb_1740_e651_1d41,
                0x_8f29_dd04_80de_e731,
                0x_7f01_a2f7_39da_6f23,
                0x_fe3a_d01c_d130_3e12,
                0x_cd0f_e0a8_af82_592c,
                0x_7dad_b2ef_c287_ce75,
                0x_1302_904f_2e72_3385,
                0x_8dcf_a981_e2c4_272f,
                0x_7a9f_52e1_1538_2bfc,
                0x_42c7_08e4_0955_5e8c
            };
            const string key = "Xe3czSLq";
            var bytesArray = Encoding.ASCII.GetBytes(key);
            var k = new byte[12, 8];
            for (var col = 0; col < 8; col++)
            {
                k[0, col] = bytesArray[col];
            }
            for (var row = 1; row < 12; row++)
            {
                for (var col = 0; col < 8; col++)
                {
                    k[row, col] = (byte)((byte)((k[row - 1, col] << 3) | (k[row - 1, col] >> 5))+c[row - 1]);
                }
            }
            const string path = @"PlainText.TXT";

            using (var sw = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                // Считываем весь файл, кратный 8 байтам
                var result = sw.ReadBytes((int)new FileInfo(path).Length);
                for (var i = 0; i < result.Length; i+=8)
                {
                    // Считываем блоки по 8 байт
                    //var eightBytes = new byte[8];
                    //for (var j = 0; j < eightBytes.Length; j++)
                    //{
                    //    eightBytes[j] = result[j];
                    //}
                    var eightBytes = result.Skip(i).Take(8).ToArray();
                    for (var round = 0; round < 6; round++)
                    {
                        int[] blockForXor = { 0, 3, 4, 7 };
                        foreach (var index in blockForXor)
                        {
                            eightBytes[index] ^= k[2 * round, index];
                            eightBytes[index] = eTable[eightBytes[index]];
                            eightBytes[index] = (byte)(eightBytes[index] + k[2 * round + 1, index]);
                        }
                        int[] blockForSum = { 1, 2, 5, 6 };
                        foreach (var index in blockForSum)
                        {
                            eightBytes[index] = (byte)(eightBytes[index] + k[2 * round, index]);
                            eightBytes[index] = lTable[eightBytes[index]];
                            eightBytes[index] ^= k[2 * round + 1, index];
                        }

                    }
                }

            }
            
        }

    }
}
