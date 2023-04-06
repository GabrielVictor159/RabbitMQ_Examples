using RabbitMQ.Client;
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
            string message = "Olá, RabbitMQ!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: "fila_teste", basicProperties: null, body: body);
            Console.WriteLine("Mensagem enviada: {0}", message);
        }

        Console.WriteLine("Pressione qualquer tecla para sair.");
        Console.ReadKey();
    }
}
