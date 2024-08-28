using System.Globalization;

namespace CashFLow.Api.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;
    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {

        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        //pega do header qual é a cultura que o app está usando
        var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        //Instancia a cultura padrão
        var cultureInfo = new CultureInfo("en");

        if (string.IsNullOrWhiteSpace(requestedCulture) == false
            && supportedLanguages.Exists(language => language.Name.Equals(requestedCulture)))
        {
            //Se a cultura tiver sido informado, alterna a cultura anterior para a selecionada pelo usuário
            cultureInfo = new CultureInfo(requestedCulture);
        }


        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);
    }
}
