using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TurntableLottery.Configuration;
using TurntableLottery.Token.Model;

namespace TurntableLottery.Token
{
    public class TurnTableLotteryToken
    {
        public TurnTableLotteryToken()
        { 
        }
        /// <summary>
        /// 颁布JWT
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="permissionRequirement"></param>
        /// <returns></returns>
        public static dynamic IssueJWT(Claim[] claims, PermissionRequirement permissionRequirement)
        {
            DateTime UTC = DateTime.UtcNow;
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: permissionRequirement.Issuer,
                audience: permissionRequirement.Audience,
                claims: claims,
                expires: DateTime.Now.Add(permissionRequirement.Expiration),
                signingCredentials: permissionRequirement.SigningCredentials
                );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);//生成最后的JWT字符串

            var responseJson = new
            {
                success = true,
                token = encodedJwt,
                expires_in = permissionRequirement.Expiration.TotalSeconds,
                token_type = "Bearer"
            };
            return responseJson;

        }
        public static dynamic IssueJWT(Claim[] claims )
        {
            DateTime UTC = DateTime.UtcNow;
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: AppSettingsConstVars.JwtConfigIssuer,
                audience: AppSettingsConstVars.JwtConfigAudience,
                claims: claims,
                expires: UTC.AddHours(12),
                signingCredentials: new Microsoft.IdentityModel.Tokens
                .SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("TurntableLottery's Secret Key")), SecurityAlgorithms.HmacSha256)
                );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);//生成最后的JWT字符串

            var responseJson = new
            {
                success = true,
                token = encodedJwt,
                token_type = "Bearer"
            };
            return responseJson;

        }
    }
}
