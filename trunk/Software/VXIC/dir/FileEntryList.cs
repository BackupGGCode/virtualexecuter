using System;
using System.Collections.Generic;
using System.Text;

namespace VXToolChain.ImageCreation
{
	class FileEntryList
	{
		private byte[] image;
		public byte[] Image
		{
			get { return image; }
		}

		public FileEntryList(List<FileEntry> fileEntries)
		{
			image = new byte[CalculateImageSize(fileEntries)];

			long dirArea = 0;
			long dataArea = CalculateStartOfDataArea(fileEntries);
			foreach (FileEntry fe in fileEntries)
			{
				fe.Start = dataArea;
				InsertBuffer(image, fe.DirectoryArea, dirArea);
				InsertBuffer(image, fe.DataArea, dataArea);
				dirArea += fe.DirectoryArea.Length;
				dataArea += fe.DataArea.Length;
			}
		}

		private void InsertBuffer(byte[] destination, byte[] source, long index)
		{
			for (long i = 0; i < source.Length; i++)
				destination[index + i] = source[i];
		}

		private long CalculateStartOfDataArea(List<FileEntry> fileEntries)
		{
			long start = 0;
			foreach (FileEntry fe in fileEntries)
				start += fe.DirectorySize;
			return start;
		}

		private long CalculateImageSize(List<FileEntry> fileEntries)
		{
			long size = 0;
			foreach (FileEntry fe in fileEntries)
				size += (fe.DirectorySize + fe.FileSize);
			return size;
		}
	}
}
