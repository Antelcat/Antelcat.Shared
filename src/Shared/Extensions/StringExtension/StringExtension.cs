﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Antelcat.Extensions;
#nullable enable
public static partial class StringExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToUpperCamelCase(this string value) =>
#if NETSTANDARD2_1_OR_GREATER || NET
        $"{(char)(value[0] - 32)}{value[1..]}";
#else
        $"{(char)(value[0] - 32)}{value.Substring(1)}";
#endif
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToLowerCamelCase(this string value) =>
#if !NETSTANDARD2_1_OR_GREATER
        $"{(char)(value[0] + 32)}{value.Substring(1)}";
#else
        $"{(char)(value[0] + 32)}{value[1..]}";
#endif
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string AnotherCamelCase(this string value) =>
        value.Length == 0
            ? throw new ArgumentNullException($"At least one char in string {value}")
            : value[0] switch
            {
                >= 'a' and <= 'z' => value.ToUpperCamelCase(),
                >= 'A' and <= 'Z' => value.ToLowerCamelCase(),
                _ => throw new ArgumentOutOfRangeException($"First char should be letter but {value[0]}")
            };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrEmpty(this string? str) => string.IsNullOrEmpty(str);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrWhiteSpace(this string? str) => string.IsNullOrWhiteSpace(str);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ToBool(this string? str, out bool result) => bool.TryParse(str, out result);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ToBool(this string? str) => bool.TryParse(str, out var result) && result;

#if NET || NETFRAMEWORK
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ToIntPtr(this string? str, out nint result) => nint.TryParse(str,out result);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nint ToIntPtr(this string? str) => nint.TryParse(str, out var result) ? result : nint.Zero;
#endif
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Format(this string str, params object[] args) => string.Format(str, args);

    /// <summary>
    /// 使用<see cref="Encoding.UTF8"/>为默认编码字符集
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte[] GetBytes(this string str) => Encoding.UTF8.GetBytes(str);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte[]GetBytes(this string str, Encoding encoding) => encoding.GetBytes(str);
    
    public static StringBuilder AppendForEach<T>(this StringBuilder builder, 
        IEnumerable<T> enumerable, 
        Func<T, string> func)
    {
        foreach (var item in enumerable)
        {
            builder.Append(func(item));
        }

        return builder;
    }
    
    public static StringBuilder AppendLineForEach<T>(this StringBuilder builder, 
        IEnumerable<T> enumerable, 
        Func<T, string> func)
    {
        foreach (var item in enumerable)
        {
            builder.AppendLine(func(item));
        }

        return builder;
    }
}