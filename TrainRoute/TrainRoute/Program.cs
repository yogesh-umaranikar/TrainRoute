using System;
using System.IO;
using System.Windows.Forms;
using TrainRoute.Services;

namespace TrainRoute
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            TripsSearchService tripService;
            string graph = string.Empty;
            graph = loadText();
            if (string.IsNullOrEmpty(graph))
                graph = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";
            tripService = new TripsSearchService(graph);
            int distance = 0;
            try
            {
                distance = tripService.GetDistanceFromRoute("A-B-C");
                Console.WriteLine("Distance between A-B-C is " + distance.ToString());
                distance = tripService.GetDistanceFromRoute("A-D");
                Console.WriteLine("Distance between A-D is " + distance.ToString());
                distance = tripService.GetDistanceFromRoute("A-D-C");
                Console.WriteLine("Distance between A-D-C is " + distance.ToString());
                distance = tripService.GetDistanceFromRoute("A-E-B-C-D");
                Console.WriteLine("Distance between A-E-B-C-D is " + distance.ToString());
                distance = tripService.GetDistanceFromRoute("A-E-D");
                Console.WriteLine("Distance between A-E-D is " + distance.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            int stop = tripService.GetRoutesWithMaxNumberOfStops('C', 'C', 3).Count;
            var shortestRoute = tripService.GetShortestRouteBetween('A', 'C');
            var maxStops = tripService.GetRoutesWithMaxNumberOfStops('A', 'E', 7);
            var result = tripService.GetRoutesWithExactNumberOfStops('A', 'C', 4).Count;
        }

        public static string loadText()
        {
            try
            {
                string data = string.Empty;
                using (OpenFileDialog fileFileDialog1 = new OpenFileDialog())
                {
                    fileFileDialog1.Filter = "TXT files (*.txt)|*.txt|All files (*.*)|*.*";
                    fileFileDialog1.FilterIndex = 1;
                    fileFileDialog1.RestoreDirectory = true;

                    if (fileFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamReader streamReader = new StreamReader(fileFileDialog1.FileName))
                        {
                            data = streamReader.ReadToEnd();
                            streamReader.Close();
                        }
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
