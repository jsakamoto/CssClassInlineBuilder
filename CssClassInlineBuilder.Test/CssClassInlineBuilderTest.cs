using Xunit;
using static Toolbelt.Web.CssClassInlineBuilder;

namespace Toolbelt.Web.Test;

public class CssClassInlineBuilderTest
{
    [Fact(DisplayName = "classes - Camel Case To Hyphenation")]
    public void Classes_CamelCaseToHyphenation_Test()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        CssClass("FooBar", new { ThisIsTESTSample = true, itsOK = true }, "FizzBuzz")
            .Is("FooBar this-is-testsample its-ok FizzBuzz");
#pragma warning restore CS0618 // Type or member is obsolete
    }

    [Fact(DisplayName = "classes - Evaluate bool values")]
    public void Classes_EvaludateBoolValues_Test()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        CssClass(new { Foo = false, Fizz = true })
            .Is("fizz");
#pragma warning restore CS0618 // Type or member is obsolete
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
#pragma warning disable CS0618 // Type or member is obsolete
        CssClass(EnumForTest.Foo, EnumForTest.Bar, EnumForTest.FizzBuzz)
            .Is("foo bar fizz-buzz");
#pragma warning restore CS0618 // Type or member is obsolete
    }

    [Fact(DisplayName = "classes - any other values in members of an object")]
    public void Classes_AnyOtherValues_in_Members_of_an_Object_Test()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        CssClass(new { ActionState = EnumForTest.Foo })
            .Is("action-state-foo");
        CssClass(new { ItsBad = false, ActionState = EnumForTest.FizzBuzz, NumberOfStars = 5 })
            .Is("action-state-fizz-buzz number-of-stars-5");
        CssClass(new { ItsBad = true, ActionState = EnumForTest.Bar, Theme = "Crystal Blue" })
            .Is("its-bad action-state-bar theme-crystal-blue");
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
