using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAPI.Domain.Constants;

namespace WebAPI.Application.Kafka;

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaProducer> _logger;
    private readonly KafkaOptions _options;
    private readonly string _topic;

    public KafkaProducer(IOptions<KafkaOptions> options, ILogger<KafkaProducer> logger)
    {
        _logger = logger;
        _options = options.Value;
        var config = new ProducerConfig
        {
            BootstrapServers = _options.BootstrapServers
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
        _topic = _options.Topic;
    }

    public async Task ProduceAsync(string key, string message)
    {
        try
        {
            var result = await _producer.ProduceAsync(_topic, new Message<string, string> { Key = key, Value = message });
            _logger.LogInformation($"Delivered '{result.Value}' to '{result.TopicPartitionOffset}'");
        }
        catch (ProduceException<string, string> e)
        {
            _logger.LogError($"Delivery failed: {e.Error.Reason}");
        }
    }
}