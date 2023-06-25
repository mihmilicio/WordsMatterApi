using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

public class RabbitMQProducer
{
  public void SendEnvioMessage<T>(T message)
  {
    var factory = new ConnectionFactory
    {
      HostName = "localhost"
    };

    var connection = factory.CreateConnection();
    using var channel = connection.CreateModel();

    channel.QueueDeclare("EnvioParticipante", exclusive: false);

    var json = JsonConvert.SerializeObject(message);
    var body = Encoding.UTF8.GetBytes(json);

    channel.BasicPublish(exchange: "", routingKey: "EnvioParticipante", body: body);
  }
}