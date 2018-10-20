using System;
using System.Collections.Generic;
using ObjectDictionaryMapper.TypeMapping;

namespace ObjectDictionaryMapper
{
	public class MapperConfiguration
	{
		public static MapperConfiguration Default { get; } = new MapperConfiguration();

		public IntTypeMapper IntTypeMapper { get; } = new IntTypeMapper();
		public StringTypeMapper StringTypeMapper { get; } = new StringTypeMapper();
		public DoubleTypeMapper DoubleTypeMapper { get; } = new DoubleTypeMapper();
		public DateTimeTypeMapper DateTimeTypeMapper  { get; } = new DateTimeTypeMapper();
		public BooleanTypeMapper BooleanTypeMapper { get; } = new BooleanTypeMapper();

		public MapperConfiguration()
		{
			TypeMappers = new List<ITypeMapper> {
				IntTypeMapper,
				StringTypeMapper,
				DoubleTypeMapper,
				DateTimeTypeMapper,
				BooleanTypeMapper
			};
		}

		public List<ITypeMapper> TypeMappers { get; }

		public MapperConfiguration Add(ITypeMapper typeMapper)
		{
			TypeMappers.Add(typeMapper);
			return this;
		}

		public MapperConfiguration AddAll(params ITypeMapper[] typeMappers)
		{
			TypeMappers.AddRange(typeMappers);
			return this;
		}

		public MapperConfiguration AddFirst(ITypeMapper typeMapper)
		{
			TypeMappers.Insert(0, typeMapper);
			return this;
		}
		
		public MapperConfiguration AddFirstAll(params ITypeMapper[] typeMapper)
		{
			TypeMappers.InsertRange(0, typeMapper);
			return this;
		}
	}
}