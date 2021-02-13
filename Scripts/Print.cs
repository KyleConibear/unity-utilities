namespace Conibear {
	using System.Diagnostics;
	using System.Text;
	using UnityEngine;


	public static class Print {
		// To be used in a comparison with the new log being created. If they are the same don't print the same log twice
		private static string preivousLog = string.Empty;

		public static void Message(string gameobjectName = "", string message = "Method has executed.", bool isSinglePrint = false) {
			Log(LogType.Log, gameobjectName, message, isSinglePrint);
		}

		public static void Warning(string gameobjectName = "", string message = "Method has executed.", bool isSinglePrint = false) {
			Log(LogType.Warning, gameobjectName, message, isSinglePrint);
		}

		public static void Error(string gameobjectName = "", string message = "Method has executed.", bool isSinglePrint = false) {
			Log(LogType.Error, gameobjectName, message, isSinglePrint);
		}

		private static void Log(LogType type, string gameobjectName, string message, bool isSinglePrint) {
			StackFrame frame = new StackFrame(2);

			StringBuilder log = new StringBuilder();

			string callers_method_name = frame.GetMethod().Name;

			string callers_class_name = frame.GetMethod().DeclaringType.Name;

			// Create signature
			string signature = $"[{gameobjectName}]:{callers_class_name}.{callers_method_name}";

			log.Append(signature + message);

			if (isSinglePrint) {
				if (log.ToString() != preivousLog) {
					preivousLog = log.ToString();
					PrintLogByType(type, log.ToString());
				}
			} else {
				PrintLogByType(type, log.ToString());
			}
		}

		private static void PrintLogByType(LogType type, string message) {
			switch (type) {
				default:
					UnityEngine.Debug.Log(message);
					break;
				case LogType.Warning:
					UnityEngine.Debug.LogWarning(message);
					break;
				case LogType.Error:
					UnityEngine.Debug.LogError(message);
					break;
			}
		}
	}
}