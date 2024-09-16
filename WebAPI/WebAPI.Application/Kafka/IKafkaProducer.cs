namespace WebAPI.Application.Kafka;

public interface IKafkaProducer
{
    Task ProduceAsync(string key, string message);
}