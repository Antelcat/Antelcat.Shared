#if !NET && !NETSTANDARD
using System;
using System.Collections.Generic;
#nullable enable
#endif
using Antelcat.Extensions;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
namespace Antelcat.Implements.Converters;
///<summary>
/// Convert between <see cref="string"/> and <see cref="sbyte"/>
///</summary>
public class StringToSbyteConverter : TypeConverter
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToSbyte();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
   	public override object? ConvertFrom(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object value) => value.ToString();

   	public override bool CanConvertTo(
		ITypeDescriptorContext? _, 
		Type? destinationType) => destinationType == typeof(sbyte);

   	public override bool CanConvertFrom(
		ITypeDescriptorContext? _, 
		Type sourceType) => sourceType == typeof(sbyte);
}

///<summary>
/// Convert between <see cref="string"/> and <see cref="byte"/>
///</summary>
public class StringToByteConverter : TypeConverter
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToByte();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
   	public override object? ConvertFrom(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object value) => value.ToString();

   	public override bool CanConvertTo(
		ITypeDescriptorContext? _, 
		Type? destinationType) => destinationType == typeof(byte);

   	public override bool CanConvertFrom(
		ITypeDescriptorContext? _, 
		Type sourceType) => sourceType == typeof(byte);
}

///<summary>
/// Convert between <see cref="string"/> and <see cref="bool"/>
///</summary>
public class StringToBoolConverter : TypeConverter
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToBool();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
   	public override object? ConvertFrom(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object value) => value.ToString();

   	public override bool CanConvertTo(
		ITypeDescriptorContext? _, 
		Type? destinationType) => destinationType == typeof(bool);

   	public override bool CanConvertFrom(
		ITypeDescriptorContext? _, 
		Type sourceType) => sourceType == typeof(bool);
}

///<summary>
/// Convert between <see cref="string"/> and <see cref="int"/>
///</summary>
public class StringToIntConverter : TypeConverter
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToInt();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
   	public override object? ConvertFrom(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object value) => value.ToString();

   	public override bool CanConvertTo(
		ITypeDescriptorContext? _, 
		Type? destinationType) => destinationType == typeof(int);

   	public override bool CanConvertFrom(
		ITypeDescriptorContext? _, 
		Type sourceType) => sourceType == typeof(int);
}

///<summary>
/// Convert between <see cref="string"/> and <see cref="uint"/>
///</summary>
public class StringToUintConverter : TypeConverter
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToUint();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
   	public override object? ConvertFrom(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object value) => value.ToString();

   	public override bool CanConvertTo(
		ITypeDescriptorContext? _, 
		Type? destinationType) => destinationType == typeof(uint);

   	public override bool CanConvertFrom(
		ITypeDescriptorContext? _, 
		Type sourceType) => sourceType == typeof(uint);
}

///<summary>
/// Convert between <see cref="string"/> and <see cref="long"/>
///</summary>
public class StringToLongConverter : TypeConverter
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToLong();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
   	public override object? ConvertFrom(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object value) => value.ToString();

   	public override bool CanConvertTo(
		ITypeDescriptorContext? _, 
		Type? destinationType) => destinationType == typeof(long);

   	public override bool CanConvertFrom(
		ITypeDescriptorContext? _, 
		Type sourceType) => sourceType == typeof(long);
}

///<summary>
/// Convert between <see cref="string"/> and <see cref="ulong"/>
///</summary>
public class StringToUlongConverter : TypeConverter
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToUlong();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
   	public override object? ConvertFrom(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object value) => value.ToString();

   	public override bool CanConvertTo(
		ITypeDescriptorContext? _, 
		Type? destinationType) => destinationType == typeof(ulong);

   	public override bool CanConvertFrom(
		ITypeDescriptorContext? _, 
		Type sourceType) => sourceType == typeof(ulong);
}

///<summary>
/// Convert between <see cref="string"/> and <see cref="double"/>
///</summary>
public class StringToDoubleConverter : TypeConverter
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToDouble();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
   	public override object? ConvertFrom(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object value) => value.ToString();

   	public override bool CanConvertTo(
		ITypeDescriptorContext? _, 
		Type? destinationType) => destinationType == typeof(double);

   	public override bool CanConvertFrom(
		ITypeDescriptorContext? _, 
		Type sourceType) => sourceType == typeof(double);
}

///<summary>
/// Convert between <see cref="string"/> and <see cref="float"/>
///</summary>
public class StringToFloatConverter : TypeConverter
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToFloat();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
   	public override object? ConvertFrom(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object value) => value.ToString();

   	public override bool CanConvertTo(
		ITypeDescriptorContext? _, 
		Type? destinationType) => destinationType == typeof(float);

   	public override bool CanConvertFrom(
		ITypeDescriptorContext? _, 
		Type sourceType) => sourceType == typeof(float);
}

///<summary>
/// Convert between <see cref="string"/> and <see cref="DateTime"/>
///</summary>
public class StringToDateTimeConverter : TypeConverter
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToDateTime();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
   	public override object? ConvertFrom(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object value) => value.ToString();

   	public override bool CanConvertTo(
		ITypeDescriptorContext? _, 
		Type? destinationType) => destinationType == typeof(DateTime);

   	public override bool CanConvertFrom(
		ITypeDescriptorContext? _, 
		Type sourceType) => sourceType == typeof(DateTime);
}

///<summary>
/// Convert between <see cref="string"/> and <see cref="Guid"/>
///</summary>
public class StringToGuidConverter : TypeConverter
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToGuid();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
   	public override object? ConvertFrom(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object value) => value.ToString();

   	public override bool CanConvertTo(
		ITypeDescriptorContext? _, 
		Type? destinationType) => destinationType == typeof(Guid);

   	public override bool CanConvertFrom(
		ITypeDescriptorContext? _, 
		Type sourceType) => sourceType == typeof(Guid);
}


public static class StringValueConverters
{
	private static readonly Dictionary<Type,TypeConverter> Instances = new ()
	{
		{ typeof(sbyte) , new StringToSbyteConverter() },
		{ typeof(byte) , new StringToByteConverter() },
		{ typeof(bool) , new StringToBoolConverter() },
		{ typeof(int) , new StringToIntConverter() },
		{ typeof(uint) , new StringToUintConverter() },
		{ typeof(long) , new StringToLongConverter() },
		{ typeof(ulong) , new StringToUlongConverter() },
		{ typeof(double) , new StringToDoubleConverter() },
		{ typeof(float) , new StringToFloatConverter() },
		{ typeof(DateTime) , new StringToDateTimeConverter() },
		{ typeof(Guid) , new StringToGuidConverter() },
	};

	public static bool FindByType(Type type, out TypeConverter? converter) 
		=> Instances.TryGetValue(type, out converter); 
}