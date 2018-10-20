using System;
using System.ComponentModel;
using System.Globalization;

namespace ObjectDictionaryMapper.TypeMapping
{
	public class DateTimeTypeMapper : DefaultMapper
	{
		public DateTimeFormatInfo DateTimeFormatInfo { get; set; } = DateTimeFormatInfo.InvariantInfo;
		
		public override bool CanHandle(Type type)
		{
			return type == typeof(DateTime);
		}

		public override object ToDictionaryType(object src)
		{
			return ((DateTime)src).ToString(DateTimeFormatInfo);
		}

		public override object FromDictionaryType(object src, Type destType)
		{
			if (src == null) return null;
			if (destType.IsInstanceOfType(src)) return src;
			if (src is string s)
				return DateTime.Parse(s, DateTimeFormatInfo);
			return DateTime.MinValue;
		}
	}
}