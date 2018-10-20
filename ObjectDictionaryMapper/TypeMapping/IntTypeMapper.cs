using System;
using System.ComponentModel;

namespace ObjectDictionaryMapper.TypeMapping
{
	public class IntTypeMapper : DefaultMapper
	{
		public override bool CanHandle(Type type)
		{
			return type == typeof(int) || type == typeof(short) || type == typeof(byte) || type == typeof(long);
		}
	}
}