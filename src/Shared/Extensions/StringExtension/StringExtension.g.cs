#if !NET && !NETSTANDARD
using System;
#endif
using System.Runtime.CompilerServices;
namespace Antelcat.Extensions;
#nullable enable
public static partial class StringExtension
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ToSbyte(this string? str,out sbyte result) => sbyte.TryParse(str,out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static sbyte ToSbyte(this string? str) => sbyte.TryParse(str,out var result) ? result : sbyte.MinValue;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ToByte(this string? str,out byte result) => byte.TryParse(str,out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static byte ToByte(this string? str) => byte.TryParse(str,out var result) ? result : byte.MinValue;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ToInt(this string? str,out int result) => int.TryParse(str,out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int ToInt(this string? str) => int.TryParse(str,out var result) ? result : int.MinValue;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ToUint(this string? str,out uint result) => uint.TryParse(str,out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static uint ToUint(this string? str) => uint.TryParse(str,out var result) ? result : uint.MinValue;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ToLong(this string? str,out long result) => long.TryParse(str,out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static long ToLong(this string? str) => long.TryParse(str,out var result) ? result : long.MinValue;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ToUlong(this string? str,out ulong result) => ulong.TryParse(str,out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ulong ToUlong(this string? str) => ulong.TryParse(str,out var result) ? result : ulong.MinValue;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ToDouble(this string? str,out double result) => double.TryParse(str,out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double ToDouble(this string? str) => double.TryParse(str,out var result) ? result : double.NaN;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ToFloat(this string? str,out float result) => float.TryParse(str,out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float ToFloat(this string? str) => float.TryParse(str,out var result) ? result : float.NaN;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ToDateTime(this string? str,out DateTime result) => DateTime.TryParse(str,out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DateTime ToDateTime(this string? str) => DateTime.TryParse(str,out var result) ? result : DateTime.MinValue;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ToGuid(this string? str,out Guid result) => Guid.TryParse(str,out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Guid ToGuid(this string? str) => Guid.TryParse(str,out var result) ? result : Guid.Empty;


}
