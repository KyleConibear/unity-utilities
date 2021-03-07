namespace Conibear {
	using System.Diagnostics;
	using System.Text;
	using UnityEngine;


	public static class Print {
		// To be used in a comparison with the new log being created. If they are the same don't print the same log twice
		private static string preivousLog = string.Empty;

		public static void NotInitializedWarning(Component component) {
			Warning($"{component.name} not initialized on Awake");
		}
		public static void Message(string message = "Method has executed.", UnityEngine.Object context = null, bool isSinglePrint = false) {
			Log(LogType.Log, context, message, isSinglePrint);
		}

		public static void Warning(string message = "Method has executed.", UnityEngine.Object context = null, bool isSinglePrint = false) {
			Log(LogType.Warning, context, message, isSinglePrint);
		}

		public static void Error(string message = "Method has executed.", UnityEngine.Object context = null, bool isSinglePrint = false) {
			Log(LogType.Error, context, message, isSinglePrint);
		}

		private static void Log(LogType type, UnityEngine.Object context, string message, bool isSinglePrint) {
			StackTrace trace = new StackTrace(true);
			var stackFrame = trace.GetFrame(2);

			StringBuilder log = new StringBuilder();

			var fullFileName = stackFrame.GetFileName();
			int indexOfFileStart = fullFileName.IndexOf("Assets");
			string shortFileName = fullFileName.Substring(indexOfFileStart, fullFileName.Length - indexOfFileStart);
			string className = stackFrame.GetMethod().DeclaringType.Name;
			string methodName = stackFrame.GetMethod().Name;

			var lineNumber = stackFrame.GetFileLineNumber();

			// Create signature
			
			string signature = $"{context.name}\\{className}.{methodName}:{lineNumber}";

#if UNITY_EDITOR
			log.Append($"<b>{message}</b>" + $"\n{shortFileName}-{signature}");
			#else
			log.Append(message + $"\n{shortFileName}-{signature}");
#endif
			if (isSinglePrint) {
				if (log.ToString() != preivousLog) {
					preivousLog = log.ToString();
					PrintLogByType(type, log.ToString(), context);
				}
			} else {
				PrintLogByType(type, log.ToString(), context);
			}
		}

		private static void PrintLogByType(LogType type, string message, UnityEngine.Object context) {
			switch (type) {
				default:
					UnityEngine.Debug.Log(message, context);
					break;
				case LogType.Warning:
					UnityEngine.Debug.LogWarning(message, context);
					break;
				case LogType.Error:
					UnityEngine.Debug.LogError(message, context);
					break;
			}
		}
	}
}