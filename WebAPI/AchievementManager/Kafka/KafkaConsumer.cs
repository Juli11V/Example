using AchievementManager.Services;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAPI.Domain.Constants;

namespace AchievementManager.Kafka
{
    public class KafkaConsumer : IHostedService
    {
        private readonly ILogger<KafkaConsumer> _logger;
        private readonly IConsumer<string, string> _consumer;
        private readonly KafkaOptions _options;
        private readonly string _topic;
        private readonly IAchievementService _service;

        public KafkaConsumer(ILogger<KafkaConsumer> logger, IOptions<KafkaOptions> options, IAchievementService service)
        {
            _options = options.Value;
            _logger = logger;
            _service = service;
            _topic = _options.Topic;
            var config = new ConsumerConfig
            {
                BootstrapServers = _options.BootstrapServers,
                GroupId = _options.Group,
            };
            _consumer = new ConsumerBuilder<string, string>(config).Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe(_topic);
            _logger.LogInformation($"Subscribed to topic '{_topic}'");

            ConsumeMessages(cancellationToken);

            return  Task.CompletedTask;
        }

        private void ConsumeMessages(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(cancellationToken);
                    _logger.LogInformation($"New transfer is processed: {result.Message.Value}");
                    _service.ProvideAchievement(result.Message.Value);
                }
                catch (ConsumeException e)
                {
                    _logger.LogError($"Error occurred: {e.Error.Reason}");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Close();
            _consumer.Dispose();
            return Task.CompletedTask;
        }
    }
}