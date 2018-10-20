using System;
using System.Collections.Generic;

namespace ObjectDictionaryMapper.Tests.TestClasses
{
	public class SimpleObjectTwo
	{
		public SimpleObject SimpleObject { get; set; } = new SimpleObject();
		public string StringProperty { get; set; } = "Test321";
		public List<string> StringsProperty { get; set; } = new List<string> {
			"Test123",
			"Test456"
		};

		public List<List<string>> StringssProperty { get; set; } = new List<List<string>>
		{
			new List<string> {"Test123", "Test456"},
			new List<string> {"Test789", "Test012"}
		};
	}
}