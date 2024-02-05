using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Dtos;

namespace SharedLibrary.Extensions
{
    public static class CustomValidatonResponse
    {
        public static void UseCustomValidatonResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.InvalidModelStateResponseFactory = context =>
                {

                    //ıenumerable içinden sadece errorları al selectmany ile yapmak istediğimiz
                    var errors = context.ModelState.Values.Where(x => x.Errors.Count > 0).SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                    ErrorDto errorDto = new ErrorDto(errors.ToList(), true);

                    var response = Response<NoContentResult>.Fail(errorDto, 400);

                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
