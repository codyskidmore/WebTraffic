using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;

namespace cd.Infrastructure.ExtensionMethods
{
	public static class StringExtensionMethods
	{
		/// <summary>
		/// Removes a given string value.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="valueToRemove"></param>
		/// <returns></returns>
		public static string Remove(this string s, string valueToRemove)
		{
			return s.Replace(valueToRemove, "");
		}
		/// <summary>
		/// Converts a string to bool. If the
		/// string is not a valid bool, the default
		/// is returned.
		/// </summary>
		/// <param name="defaultValue">Default value to use</param>
		/// <returns></returns>
		public static bool ToBoolean(this string s, bool defaultValue = false)
		{
			bool result;

			if (bool.TryParse(s, out result))
			{
				return result;
			}

			return defaultValue;
		}
		/// <summary>
		/// Converts a string to int. If the
		/// string is not a valid int, the default
		/// is returned.
		/// </summary>
		/// <param name="defaultValue">Default value to use</param>
		/// <returns></returns>
		public static int ToInteger(this string s, int defaultValue = 0)
		{
			int result;

			if (int.TryParse(s, out result))
			{
				return result;
			}

			return defaultValue;
		}

		/// <summary>
		/// Returns the last few characters of the string with a length
		/// specified by the given parameter. If the string's length is less than the 
		/// given length the complete string is returned. If length is zero or 
		/// less an empty string is returned
		/// </summary>
		/// <param name="s">the string to process</param>
		/// <param name="length">Maximum characters to return</param>
		/// <returns></returns>
		public static string Right(this string s, int length)
		{
			length = Math.Max(length, 0);

			if (s.Length > length)
			{
				return s.Substring(s.Length - length, length);
			}
			return s;
		}

		/// <summary>
		/// Returns the last few characters of the string with a length
		/// specified by the given parameter. If the string's length is less than the 
		/// given length the complete string is returned. If length is zero an empty 
		/// string is returned.  If the string is empty, the default is returned.
		/// </summary>
		/// <param name="s">the string to process</param>
		/// <param name="length">Number of characters to return</param>
		/// <returns></returns>
		public static string RightDef(this string s, int length, string defaultString)
		{
			if (String.IsNullOrWhiteSpace(s))
			{
				return defaultString;
			}

			return s.Right(4);
		}

		/// <summary>
		/// Returns the first few characters of the string with a length
		/// specified by the given parameter. If the string's length is less than the 
		/// given length the complete string is returned. If length is zero or 
		/// less an empty string is returned
		/// </summary>
		/// <param name="s">the string to process</param>
		/// <param name="length">Number of characters to return</param>
		/// <returns></returns>
		public static string Left(this string s, int length)
		{
			length = Math.Max(length, 0);

			if (s.Length > length)
			{
				return s.Substring(0, length);
			}
			return s;
		}

		/// <summary>
		/// Returns the first few characters of the string with a length
		/// specified by the given parameter. If the string's length is less than the 
		/// given length the complete string is returned.  If length is zero an empty 
		/// string is returned.  If the string is empty, the default is returned.
		/// </summary>
		/// <param name="s">the string to process</param>
		/// <param name="length">Number of characters to return</param>
		/// <param name="defaultString">Default value to use if string is empty.</param>
		/// <returns></returns>
		public static string LeftDef(this string s, int length, string defaultString)
		{
			if (String.IsNullOrWhiteSpace(s))
			{
				return defaultString;
			}

			return s.Left(length);
		}

		/// <summary>
		/// Returns a default string value if a string is empty or null.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="defaultString"></param>
		/// <returns></returns>
		public static string GetValueOrDefault(this string s, string defaultString)
		{
			// Deprecate this code.
			if (String.IsNullOrWhiteSpace(s))
			{
				return defaultString;
			}
			return s;
		}

		/// <summary>
		/// Returns a default string value if a string is empty or null.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static string DefaultIfNullOrEmpty(this string x, string defaultValue)
		{
			return string.IsNullOrEmpty(x) ? defaultValue : x;
		}

		/// <summary>
		/// Compares two strings to see if they match. Works 
		/// with null or empty strings. Depends on 
		/// String.GetValueOrDefault().
		/// </summary>
		/// <param name="s1"></param>
		/// <param name="s2"></param>
		/// <returns></returns>
		public static bool Matches(this string s1, string s2)
		{
			string s1Trimmed = s1.GetValueOrDefault("").Trim();
			string s2Trimmed = s2.GetValueOrDefault("").Trim();

			return String.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// The inverse of String.Matches(). Used to improve
		/// readability. 
		/// </summary>
		/// <param name="s1"></param>
		/// <param name="s2"></param>
		/// <returns></returns>
		public static bool DoesNotMatch(this string s1, string s2)
		{
			return !Matches(s1, s2);
		}

		/// <summary>
		/// Same as String.IsNullOrWhiteSpace() but 
		/// adds readability.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool IsEmpty(this string s)
		{
			return string.IsNullOrWhiteSpace(s.GetValueOrDefault(""));
		}

		public static bool IsNotEmpty(this string s)
		{
			return !string.IsNullOrWhiteSpace(s.GetValueOrDefault(""));
		}

		/// Replaced by
		/// https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/using-data-protection?view=aspnetcore-2.1
		/// <summary>
		/// Encrypt the string
		/// </summary>
		/// <param name="purpose">A key describing what the encrypted data is meant for. Must match the purpose originally passed to String.Encrypt()</param>
		/// <returns></returns>
		//public static string Decript(this string encryptedValue, string purpose)
		//{
		//    byte[] encrypedCp = Convert.FromBase64String(encryptedValue);
		//    return Encoding.UTF8.GetString(MachineKey.Unprotect(encrypedCp, purpose.ToLower()));
		//}

		/// Replaced by
		/// https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/using-data-protection?view=aspnetcore-2.1
		/// <summary>
		/// Encrypt the string
		/// </summary>
		/// <param name="purpose">A key describing what the encrypted data is meant for.</param>
		/// <returns></returns>
		//public static string Encrypt(this string value, string purpose)
		//{
		//	Byte[] encriptedCp = MachineKey.Protect(Encoding.UTF8.GetBytes(value), purpose.ToLower());
		//	return Convert.ToBase64String(encriptedCp);
		//}

		/// <summary>
		/// Converts a JSON string to an object of type "T"
		/// </summary>
		/// <typeparam name="T">The object type to return.</typeparam>
		/// <returns>Object of type "T"</returns>
		public static T JsonToObject<T>(this string json)
		{
            //return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions()
            //{
            //	ReferenceHandling = System.Text.Json.Serialization.ReferenceHandling.Default
            //	//ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //});
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        /// <summary>
        /// Loads an object of type "T" from a JSON file.
        /// </summary>
        /// <typeparam name="T">The object type to create.</typeparam>
        /// <param name="pathAndFileName">The path and filename of the JSON file.</param>
        /// <returns>An object of type "T"</returns>
        public static T LoadObjectFromJsonFile<T>(this string pathAndFileName)
		{
			string json = File.ReadAllText(pathAndFileName);
			return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings()
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});
		}

		/// Replaced by
		/// https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/using-data-protection?view=aspnetcore-2.1
		/// <summary>
		/// Convert the encrypted JSON to an object.
		/// </summary>
		/// <typeparam name="T">The type to convert to.</typeparam>
		/// <param name="purpose">A key describing what the encrypted data is meant for. Must match the purpose originally passed to String.Encrypt()</param>
		/// <returns></returns>
		//public static T EncryptedJsonToObject<T>(this string encryptedJson, string purpose)
		//      {
		//       string compressed = encryptedJson.Decript(purpose);
		//       string json = compressed.Decompress();
		//          return JsonToObject<T>(json);
		//      }

		/// <summary>
		/// Compresses the string.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public static string Compress(this string text)
		{
			byte[] buffer = Encoding.UTF8.GetBytes(text);
			var memoryStream = new MemoryStream();
			using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
			{
				gZipStream.Write(buffer, 0, buffer.Length);
			}

			memoryStream.Position = 0;

			var compressedData = new byte[memoryStream.Length];
			memoryStream.Read(compressedData, 0, compressedData.Length);

			var gZipBuffer = new byte[compressedData.Length + 4];
			Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
			Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
			return Convert.ToBase64String(gZipBuffer);
		}

		/// <summary>
		/// Decompresses the string.
		/// </summary>
		/// <param name="compressedText">The compressed text.</param>
		/// <returns></returns>
		public static string Decompress(this string compressedText)
		{
			byte[] gZipBuffer = Convert.FromBase64String(compressedText);
			using (var memoryStream = new MemoryStream())
			{
				int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
				memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

				var buffer = new byte[dataLength];

				memoryStream.Position = 0;
				using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
				{
					gZipStream.Read(buffer, 0, buffer.Length);
				}

				return Encoding.UTF8.GetString(buffer);
			}
		}

		/// <summary>
		/// Escapes the characters in order to return proper Json that can be injected onto a form and not cause javascript errors
		/// </summary>
		/// <param name="oldString">The original string</param>
		/// <returns>Processed string with special characters escaped for injection</returns>
		public static string JsonEscapeChars(this string oldString)
		{
			if (oldString != null)
			{
				string newString;
				newString = oldString.Replace("\\", "/");
				newString = newString.Replace("'", @"\u0027");
				newString = newString.Replace("\n", @" ");
				newString = newString.Replace("\r", @" ");
				return newString;
			}

			return string.Empty;
		}
		/// <summary>
		/// Remove non-printable characters from a string.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string RemoveNonPrintableCharacters(this string value)
		{
			return Encoding.ASCII.GetString(
						Encoding.Convert(
							Encoding.UTF8,
							Encoding.GetEncoding(
								Encoding.ASCII.EncodingName,
								new EncoderReplacementFallback(string.Empty),
								new DecoderExceptionFallback()
								),
							Encoding.UTF8.GetBytes(value)
						)
					);
		}

		/// <summary>
		/// Strips HTML encoding from a URL string.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string DecodeUrl(this string s)
		{
			return WebUtility.UrlDecode(s);
		}

		/// <summary>
		/// Creates a SHA256 hash of the specified input.
		/// </summary>
		/// <param name="s">The input.</param>
		/// <param name="useUpperCase">Return the hash string in upper case format. Default is TRUE</param>
		/// <returns>A hash</returns>
		public static string ToSha256(this string s, bool useUpperCase = true)
		{
			if (s.IsEmpty()) return string.Empty;

			StringBuilder Sb = new StringBuilder();

			using (var sha = SHA256.Create())
			{
				var bytes = Encoding.UTF8.GetBytes(s);
				var result = sha.ComputeHash(bytes);

				foreach (byte b in result)
					Sb.Append(b.ToString("x2"));

				if (!useUpperCase)
				{
					return Sb.ToString();
				}

				return Sb.ToString().ToUpper();
			}
		}

		/// <summary>
		/// Creates a SHA256 hash of the specified input 
		/// converted to a base 64 string.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>A hash</returns>
		public static string ToSha256Base64(this string s)
		{
			if (s.IsEmpty()) return string.Empty;

			using (var sha = SHA256.Create())
			{
				var bytes = Encoding.UTF8.GetBytes(s);
				var hash = sha.ComputeHash(bytes);

				return Convert.ToBase64String(hash).ToUpper();
			}
		}

		/// <summary>
		/// Creates a SHA256 hash of the specified input.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>A hash.</returns>
		public static byte[] ToSha256(this byte[] input)
		{
			if (input == null)
			{
				return null;
			}

			using (var sha = SHA256.Create())
			{
				return sha.ComputeHash(input);
			}
		}

		/// <summary>
		/// Creates a SHA512 hash of the specified input.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>A hash</returns>
		public static string Sha512Base64(this string input)
		{
			if (input.IsEmpty()) return string.Empty;

			using (var sha = SHA512.Create())
			{
				var bytes = Encoding.UTF8.GetBytes(input);
				var hash = sha.ComputeHash(bytes);

				return Convert.ToBase64String(hash);
			}
		}

		public static bool ContainsIgnoreCase(this string text, string value)
		{
			StringComparison stringComparison = StringComparison.OrdinalIgnoreCase;
			return text.IndexOf(value, stringComparison) >= 0;
		}
	}
}