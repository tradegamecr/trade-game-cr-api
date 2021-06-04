using HotChocolate;
using HotChocolate.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Helpers;
using TradeGameCRAPI.Interfaces;
using TradeGameCRAPI.Models;
using TradeGameCRAPI.Models.GraphQLInputs;

namespace TradeGameCRAPI.Resolvers
{
    public static class SignInResolver
    {
        [ExtendObjectType(Constants.GraphQLOperationTypes.Mutation)]
        public class SignInMutation
        {
            public async Task<UserToken> Login
                ([Service] IConfiguration configuration,
                [Service] UserManager<User> userManager,
                [Service] IFacebookAuthService facebookAuthService,
                FacebookAuthResponseInput facebookAuthResponseInput)
            {
                // Validate the access token.
                var validationResult = await facebookAuthService.ValidateAccessTokenAsync(facebookAuthResponseInput.AccessToken);

                if (validationResult.Data.IsValid == false)
                {
                    throw QueryExceptionBuilder.Custom("Access token invalid", Constants.GraphQLExceptionCodes.BadRequest);
                }

                // Get the user information
                var userInfoResult = await facebookAuthService.GetUserInfoAsync(facebookAuthResponseInput.AccessToken);

                if (userInfoResult == null || string.IsNullOrEmpty(userInfoResult.Email))
                {
                    throw QueryExceptionBuilder.Custom("Invalid user info from Facebook", Constants.GraphQLExceptionCodes.BadRequest);
                }

                // Check the user existence
                var user = await userManager.FindByEmailAsync(userInfoResult.Email);

                if (user == null)
                {
                    // Create the new user if not exist
                    var newUser = new User()
                    {
                        Name = userInfoResult.FirstName,
                        LastName = userInfoResult.LastName,
                        Email = userInfoResult.Email,
                        UserName = userInfoResult.Email
                    };
                    var createResult = await userManager.CreateAsync(newUser);

                    if (createResult.Errors.Any())
                    {
                        var error = createResult.Errors.First().Description;

                        throw QueryExceptionBuilder.Custom(error, Constants.GraphQLExceptionCodes.BadRequest);
                    }
                }

                var jwtKey = configuration["JWT:Key"];
                var userToken = BuildToken(facebookAuthResponseInput, userInfoResult, jwtKey);

                return userToken;
            }

            private UserToken BuildToken
                (FacebookAuthResponseInput facebookAuthResponseInput,
                FacebookUserInfoResult userInfoResult,
                string jwtKey)
            {
                var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, userInfoResult.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiration = UnixToDateTime(facebookAuthResponseInput.DataAccessExpirationTime);
                var token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expiration,
                    signingCredentials: creds);
                var userToken = new UserToken()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expiration
                };

                return userToken;
            }

            private DateTime UnixToDateTime(long value)
            {
                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                
                date = date.AddSeconds(value).ToLocalTime();

                return date;
            }
        }
    }
}
