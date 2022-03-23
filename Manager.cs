using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestProg
{
    class Manager
    {
        /// <summary>
        /// Массив координат красных точек 
        /// </summary>
        private static List<Point> redPoints = new List<Point>();

        /// <summary>
        /// Массив координат зеленых точек
        /// </summary>
        private static List<Point> greenPoints = new List<Point>();

        /// <summary>
        /// Метод очистки массивов координат
        /// </summary>
        public void Clear()
        {
            redPoints.Clear();
            greenPoints.Clear();
        }

        /// <summary>
        /// Добавить зеленую точку в массив
        /// </summary>
        public void AddGreen(Point p)
        {
            greenPoints.Add(p);
        }

        /// <summary>
        /// Добавить красную точку в массив
        /// </summary>
        public void AddRed(Point p)
        {
            redPoints.Add(p);
        }

        /// <summary>
        /// Метод отрисовки соединяющих линий
        /// </summary>
        public async void Draw(Graphics _g)
        {
            await Task.Run(() =>
            {
                if (IsValid())
                {
                    double[,] matrix = new double[redPoints.Count, greenPoints.Count];
                    for (int row = 0; row < redPoints.Count; row++)
                    {
                        for (int col = 0; col < greenPoints.Count; col++)
                        {
                            matrix[row, col] = Distance(redPoints[row], greenPoints[col]);
                        }
                    }

                    int[] shortestCase = GetShortestCase(ref matrix, matrix.GetLength(0));

                    for (int rowIndex = 0; rowIndex < redPoints.Count; rowIndex++)
                    {
                        int colIndex = shortestCase[rowIndex];
                        Point p1 = new Point(redPoints[rowIndex].X, redPoints[rowIndex].Y);
                        Point p2 = new Point(greenPoints[colIndex].X, greenPoints[colIndex].Y);
                        _g.DrawLine(new Pen(Color.Blue), p1, p2);
                        _g.DrawString(rowIndex.ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), redPoints[rowIndex].X, redPoints[rowIndex].Y - 15);
                        _g.DrawString(rowIndex.ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), greenPoints[rowIndex].X, greenPoints[rowIndex].Y - 15);
                    }
                }
                else
                {
                    MessageBox.Show("Необходимо одинаковое количество красных и зеленых точек");
                }
            });
        }

        /// <summary>
        /// Проверить правильность условий отрисовки линий
        /// </summary>
        private bool IsValid()
        {
            if ((redPoints != null) && (greenPoints != null) && (greenPoints.Count == redPoints.Count))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Метод возвращает расстояние между двумя точками
        /// </summary>
        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        /// <summary>
        /// Поменять местами две точки в массиве
        /// </summary>
        private void Trans(int[] a, int i, int j)
        {
            int t = a[j];
            a[j] = a[i];
            a[i] = t;
        }

        /// <summary>
        /// Метод для поиска следующей перестановки. Возвращает true, если перестановка найдена 
        /// </summary>
        private bool Next(ref int[] perm, int n)
        {
            int i = n - 1;
            while (--i >= 0 && perm[i] > perm[i + 1]) ;
            if (i == -1)
            {
                return false;
            }

            for (int j = i + 1, k = n - 1; j < k; j++, k--)
            {
                Trans(perm, j, k);
            }

            int b = i + 1;
            while (perm[b] < perm[i])
            {
                b++;
            }

            Trans(perm, i, b);
            return true;
        }

        /// <summary>
        /// Поиск набора кратчайших расстояний между точками разных цветов. Возвращает набор индексов колонок матрицы.
        /// </summary>
        private int[] GetShortestCase(ref double[,] matrix, int n)
        {
            int[] permutations = new int[n];
            int[] answer = new int[n];
            for (int i = 0; i < n; i++)
                permutations[i] = i;
            double min = Double.PositiveInfinity;
            int k = 0;
            do
            {
                k++;
                double temp = GetCaseLength(ref matrix, ref permutations);
                if (min > temp)
                {
                    min = temp;
                    for (int i = 0; i < permutations.Length; i++)
                    {
                        answer[i] = permutations[i];
                    }

                }                             
            }
            while (Next(ref permutations, n));

            return answer;
        }

        /// <summary>
        /// Метод возвращает длину расстояний текущей перестановки
        /// </summary>
        private double GetCaseLength(ref double[,] matrix, ref int[] permutations)
        {
            double length = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                int currentNum = permutations[i];
                length += matrix[i, currentNum];
            }
            return length;
        }
    }
}
