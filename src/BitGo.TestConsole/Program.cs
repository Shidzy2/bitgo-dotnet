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
            // var user = await bitGoClient.Users.LoginAsync("", "", "");
            // bitGoClient.SetAccessToken(user.AccessToken);
            // var session = await bitGoClient.Users.GetSessionAsync();
            // Console.WriteLine(session.Client);
            var wallets = await bitGoClient.Wallets.GetListAsync();
            Console.WriteLine(wallets.Start);
            Console.WriteLine(wallets.Count);
            Console.WriteLine(wallets.Total);
            foreach(var k in wallets.Wallets) {
                // Console.WriteLine(k.ExtendedPublicKey);
                Console.WriteLine((await bitGoClient.Wallets.GetAsync(k.Id)).Label);
            }
        }
    }
}
