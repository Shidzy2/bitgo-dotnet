using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BitGo.Types;
using BitGo.Args;
using NBitcoin;

namespace BitGo.Services
{
    public sealed class WebhookService : ApiService, IWebhookService {
        
        internal WebhookService(BitGoClient client) : base(client, "webhooks") {

        }

        public Task<UserWebhook[]> GetListAsync(CancellationToken cancellationToken = default(CancellationToken))
            => _client.GetAsync<UserWebhook[]>($"{_url}", true, cancellationToken);
        public Task<UserWebhook> AddAsync(string type, string url, string coin = "bitcoin", CancellationToken cancellationToken = default(CancellationToken))
            => _client.PostAsync<UserWebhook>($"{_url}", new AddWebhookArgs { Type = type, Url = url, Coin = coin }, cancellationToken);

        public Task RemoveAsync(string type, string url, CancellationToken cancellationToken = default(CancellationToken))
            => _client.DeleteAsync<object>($"{_url}", new RemoveWebhookArgs { Type = type, Url = url }, cancellationToken);
        
    }
}