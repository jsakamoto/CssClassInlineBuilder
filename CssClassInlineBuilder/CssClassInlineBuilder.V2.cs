using System.Diagnostics.CodeAnalysis;
using static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes;

namespace Toolbelt.Web;

/// <summary>
/// Build CSS class string for "class" attribute dynamically based on boolean switch values.
/// </summary>
public static partial class CssClassInlineBuilder
{
    /// <summary>
    /// Build CSS class string for "class" attribute dynamically based on boolean switch values.
    /// </summary>
    public static class V2
    {
        private const DynamicallyAccessedMemberTypes MemberTypes = PublicProperties | PublicConstructors;

        /// <summary>
        /// Build CSS class string from boolean properties of objects in arguments, from strings in arguments.
        /// </summary>
        public static string CssClass<[DynamicallyAccessedMembers(MemberTypes)] T1>(T1 arg1)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return CssClassInlineBuilder.CssClass(arg1);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Build CSS class string from boolean properties of objects in arguments, from strings in arguments.
        /// </summary>
        public static string CssClass<
            [DynamicallyAccessedMembers(MemberTypes)] T1,
            [DynamicallyAccessedMembers(MemberTypes)] T2
            >(T1 arg1, T2 args2)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return CssClassInlineBuilder.CssClass(arg1, args2);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Build CSS class string from boolean properties of objects in arguments, from strings in arguments.
        /// </summary>
        public static string CssClass<
            [DynamicallyAccessedMembers(MemberTypes)] T1,
            [DynamicallyAccessedMembers(MemberTypes)] T2,
            [DynamicallyAccessedMembers(MemberTypes)] T3
            >(T1 arg1, T2 args2, T3 arg3)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return CssClassInlineBuilder.CssClass(arg1, args2, arg3);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Build CSS class string from boolean properties of objects in arguments, from strings in arguments.
        /// </summary>
        public static string CssClass<
            [DynamicallyAccessedMembers(MemberTypes)] T1,
            [DynamicallyAccessedMembers(MemberTypes)] T2,
            [DynamicallyAccessedMembers(MemberTypes)] T3,
            [DynamicallyAccessedMembers(MemberTypes)] T4
            >(T1 arg1, T2 args2, T3 arg3, T4 arg4)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return CssClassInlineBuilder.CssClass(arg1, args2, arg3, arg4);
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
