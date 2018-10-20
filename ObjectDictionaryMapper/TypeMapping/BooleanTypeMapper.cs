using System;
using System.ComponentModel;

namespace ObjectDictionaryMapper.TypeMapping
{
	public class BooleanTypeMapper : DefaultMapper
	{
		public override bool CanHandle(Type type)
		{
			return type == typeof(bool);
		}
	}
}