using System;
using System.Threading.Tasks;
using BitGo;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RunAsync().Wait();
            // string json = @"";//JSON string from sjcl
            // string password = "";//password string
            // SJCLDecryptor sd = new SJCLDecryptor(json, password);
            // string decodedString = sd.Plaintext;
            // Console.WriteLine(decodedString);
        }

        private static async Task RunAsync() {
            var bitGoClient = new BitGoClient(BitGoNetwork.Main, "");
            var keychains = await bitGoClient.Keychains.GetListAsync(10);
            Console.WriteLine(keychains.Start);
            Console.WriteLine(keychains.Count);
            Console.WriteLine(keychains.Total);
            foreach(var k in keychains.Keychains) {
                // Console.WriteLine(k.ExtendedPublicKey);
                Console.WriteLine((await bitGoClient.Keychains.GetAsync(k.ExtendedPublicKey)).EncryptedExtendedPrivateKey);
            }
        }
    }
}
