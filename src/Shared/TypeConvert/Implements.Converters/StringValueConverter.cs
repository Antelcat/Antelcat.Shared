#if !NET && !NETSTANDARD
using System;
using System.Collections.Generic;
#nullable enable
#endif
using Antelcat.Extensions;
using System.ComponentModel;
using System.Globalization;
namespace Antelcat.Implements.Converters;
///<summary>
/// Convert between <see cref="string"/> and <see cref="sbyte"/>
///</summary>
public class StringToSbyteConverter : TypeConverter
{
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToSbyte();

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
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToByte();

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
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToBool();

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
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToInt();

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
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToUint();

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
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToLong();

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
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToUlong();

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
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToDouble();

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
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToFloat();

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
	public override object ConvertTo(
		ITypeDescriptorContext? _, 
		CultureInfo? __, 
		object? value, 
		Type ___) => (value as string).ToDateTime();

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
	};

	public static bool FindByType(Type type, out TypeConverter? converter) 
		=> Instances.TryGetValue(type, out converter); 
}