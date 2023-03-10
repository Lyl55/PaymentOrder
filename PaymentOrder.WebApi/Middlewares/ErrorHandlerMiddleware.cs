using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace PaymentOrder.WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DivideByZeroException ex)
            {
                // Do something
            }
            catch (IndexOutOfRangeException ex)
            {
                // Do something
            }
            catch (Exception ex)
            {

            }
        }
    }
}