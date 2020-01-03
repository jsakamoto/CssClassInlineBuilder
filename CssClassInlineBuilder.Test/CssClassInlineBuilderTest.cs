using System;
using Xunit;
using static Toolbelt.Web.CssClassInlineBuilder;

namespace Toolbelt.Web.Test
{
    public class CssClassInlineBuilderTest
    {
        [Fact(DisplayName = "classes - Camel Case To Hyphenation")]
        public void Classes_CamelCaseToHyphenation_Test()
        {
            CssClass("FooBar", new { ThisIsTESTSample = true, itsOK = true }, "FizzBuzz")
                .Is("FooBar this-is-testsample its-ok FizzBuzz");
        }

        [Fact(DisplayName = "classes - Evaluate bool values")]
        public void Classes_EvaludateBoolValues_Test()
        {
            CssClass(new { Foo = false, Bar = "BAR", Fizz = true, Buzz = 123 })
                .Is("fizz");
        }
    }
}
