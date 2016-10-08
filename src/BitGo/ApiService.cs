using System;


namespace BitGo
{
    public abstract class ApiService {
        internal readonly string _url;
        internal readonly BitGoClient _client;
        internal ApiService(BitGoClient client, string url) {
            _url = url;
            _client = client;
        }
    }
}