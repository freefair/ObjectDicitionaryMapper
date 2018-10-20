using System.Collections.Generic;
using ObjectDictionaryMapper.TypeMapping;

namespace ObjectDictionaryMapper
{
	public class MapperConfiguration
	{
		public static MapperConfiguration Default { get; set; } = new MapperConfiguration();

		public List<ITypeMapper> TypeMappers { get; set; } = new List<ITypeMapper>() {
			new IntTypeMapper(),
			new StringTypeMapper(),
			new DoubleTypeMapper(),
			new DateTimeTypeMapper(),
			new BooleanTypeMapper()
		};
	}
}