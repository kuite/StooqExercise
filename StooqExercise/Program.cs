using System;
using System.Timers;
using StooqExercise.Persisters;

namespace StooqExercise
{
    class Program
    {
        private const int Interval = 1000;

        static void Main(string[] args)
        {
            var processor = new StooqProcessor(new FilePersister());
            var timer = new Timer(Interval);

            timer.Elapsed += processor.Update;
            timer.Elapsed += processor.Save;
            timer.Enabled = true;
            timer.AutoReset = true;
            
            Console.ReadLine();
        }
    }
}
