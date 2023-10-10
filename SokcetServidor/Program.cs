using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SokcetServidor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string direccionIP = "127.0.0.1"; 
            int puerto = 11200; 

            // Crear el socket del servidor
            Socket servidorSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Asociar el socket a la dirección y puerto
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(direccionIP), puerto);
            servidorSocket.Bind(endPoint);

            // Comenzar a escuchar conexiones entrantes
            servidorSocket.Listen(10);

            Console.WriteLine("Esperando conexiones entrantes...");

            // Aceptar una conexión entrante
            Socket clienteSocket = servidorSocket.Accept();
            Console.WriteLine($"Cliente conectado: {clienteSocket.RemoteEndPoint}");

            // Recibir datos del cliente
            byte[] buffer = new byte[1024];
            int bytesRecibidos = clienteSocket.Receive(buffer);
            string mensajeRecibido = Encoding.ASCII.GetString(buffer, 0, bytesRecibidos);
            Console.WriteLine($"Mensaje recibido: {mensajeRecibido}");

            // Enviar una respuesta al cliente
            string respuesta = "¡Hola, cliente!";
            byte[] respuestaBytes = Encoding.ASCII.GetBytes(respuesta);
            clienteSocket.Send(respuestaBytes);

            // Cerrar el socket del cliente y el socket del servidor
            clienteSocket.Close();
            servidorSocket.Close();
            Console.ReadKey();
        }
    }
}
