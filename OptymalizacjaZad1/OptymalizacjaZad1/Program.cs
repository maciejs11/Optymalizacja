using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.IO;

//Maciej Ślusarek

namespace OptymalizacjaZad1
{
    class Program
    {
        public static double Distance(Point p1, Point p2)
        {
            int x1 = p1.X;
            int y1 = p1.Y;
            int x2 = p2.X;
            int y2 = p2.Y;
            double distBetweenPoints = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            return distBetweenPoints;
        }

        public static List<int> RandomPointsIndexes(int k, List<Point> points)
        {
            var randomPointsIndexes = new List<int>();
            var random = new Random();

            for (int i = 0; i < k; i++)
            {
                int num;
                do num = random.Next(points.Count);
                while (randomPointsIndexes.Contains(num));
                randomPointsIndexes.Add(num);
            }

            return randomPointsIndexes;
        }

        public static List<Point> IndexesToPoints(List<int> selectedIndexes, List<Point> points)
        {
            var selectedPoints = new List<Point>();
            foreach (int item in selectedIndexes)
            {
                selectedPoints.Add(points[item]);
            }
            return selectedPoints;
        }

        public static double SumDistanceList(List<Point> points)
        {
            double Sum = 0;

            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    double temp = Distance(points[i], points[j]);
                    Sum += temp;
                }
            }
            return Sum;
        }

        public static double MaxDistance(List<Point> points, int k)
        {
            double max = 0;
            List<int> randomPointsIndexes = new List<int>();
            List<Point> selectedPointsFromIndexes = new List<Point>();
            List<int> maxDistIndex = new List<int>();
            List<Point> maxDistPoint = new List<Point>();
            for (int i = 0; i < 100000; i++)
            {

                randomPointsIndexes = RandomPointsIndexes(k, points);
                selectedPointsFromIndexes = IndexesToPoints(randomPointsIndexes, points);

                double tempDistance = SumDistanceList(selectedPointsFromIndexes);

                if (tempDistance > max)
                {
                    max = tempDistance;
                    maxDistIndex = randomPointsIndexes.Skip(Math.Max(0, randomPointsIndexes.Count - k)).Take(k).ToList();
                    maxDistIndex.Sort();
                    maxDistPoint = selectedPointsFromIndexes.Skip(Math.Max(0, selectedPointsFromIndexes.Count - k)).Take(k).ToList();

                }

            }
            Console.WriteLine("Największa suma odległości: " + Math.Round(max, 2) + "\nIndeksy punktów:");
            for (int i = 0; i < k; i++)
            {
                Console.Write(maxDistIndex[i] + ", ");
            }
            return max;
        }

        static void Main(string[] args)
        {
            int k = 0;
            string path = "";
           
            try
            {
                path = Convert.ToString(args[0]);
                k = Convert.ToInt32(args[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var pointList = new List<Point>();
            try
            {
                pointList.Clear();
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] split = line.Split(',');
                        Point point = new Point(Convert.ToInt32(split[0]), Convert.ToInt32(split[1]));
                        pointList.Add(point);
                    }
                    sr.Close();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            double maxDistance = MaxDistance(pointList, k);
            Console.ReadLine();
        }
    }
}
