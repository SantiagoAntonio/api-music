using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace api_music.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertParamsPage<T>(this HttpContext httpContext, IQueryable<T> queryable, int cantRegisterPorPage)
        {
            double cant = await queryable.CountAsync();
            double cantPage = Math.Ceiling(cant / (double)cantRegisterPorPage);
            httpContext.Response.Headers.Add("cantidadPaginas",cantPage.ToString());
        }
    }
}
