using System;
using System.IO;
using System.Threading.Tasks;
using BitGo;
using Microsoft.Extensions.Configuration;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                .AddUserSecrets();
            RunAsync(builder.Build()).Wait();
        }

        private static async Task RunAsync(IConfigurationRoot configuration) {
            var bitGoClient = new BitGoClient(BitGoNetwork.Main, configuration["Token"]);
            
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
