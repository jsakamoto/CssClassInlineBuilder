# CSS Class Inline Builder (designed for Blazor) [![NuGet Package](https://img.shields.io/nuget/v/Toolbelt.Web.CssClassInlineBuilder.svg)](https://www.nuget.org/packages/Toolbelt.Web.CssClassInlineBuilder/)

## Summary

Build CSS class string for "class" attribute dynamically based on boolean switch values, in Razor files in-line.

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

Add `@using static Toolbelt.Web.CssClassInlineBuilder` declaration in head of each `.razor` file which you want to use CSS class inline builder.

You can also add the declaration in `_Imports.razor` once, instead.

```csharp
@using static Toolbelt.Web.CssClassInlineBuilder
```

### 3. Use "CssClass(...)" method to build  CSS class string!

You can use the `CssClass(...)` method anywhere you want to build CSS class string.

Basically, you can pass any number of CSS class name strings to the arguments of `CssClass()` method.

The `CssClass()` method returns a string that is concatinated with all of those passed to argument strings with space separator.

```html
<div class="@CssClass("foo", "bar")">
<!-- You will be got `class="foo bar"` -->
```

Next, you can pass any number of objects (include anonymous type) that contains `bool` properties to the arguments of the `CssClass()` method.

The `CssClass()` method picks up the `bool` properties of argument objects which it's value is `true`, and concatinate those property's name strings with space separator, and return it. (The name of properties are converted lowercase.)

```html
<div class="@CssClass(new {Foo=true, Bar=false}, new {Fizz=true})">
<!-- You will be got `class="foo fizz"` -->
```

As you know, the anonymous type can omit explicit property names if the name is the same with a variable name.

```html
<div class="@CssClass(new {Foo, Bar}, new {Fizz})">
<!-- You will be got `class="bar fizz"` -->
@code {
  private bool Foo = false;
  private bool Bar = true;
  private bool Fizz = true;
  ...
```

The property name will be converted from camel case/snake case naming convention to hyphenated lower case.

```html
<div class="@CssClass(new {FizzBuzz})">
<!-- You will be got `class="fizz-buzz"` -->
@code {
  private bool FizzBuzz = true;
  ...
```

You can pass mixing strings and objects to the argument of `CssClass()` method.

```html
<div class="@CssClass(new {Fizz, Buzz}, $"stars-{NumOfStars}")">
<!-- You will be got `class="fizz stars-5"` -->
@code {
  private bool Fizz = true;
  private bool Buzz = false;
  private int NumOfStars = 5;
  ...
```

## Notice

The `CssClass()` method uses the .NET CLR "Reflection" feature to parse the object's `bool` property.

This means that using the `CssClass ()` method can degrade performance.

The `CssClass ()` method includes a caching mechanism to avoid performance degradation, but it will be better to let you know this information anyway.

## License

[Mozilla Public License Version 2.0](https://github.com/jsakamoto/CssClassInlineBuilder/blob/master/LICENSE)