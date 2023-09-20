namespace WebHook.API
{
    public interface IReceiveWebhook
    {
        Task<string> ProcessRequest(string requestBody);
    }
}
