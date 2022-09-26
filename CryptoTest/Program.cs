using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CryptoTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            string dataToProtect;
            byte[] entropy = new byte[256];
            RNGCryptoServiceProvider.Create().GetBytes(entropy);
            Console.WriteLine("Введите пароль для шифрования");
            dataToProtect = Console.ReadLine();

            byte[] hashedPassword = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(dataToProtect));
            Console.WriteLine("Hashed password: {0}", Encoding.UTF8.GetString(hashedPassword));

            byte[] protectedData = ProtectedData.Protect(hashedPassword, entropy, DataProtectionScope.CurrentUser);
            Console.WriteLine("Protected data: {0}", Encoding.UTF8.GetString(protectedData));

            using (FileStream fs = new FileStream("test", FileMode.OpenOrCreate))
            {
                for (int i = 0; i < protectedData.Length; i++)
                {
                    fs.WriteByte(protectedData[i]);
                }
            }
            
            Console.WriteLine("Пароль зашифрован");

            Console.WriteLine("Введите пароль для дешифровки");
            string unprotectData = Console.ReadLine();
            
            byte[] equalHashedPassword = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(unprotectData));
            Console.WriteLine("Hashed un password {0}", Encoding.UTF8.GetString(equalHashedPassword));
            byte[] file = File.ReadAllBytes(Environment.CurrentDirectory + "/test");
            byte[] unprotectedData = ProtectedData.Unprotect(file, entropy, DataProtectionScope.CurrentUser);
            Console.WriteLine("Unprotected pass: {0}", Encoding.UTF8.GetString(unprotectedData));
            if (equalHashedPassword == unprotectedData)
            {
                Console.WriteLine("Match");
            }
            Console.ReadKey();

        }
    }
}
