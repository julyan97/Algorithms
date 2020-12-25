using System;
using System.Diagnostics;
using System.Linq;

namespace N_QueenGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var num = int.Parse(Console.ReadLine());

            //var stop = new Stopwatch();
            //stop.Start();

            //var queen = QueenInitializer.InitializeQueenPart2
            //    .SetNum(num)
            //    .ResetMatrixes()
            //    .RandomizationStartingMatrix()
            //    .TryToSolve();

            //stop.Stop();

            //Console.WriteLine(stop.Elapsed);

            int[][] matrix = new int[][]
            {
            new int[] {0,0,1,0 },
            new int[] {1,0,0,0 },
            new int[] {0,0,1,0 },
            new int[] {0,1,0,0 },
            };
            int[][] conf = new int[6][];
            conf = conf.Select(x => x = new int[6]).ToArray();
            ResetColissionsAt((1, 4), num, ref conf);

            for (int j = 0; j < conf.Length; j++)
            {
                conf[j].ToList().ForEach(x => Console.Write(x+" "));
                Console.WriteLine();
            }
        }

        public static void ResetColissionsAt((int i, int j) plus, int num, ref int[][] conflictMatrix)// Updated Colision Check
        {
            var colisions = 0;

            int down = conflictMatrix.Length - (plus.i + 1);
            int up = (conflictMatrix.Length - down) - 1;
            int counter = 1;
            int counterStopDown = 0;
            int counterStopUp = up;

            //DOWN Check
            for (int i = 0; i < conflictMatrix.Length / 2; i++)
            {

                if (counterStopDown != down && plus.i + counter <= conflictMatrix.Length - 1 && plus.j + counter <= conflictMatrix.Length - 1)// down | right//++
                {
                    conflictMatrix[plus.i + counter][plus.j + counter] += num;
                }

                if (counterStopDown != down && plus.i + counter < conflictMatrix.Length && plus.j - counter >= 0)// down | left//++
                {
                    conflictMatrix[plus.i + counter][plus.j - counter] += num;
                }

                if (counterStopDown != down && plus.i + counter <= conflictMatrix.Length - 1)// down//++
                {
                    conflictMatrix[plus.i + counter][plus.j] += num;
                }

                counterStopDown++;
                //------------------

                if (counterStopUp != 0 && plus.i - counter >= 0 && plus.j + counter <= conflictMatrix.Length - 1)// up | right //++
                {
                    conflictMatrix[plus.i - counter][plus.j + counter] += num;
                }

                if (counterStopUp != 0 && plus.i - counter >= 0 && plus.j - counter >= 0)// up | left//++
                {
                    conflictMatrix[plus.i - counter][plus.j - counter] += num;
                }

                if (counterStopUp != 0 && plus.i - counter >= 0)// up//++
                {
                    conflictMatrix[plus.i - counter][plus.j] += num;
                }
                counterStopUp--;


                counter++;


            }


            //UP Check  counter++;
        }
        //  if (colisions > 0) return true;
        //  return false;



    }
}

