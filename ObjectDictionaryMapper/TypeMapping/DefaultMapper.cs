using System;
using System.ComponentModel;

namespace ObjectDictionaryMapper.TypeMapping
{
	public abstract class DefaultMapper : ITypeMapper
	{
		public abstract bool CanHandle(Type type);

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