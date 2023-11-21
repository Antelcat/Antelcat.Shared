﻿using System.Net;
using Antelcat.Exceptions;
using Antelcat.Extensions;

namespace Antelcat.Server.Exceptions;

public class ServerException : StackTraceException
{
    public new object? Data { get; init; }

    public int StatusCode { get; init; } = (int)HttpStatusCode.InternalServerError;

    public ServerException(string? message = null) : base(message) { }

    public static implicit operator ServerException(HttpStatusCode statusCode) => (int)statusCode;
    public static implicit operator ServerException(int statusCode)
        => new()
        {
            StatusCode = statusCode
        };
    
    public static implicit operator ServerException((HttpStatusCode statusCode, object? responseData) tuple)
        => ((int)tuple.statusCode, tuple.responseData);
    public static implicit operator ServerException((int statusCode, object? responseData) tuple)
        => new()
        {
            StatusCode = tuple.statusCode,
            Data       = tuple.responseData
        };
    
    public static implicit operator ServerException((HttpStatusCode statusCode, object? responseData, string message) tuple)
        => ((int)tuple.statusCode, tuple.responseData, tuple.message);
    public static implicit operator ServerException((int statusCode, object? responseData, string message) tuple)
        => new(tuple.message)
        {
            StatusCode = tuple.statusCode,
            Data       = tuple.responseData
        };



    protected override IEnumerable<(string key, object? value)> WriteExtraData()
    {
        yield return ("statusCode", StatusCode);
        yield return ("data", Data);
    }
}