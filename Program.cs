// See https://aka.ms/new-console-template for more information


using EventStore.Client;
using System.Text;

var client = new EventStorePersistentSubscriptionsClient(EventStoreClientSettings.Create("esdb://localhost:2113?tls=false"));

await client.CreateAsync("HelloWorld", "TestGroup", new PersistentSubscriptionSettings(startFrom: StreamPosition.End));

await SubscribeAsync(client);

static Task<PersistentSubscription> SubscribeAsync(EventStorePersistentSubscriptionsClient client)
{
    return client.SubscribeToStreamAsync("HelloWorld", "TestGroup",
        async (subscription, evt, retryCount, cancelToken) =>
        {
           // await HandleEvent(evnt);
            await subscription.Ack(evt);
            Console.WriteLine("Received: " + Encoding.UTF8.GetString(evt.Event.Data.Span));

            //return Task.CompletedTask;
        });
}

Console.ReadLine();