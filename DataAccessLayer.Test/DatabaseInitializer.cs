using System;
using System.Diagnostics;
using System.IO;

namespace DataAccessLayer.Test
{
	public static class DatabaseInitializer
	{
		public static void InitDatabase()
		{
			Debug.WriteLine("Database and data are generating...");

			ExecuteCommand("/c RunScript.bat");

			Debug.WriteLine("Database and data are generated...");
		}

		#region Helpers
		private static void ExecuteCommand(string command)
		{
			ProcessStartInfo processInfo;
			Process process;

			processInfo = new ProcessStartInfo();
			processInfo.WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"Scripts\");
			processInfo.FileName = "cmd.exe";
			processInfo.Arguments = command;
			processInfo.CreateNoWindow = true;
			processInfo.UseShellExecute = false;
			// *** Redirect the output ***
			processInfo.RedirectStandardError = true;
			processInfo.RedirectStandardOutput = true;

			process = Process.Start(processInfo);
			process.WaitForExit();

			// *** Read the streams ***
			// Warning: This approach can lead to deadlocks, see Edit #2
			string output = process.StandardOutput.ReadToEnd();
			string error = process.StandardError.ReadToEnd();

			if (!string.IsNullOrEmpty(error))
				throw new Exception(error);

			process.Close();
		}
		#endregion
	}
}
