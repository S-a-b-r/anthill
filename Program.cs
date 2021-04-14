using System;

namespace anthill
{
    class Program
    {
        static void Main(string[] args)
        {
            Anthill a = new Anthill();
            while (true)
            {
                while (Console.KeyAvailable)
                { 
                    var ck = Console.ReadKey(true);
                    if (ck.Key == ConsoleKey.Spacebar)
                        a.AddFood();
                    if (ck.Key == ConsoleKey.F)
                        a.AddAnt();
                    if(ck.Key == ConsoleKey.Q)
                        a.AddQueen();
                }

                a.Update();
                a.Draw();
                System.Threading.Thread.Sleep(200);
            }
        }
    }
}
