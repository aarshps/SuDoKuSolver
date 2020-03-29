using SuDoKu.Entities;
using SuDoKu.Managers;
using System;

namespace SuDoKu
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleManager.isDebug = false;

            var values = new Value[9, 9]
            {
                { Value.Zero, Value.Zero, Value.Zero, Value.Two, Value.Six, Value.Zero, Value.Seven, Value.Zero, Value.One },
                { Value.Six, Value.Eight, Value.Zero, Value.Zero, Value.Seven, Value.Zero, Value.Zero, Value.Nine, Value.Zero },
                { Value.One, Value.Nine, Value.Zero, Value.Zero, Value.Zero, Value.Four, Value.Five, Value.Zero, Value.Zero },
                
                { Value.Eight, Value.Two, Value.Zero, Value.One, Value.Zero, Value.Zero, Value.Zero, Value.Four, Value.Zero },
                { Value.Zero, Value.Zero, Value.Four, Value.Six, Value.Zero, Value.Two, Value.Nine, Value.Zero, Value.Zero },
                { Value.Zero, Value.Five, Value.Zero, Value.Zero, Value.Zero, Value.Three, Value.Zero, Value.Two, Value.Eight },
                
                { Value.Zero, Value.Zero, Value.Nine, Value.Three, Value.Zero, Value.Zero, Value.Zero, Value.Seven, Value.Four },
                { Value.Zero, Value.Four, Value.Zero, Value.Zero, Value.Five, Value.Zero, Value.Zero, Value.Three, Value.Six },
                { Value.Seven, Value.Zero, Value.Three, Value.Zero, Value.One, Value.Eight, Value.Zero, Value.Zero, Value.Zero }
            };
            var mat = new Mat(values);
            var matManager = new MatManager(mat);
            MatManager.ShowMat(mat);

            var successMat = matManager.SolveMat();
            Console.WriteLine($"The success mat is ready, press enter to show the mat...");
            Console.ReadLine();
            MatManager.ShowMat(successMat);
            Console.ReadLine();
        }

        public static Mat ReadMat()
        {
            Console.Write("\n");
            var values = new Value[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    values[i, j] = (Value)int.Parse(Console.ReadKey().Key.ToString());
                    Console.Clear();
                    MatManager.ShowMat(new Mat(values));
                }
            }

            return new Mat(values);
        }
    }
}
