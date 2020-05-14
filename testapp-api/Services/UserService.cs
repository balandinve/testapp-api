using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using testapp_api.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace testapp_api.Services
{
    public class UserService
    {
        private IMapper _mapper;
        private UserManager<StoreUser> _userMgr;

        public UserService(IMapper mapper, UserManager<StoreUser> userMgr)
        {
            _mapper = mapper;
            _userMgr = userMgr;
        }

        public async Task AddAsync(StoreUserRegisterView item)
        {
            var isentityUser = _mapper.Map<StoreUser>(item);
            var result = await _userMgr.CreateAsync(isentityUser, item.Password);
            if (result.Succeeded)
            {
                throw new Exception($"Create user error: {String.Join(',', result.Errors.Select(s => s.Description))}.");
            }
        }

        public async Task<string> GetTokenAsync(string username, string password)
        {
            var now = DateTime.UtcNow;
            var identity = await GetIdentityAsync(username, password);
            if (identity == null)
            {
                return null;
            }
            var jwt = new JwtSecurityToken(
                    issuer: IdentityOptions.ISSUER,
                    audience: IdentityOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(IdentityOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(IdentityOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            string encodedJwt = null;
            try
            {
                encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                return encodedJwt;
            }
            catch (Exception)
            {
                throw; 
            }
        }
        private async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            var userToIdentity = await _userMgr.FindByNameAsync(username);
            if (userToIdentity != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, username),
                    new Claim(JwtRegisteredClaimNames.NameId, userToIdentity.Id)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", JwtRegisteredClaimNames.UniqueName,
                    JwtRegisteredClaimNames.NameId);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
