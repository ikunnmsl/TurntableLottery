using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TurntableLottery.Token.Model;

namespace TurntableLottery.Token
{
    public class TurnTableLotteryToken
    {
        public TurnTableLotteryToken()
        { 
        }

        public static dynamic IssueJWT(Claim[] claims, PermissionRequirement permissionRequirement)
        {
            DateTime UTC = DateTime.UtcNow;
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: permissionRequirement.Issuer,
                audience: permissionRequirement.Audience,
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
                expires_in = permissionRequirement.Expiration.TotalSeconds,
                token_type = "Bearer"
            };
            return responseJson;


        }
    }
}
