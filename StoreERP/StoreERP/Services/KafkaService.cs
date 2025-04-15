using StoreERP.Model.EventModel;

namespace StoreERP.Services;

using Confluent.Kafka;

public class KafkaService: IHostedService
{
    private ConsumerConfig Config { get; set; }
    private CancellationTokenSource CTS { get; set; }
    public required List<string> Topics { get; set; }
    public event EventHandler<Message<string, MessageModel>> Event;
    private IConsumer<string, string> _smthConsumer;

    public KafkaService()
    {
        Config = new ConsumerConfig()
        {
            BootstrapServers = "localhost:9090",

            ClientId = "erp-web",
            GroupId = "erp-app",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            Acks = Acks.All
        };

        CTS = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            CTS.Cancel();
        };
    }

    public void SetupConsumer<TValueType>(string topic)
    where TValueType: MessageModel
    {
        using (var consumer = new ConsumerBuilder<string, TValueType>(Config)
                   .Build())
        {
            consumer.Subscribe(topic);

            while (!CTS.IsCancellationRequested)
            {
                var message = consumer.Consume(CTS.Token);

                if (message is not null)
                {
                    Console.WriteLine("MESSAGE :--)");
                    // Event?.Invoke(consumer, message.Message);
                }
            }
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}