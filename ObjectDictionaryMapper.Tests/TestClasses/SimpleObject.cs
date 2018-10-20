using System;

namespace ObjectDictionaryMapper.Tests.TestClasses
{
	public class SimpleObject
	{
		public String StringProperty { get; set; } = "Test123";
		public int IntProperty { get; set; } = 123;
		public double DoubleProperty { get; set; } = 123.321;
		public DateTime DateTimeProperty { get; set; } = DateTime.MaxValue;
		public bool BoolProperty { get; set; } = true;
		public short ShortProperty { get; set; } = 123;
		public byte ByteProperty { get; set; } = 123;
		public long LongProperty { get; set; } = 123;
		public uint UIntProperty { get; set; } = 123;
	}
}