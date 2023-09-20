namespace WebHook.API
{
    public class ConsoleWebhookReceiver : IReceiveWebhook
    {
        private readonly ILogger<ConsoleWebhookReceiver> _logger;

        public ConsoleWebhookReceiver(ILogger<ConsoleWebhookReceiver> logger)
        {
            _logger = logger;
        }
        public async Task<string> ProcessRequest(string requestBody)
        {
            _logger.LogInformation($"Request body: {requestBody}");
            return await Task.FromResult("{\"message\" : \"Thanks! We got your webhook\"}");
        }
    }
}
