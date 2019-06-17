using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace RemoteSettingsProvider.Controllers
{
    public class ContentFileProvider
    {
        private IHostingEnvironment HostingEnvironment { get; }
        private ILogger<ContentFileProvider> Logger { get; }

        public ContentFileProvider(IHostingEnvironment hostingEnvironment, ILogger<ContentFileProvider> logger)
        {
            HostingEnvironment = hostingEnvironment;
            Logger = logger;
            Logger.LogTrace($"HostingEnvironment.ContentRootPath = {hostingEnvironment.ContentRootPath}");
        }

        private static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);
            
            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var jr = new JsonSerializer();
                var searchResult = jr.Deserialize<T>(jtr);
                return searchResult;
            }
        }

        public T GetValue<T>(string relativePath)
        {
            using (var stream = File.OpenRead(Path.Combine(HostingEnvironment.ContentRootPath, relativePath)))
            {
                var result = DeserializeJsonFromStream<T>(stream);
                Logger.LogTrace(JsonConvert.SerializeObject(result));
                return result;
            }    
        }
    }
}