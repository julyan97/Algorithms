using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N_QueenGame
{
    class QueensPart2
    {
        private int[][] matrix;
        private int num;
        private int[][] conflictMatrix;
        private List<(int i, int j, int conflictNumber)> queensDescription = new List<(int i, int j, int conflictNumber)>();
        private Random r = new Random();

        public QueensPart2(int[][] matrix)
        {
            this.Matrix = matrix;

        }
        public QueensPart2()
        {
        }

        public int[][] Matrix { get => matrix; set => matrix = value; }
        public int[][] ConflictMatrix { get => conflictMatrix; set => conflictMatrix = value; }

        public void ClearConflictMatrix()
        {
            ConflictMatrix = new int[matrix.Length][];
            // ConflictMatrix = ConflictMatrix.Select(x => x = new int[matrix.Length]).ToArray();

            for (int i = 0; i < matrix.Length; i++)
            {
                conflictMatrix[i] = new int[matrix.Length];
            }
        }
        public QueensPart2 SetNum(int num)
        {
            this.num = num;
            return this;
        }
        public QueensPart2 ResetMatrixes()
        {
            matrix = new int[num][];
            conflictMatrix = new int[num][];
            /*{
            new int[] {0,0,1,0 },
            new int[] {1,0,0,0 },
            new int[] {0,0,0,1 },
            new int[] {0,1,0,0 },
        };*/

            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new int[num];
                conflictMatrix[i] = new int[num];
            }
            return this;
        }
        public QueensPart2 PrintMatrix()
        {

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    Console.Write(Matrix[i][j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            for (int i = 0; i < ConflictMatrix.Length; i++)
            {
                for (int j = 0; j < ConflictMatrix.Length; j++)
                {
                    Console.Write(ConflictMatrix[i][j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            return this;
        }
        public int ConflictCount(int row, int col)//(i,j) = Conflicts
        {
            int down = matrix.Length - (row + 1);
            int up = (matrix.Length - down) - 1;
            int counter = 1;

            //UP Check
            for (int i = 0; i < up; i++)
            {
                if (row - counter >= 0 && col + counter <= matrix.Length - 1)// up | right
                {
                    if (matrix[row - counter][col + counter] == 1)
                    {
                        conflictMatrix[row][col]++;
                    }
                }

                if (row - counter >= 0 && col - counter >= 0)// up | left
                {
                    if (matrix[row - counter][col - counter] == 1)
                    {
                        conflictMatrix[row][col]++;
                    }
                }

                if (row - counter >= 0)// up
                {
                    if (matrix[row - counter][col] == 1)
                    {
                        conflictMatrix[row][col]++;
                    }
                }
                counter++;
            }
            return conflictMatrix[row][col];
            //  if (colisions > 0) return true;
            //  return false;



        }
        private bool PlusConflicts(int row, int col)// Creates conflicts upword during randomization
        {
            int down = matrix.Length - (row + 1);
            int up = (matrix.Length - down) - 1;
            int counter = 1;

            //UP Check
            for (int i = 0; i < up; i++)
            {
                if (row - counter >= 0 && col + counter <= matrix.Length - 1)// up | right
                {
                    conflictMatrix[row - counter][col + counter]++;
                }

                if (row - counter >= 0 && col - counter >= 0)// up | left
                {
                    conflictMatrix[row - counter][col - counter]++;
                }

                if (row - counter >= 0)// up
                {
                    conflictMatrix[row - counter][col]++;
                }
                counter++;
            }
            return true;
            //  if (colisions > 0) return true;
            //  return false;



        }
        public QueensPart2 ResetColissionsAt((int i, int j) plus, int num)// Updated Colision Check
        {
            var colisions = 0;

            int down = matrix.Length - (plus.i + 1);
            int up = (matrix.Length - down) - 1;
            int counter = 1;

            //DOWN Check
            for (int i = 0; i < down; i++)
            {

                if (plus.i + counter <= matrix.Length - 1 && plus.j + counter <= matrix.Length - 1)// down | right//++
                {
                    conflictMatrix[plus.i + counter][plus.j + counter] += num;
                }


                if (plus.i + counter < matrix.Length && plus.j - counter >= 0)// down | left//++
                {
                    conflictMatrix[plus.i + counter][plus.j - counter] += num;
                }

                if (plus.i + counter <= matrix.Length - 1)// down//++
                {
                    conflictMatrix[plus.i + counter][plus.j] += num;
                }
                counter++;
            }

            counter = 1;

            //UP Check
            for (int i = 0; i < up; i++)
            {

                if (plus.i - counter >= 0 && plus.j + counter <= matrix.Length - 1)// up | right //++
                {
                    conflictMatrix[plus.i - counter][plus.j + counter] += num;
                }

                if (plus.i - counter >= 0 && plus.j - counter >= 0)// up | left//++
                {
                    conflictMatrix[plus.i - counter][plus.j - counter] += num;
                }


                if (plus.i - counter >= 0)// up//++
                {
                    conflictMatrix[plus.i - counter][plus.j] += num;
                }

                counter++;
            }
            return this;
            //  if (colisions > 0) return true;
            //  return false;



        }
        public QueensPart2 RandomizationStartingMatrix()
        {
            int min = int.MaxValue;
            List<int> jList = new List<int>();
            for (int i = 0; i < matrix.Length; i++)
            {

                //infinityColision = false;
                if (i == 0)
                {
                    matrix[i][r.Next(0, matrix.Length)] = 1;
                    continue;
                }
                for (int j = 0; j < matrix.Length; j++)
                {
                    var conflictCount = ConflictCount(i, j);
                    if (min == conflictCount) jList.Add(j);
                    if (min > conflictCount)
                    {
                        min = conflictCount;
                        jList.Clear();
                        jList.Add(j);
                    }


                }
                var rand = r.Next(0, jList.Count);
                var col = jList[rand];
                matrix[i][col] = 1;
                PlusConflicts(i, col);
                min = int.MaxValue;
            }
            for (int i = 0; i < matrix.Length; i++)
            {
                var j = matrix[i].ToList().IndexOf(1);
                queensDescription.Add((i, j, conflictMatrix[i][j]));
            }
            return this;
        }
        private void RefreshQueenDescription()
        {
            queensDescription = new List<(int i, int j, int conflictNumber)>();
            for (int i = 0; i < matrix.Length; i++)
            {
                var j = matrix[i].ToList().IndexOf(1);
                queensDescription.Add((i, j, conflictMatrix[i][j]));
            }
        }
        public QueensPart2 TryToSolve()
        {

            while (true)
            {
                var steps = 0;

                while (steps < 60)
                {
                    queensDescription.Sort((x, y) => y.conflictNumber.CompareTo(x.conflictNumber));
                    var sortedQDescription = queensDescription;
                    var toplist = new List<(int i, int j, int conflixtNumber)>();
                    for (int i = 0; i < 3; i++)
                    {
                        toplist.Add(queensDescription[i]);
                    }
                    toplist = toplist.Where(x => x.conflixtNumber == queensDescription.First().conflictNumber).ToList();

                    var max = toplist[r.Next(0, toplist.Count)];
                    if (sortedQDescription.First().conflictNumber == 0) return this;
                    if (sortedQDescription.First().conflictNumber < 0) break;
                    var min = conflictMatrix[max.i][0];
                    var minIndexes = new List<int>();

                    for (int j = 0; j < conflictMatrix[max.i].Length; j++)//find min number at row max.i
                    {

                        if (min == conflictMatrix[max.i][j]) minIndexes.Add(j);
                        if (min > conflictMatrix[max.i][j])
                        {
                            minIndexes.Clear();
                            min = conflictMatrix[max.i][j];
                            minIndexes.Add(j);
                        }

                    }

                    var randMinJ = r.Next(minIndexes.Count);
                    matrix[max.i][max.j] = 0;
                    matrix[max.i][minIndexes[randMinJ]] = 1;

                    ResetColissionsAt((max.i, max.j), -1);
                    ResetColissionsAt((max.i, minIndexes[randMinJ]), +1);
                    RefreshQueenDescription();

                    steps++;
                }
                // Console.WriteLine("here");
                ResetMatrixes();
                RandomizationStartingMatrix();
            }
        }
    }
}
