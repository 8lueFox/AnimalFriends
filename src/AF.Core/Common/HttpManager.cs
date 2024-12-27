using Microsoft.AspNetCore.Http;

namespace AF.Core.Common;

public static class HttpManager
{
    private static IHttpContextAccessor _httpContextAccessor;

    public static HttpContext Current => _httpContextAccessor.HttpContext;

    public static void Configure(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
}
