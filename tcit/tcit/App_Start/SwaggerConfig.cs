using System.Web.Http;
using WebActivatorEx;
using tcit;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace tcit
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {

                        c.SingleApiVersion("v1", "tcit")
                         .Description("descripción de API")
                         .TermsOfService("Términos de servicio");


                    })
                .EnableSwaggerUi(c =>
                    {

                    });
        }
    }
}
