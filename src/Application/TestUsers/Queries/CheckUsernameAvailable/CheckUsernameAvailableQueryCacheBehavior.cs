using ErrorOr;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.TestUsers.Queries.CheckUsernameAvailable;

public class CheckUsernameAvailableQueryCacheBehavior : IPipelineBehavior<CheckUsernameAvailableQuery, ErrorOr<bool>>
{
    private readonly IMemoryCache _memoryCache;
    private static readonly MemoryCacheEntryOptions _cacheEntryOptions = new MemoryCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromMinutes(1))
        .SetAbsoluteExpiration(TimeSpan.FromMinutes(60))
        .SetPriority(CacheItemPriority.Normal)
        .SetSize(1024);

    public CheckUsernameAvailableQueryCacheBehavior(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<ErrorOr<bool>> Handle(CheckUsernameAvailableQuery request, RequestHandlerDelegate<ErrorOr<bool>> next, CancellationToken cancellationToken)
    {
        if (_memoryCache.TryGetValue(request.Username, out bool isAvailable))
        {
            return isAvailable;
        }

        ErrorOr<bool> response = await next();

        _memoryCache.Set(request.Username, response.Value, _cacheEntryOptions);

        return response;
    }
}
