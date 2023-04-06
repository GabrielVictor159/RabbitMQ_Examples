using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

class Program
{
    static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" }; // Altere o nome do host se necessário
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "fila_teste", durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Mensagem recebida: {0}", message);
            };
            channel.BasicConsume(queue: "fila_teste", autoAck: true, consumer: consumer);

            Console.WriteLine("Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }
    }
}
