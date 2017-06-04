//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Text.RegularExpressions;

//namespace DartSimulatorTests.Extensions
//{
//	public static class PrettyPrintExtension
//	{
//		#region Separator Characters

//		private const string Bullet1 = "Δ";
//		private const string Bullet2 = "└➝";

//		#endregion

//		#region Extension Method

//		public static string ToPrettyString(this object anything, params object[] furtherObjects)
//		{
//			string prettyString = string.Empty;

//			prettyString += PrettyAnything(anything);

//			foreach (object anotherObject in furtherObjects)
//			{
//				prettyString += Environment.NewLine + Environment.NewLine;
//				prettyString += PrettyAnything(anotherObject);
//			}

//			return prettyString;
//		}

//		#endregion

//		#region Pretty Methods

//		private static readonly List<KeyValuePair<object, int>> PrintedObjects = new List<KeyValuePair<object, int>>();

//		public static string PrettyAnything(object anything, string title = "", int inherit = 0)
//		{
//			string result = Prettify(anything, title, inherit);
//			PrintedObjects.Clear();
//			return result;
//		}

//		private static string Prettify(object anything, string title = "", int inherit = 0)
//		{
//			if (anything == null)
//				return String.Empty;

//			if (PrintedObjects.Any(kvp => kvp.Key == anything && kvp.Value != inherit))
//			{
//				string tabs = String.Empty;
//				for (int i = 0; i < inherit; i++)
//					tabs += "\t";

//				Dictionary<string, object> simpleProperties = GetProperties(anything, true, false);
//				List<string> alreadyPrintedInfos = new List<string>();

//				if (simpleProperties.Keys.Any(k => k.ToUpper() == "ID"))
//				{
//					string objId = simpleProperties.First(kvp => kvp.Key.ToUpper() == "ID").Value.ToString();
//					if (!String.IsNullOrWhiteSpace(objId))
//						alreadyPrintedInfos.Add("ID:" + objId);
//				}

//				if (simpleProperties.Keys.Any(k => k.ToUpper() == "NAME"))
//				{
//					string objName = simpleProperties.First(kvp => kvp.Key.ToUpper() == "NAME").Value.ToString();
//					if (!String.IsNullOrWhiteSpace(objName))
//						alreadyPrintedInfos.Add("Name:" + objName);
//				}

//				string alreadyPrinted = tabs + Bullet2 + " " + anything.GetType().Name + " ";
//				alreadyPrinted += alreadyPrintedInfos.Count > 0 ? "(" + String.Join(", ", alreadyPrintedInfos) + ") " : "";
//				alreadyPrinted += "[already printed]";

//				return alreadyPrinted;
//			}
//			PrintedObjects.Add(new KeyValuePair<object, int>(anything, inherit));

//			if (TypeIsSimpleType(anything.GetType()))
//				return PrettyObject(anything, title, inherit);

//			var enumerable = anything as IEnumerable;
//			if (enumerable == null)
//				return PrettyObject(anything, title, inherit);


//			try
//			{


//				List<object> objectList = enumerable.Cast<object>().ToList();
//				object first = objectList.FirstOrDefault();
//				if (first == null)
//					return string.Empty;

//				if (string.IsNullOrEmpty(title))
//					title = first.GetType().Name + "s";

//				if (TypeIsSimpleType(first.GetType()) || !HasComplexProperties(first))
//					return PrettySimpleList(objectList, title, inherit);

//				if (first.GetType().Name.ToLower().Contains("keyvaluepair"))
//				{
//					if (first is KeyValuePair<string, string>)
//						return PrettySimpleList(objectList, title, inherit);
//				}

//				else
//					return PrettyComplexList(objectList, title, inherit);

//				return PrettyObject(anything, title, inherit);

//			}
//			catch (Exception)
//			{

//				throw;
//			}

//		}

//		private static string PrettyObject(object anything, string title = "", int inherit = 0)
//		{
//			if (anything == null)
//				return String.Empty;

//			StringBuilder prettyString = new StringBuilder();

//			if (TypeIsSimpleType(anything.GetType()))
//			{
//				if (!String.IsNullOrEmpty(title) && !String.IsNullOrEmpty(anything.ToString()))
//				{
//					AddHeader(prettyString, title, inherit);
//					AddString(prettyString, anything.ToString(), inherit + 1);
//				}
//				else
//					AddString(prettyString, anything.ToString(), inherit);

//				return GetString(prettyString);
//			}

//			if (String.IsNullOrEmpty(title))
//				title = anything.GetType().Name;

//			StringTable table = new StringTable();

//			Dictionary<string, object> simpleProperties = GetProperties(anything, true, false);
//			Dictionary<string, object> complexProperties = GetProperties(anything, false, true);

//			bool hasSimpleProperties = CheckProperties(simpleProperties);
//			bool hasComplexProperties = CheckProperties(complexProperties);

//			if (hasSimpleProperties || hasComplexProperties)
//				AddHeader(prettyString, title, inherit);

//			if (hasSimpleProperties)
//			{
//				table.AddTitleRow(simpleProperties.Keys.ToArray<object>());
//				table.AddRow(simpleProperties.Values.ToArray());
//				AddString(prettyString, table.GetFormattedString(inherit));
//			}

//			if (hasComplexProperties)
//			{
//				foreach (KeyValuePair<string, object> property in complexProperties)
//				{
//					if (property.Value == null)
//						continue;

//					AddString(prettyString, Prettify(property.Value, property.Key, inherit + 1));
//				}
//			}

//			return prettyString.ToString();
//		}

//		private static string PrettySimpleList(IEnumerable<object> objects, string title = "", int inherit = 0)
//		{
//			var enumerable = objects as object[] ?? objects.ToArray();
//			if (objects == null || !enumerable.Any())
//				return String.Empty;

//			List<object> objectList = enumerable.ToList();

//			StringBuilder prettyString = new StringBuilder();

//			List<object> namesList = new List<object>();

//			if (TypeIsSimpleType(objectList[0].GetType()))
//			{
//				if (!String.IsNullOrEmpty(title))
//					namesList.Add(title);
//			}
//			else
//			{
//				if (!String.IsNullOrEmpty(title))
//					AddHeader(prettyString, $"{title} <{objectList.Count}>", inherit);

//				namesList = GetProperties(objectList[0], true, true).Keys.ToList<object>();
//			}

//			StringTable table = new StringTable();
//			table.AddTitleRow(namesList.ToArray<object>());

//			foreach (object element in objectList.Where(element => element != null))
//			{
//				table.AddRow(TypeIsSimpleType(element.GetType()) ? new[] { element } : GetProperties(element, true, true).Values.ToArray());
//			}

//			AddString(prettyString, table.GetFormattedString(inherit));

//			return GetString(prettyString);
//		}

//		private static string PrettyComplexList(IEnumerable<object> objects, string title = "", int inherit = 0)
//		{
//			var enumerable = objects as object[] ?? objects.ToArray();
//			if (objects == null || !enumerable.Any())
//				return String.Empty;

//			List<object> objectList = enumerable.ToList();

//			StringBuilder prettyString = new StringBuilder();

//			if (!String.IsNullOrEmpty(title))
//				AddHeader(prettyString, $"{title} <{objectList.Count}>", inherit);

//			foreach (object element in objectList)
//			{
//				if (element == null)
//					continue;

//				AddString(prettyString, !string.IsNullOrEmpty(title) ? Prettify(element, "", inherit + 1) : Prettify(element, "", inherit));
//			}

//			return GetString(prettyString);
//		}

//		#endregion

//		#region Helper

//		private static bool HasComplexProperties(object obj)
//		{
//			Dictionary<string, object> properties = GetProperties(obj, false, true);
//			if (properties != null && properties.Count > 0)
//				return true;

//			return false;
//		}

//		private static Dictionary<string, object> GetProperties(object obj, bool simpleTypes, bool complexTypes)
//		{
//			Dictionary<string, object> properties = new Dictionary<string, object>();

//			if (obj == null)
//				return properties;


//			PropertyInfo[] propertyInfos = obj.GetType().GetProperties();
//			IList<PropertyInfo> propertyInfoList = SortArrayToList(propertyInfos);

//			foreach (PropertyInfo property in propertyInfoList)
//			{
//				try
//				{
//					if ((simpleTypes && TypeIsSimpleType(property.PropertyType)) || (complexTypes && !TypeIsSimpleType(property.PropertyType)))
//					{
//						if (properties.All(item => !string.Equals(item.Key, property.Name, StringComparison.CurrentCultureIgnoreCase)))
//							properties.Add(property.Name, property.GetValue(obj, null));
//					}

//				}
//				catch (Exception ex)
//				{
//					// ignored
//				}
//			}

//			if (complexTypes)
//			{
//				FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
//				int depth = 0;
//				foreach (FieldInfo field in fields.ToList().OrderBy(f => f.Name))
//				{
//					if (field == null)
//						break;
//					try
//					{

//						if (!TypeIsSimpleType(field.FieldType))
//						{
//							string fieldName = field.Name;
//							Match m = Regex.Match(field.Name, "<(.*)>.*");
//							if (m.Success && m.Groups.Count == 2)
//								fieldName = m.Groups[1].Value;

//							if (properties.All(item => !string.Equals(item.Key, fieldName, StringComparison.CurrentCultureIgnoreCase)))
//								properties.Add(fieldName, field.GetValue(obj));
//						}

//					}
//					catch (Exception ex)
//					{
//						// ignored
//					}
//				}
//			}


//			return properties;
//		}

//		private static bool TypeIsSimpleType(Type typeToCheck)
//		{
//			var typeCode = Type.GetTypeCode(GetUnderlyingType(typeToCheck));

//			switch (typeCode)
//			{
//				case TypeCode.Boolean:
//				case TypeCode.Byte:
//				case TypeCode.Char:
//				case TypeCode.DateTime:
//				case TypeCode.Decimal:
//				case TypeCode.Double:
//				case TypeCode.Int16:
//				case TypeCode.Int32:
//				case TypeCode.Int64:
//				case TypeCode.SByte:
//				case TypeCode.Single:
//				case TypeCode.String:
//				case TypeCode.UInt16:
//				case TypeCode.UInt32:
//				case TypeCode.UInt64:
//					return true;
//				default:
//					return false;
//			}
//		}

//		private static Type GetUnderlyingType(Type typeToCheck)
//		{
//			if (typeToCheck.IsGenericType && typeToCheck.GetGenericTypeDefinition() == typeof(Nullable<>))
//				return Nullable.GetUnderlyingType(typeToCheck);

//			return typeToCheck;
//		}

//		private static string GetString(StringBuilder sb)
//		{
//			return sb.ToString().TrimEnd('\r', '\n');
//		}

//		private static void AddHeader(StringBuilder sb, string title, int inherit = 0)
//		{
//			title = FormatTitleString(title);

//			string tabs = String.Empty;
//			for (int i = 0; i < inherit; i++)
//				tabs += "\t";

//			string bullet = Bullet1;

//			if (inherit > 0)
//				bullet = Bullet2;

//			if (inherit == 0 && !String.IsNullOrEmpty(sb.ToString()))
//				AddEmptyLine(sb);

//			AddString(sb, tabs + bullet + " " + title);
//		}

//		private static string FormatTitleString(string title)
//		{
//			if (String.IsNullOrWhiteSpace(title))
//				return title;

//			if (title.Length > 1)
//			{
//				char[] titleChars = title.ToArray();
//				for (int i = 0; i < titleChars.Length; i++)
//				{
//					if (i == 0)
//						titleChars[i] = Char.ToUpper(titleChars[i]);

//					if (Char.IsDigit(titleChars[i]) && i <= titleChars.Length - 2)
//						titleChars[i + 1] = Char.ToUpper(titleChars[i + 1]);
//				}

//				return String.Join("", titleChars);
//			}

//			return title.ToUpper();
//		}

//		private static void AddEmptyLine(StringBuilder sb)
//		{
//			sb.AppendLine("");
//		}

//		private static void AddString(StringBuilder sb, string text, int inherit = 0)
//		{
//			if (String.IsNullOrEmpty(text))
//				return;

//			string tabs = String.Empty;

//			for (int i = 0; i < inherit; i++)
//				tabs += "\t";

//			sb.AppendLine(tabs + text.TrimEnd('\r', '\n'));
//		}

//		private static IList<PropertyInfo> SortArrayToList(PropertyInfo[] propertyInfos, string firstElementName = "ID")
//		{
//			List<PropertyInfo> existingList = propertyInfos.ToList();
//			List<PropertyInfo> newList = new List<PropertyInfo>();

//			if (!String.IsNullOrEmpty(firstElementName) && existingList.Any(p => p.Name.ToUpper() == firstElementName.ToUpper()))
//			{
//				PropertyInfo firstElement = existingList.Single(p => p.Name.ToUpper() == firstElementName.ToUpper());
//				if (firstElement != null)
//				{
//					existingList.Remove(firstElement);
//					newList.Add(firstElement);
//				}
//			}

//			newList.AddRange(existingList.OrderBy(p => p.Name));

//			return newList;
//		}

//		private static bool CheckProperties(Dictionary<string, object> properties)
//		{
//			foreach (KeyValuePair<string, object> property in properties)
//			{
//				var value = property.Value as string;
//				if (value != null)
//				{
//					if (!String.IsNullOrEmpty(value))
//						return true;
//				}
//				else
//				if (property.Value != null)
//					return true;
//			}

//			return false;
//		}

//		#endregion
//	}
//}
