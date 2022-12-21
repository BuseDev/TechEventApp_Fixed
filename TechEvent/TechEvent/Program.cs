using System;

namespace TechEvent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TechEventApp app = new TechEventApp();
            app.Start();

            Console.ReadKey();
        }

    }
}
