﻿using System.Text.Json;

namespace FileServer_Asp.Middlewares;

public class IpWhitelistMiddleWare
{
    private readonly RequestDelegate _next;
    private readonly string[] _allowedIps;

    public IpWhitelistMiddleWare()
    {
    }

    public IpWhitelistMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string remoteIp = context.Connection.RemoteIpAddress.ToString();

        if (remoteIp == null || !_allowedIps.Contains(remoteIp))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Forbidden: Your ip address is not allowed");


            return;
        }

        await _next(context);
    }

    
}
