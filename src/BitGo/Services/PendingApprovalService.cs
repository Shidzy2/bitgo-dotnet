using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BitGo.Types;
using BitGo.Args;
using NBitcoin;

namespace BitGo.Services
{
    public sealed class PendingApprovalService : ApiService {
        
        internal PendingApprovalService(BitGoClient client) : base(client, "pendingapprovals") {

        }

        public Task<PendingApprovalList> GetListAsync(string walletId = null, string enterprise = null, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.GetAsync<PendingApprovalList>($"{_url}{_client.ConvertToQueryString(new Dictionary<string, object>(){ { "walletId", walletId }, { "enterprise", enterprise } })}", true, cancellationToken);

        public Task<PendingApproval> UpdateAsync(string id, string state, CancellationToken cancellationToken = default(CancellationToken)) 
            => _client.PutAsync<PendingApproval>($"{_url}/{id}", new UpdatePendingApprovalStateArgs { State = id }, cancellationToken);
        
    }
}