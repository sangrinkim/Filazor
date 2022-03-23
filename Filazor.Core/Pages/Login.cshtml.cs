﻿using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Filazor.Core.Data;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Filazor.Core.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string paramUsername, string paramPassword)
        {
            string returnUrl = Url.Content("~/");
            try
            {
                // Clear the existing external cookie
                await HttpContext
                    .SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch { }

            string loginResult = LoginService.Login(paramUsername, paramPassword);
            if (string.IsNullOrEmpty(loginResult) == false)
            {
                return LocalRedirect($"/loginControl/{ loginResult }");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, paramUsername),
                new Claim(ClaimTypes.Role, "Administrators"),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                RedirectUri = "/main"
            };

            try
            {
                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return LocalRedirect(returnUrl);
        }

        public void OnPost()
        {
            Console.WriteLine("post");
        }

        public async Task<IActionResult> OnPostAsync(string text)
        {
            //string returnUrl = Url.Content("~/");
            //try
            //{
            //    // Clear the existing external cookie
            //    await HttpContext
            //        .SignOutAsync(
            //        CookieAuthenticationDefaults.AuthenticationScheme);
            //}
            //catch { }

            //string loginResult = LoginService.Login(userLoginModel.UserID, userLoginModel.Password);
            //if (string.IsNullOrEmpty(loginResult) == false)
            //{
            //    return LocalRedirect($"/loginControl/{ loginResult }");
            //}

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, userLoginModel.UserID),
            //    new Claim(ClaimTypes.Role, "Administrators"),
            //};
            //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //var authProperties = new AuthenticationProperties
            //{
            //    IsPersistent = true,
            //    ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
            //    RedirectUri = "/main"
            //};

            //try
            //{
            //    await HttpContext.SignInAsync(
            //    CookieAuthenticationDefaults.AuthenticationScheme,
            //    new ClaimsPrincipal(claimsIdentity),
            //    authProperties);
            //}
            //catch (Exception ex)
            //{
            //    string error = ex.Message;
            //}

            //return LocalRedirect(returnUrl);
            return null;
        }
    }
}
