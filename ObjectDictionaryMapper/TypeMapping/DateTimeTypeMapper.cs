using System;
using System.ComponentModel;
using System.Globalization;

namespace ObjectDictionaryMapper.TypeMapping
{
	public class DateTimeTypeMapper : ITypeMapper
	{
		public DateTimeFormatInfo FormatInfo { get; set; } = DateTimeFormatInfo.InvariantInfo;
		
		public bool CanHandle(Type type)
		{
			return type == typeof(DateTime);
		}

		public object ToDictionaryType(object src)
		{
			return ((DateTime)src).ToString(FormatInfo);
		}

		public object FromDictionaryType(object src, Type destType)
		{
			if (src == null) return null;
			if (destType.IsInstanceOfType(src)) return src;
			if (src is string s)
				return DateTime.Parse(s, FormatInfo);
			return default(DateTime);
		}
	}
}