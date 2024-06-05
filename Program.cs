using System;
using System.Drawing;
using System.IO;

namespace ImageSplitter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the path to the image:");
            string imagePath = Console.ReadLine();

            Console.WriteLine("Enter the width of each tile:");
            int tileWidth = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the height of each tile:");
            int tileHeight = int.Parse(Console.ReadLine());

            // Create the "SplitImages" folder if it doesn't exist
            string outputFolder = "SplitImages";
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Load the image
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath))
            {
                // Calculate the number of tiles in width and height
                int numTilesWidth = (int)Math.Ceiling((double)image.Width / tileWidth);
                int numTilesHeight = (int)Math.Ceiling((double)image.Height / tileHeight);

                // Split the image into tiles and save them
                for (int y = 0; y < numTilesHeight; y++)
                {
                    for (int x = 0; x < numTilesWidth; x++)
                    {
                        // Create a rectangle for the current tile
                        Rectangle tileRectangle = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);

                        // Crop the image to the current tile
                        using (Bitmap tile = new Bitmap(tileWidth, tileHeight))
                        {
                            using (Graphics graphics = Graphics.FromImage(tile))
                            {
                                graphics.DrawImage(image, new Rectangle(0, 0, tileWidth, tileHeight), tileRectangle, GraphicsUnit.Pixel);
                            }

                            // Save the tile to the output folder with the appropriate naming convention
                            string tileFileName = $"{y}_{x}.png"; // Naming convention: 0_0.png, 0_1.png, 1_0.png, 1_1.png, ...
                            string tileFilePath = Path.Combine(outputFolder, tileFileName);

                            Console.WriteLine($"Renaming tile at position ({x}, {y}) to: {tileFileName}");

                            tile.Save(tileFilePath, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                }
            }

            Console.WriteLine("Image split and tiles saved successfully!");
        }
    }
}