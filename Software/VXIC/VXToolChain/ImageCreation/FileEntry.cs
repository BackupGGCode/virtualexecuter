using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VXToolChain.ImageCreation
{
	class FileEntry
	{
		private const int FLAG_TYPE = 0x03;
		private const int FLAG_FILE = (1 << 0);
		private const int FLAG_DIRECTORY_TERMINATOR = (1 << 1);

		private const int OFFSET_FLAGS = 0;
		private const int OFFSET_START = 1;
		private const int OFFSET_SIZE = 5;
		private const int OFFSET_NAMELENGTH = 9;
		private const int OFFSET_NAME = 10;

		private string fileName;
		public string FileName
		{
			get { return fileName; }
		}
		private string fullFileName;
		public string FullFileName
		{
			get { return fullFileName; }
		}
		private long directorySize;
		public long DirectorySize
		{
			get { return directorySize; }
		}
		private long fileSize;
		public long FileSize
		{
			get { return fileSize; }
		}
		private byte[] directoryArea;
		public byte[] DirectoryArea
		{
			get { return directoryArea; }
		}
		private byte[] dataArea;
		public byte[] DataArea
		{
			get { return dataArea; }
		}
		private long start;
		public long Start
		{
			get { return start; }
			set
			{
				start = value;

				directoryArea[OFFSET_START] = Convert.ToByte(start & 0xff);
				directoryArea[OFFSET_START + 1] = Convert.ToByte((start >> 8) & 0xff);
				directoryArea[OFFSET_START + 2] = Convert.ToByte((start >> 16) & 0xff);
				directoryArea[OFFSET_START + 3] = Convert.ToByte((start >> 24) & 0xff);
			}
		}
		
		public FileEntry()
		{
			fullFileName = "";
			fileName = "";

			directorySize = 10;

			directoryArea = new byte[directorySize];
			dataArea = new byte[0];

			directoryArea[OFFSET_FLAGS] = FLAG_DIRECTORY_TERMINATOR;

			directoryArea[OFFSET_SIZE] = 0;
			directoryArea[OFFSET_SIZE + 1] = 0;
			directoryArea[OFFSET_SIZE + 2] = 0;
			directoryArea[OFFSET_SIZE + 3] = 0;
		}

		public FileEntry(string fullFileName)
		{
			this.fullFileName = fullFileName;
			this.fileName = Path.GetFileName(fullFileName);

			directorySize = 10 + fileName.Length;
			FileInfo fi = new FileInfo(fullFileName);
			fileSize = fi.Length;

			directoryArea = new byte[directorySize];
			dataArea = new byte[fileSize];

			directoryArea[OFFSET_FLAGS] = FLAG_FILE;
			if (fileName.Length > 255)
			{
				throw new Exception("File name " + fileName + "too long. Maximum is 255 characters");
			}

			directoryArea[OFFSET_SIZE] = Convert.ToByte(fileSize & 0xff);
			directoryArea[OFFSET_SIZE + 1] = Convert.ToByte((fileSize >> 8) & 0xff);
			directoryArea[OFFSET_SIZE + 2] = Convert.ToByte((fileSize >> 16) & 0xff);
			directoryArea[OFFSET_SIZE + 3] = Convert.ToByte((fileSize >> 24) & 0xff);

			directoryArea[OFFSET_NAMELENGTH] = (byte)fileName.Length;
			for (int i = 0; i < fileName.Length; i++)
				directoryArea[OFFSET_NAME + i] = (byte)fileName[i];

			FileStream fs = fi.OpenRead();

			if (fileSize != fs.Read(dataArea, 0, (int)fileSize))
			{
				throw new Exception("Error reading file: " + fullFileName);
			}

			fs.Close();
		}
	}
}
