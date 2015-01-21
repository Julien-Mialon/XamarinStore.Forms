using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using PCLStorage;
using XamarinStore.Forms.Helpers;
using FileAccess = PCLStorage.FileAccess;

namespace XamarinStore.Forms.Data
{
	public class FileCache
	{
		public static async Task<string> Download(string url)
		{
			var fileName = Md5 (url);

			return await Download (url, fileName);
		}

		static object locker = new object ();
		public static async Task<string> Download(string url, string fileName)
		{
			try{
				try
				{
					IFile result = await FileSystem.Current.LocalStorage.GetFileAsync(fileName);
					if (result != null)
					{
						return result.Path;
					}
				}
				catch (Exception)
				{
					
				}
					
				IFile resultFile = await GetDownload(url,fileName);

				return resultFile.Path;
			}
			catch(Exception ex) {
				return  "";
			}
		}

		static Dictionary<string,Task<IFile>> downloadTasks = new Dictionary<string, Task<IFile>> ();
		static Task<IFile> GetDownload(string url, string fileName)
		{
			lock (locker) 
			{
				Task<IFile> task;
				if (downloadTasks.TryGetValue (fileName, out task))
					return task;
				var client = new HttpClient();

				downloadTasks.Add (fileName, task = DownloadFileTaskAsync (url, fileName, client));
				return task;
			}
		}

		static async Task<IFile> DownloadFileTaskAsync(string url, string filename, HttpClient client)
		{
			try
			{
				Stream inputStream = await client.GetStreamAsync(url);
				IFile output = await FileSystem.Current.LocalStorage.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
				Stream outputStream = await output.OpenAsync(FileAccess.ReadAndWrite);
				using (outputStream)
				{
					await inputStream.CopyToAsync(outputStream);
				}
				outputStream = null;
				lock (locker)
				{
					RemoveTask(filename);
				}

				return output;
			}
			catch (Exception ex)
			{	
				throw;
			}
		}

		static void RemoveTask(string fileName)
		{
			lock (locker) {
				downloadTasks.Remove (fileName);
			}
		}

		static string Md5 (string input)
		{
			MD5 md5 = new MD5();
			md5.Value = input;
			return md5.FingerPrint;
		}
	}
}

