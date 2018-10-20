using System;

namespace ObjectDictionaryMapper.TypeMapping
{
	public class StringTypeMapper : DefaultMapper
	{
		public override bool CanHandle(Type type)
		{
			return type == typeof(string) || type == typeof(char);
		}
	}
}