using Xunit;
using static Toolbelt.Web.CssClassInlineBuilder.V2;

namespace Toolbelt.Web.Test;

public class CssClassInlineBuilderV2Test
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
        CssClass(new { Foo = false, Fizz = true })
            .Is("fizz");
    }

    private enum EnumForTest
    {
        Foo,
        Bar,
        FizzBuzz
    }

    [Fact(DisplayName = "classes - Evaluate enum values")]
    public void Classes_EvaludateEnumlValues_Test()
    {
        CssClass(EnumForTest.Foo, EnumForTest.Bar, EnumForTest.FizzBuzz)
            .Is("foo bar fizz-buzz");
    }

    [Fact(DisplayName = "classes - any other values in members of an object")]
    public void Classes_AnyOtherValues_in_Members_of_an_Object_Test()
    {
        CssClass(new { ActionState = EnumForTest.Foo })
            .Is("action-state-foo");
        CssClass(new { ItsBad = false, ActionState = EnumForTest.FizzBuzz, NumberOfStars = 5 })
            .Is("action-state-fizz-buzz number-of-stars-5");
        CssClass(new { ItsBad = true, ActionState = EnumForTest.Bar, Theme = "Crystal Blue" })
            .Is("its-bad action-state-bar theme-crystal-blue");
    }
}
