using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            // Log and error if you get 0 lines and a warning if you get 1 line



            var lines = File.ReadAllLines(csvPath);
            logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable store1 = null;
            ITrackable store2 = null;

            double maxDistance = 0;

            foreach (var location1 in locations)
            {
                var coord1 = new GeoCoordinate(location1.Location.Latitude, location1.Location.Longitude);

                foreach (var location2 in locations)
                {
                    var coord2 = new GeoCoordinate(location2.Location.Latitude, location2.Location.Longitude);
                    double distance = coord2.GetDistanceTo(coord1);

                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        store1 = location1;
                        store2 = location2;
                    }
                }
            }

            int convertToMiles = Convert.ToInt32(maxDistance / 1609.34); //Converted meters to miles. 1609.34 meters per mile
            var newStore1 = store1.Name.Replace('.', ' ').Trim();
            var newStore2 = store2.Name.Replace('.', ' ').Trim();

            Console.WriteLine($"\n{newStore2} and {newStore1} are {convertToMiles} miles away from each other.");
        }
    }
}
