using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace N_QueenGame
{
    class Queens
    {
        private int[][] matrix;
        private int num;
        private int[][] conflictMatrix;
        private List<(int i, int j, int conflictNumber)> queensDescription = new List<(int i, int j, int conflictNumber)>();
        private Random r = new Random();


        public Queens(int[][] matrix)
        {
            this.Matrix = matrix;

        }
        public Queens()
        {
        }


        public int[][] Matrix { get => matrix; set => matrix = value; }
        public int[][] ConflictMatrix { get => conflictMatrix; set => conflictMatrix = value; }

        public Queens ResetConflictMatrix()
        {
            queensDescription.Clear();
            ConflictMatrix = new int[matrix.Length][];
            // ConflictMatrix = ConflictMatrix.Select(x => x = new int[matrix.Length]).ToArray();

            for (int i = 0; i < matrix.Length; i++)
            {
                conflictMatrix[i] = new int[matrix.Length]; 
            }
            
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    ConflictMatrix[i][j] = HasColisionUpdated(i, j);
                }
                //  Console.WriteLine( queenDestination[(i, matrix[i].ToList().IndexOf(1))] = HasColisionUpdated(i, matrix[i].ToList().IndexOf(1))) ;
            }
            for (int i = 0; i < matrix.Length; i++)
            {

                var j = 0;
                for (int x = 0; x < matrix[i].Length; x++)
                {
                    if (matrix[i][x] == 1) j = x;
                }
                if (j == -1) break;
                queensDescription.Add((i, j, conflictMatrix[i][j]));
            }
            return this;
        }
        public Queens ResetQueenMatrixRndomly()
        {
            GenerateEmptyMatrix();
            GenerateQueensUpdate();
            ResetConflictMatrix();

            return this;
        }
        private void Swap(int a, int b)
        {
            var temp = a;
            a = b;
            b = a;
        }
        public Queens SetNum(int num)
        {
            this.num = num;
            return this;
        }
        public Queens GenerateEmptyMatrix()
        {
            int[][] matrix = new int[num][];
            /*{
            new int[] {0,0,1,0 },
            new int[] {1,0,0,0 },
            new int[] {0,0,0,1 },
            new int[] {0,1,0,0 },
        };*/

            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new int[num];
            }
            this.Matrix = matrix;
            return this;
        }
        public Queens PrintMatrix()
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
        private bool HasColision(int row)
        {

            var col = matrix[row].ToList().IndexOf(1);

            int down = matrix.Length - (row + 1);
            int up = (matrix.Length - down) - 1;
            int count = 1;

            //DOWN Check
            for (int i = 0; i < down; i++)
            {
                if (row + count <= matrix.Length - 1 && col + count <= matrix.Length - 1)//down | right
                {
                    if (matrix[row + count][col + count] == 1)
                    {
                        return true;
                    }
                    if (matrix[row + count][col] == 1)
                    {
                        return true;
                    }
                    if (matrix[row][col + count] == 1)
                    {
                        return true;
                    }

                }

                if (row + count < matrix.Length && col - count >= 0)//down | left
                {
                    if (matrix[row + count][col - count] == 1)
                    {
                        return true;
                    }
                    if (matrix[row][col - count] == 1)
                    {
                        return true;
                    }
                    if (matrix[row + count][col] == 1)
                    {
                        return true;
                    }

                }


                count++;
            }

            count = 1;

            //UP Check
            for (int i = 0; i < up; i++)
            {
                if (row - count >= 0 && col - count >= 0)//up | left
                {
                    if (matrix[row - count][col - count] == 1)
                    {
                        return true;
                    }
                    if (matrix[row][col - count] == 1)
                    {
                        return true;
                    }
                    if (matrix[row - count][col] == 1)
                    {
                        return true;
                    }

                }

                if (row - count >= 0 && col + count <= matrix.Length - 1)//up | right
                {
                    if (matrix[row - count][col + count] == 1)
                    {
                        return true;
                    }
                    if (matrix[row][col + count] == 1)
                    {
                        return true;
                    }
                    if (matrix[row - count][col] == 1)
                    {
                        return true;
                    }

                }
                count++;
            }
            return false;



        }
        private int HasColisionUpdated(int row, int col)// Updated Colision Check
        {
            var colisions = 0;

            int down = matrix.Length - (row + 1);
            int up = (matrix.Length - down) - 1;
            int count = 1;

            //DOWN Check
            for (int i = 0; i < down; i++)
            {
                if (row + count <= matrix.Length - 1 && col + count <= matrix.Length - 1)// down | right
                {
                    if (matrix[row + count][col + count] == 1) colisions++;
                }

                if (row + count < matrix.Length && col - count >= 0)// down | left
                {
                    if (matrix[row + count][col - count] == 1) colisions++;
                }

                if (row + count <= matrix.Length - 1)// down
                {
                    if (matrix[row + count][col] == 1) colisions++;
                }
                count++;
            }

            count = 1;

            //UP Check
            for (int i = 0; i < up; i++)
            {
                if (row - count >= 0 && col + count <= matrix.Length - 1)// up | right
                {
                    if (matrix[row - count][col + count] == 1) colisions++;
                }

                if (row - count >= 0 && col - count >= 0)// up | left
                {
                    if (matrix[row - count][col - count] == 1) colisions++;
                }

                if (row - count >= 0)// up
                {
                    if (matrix[row - count][col] == 1) colisions++;
                }

                count++;
            }
            return colisions;
            //  if (colisions > 0) return true;
            //  return false;



        }
        private bool HasColisionUpdated(int row)// Updated Colision Check
        {

            var col = matrix[row].ToList().IndexOf(1);

            int down = matrix.Length - (row + 1);
            int up = (matrix.Length - down) - 1;
            int count = 1;

            //DOWN Check
            for (int i = 0; i < down; i++)
            {
                if (row + count <= matrix.Length - 1 && col + count <= matrix.Length - 1)// down | right
                {
                    if (matrix[row + count][col + count] == 1) return true;
                }

                if (row + count < matrix.Length && col - count >= 0)// down | left
                {
                    if (matrix[row + count][col - count] == 1) return true;
                }

                if (row + count <= matrix.Length - 1)// down
                {
                    if (matrix[row + count][col] == 1) return true;
                }
                count++;
            }

            count = 1;

            //UP Check
            for (int i = 0; i < up; i++)
            {
                if (row - count >= 0 && col + count <= matrix.Length - 1)// up | right
                {
                    if (matrix[row - count][col + count] == 1) return true;
                }

                if (row - count >= 0 && col - count >= 0)// up | left
                {
                    if (matrix[row - count][col - count] == 1) return true;
                }

                if (row - count >= 0)// up
                {
                    if (matrix[row - count][col] == 1) return true;
                }

                count++;
            }
            return false;



        }
        //First Algorithm
        public Queens GenerateQueens()
        {
            Random r = new Random();
            int i = 0;
            while (i < matrix.Length)
            {
                int j = r.Next(0, matrix.Length);
                matrix[i][j] = 1;
                if (HasColisionUpdated(i))
                {
                    matrix[i][j] = 0;
                    continue;
                }
                // if (hasColision(matrix.Length - 2 ) == false) break;
                i++;
            }
            return this;
        }
        //Updated Algorithm
        public Queens GenerateQueensUpdate()
        {

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
                    var rand = r.Next(0, matrix.Length);
                    
                    matrix[i][rand] = 1;
                    if(HasColisionUpdated(i))
                    {
                        matrix[i][rand] = 0;
                        if (j == matrix.Length/2) break;
                        continue;
                    }

                    break;
                }
            }
            return this;
        }

        //TODO: optimizes (faster solving)
        public Queens TryToSolve()
        {
            var steps = 0;
            bool allZeros = true;
            var last = queensDescription.First(); 
            while (true)
            {
                while (steps < 60)
                {
                    queensDescription.Sort((x, y) => y.conflictNumber.CompareTo(x.conflictNumber));

                    var max = queensDescription.First();
                    

                   // var  = queensDescription.Where(x => x.conflictNumber == max.conflictNumber).ToList();
                    List<(int i, int j, int conflictNumber)> TakeRandomElWithTheSameConflict = new List<(int i, int j, int conflictNumber)>();
                   // var list = queensDescription.Where(x => x.conflictNumber == max.conflictNumber).ToList();

                    for (int i = 0; i < queensDescription.Count; i++)
                    {
                        if (queensDescription[i].conflictNumber == max.conflictNumber) TakeRandomElWithTheSameConflict.Add(queensDescription[i]);
                        if (TakeRandomElWithTheSameConflict.Count == 3) break;

                    }
                    max = TakeRandomElWithTheSameConflict[r.Next(TakeRandomElWithTheSameConflict.Count)];
                    var min = conflictMatrix[max.i].Min();
                   // var list = conflictMatrix[max.i].Count(x => x == min);
                    List<int> list = new List<int>();
                    for (int i = 0; i < conflictMatrix[max.i].Length; i++)
                    {
                        if (conflictMatrix[max.i][i] == min) list.Add(conflictMatrix[max.i][i]);
                    }
                    var indexOfMin = conflictMatrix[max.i].ToList().IndexOf(list[r.Next(list.Count)]);
                    matrix[max.i][max.j] = 0;
                    matrix[max.i][indexOfMin] = 1;

                    ResetConflictMatrix();


                    steps++;
                   
                    if (max == last) continue;
                    last = max;
                    if (max.conflictNumber == 0)
                    {
                        Console.WriteLine(steps);
                        return this;
                    }

                }
                // GenerateQueensUpdate();
                steps = 0;
                ResetQueenMatrixRndomly();

            }


        }
    }
}
