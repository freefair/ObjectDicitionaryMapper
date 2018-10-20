using System;
using System.ComponentModel;
using System.Globalization;

namespace ObjectDictionaryMapper.TypeMapping
{
	public class IntTypeMapper : ITypeMapper
	{
		public NumberFormatInfo FormatInfo { get; set; } = NumberFormatInfo.InvariantInfo;
		
		public bool CanHandle(Type type)
		{
			return type == typeof(int) || type == typeof(short) || type == typeof(byte) || type == typeof(long);
		}

		public object ToDictionaryType(object src)
		{
			return ((IFormattable) src).ToString(null, FormatInfo);
		}

		public object FromDictionaryType(object src, Type destType)
		{
			if (src == null) return null;
			if (destType.IsInstanceOfType(src)) return src;
			
			if (destType == typeof(long))
			{
				if (src is string s)
					return long.Parse(s, FormatInfo);
				return default(long);
			}

			if (destType == typeof(byte))
			{
				if (src is string s)
					return byte.Parse(s, FormatInfo);
				return default(byte);
			}

			if (destType == typeof(short))
			{
				if (src is string s)
					return short.Parse(s, FormatInfo);
				return default(short);
			}

			if (destType == typeof(int))
			{
				if (src is string s)
					return int.Parse(s, FormatInfo);
				return default(int);
			}

			return Activator.CreateInstance(destType);
		}
	}
}