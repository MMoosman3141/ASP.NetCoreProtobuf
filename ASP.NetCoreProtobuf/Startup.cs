using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NetCoreProtobuf.Formatters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASP.NetCoreProtobuf {
  public class Startup {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      services.AddMvc(options => {
        options.InputFormatters.RemoveType(typeof(JsonOutputFormatter));
        options.OutputFormatters.RemoveType(typeof(JsonInputFormatter));

        options.InputFormatters.Insert(0, new ProtobufInputFormatter());
        options.InputFormatters.Insert(0, new ProtobufJsonInputFormatter());

        options.OutputFormatters.Insert(0, new ProtobufOutputFormatter());
        options.OutputFormatters.Insert(0, new ProtobufJsonOutputFormatter());
      }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      } else {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}
