using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services.MyLogger;
using Services.MyMemoryCache;
using Services.WebServices;
using Services.WebServices.WeChat;
using Services.WeChatBackend.AutoReply;
using WeChatPlugin.Settings;

namespace WeChatPlugin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddXmlSerializerFormatters();
            services.AddMemoryCache();
            


            // DI
            services.AddSingleton<ICacheControl, CacheControl>();
            services.AddSingleton<IMyMemoryCache, MyMemoryCache>();

            services.AddSingleton<IMyLogger>(x => new FileLogger("f"));
            services.AddSingleton<IWeChatAPI>(x => new WeChatAPI(
                Configuration.GetSection("WeChatOfficialAccount:appid").Value, 
                Configuration.GetSection("WeChatOfficialAccount:appsecret").Value)
            );

            services.AddSingleton<IAutoReply, AutoReply>();

            
            // Settings
            services.Configure<WeChatSettings>(Configuration.GetSection("WeChatOfficialAccount"));
            services.Configure<ReplyMsgPathSettings>(Configuration.GetSection("AppConfig:AutoReplyLibJsonPath"));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
