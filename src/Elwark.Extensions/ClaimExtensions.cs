using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Elwark.Extensions
{
    public static class ClaimExtensions
    {
        public static bool TryGetByte(this IEnumerable<Claim> claims, string name, out byte result) =>
            byte.TryParse(claims.GetClaimValueOrDefault(name), out result);
        
        public static bool TryGetShort(this IEnumerable<Claim> claims, string name, out short result) =>
            short.TryParse(claims.GetClaimValueOrDefault(name), out result);
        
        public static bool TryGetInt(this IEnumerable<Claim> claims, string name, out int result) =>
            int.TryParse(claims.GetClaimValueOrDefault(name), out result);

        public static bool TryGetLong(this IEnumerable<Claim> claims, string name, out long result) =>
            long.TryParse(claims.GetClaimValueOrDefault(name), out result);

        public static Claim? GetClaimOrDefault(this IEnumerable<Claim> claims, string value) =>
            GetClaims(claims, value)?.FirstOrDefault();

        public static string? GetClaimValueOrDefault(this IEnumerable<Claim> claims, string value) =>
            GetClaimOrDefault(claims, value)?.Value;

        public static IEnumerable<Claim> GetClaims(this IEnumerable<Claim> claims, string value) =>
            claims.Where(x => x.Type == value);
    }
}