using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RemoteSettingsProvider.Controllers;

namespace RemoteSettingsProvider
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {

        }

        public class Test
        {
            [JsonProperty("date")]
            [JsonConverter(typeof(CustomDateTimeConverter))]
            public DateTime Date { get; set; }
        }
        
        public class CustomDateTimeConverter : JsonConverter<DateTime>
        {
            private const string customFormat = "yyyy-MM-ddTHH:mm:ss";

            public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
            {
                writer.WriteValue(value.ToString(customFormat));
            }            

            public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue,
                JsonSerializer serializer)
            {   
                if (reader.TokenType == JsonToken.Null)
                    return existingValue;

                var token = JToken.Load(reader);
                string value = (string)token;
                
                if (DateTime.TryParseExact(value, customFormat, CultureInfo.InvariantCulture, 
                    DateTimeStyles.None,
                    out DateTime dt))
                {
                    return dt;                    
                }

                return existingValue;
                //throw new JsonSerializationException($"Can't serialize {reader.Value} to DateTime");
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCommandHandler<ClientHasAccessCommand, ClientHasAccessCommandResult, ClientHasAccessCommandHandler>();
            services.AddCommandHandler<SettingGetByNameCommand, SettingGetByNameCommandResult, SettingGetByNameCommandHandler>();
            services.AddCommandHandler<SettingGetAllCollectionCommand, SettingGetAllCollectionCommandResult, SettingGetAllCollectionCommandHandler>();
            services.AddCommandHandler<ClientHasAccessCommand, ClientHasAccessCommandResult, ClientHasAccessCommandHandler>();
                        
            services.AddSingleton<ContentFileProvider>();
            services.AddSingleton<IClientProvider, ClientProvider>();
            services.AddSingleton<ISettingProvider, SettingProvider>();            

            services.AddSingleton<CachedJsonFileDataProvider<ClientStorageModel>>();
            services.Configure<CachedJsonFileDataProviderOptions<ClientStorageModel>>(opt =>
            {
                opt.CacheTime = TimeSpan.FromMinutes(5);
                opt.FileName = "client-storage.json";
            });

            services.AddSingleton<CachedJsonFileDataProvider<SettingStorageModel>>();
            services.Configure<CachedJsonFileDataProviderOptions<SettingStorageModel>>(opt =>
            {
                opt.CacheTime = TimeSpan.FromMinutes(5);
                opt.FileName = "setting-storage.json";
            });

            services.AddMemoryCache();

            //services.Configure<ClientStorageModel>(options => Configuration.GetSection("client-storage").Bind(options));

            //var cs = new ClientStorageModel();
            //Configuration.GetSection("client-storage").Bind(cs);
            //var cs = Configuration.GetValue<ClientStorageModel>("client-storage");            
            //Debug.WriteLine(csv);
            
            //services.Configure<SettingStorageModel>(options => Configuration.GetSection("setting-storage").Bind(options));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();            
        }
    }

    //public class JsonNetConfigurationSource : FileConfigurationSource
    //{
    //    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    //    {
    //        FileProvider = FileProvider ?? builder.GetFileProvider();
    //        return new JsonNetConfigurationProvider(this);
    //    }
    //}

    //public class JsonNetConfigurationProvider : FileConfigurationProvider
    //{
    //    public JsonNetConfigurationProvider(JsonNetConfigurationSource source) : base(source) { }

    //    public override void Load(Stream stream)
    //    {
    //        var parser = new JsonNetConfigurationFileParser();

    //        Data = parser.Parse(stream);
    //    }
    //}

    //public class JsonNetConfigurationFileParser
    //{
    //    public IDictionary<string, string> Parse(IStream stream)
    //    {
    //        JsonConverter<>
    //    }
    //}
}
