using System;
using System.Globalization;

namespace ObjectDictionaryMapper.TypeMapping
{
	public class DoubleTypeMapper : ITypeMapper
	{
		public NumberFormatInfo FormatInfo { get; set; } = NumberFormatInfo.InvariantInfo;
	
		public bool CanHandle(Type type)
		{
			return type == typeof(double);
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
				return double.Parse(s, FormatInfo);
			return default(double);
		}
	}
}