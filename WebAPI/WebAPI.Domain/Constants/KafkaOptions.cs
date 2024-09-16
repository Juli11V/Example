namespace WebAPI.Domain.Constants;

public class KafkaOptions
{
    public const string Kafka = "Kafka";
    public string BootstrapServers { get; set; } = String.Empty;
    public string Topic { get; set; } = String.Empty;
    public string Group { get; set; } = String.Empty;

}