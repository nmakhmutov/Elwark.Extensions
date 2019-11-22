using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace Elwark.Extensions
{
    public static class ClaimExtensions
    {
        [DebuggerStepThrough]
        public static bool TryGetUserId(this ClaimsPrincipal claimsPrincipal, out long userId) =>
            claimsPrincipal.Claims.TryGetUserId(out userId);

        [DebuggerStepThrough]
        public static bool TryGetUserId(this IEnumerable<Claim> claims, out long userId)
        {
            var subjectId = claims.GetClaimValueOrDefault("sub") ?? string.Empty;
            
            return long.TryParse(subjectId, out userId);
        }

        [DebuggerStepThrough]
        public static bool TryGetIdentityId(this ClaimsPrincipal claimsPrincipal, out Guid identityId) =>
            claimsPrincipal.Claims.TryGetIdentityId(out identityId);
        
        [DebuggerStepThrough]
        public static bool TryGetIdentityId(this IEnumerable<Claim> claims, out Guid identityId)
        {
            var identity = claims.GetClaimValueOrDefault("identity") ?? string.Empty;

            return Guid.TryParse(identity, out identityId);
        }

        [DebuggerStepThrough]
        public static string GetClientId(this IEnumerable<Claim> claims) =>
            claims.GetClaimValueOrDefault("client_id");

        [DebuggerStepThrough]
        public static Claim GetClaimOrDefault(this IEnumerable<Claim> claims, string value) =>
            GetClaims(claims, value)?.FirstOrDefault();

        [DebuggerStepThrough]
        public static string GetClaimValueOrDefault(this IEnumerable<Claim> claims, string value) =>
            GetClaimOrDefault(claims, value)?.Value;

        [DebuggerStepThrough]
        public static IEnumerable<Claim> GetClaims(this IEnumerable<Claim> claims, string value) =>
            claims.Where(x => x.Type == value);

    }
}