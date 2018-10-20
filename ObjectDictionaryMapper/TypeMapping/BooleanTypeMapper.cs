using System;

namespace ObjectDictionaryMapper.TypeMapping
{
	public class BooleanTypeMapper : ITypeMapper
	{
		public bool CanHandle(Type type)
		{
			return type == typeof(bool);
		}

		public object ToDictionaryType(object src)
		{
			return ((bool) src).ToString();
		}

		public object FromDictionaryType(object src, Type destType)
		{
			if (src == null) return null;
			if (destType.IsInstanceOfType(src)) return src;
			if (src is string s)
				return bool.Parse(s);
			return default(bool);
		}
	}
}