using LightningAlert.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LightningAlert
{
    public class Program
    {
        private const string Lightning_File = "lightning.json";
        private const string Assets_File = "assets.json";
        static readonly int ZOOM_LEVEL = 12;

        private static List<Assets> assets = new List<Assets>();
        private static List<string> quadKeysAlertShown = new List<string>();

        static void Main(string[] args)
        {
            ShowLightiningAlert(Lightning_File);
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();

        }

        public static void ShowLightiningAlert(string fileName)
        {
            var enviroment = System.Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(enviroment).Parent.FullName;

            //Read assets file first and create a list of QuadKey's
            using (StreamReader streamReader = new StreamReader(Path.Combine(projectDirectory, Assets_File)))
            {
                try
                {
                    string json = streamReader.ReadToEnd();
                    assets = JsonConvert.DeserializeObject<List<Assets>>(json);
                }
                catch(JsonException ex)
                {
                    //Report exception
                    Console.WriteLine("Exception Occured: " + ex.Message);
                }
                    
            }

            //Read lightning file and check if quadKey is found in assets and alert is not shown already.
            using (StreamReader streamReader = new StreamReader(Path.Combine(projectDirectory, fileName)))
            {
                while (!streamReader.EndOfStream)
                {
                    try
                    {
                        string json = streamReader.ReadLine();
                        Lightning lighting = JsonConvert.DeserializeObject<Lightning>(json);

                        //Check first if flashType is not heartbeat
                        if (lighting.flashType != 9)
                        {
                            //Convert latitude and longitude to Quadkeys and save it to list.
                            string quadKey = GetQuadKeyFromLatLong(lighting.latitude, lighting.longitude);
                            Assets assetFound = assets.Where(asset => asset.quadKey == quadKey).FirstOrDefault();
                            if (!quadKeysAlertShown.Contains(quadKey) && assetFound != null)
                            {
                                Console.WriteLine("lightning alert for " + assetFound.assetOwner + ":" + assetFound.assetName);
                                quadKeysAlertShown.Add(assetFound.quadKey);
                                Console.WriteLine(assetFound.quadKey);
                                Console.WriteLine(lighting.latitude.ToString(), lighting.longitude);
                            }
                        }
                    }
                    catch (JsonException ex)
                    {
                        //Report exception
                        Console.WriteLine("Exception Occured: " + ex.Message);
                    }
                }
            }
        }

        public static string GetQuadKeyFromLatLong(double latitude, double longitude)
        {
            //Convert Latitude and longitude to pixelX and PixelY
            TileSystem.LatLongToPixelXY(latitude, longitude, ZOOM_LEVEL, out int pixelX, out int pixelY);

            //Convert PixelX and PixelY to TileX and TileY respectively
            TileSystem.PixelXYToTileXY(pixelX, pixelY, out int tileX, out int tileY);

            //Convert tileX and tileY to Quadkey
            string quadKey = TileSystem.TileXYToQuadKey(tileX, tileY, ZOOM_LEVEL);

            return quadKey;
        }
    }
}
