using System;
using System.IO;

namespace cd.Domain.Infrastructure
{
	public static class ObjectExtensionMethods
	{
		public static string ToJson(this object o)
		{
			throw new NotImplementedException();
			//return JsonConvert.SerializeObject(o,
			//	Formatting.None,
			//	new JsonSerializerSettings()
			//	{
			//		ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			//	});
		}

		/// <summary>
		/// Saves an object or a list of objects to 
		/// a to a text file in JSON format.
		/// </summary>
		/// <param name="path">Path including a trailing backslash.</param>
		/// <param name="fileName">Filename with a .json extension. Ex: MyConfig.json</param>
		public static void SaveToJsonFile(this object o, string path, string fileName)
		{
			SaveToJsonFile(o, path + fileName);
		}

		/// <summary>
		/// Saves an object or a list of objects to 
		/// a to a text file in JSON format.
		/// </summary>
		/// <param name="o"></param>
		/// <param name="filePath">The path and filename to save JSON to.</param>
		public static void SaveToJsonFile(this object o, string filePath)
		{
            throw new NotImplementedException();
			//string json = JsonConvert.SerializeObject(o,
			//	Formatting.None,
			//	new JsonSerializerSettings()
			//	{
			//		ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			//	});
			//File.WriteAllText(filePath, json);
		}

		/// Replaced by
		/// https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/using-data-protection?view=aspnetcore-2.1
		/// <summary>
		/// Convert the object to JSON and encrypt it.
		/// </summary>
		/// <param name="o"></param>
		/// <param name="purpose">A key describing what the encrypted data is meant for.</param>
		/// <returns></returns>
		//public static string ToEncryptedJson(this object o, string purpose)
		//      {
		//          string json = JsonConvert.SerializeObject(o,
		//              Newtonsoft.Json.Formatting.None,
		//              new JsonSerializerSettings()
		//              {
		//                  ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		//              });

		//       string compressed = json.Compress();
		//	return compressed.Encrypt(purpose);

		//      }
	}
}