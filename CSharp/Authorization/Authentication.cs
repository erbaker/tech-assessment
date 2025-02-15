﻿namespace CSharp.Authorization;

using System.Net.Http.Headers;
using System.Text;
using CSharp.Services;

public class Authentication
{
    private readonly RequestDelegate _next;

    public Authentication(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserAuthenticateService authService)
    {
        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
            var username = credentials[0];
            var password = credentials[1];

            // authenticate credentials with user service and attach user to http context
            context.Items["User"] = await authService.Authenticate(username, password);
        }
        catch
        {
            // do nothing if invalid auth header
            // user is not attached to context so request won't have access to secure routes
        }

        await _next(context);
    }
}