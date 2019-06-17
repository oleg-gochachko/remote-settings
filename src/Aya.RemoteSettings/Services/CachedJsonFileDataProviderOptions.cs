using System;

namespace RemoteSettingsProvider.Controllers
{
    public class CachedJsonFileDataProviderOptions<T>
    {
        public CachedJsonFileDataProviderOptions()
        {
            CacheTime = TimeSpan.MaxValue;
        }

        public TimeSpan CacheTime { get; set; }

        public string FileName { get; set; }
    }

    //public class ClientStorageProvider
    //{
    //    private IMemoryCache MemoryCache { get; }

    //    private ContentFileProvider FileProvider { get; }

    //    public ClientStorageProvider(ContentFileProvider fileProvider, IMemoryCache memoryCache)
    //    {
    //        MemoryCache = memoryCache;
    //        FileProvider = fileProvider;
    //        CacheTime = TimeSpan.FromMinutes(5);
    //    }

    //    private ClientStorageModel _value;
    //    private DateTime LastTimeRefreshed { get; set; }
    //    public ClientStorageModel Value
    //    {
    //        get
    //        {                

    //            const string fileName = "client-storage.json";
    //            //return FileProvider.GetValue<ClientStorageFile>(fileName).ClientStorageModel;
    //            var result = MemoryCache.GetOrCreate(fileName, entry =>
    //            {
    //                entry.AbsoluteExpiration = DateTimeOffset.Now.Add(CacheTime);
    //                return FileProvider.GetValue<ClientStorageModel>(fileName);
    //            });
    //            return result;
    //        }
    //    }

    //    public TimeSpan CacheTime { get; set; }
    //}
}