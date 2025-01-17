﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Alamut.Data.Structure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.CrossCutting.Security.Helper;
using Mobin.CoreProject.CrossCutting.Security.Models;

namespace Mobin.CoreProject.CrossCutting.Security.Services
{
    public class UserService : IUserService
    {
        // TODO : put it in config
        private const string DomainName = "MOBINNET";
        private const string DomainEMail = "mobinnet.net";

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEmailSender _emailSender;

        public UserService(UserManager<AppUser> userManager, 
            RoleManager<AppRole> roleManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        public async Task<ServiceResult<AppUser>> CreateDomainUserAsync(string username)
        {
            // TODO : [Soren] -> check user name with DS

            // clean up username
            username = username.ToLower()
                .Replace(DomainName.ToLower() + "\\", "")
                .Replace("@" + DomainEMail.ToLower(), "");

            var user = new AppUser {UserName = $"{DomainName}\\{username}", Email = $"{username}@{DomainEMail}"};
            var result = await _userManager.CreateAsync(user);

            return result.AsServiceResult<AppUser>(user);
        }

        public async Task<ServiceResult<AppUser>> CreatePublicUserAsync(string username, string password,
            string email = null, string emailConformationCallbackUrl = null)
        {
            var user = new AppUser
            {
                UserName = username,
                Email = username
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded && email != null && emailConformationCallbackUrl != null)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = emailConformationCallbackUrl + $"?userId={user.Id}&code={code}";

                await _emailSender.SendEmailAsync(email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            }


            return result.AsServiceResult<AppUser>(user);
        }

        public async Task<ServiceResult> DeleteAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            
            if (user == null)
                { return ServiceResult.Error($"There is no user with userId {userId}"); }

            var result = await _userManager.DeleteAsync(user);

            return result.AsServiceResult();
        }

        public async Task<ServiceResult> DeleteAsync(string username)
        {
            var user = await _userManager.FindByNameAsync($"{DomainName}\\{username}");

            if (user == null)
                { return ServiceResult.Error($"There is no user with username {username}"); }
            
            var result = await _userManager.DeleteAsync(user);

            return result.AsServiceResult();
        }

        public async Task<ServiceResult> UpdateRoles(int userId, List<int> roleIds)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            { return ServiceResult.Error($"There is no user with userId {userId}"); }

            var userRoles = await _userManager.GetRolesAsync(user);

            var roles = _roleManager.Roles
                .Where(q => roleIds.Contains(q.Id))
                .Select(s => s.Name);

            var result = await _userManager.RemoveFromRolesAsync(user, userRoles);

            if (!result.Succeeded)
                { return result.AsServiceResult(); }

            result = await _userManager.AddToRolesAsync(user, roles);

            return result.AsServiceResult();
        }

        public async Task<ServiceResult> RenameUsername(int id, string newUsername)
        {
            // clean up username
            newUsername = newUsername.ToLower()
                .Replace(DomainName.ToLower() + "\\", "")
                .Replace("@" + DomainEMail.ToLower(), "");

            var user = await FindByIdAsync(id);
            user.UserName = $"{DomainName}\\{newUsername}";
            user.Email = $"{newUsername}@{DomainEMail}";

            var result = await _userManager.UpdateAsync(user);
            return result.AsServiceResult();
        }

        public IList<AppUser> GetAll() =>
            _userManager.Users
                .OrderBy(q => q.UserName)
                .ToList();

        public async Task<IList<AppUser>> GetUsersByClaim(string claimType, string claimValue)
        {
            return await _userManager.GetUsersForClaimAsync(new Claim(claimType, claimValue));
        }

        public Task<AppUser> FindByIdAsync(int id) => 
            _userManager.FindByIdAsync(id.ToString());

        public Task<IList<string>> GetRolesAsync(AppUser user) => 
            _userManager.GetRolesAsync(user);

        public async Task<ServiceResult> SetClaimAsync(int userId, string type, string value)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                { return ServiceResult.Error($"There is no user with userId {userId}"); }

            var claim = new Claim(type, value);

            var result = await _userManager.AddClaimAsync(user, claim);

            return result.AsServiceResult();
        }

        public async Task<IList<Claim>> GetClaimsAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                       ?? throw new NullReferenceException($"There is no user with userId {userId}");

            return await _userManager.GetClaimsAsync(user);
        }

        public async Task<string> GetClaimValue(int userId, string type)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                       ?? throw new NullReferenceException($"There is no user with userId {userId}");

            var claims = await _userManager.GetClaimsAsync(user);

            return claims
                .FirstOrDefault(q => q.Type == type)
                ?.Value;
        }

        public async Task<List<string>> GetClaimValues(int userId, object type)
        {
            var claimType = type?.ToString() 
                            ?? throw new ArgumentNullException(nameof(type));

            var user = await _userManager.FindByIdAsync(userId.ToString())
                       ?? throw new ArgumentNullException($"There is no user with userId {userId}");

            var claims = await _userManager.GetClaimsAsync(user);

            return claims
                .Where(q => q.Type == claimType)
                .Select(s => s.Value)
                .ToList();
        }

        public async Task<ServiceResult> RemoveClaimAsync(int userId, string type, string value)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            { return ServiceResult.Error($"There is no user with userId {userId}"); }

            var claim = new Claim(type, value);

            var result = await _userManager.RemoveClaimAsync(user, claim);

            return result.AsServiceResult();
        }

        public async Task<ServiceResult> RemoveClaimAsync(int userId, Claim userClaim)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.RemoveClaimAsync(user, userClaim);
            return result.AsServiceResult();
        }

        public async Task<ServiceResult> UpdateClaims(int userId, List<KeyValuePair<string, string>> claims)
        {
            var userClaims = await GetClaimsAsync(userId);

            foreach (var userClaim in userClaims)
            {
                await RemoveClaimAsync(userId, userClaim);
            }

            foreach (var claim in claims.Where(q => !string.IsNullOrWhiteSpace(q.Value)))
            {
                await SetClaimAsync(userId, claim.Key, claim.Value);
            }

            return ServiceResult.Okay();
        }
    }
}