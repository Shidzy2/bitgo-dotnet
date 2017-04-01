# BitGo Api Library

[![Build Status](https://travis-ci.org/playhub/bitgo-dotnet.svg?branch=master)](https://travis-ci.org/playhub/bitgo-dotnet)
[![Build status](https://ci.appveyor.com/api/projects/status/5b4kgqt874b86g6q/branch/master?svg=true)](https://ci.appveyor.com/project/bdangh/bitgo-dotnet/branch/master)
[![NuGet](https://img.shields.io/nuget/vpre/BitGo.svg)](https://www.nuget.org/packages/BitGo/)
[![license](https://img.shields.io/github/license/playhub/bitgo-dotnet.svg?maxAge=2592000)](https://raw.githubusercontent.com/playhub/bitgo-dotnet/master/LICENSE.txt)

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

