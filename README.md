# CSS Class Inline Builder (designed for Blazor) [![NuGet Package](https://img.shields.io/nuget/v/Toolbelt.Web.CssClassInlineBuilder.svg)](https://www.nuget.org/packages/Toolbelt.Web.CssClassInlineBuilder/)

## Summary

Build CSS class string for "class" attribute dynamically based on boolean switch and enum values, in Razor files in-line.

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

You can use the `CssClass(...)` method anywhere you want to build a CSS class string.

#### 3-1. CSS class name strings

Basically, you can pass any number of CSS class name strings to the arguments of `CssClass()` method.

The `CssClass()` method returns a string that is concatenated with all of those passed to argument strings with a space separator.

```html
<div class="@CssClass("foo", "bar")">
<!-- You will be got `class="foo bar"` -->
```

#### 3-2. objects which has `bool` properties

Next, you can pass any number of objects (include anonymous type) that contains `bool` properties to the arguments of the `CssClass()` method.

The `CssClass()` method picks up the `bool` properties of argument objects which it values is `true`, and concatenate those property's name strings with space separator, and return it. (The name of properties are converted lowercase.)

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

### 3-3. any other type properties in an object

If you pass an object with non-boolean type properties, a CSS class name will be built for each of those properties.  
That CSS class name will be concatenated with a hyphen of the property name and its property value.

```html
@code {
  private int Stars = 5;
  ...
}

<div class="@CssClass(new {NumberOfStars = this.Stars})">
<!-- You will be got `class="number-of-stars-5"` -->
```

### 3-4. enum values

You can also pass any number of enum values to the arguments of the `CssClass()` method.

The enum value will be converted to a string to use as a CSS class name.

The name of enum value will be converted from camel case/snake case naming convention to hyphenated lower case.

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
<!-- You will be got `class="not-ready"` -->
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
<!-- You will be got `class="fizz stars-5 complete"` -->
```

## Notice

The `CssClass()` method uses the .NET CLR "Reflection" feature to parse the object's properties.

This means that using the `CssClass()` method can degrade performance.

The `CssClass()` method includes a caching mechanism to avoid performance degradation, but it will be better to let you know this information anyway.

## Release Notes

You can see the release notes [here](https://github.com/jsakamoto/CssClassInlineBuilder/blob/master/RELEASE-NOTES.txt).

## License

[Mozilla Public License Version 2.0](https://github.com/jsakamoto/CssClassInlineBuilder/blob/master/LICENSE)