﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filazor.Core.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("[action]")]
        public UserModel GetUser()
        {
            // Instantiate a UserModel
            var userModel = new UserModel
            {
                UserName = "Unknown",
                IsAuthenticated = false
            };

            // Detect if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Set the username of the authenticated user
                userModel.UserName = User.Identity.Name;
                userModel.IsAuthenticated = User.Identity.IsAuthenticated;
                userModel.Role = User.Claims.First(c => c.Type == ClaimTypes.Role).Value;
            }
            else
            {
                return null;
            }

            return userModel;
        }
    }

    public class UserModel
    {
        public string UserName { get; set; }

        public bool IsAuthenticated { get; set; }

        public string Role { get; set; }
    }
}
