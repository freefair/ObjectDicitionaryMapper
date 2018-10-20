using System;
using System.ComponentModel;

namespace ObjectDictionaryMapper.TypeMapping
{
	public class StringTypeMapper : ITypeMapper
	{
		public bool CanHandle(Type type)
		{
			return type == typeof(string) || type == typeof(char);
		}
		
		public virtual object ToDictionaryType(object src)
		{
			return src;
		}

		public virtual object FromDictionaryType(object src, Type destType)
		{
			if (destType.IsInstanceOfType(src))
				return src;
			return TypeDescriptor.GetConverter(destType).ConvertFrom(src);
		}
	}
}