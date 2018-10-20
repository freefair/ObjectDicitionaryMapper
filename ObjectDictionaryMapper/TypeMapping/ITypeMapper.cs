using System;

namespace ObjectDictionaryMapper.TypeMapping
{
	public interface ITypeMapper
	{
		bool CanHandle(Type type);
		object ToDictionaryType(object src);
		object FromDictionaryType(object src, Type destType);
	}
}