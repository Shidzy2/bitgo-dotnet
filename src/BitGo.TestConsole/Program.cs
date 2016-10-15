using System;
using System.Linq;
using Newtonsoft.Json;
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
            Console.WriteLine(Convert.FromBase64String("LXlRRxUav8Q=").Length);
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                .AddUserSecrets();
            RunAsync(builder.Build()).Wait();
        }

        private static async Task RunAsync(IConfigurationRoot configuration) {
            var bitGoClient = new BitGoClient(BitGoNetwork.Main); //, configuration["Token"]
            var user = await bitGoClient.User.LoginAsync(Console.ReadLine(), Console.ReadLine(), Console.ReadLine());
            bitGoClient.SetAccessToken(user.AccessToken);
            await bitGoClient.User.UnlockAsync(Console.ReadLine());
            // var session = await bitGoClient.Users.GetSessionAsync();
            // Console.WriteLine(session.Client);
            var keychains = await bitGoClient.Keychains.GetListAsync();
            // Console.WriteLine(wallets.Start);
            // Console.WriteLine(wallets.Count);
            // Console.WriteLine(wallets.Total);
            foreach(var k in keychains.Keychains) {
                // Console.WriteLine(k.ExtendedPublicKey);
                Console.WriteLine((await bitGoClient.Keychains.GetAsync(k.ExtendedPublicKey)).EncryptedExtendedPrivateKey);
            }
        }
    }
}
