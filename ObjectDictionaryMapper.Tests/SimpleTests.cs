using System;
using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using ObjectDictionaryMapper.Tests.TestClasses;

namespace ObjectDictionaryMapper.Tests
{
	[TestFixture]
	public class SimpleTests
	{
		[Test]
		public void SimpleDefaultConfigTest()
		{
			var obj = new SimpleObject();
			var dictionary = Mapper.ToDictionary(obj);
			Assert.That(dictionary, Does.ContainKey("StringProperty").And.ContainValue("Test123"));
			Assert.That(dictionary, Does.ContainKey("IntProperty").And.ContainValue(123));
			Assert.That(dictionary, Does.ContainKey("BoolProperty").And.ContainValue(true));
			Assert.That(dictionary, Does.ContainKey("DateTimeProperty").And.ContainValue(DateTime.MaxValue.ToString(DateTimeFormatInfo.InvariantInfo)));
			Assert.That(dictionary, Does.ContainKey("DoubleProperty").And.ContainValue(123.321));

			var simpleObject = Mapper.ToObject<SimpleObject>(dictionary);
			Assert.That(simpleObject.BoolProperty, Is.EqualTo(true));
			Assert.That(simpleObject.DoubleProperty, Is.EqualTo(123.321));
			Assert.That(simpleObject.IntProperty, Is.EqualTo(123));
			Assert.That(simpleObject.StringProperty, Is.EqualTo("Test123"));
			Assert.That(simpleObject.DateTimeProperty, Is.EqualTo(DateTime.MaxValue.AddTicks(-9999999)));
		}
		
		[Test]
		public void SimpleDefaultConfigTestTwo()
		{
			var obj = new SimpleObjectTwo();
			var dictionary = Mapper.ToDictionary(obj);
			Assert.That(dictionary, Does.ContainKey("StringProperty").And.ContainValue("Test321"));
			Assert.That(dictionary, Does.ContainKey("SimpleObject"));
			Assert.That(dictionary, Does.ContainKey("StringsProperty"));
			Assert.That(dictionary, Does.ContainKey("StringssProperty"));
			
			var subObject = dictionary["SimpleObject"];
			Assert.That(subObject, Does.ContainKey("StringProperty").And.ContainValue("Test123"));
			Assert.That(subObject, Does.ContainKey("IntProperty").And.ContainValue(123));
			Assert.That(subObject, Does.ContainKey("BoolProperty").And.ContainValue(true));
			Assert.That(subObject, Does.ContainKey("DateTimeProperty").And.ContainValue(DateTime.MaxValue.ToString(DateTimeFormatInfo.InvariantInfo)));
			Assert.That(subObject, Does.ContainKey("DoubleProperty").And.ContainValue(123.321));

			var list = dictionary["StringsProperty"];
			Assert.That(list, Does.Contain("Test123"));
			Assert.That(list, Does.Contain("Test456"));
			
			var list2 = (List<object>)dictionary["StringssProperty"];
			var list3 = list2[0];
			Assert.That(list3, Does.Contain("Test123"));
			Assert.That(list3, Does.Contain("Test456"));
			list3 = list2[1];
			Assert.That(list3, Does.Contain("Test789"));
			Assert.That(list3, Does.Contain("Test012"));

			var simpleObjectTwo = Mapper.ToObject<SimpleObjectTwo>(dictionary);
			Assert.That(simpleObjectTwo.StringssProperty, Has.Count.EqualTo(2));
			Assert.That(simpleObjectTwo.StringssProperty, Has.One.Contains("Test123"));
			Assert.That(simpleObjectTwo.StringssProperty, Has.One.Contains("Test012"));
			Assert.That(simpleObjectTwo.SimpleObject, Is.Not.Null);
			Assert.That(simpleObjectTwo.SimpleObject.BoolProperty, Is.EqualTo(true));
		}
	}
}