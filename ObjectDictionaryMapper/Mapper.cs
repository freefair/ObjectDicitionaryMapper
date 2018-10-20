using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectDictionaryMapper
{
	public class Mapper
	{
		private MapperConfiguration _configuration;

		public static Dictionary<object, object> ToDictionary(Object src)
		{
			return new Mapper().ToDict(src);
		}

		public static T ToObject<T>(IDictionary dictionary)
		{
			return new Mapper().ToObj<T>(dictionary);
		}

		public Mapper() : this(MapperConfiguration.Default) { }

		public Mapper(MapperConfiguration configuration)
		{
			_configuration = configuration;
		}

		public T ToObj<T>(IDictionary dictionary)
		{
			return (T)ToObj(dictionary, typeof(T));
		}

		private object ToObj(IDictionary dictionary, Type type)
		{
			var result = Activator.CreateInstance(type);
			
			foreach (DictionaryEntry entry in dictionary)
			{
				var propertyInfo = type.GetProperty(entry.Key.ToString());
				if (propertyInfo == null) continue;
				propertyInfo.SetValue(result, MapDictValueToObj(entry.Value, propertyInfo.PropertyType));
			}

			return result;
		}

		private object MapList(object entryValue, Type propertyInfoPropertyType)
		{
			if (typeof(IDictionary).IsAssignableFrom(propertyInfoPropertyType))
			{
				return MapDictionary((IDictionary) entryValue, propertyInfoPropertyType);
			}
			
			var resultType = typeof(object);
			if (propertyInfoPropertyType.IsGenericType)
				resultType = propertyInfoPropertyType.GetGenericArguments().First();
			var result = (IList)Activator.CreateInstance(propertyInfoPropertyType);

			foreach (var o in (IEnumerable) entryValue)
			{
				if(o == null) continue;
				result.Add(MapDictValueToObj(o, resultType));
			}
			
			return result;
		}

		private object MapDictionary(IDictionary entryValue, Type propertyInfoPropertyType)
		{
			IDictionary dictionary = (IDictionary) Activator.CreateInstance(propertyInfoPropertyType);
			var genericArguments = propertyInfoPropertyType.GetGenericArguments();
			var keyType = genericArguments[0];
			var valueType = genericArguments[1];
			var dict = entryValue;
			foreach (DictionaryEntry o in dict)
			{
				var key = MapDictValueToObj(o.Key, keyType);
				var value = MapDictValueToObj(o.Value, valueType);
				dictionary.Add(key, value);
			}
			return dictionary;
		}

		private object MapDictValueToObj(object value, Type destType)
		{
			var firstOrDefault = _configuration.TypeMappers.FirstOrDefault(t => t.CanHandle(destType));
			if (firstOrDefault != null)
				return firstOrDefault.FromDictionaryType(value, destType);
			if (destType.IsInstanceOfType(value))
				return value;
			if (typeof(IEnumerable).IsAssignableFrom(destType))
				return MapList(value, destType);
			return ToObj((IDictionary)value, destType);
		}

		public Dictionary<object, object> ToDict(object src)
		{
			if (src == null)
				return null;
			Dictionary<object, object> result = new Dictionary<object, object>();

			var type = src.GetType();
			if (src is IDictionary dict)
				foreach (DictionaryEntry o in dict)
				{
					result.Add(MapPropertyToDict(o.Key.GetType(), o.Key), MapPropertyToDict(o.Value.GetType(), o.Value));
				}
			else
				foreach (var propertyInfo in type.GetProperties())
					result.Add(propertyInfo.Name, MapPropertyToDict(propertyInfo.PropertyType, propertyInfo.GetValue(src)));

			return result;
		}

		private object MapPropertyToDict(Type srcType, object src)
		{
			var firstOrDefault = _configuration.TypeMappers.FirstOrDefault(t => t.CanHandle(srcType));
			return firstOrDefault != null ? firstOrDefault.ToDictionaryType(src) : MapObjectToDict(src);
		}

		private object MapObjectToDict(object value)
		{
			if (value == null) return null;
			if (!(value is IEnumerable enumerable)) return ToDict(value);
			if (value is IDictionary dictionary) return ToDict(dictionary);
			
			IList list = new List<object>();
			foreach (var o in enumerable)
			{
				if(o == null) continue;
				var type = o.GetType();
				var firstOrDefault = _configuration.TypeMappers.FirstOrDefault(t => t.CanHandle(type));
				list.Add(firstOrDefault != null ? firstOrDefault.ToDictionaryType(o) : MapObjectToDict(o));
			}

			return list;
		}
	}
}