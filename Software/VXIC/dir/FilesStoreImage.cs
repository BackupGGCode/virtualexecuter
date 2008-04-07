using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VXToolChain.ImageCreation
{
	class FileStoreImage
	{
		/*
		 * File name \0
		 * Flags (8 bit unsigned integer)
		 *	0 - Directory
		 *	Rest is unused.
		 * File size (32 bit unsigned integer)
		 * Offset (32 bit unsigned integer)
		 * 
		 * File section is terminated by a file with a zero length file name with size 0 and offset 0.
		*/

		List<string> files = new List<string>();
		//long imageSize = -1;

		List<FileEntry> fileEntries = new List<FileEntry>();

		FileEntryList fel;


		private bool directoryFound = false;
		public bool DirectoryFound
		{
			get { return directoryFound; }
		}


		public void SelectDirectory(string directory)
		{
			if (directory == "")
				files = new List<string>();
			else
			{
				files = new List<string>(Directory.GetFiles(directory));

				files.Sort();

				if (Directory.GetDirectories(directory).Length > 0)
					directoryFound = true;
			}
		}

		public bool ReadFiles()
		{
			fileEntries = new List<FileEntry>();
			foreach (string s in files)
			{
				FileEntry fe = new FileEntry(s);
				fileEntries.Add(fe);
			}
			return true;
		}

		public void CreateImage()
		{
			fel = new FileEntryList(fileEntries);
			System.Diagnostics.Debug.WriteLine(fel.Image.Length);
		}

		public void WriteImage(string file)
		{
			FileStream fs = File.Create(file);
			fs.Write(fel.Image, 0, fel.Image.Length);
			fs.Close();
		}

		public List<string> GetFiles()
		{
			return files;
		}

		public long GetImageSize()
		{
			return fel.Image.Length;
		}
	}
}
