using System;
using System.Collections.Generic;
using System.Text;
using Coma;
using System.IO;
using VXToolChain.ImageCreation;

namespace VXIC
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Virtual eXecuter Image Creator by Claus Andersen");
			Console.WriteLine("Version: 1.0 - March 9th 2008");

			string sourceDirectory;
			string outputFileName;
			FileStoreImage bam = new FileStoreImage();

			#region Parse command line options
			Dictionary<string, List<string>> options = CommandLineParser.Run(args);

			if (options.ContainsKey("default") == false)
			{
				Console.WriteLine("No source directory specified.");
				return;
			}
			sourceDirectory = options["default"][0];

			if (options.ContainsKey("o"))
			{
				outputFileName = options["o"][0];
			}
			else
			{
				outputFileName = Path.GetFileName(sourceDirectory) + ".vxi";
			}
			#endregion

			if (Directory.Exists(sourceDirectory) == false)
			{
				Console.WriteLine("Specified source directory was not found.");
				return;
			}

			Console.Write("Parsing directory...");
			bam.SelectDirectory(sourceDirectory);
			Console.WriteLine("done");
			if (bam.GetFiles().Count == 0)
			{
				Console.WriteLine("No files found.");
				return;
			}

			if (bam.DirectoriesFound > 0)
			{
				if (bam.DirectoriesFound == 1)
				{
					Console.WriteLine("A directory was found but not included in the image.");
				}
				else
				{
					Console.WriteLine(bam.DirectoriesFound + " directories were found but not included in the image.");
				}
			}

			Console.Write("Reading file data...");
			bam.ReadFiles();
			Console.WriteLine("done");

			Console.Write("Creating image...");
			bam.CreateImage();
			Console.WriteLine("done");

			Console.Write("Writing file...");
			bam.WriteImage(outputFileName);
			Console.WriteLine("done");

			Console.WriteLine("");

			Console.WriteLine("Image size: " + bam.GetImageSize() + " bytes / " + bam.GetImageSize() / 1024 + " kB / " + bam.GetImageSize() / 1024 / 1024 + " MB");
		}
	}
}
