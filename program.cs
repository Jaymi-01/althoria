// Program.cs
using System;
using Althoria.Core;
using System.Text;

namespace Althoria
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            Game game = new Game();
            game.Start();
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}