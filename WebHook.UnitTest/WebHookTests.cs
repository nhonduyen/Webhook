using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using WebHook.API;

namespace WebHook.UnitTest
{
    public class WebHookTests
    {
        [Fact]
        public async Task TestReceivingWebhook()
        {
            var fakeReciever = new FakewebhookReceiver();

            await WithTestServer(async (c, s) =>
            {
                var response = await c.PostAsync("webhook", new StringContent("Hi"));

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseText = await response.Content.ReadAsStringAsync();

                Assert.Equal("Hello back", responseText);

                Assert.Equal("Hi", fakeReciever.Receipts.First());
            }, s => s.AddSingleton((IReceiveWebhook) fakeReciever));
        }

        [Fact]
        public async Task TestLiveWebhook()
        {
            using var client = new HttpClient();
            var response = await client.PostAsync("http://localhost:5229/webhook", new StringContent("Hi"));
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("{\"message\" : \"Thanks! We got your webhook\"}", responseBody);
        }

        private async Task WithTestServer(Func<HttpClient, IServiceProvider, Task> test, Action<IServiceCollection> configureServices)
        {
            await using var application = new WebApplicationFactory<Program>().
                WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services => configureServices(services));
                });
            using var client = application.CreateClient();
            await test(client, application.Services);
        }
    }
}