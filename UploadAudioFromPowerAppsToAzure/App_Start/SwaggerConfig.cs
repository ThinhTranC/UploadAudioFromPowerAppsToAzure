using System.Web.Http;
using WebActivatorEx;
using UploadAudioFromPowerAppsToAzure;
using Swashbuckle.Application;
using UploadAudioFromPowerAppsToAzure.Controllers;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace UploadAudioFromPowerAppsToAzure
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        
                        c.SingleApiVersion("v1", "UploadAudioFromPowerAppsToAzure");
                        c.OperationFilter<AddFileParamTypes>();


                    })
                .EnableSwaggerUi(c =>
                    {
                        
                    });
        }
    }
}
