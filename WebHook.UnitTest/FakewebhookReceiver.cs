using WebHook.API;

namespace WebHook.UnitTest
{
    public class FakewebhookReceiver : IReceiveWebhook
    {
        public List<string> Receipts = new List<string>();

        public async Task<string> ProcessRequest(string requestBody)
        {
            Receipts.Add(requestBody);
            return await Task.FromResult("Hello back");
        }
    }
}
