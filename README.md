# CSS Class Inline Builder (designed for Blazor) [![NuGet Package](https://img.shields.io/nuget/v/Toolbelt.Web.CssClassInlineBuilder.svg)](https://www.nuget.org/packages/Toolbelt.Web.CssClassInlineBuilder/) [![unit tests](https://github.com/jsakamoto/CssClassInlineBuilder/actions/workflows/unit-tests.yml/badge.svg)](https://github.com/jsakamoto/CssClassInlineBuilder/actions/workflows/unit-tests.yml)

## Summary

Build CSS class string for a "class" attribute dynamically based on the boolean switch and enum values in Razor files in-line.

![fig1](https://raw.githubusercontent.com/jsakamoto/CssClassInlineBuilder/master/assets/fig1.png)

## How to use?

### 1. Install the package to your project.

Package Manager Console:

```shell
PM> Install-Package Toolbelt.Web.CssClassInlineBuilder
```

dotnet CLI:

```shell
$ dotnet add package Toolbelt.Web.CssClassInlineBuilder
```

### 2. Declare for using "CSS class inline builder".

Add `@using static Toolbelt.Web.CssClassInlineBuilder.V2` declaration in the head of each `.razor` file where you want to use the CSS class inline builder.

Or you can also add the declaration to `_Imports.razor` once instead.

```csharp
@using static Toolbelt.Web.CssClassInlineBuilder.V2
```

### 3. Use "CssClass(...)" method to build  CSS class string!

You can use the `CssClass(...)` method anywhere you want to build a CSS class string.

#### 3-1. CSS class name strings

Basically, you can pass CSS class name strings up to 4 to the arguments of `CssClass()` method.

The `CssClass()` method returns a string that is concatenated with all of those passed to argument strings with a space separator.

```html
<div class="@CssClass("foo", "bar")">
<!-- You will get `class="foo bar"` -->
```

#### 3-2. objects which has `bool` properties

Next, you can pass objects (including anonymous types) up to 4 that contain `bool` properties to the arguments of the `CssClass()` method.

The `CssClass()` method picks up the `bool` properties where its value is `true` from the argument objects, concatenates those property's name strings with a space separator, and returns them. (The names of properties are converted to lowercase.)

```html
<div class="@CssClass(new {Foo=true, Bar=false}, new {Fizz=true})">
<!-- You will get `class="foo fizz"` -->
```

As you know, the anonymous type can omit explicit property names when the name is the same as a variable name.

```html
<div class="@CssClass(new {Foo, Bar}, new {Fizz})">
<!-- You will get `class="bar fizz"` -->
@code {
  private bool Foo = false;
  private bool Bar = true;
  private bool Fizz = true;
  ...
```

The property name will be converted from a camel/snake case naming convention to a hyphenated lowercase.

```html
<div class="@CssClass(new {FizzBuzz})">
<!-- You will get `class="fizz-buzz"` -->
@code {
  private bool FizzBuzz = true;
  ...
```

#### 3-3. any other type properties in an object

If you pass an object with non-boolean-type properties, a CSS class name will be built for each property.  
That CSS class name will be concatenated with a hyphen of the property name and its value.

```html
@code {
  private int Stars = 5;
  ...
}

<div class="@CssClass(new {NumberOfStars = this.Stars})">
<!-- You will get `class="number-of-stars-5"` -->
```

#### 3-4. enum values

You can also pass enum values up to 4 to the arguments of the `CssClass()` method.

The enum value will be converted to a string as a CSS class name.

The name of the enum value will be converted from camel case/snake case naming convention to hyphenated lower case.

```html
@code {
  enum StateValues {
    NotReady,
    Complete,
    Error
  }

  private StateValues State = StateValues.NotReady;
  ...
}

<div class="@CssClass(this.State)">
<!-- You will get `class="not-ready"` -->
```

#### Finally, you can mix those all!

You can pass mixing strings, objects, and enum values to the argument of the `CssClass()` method.

```html
@code {
  enum StateValues {
    NotReady,
    Complete,
    Error
  }

  private bool Fizz = true;
  private bool Buzz = false;
  private int NumOfStars = 5;
  private StateValues State = StateValues.Complete;
  ...
}
...
<div class="@CssClass(new {Fizz, Buzz}, $"stars-{NumOfStars}", State)">
<!-- You will get `class="fizz stars-5 complete"` -->
```

## Notice

The `CssClass()` method uses the .NET CLR "Reflection" feature to parse the object's properties.

This means using the `CssClass()` method can degrade performance.

The `CssClass()` method includes a caching mechanism to avoid performance degradation, but it will be better to let you know this information anyway.

## Release Notes

You can see the release notes [here](https://github.com/jsakamoto/CssClassInlineBuilder/blob/master/RELEASE-NOTES.txt).

## License

[Mozilla Public License Version 2.0](https://github.com/jsakamoto/CssClassInlineBuilder/blob/master/LICENSE)