using System;
using System.ComponentModel;

namespace ObjectDictionaryMapper.TypeMapping
{
	public class DoubleTypeMapper : DefaultMapper
	{
		public override bool CanHandle(Type type)
		{
			return type == typeof(double);
		}
	}
}