# BitGo Api Library

[![Build Status](https://travis-ci.org/playhubdev/bitgo-dotnet.svg?branch=master)](https://travis-ci.org/playhubdev/bitgo-dotnet)
[![license](https://img.shields.io/github/license/playhubdev/bitgo-dotnet.svg?maxAge=2592000)](https://raw.githubusercontent.com/playhubdev/bitgo-dotnet/master/LICENSE.txt)

C# library to talk to BitGo's [Platform API](https://www.bitgo.com/api)

## Usage

```C#
static async void testApiAsync()
{
    var bitGoClient = new BitGo.BitGoClient(BitGo.BitGoNetwork.Main, "your API access Token");
    var me = await bitGoClient.User.GetAsync();
    System.Console.WriteLine("Hello my name is " + me.Name.Full);
}
```

