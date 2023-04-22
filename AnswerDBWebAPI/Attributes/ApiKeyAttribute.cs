using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AnswerDBWebAPI.Attributes
{ 
    
    //decorations that define properties in the class to use
    [AttributeUsage(validOn: AttributeTargets.All)]

    public sealed class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        //escribimos la funcionalidad para la decoracion del "ApiKey"
        //que luego usaremos en los controladores para limitar y dar seguridad 
        //la forma de consumidores un end point.


        private readonly string _apiKey = "ApiKeyFinalExam";

        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            //Validarsi en el header del request va la informacion del apikey
            if (!context.HttpContext.Request.Headers.TryGetValue(_apiKey, out var ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key no proporcionada!"
                };
                return;
            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var ApikeyValue = appSettings.GetValue<string>(_apiKey);
            if (!ApikeyValue.Equals(ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "La llave de seguridad suministrada no es correcta."
                };
                return;
            }

            await next();
        }
    }
}