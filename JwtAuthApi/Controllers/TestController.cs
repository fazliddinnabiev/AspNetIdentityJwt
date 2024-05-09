﻿using Microsoft.AspNetCore.Mvc;

namespace JwtAuthApi.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    /// <summary>
    /// Tests authorization for role 1.
    /// </summary>
    /// <returns>string if authorized.</returns>
    [HttpGet]
    [Route("role1")]
    public string TestRole1()
    {
        return "Authorized for Role1";
    }

    /// <summary>
    /// Tests authorization for role 2.
    /// </summary>
    /// <returns>string if authorized</returns>
    [HttpGet]
    [Route("role2")]
    public string TestRole2()
    {
        return "Authorized for Role2";
    }
}