﻿using ProjetoEditoraApi.Models;
using System.Security.Claims;

namespace ProjetoEditoraApi.Extensions
{
    public static class RoleClaimsExtension
    {
        public static IEnumerable<Claim> GetClaims(this Usuario user)
        {
            var result = new List<Claim>
            {
                new ( ClaimTypes.Name, user.Email)
            };

            result.AddRange(
                user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug))
            );
            return result;
        }
    }
}
