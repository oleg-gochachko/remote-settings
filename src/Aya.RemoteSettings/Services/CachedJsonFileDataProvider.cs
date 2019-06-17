using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace RemoteSettingsProvider.Controllers
{
    public class CachedJsonFileDataProvider<T>
    {
        private IMemoryCache MemoryCache { get; }

        private IOptions<CachedJsonFileDataProviderOptions<T>> Options { get; }

        private ContentFileProvider FileProvider { get; }

        public CachedJsonFileDataProvider(ContentFileProvider fileProvider, IMemoryCache memoryCache, IOptions<CachedJsonFileDataProviderOptions<T>> options)
        {
            MemoryCache = memoryCache;
            Options = options;
            FileProvider = fileProvider;
        }
       
        public async Task<T> GetValueAsync()
        {
            var result = await MemoryCache.GetOrCreateAsync(Options.Value.FileName, entry =>
            {
                entry.AbsoluteExpiration = DateTimeOffset.Now.Add(Options.Value.CacheTime);                
                return ReadFileJsonAsync();
            });
            return result;
        }

        private Task<T> ReadFileJsonAsync()
        {
            var result = FileProvider.GetValue<T>(Options.Value.FileName);
            return Task.FromResult(result);            
        }
    }
}