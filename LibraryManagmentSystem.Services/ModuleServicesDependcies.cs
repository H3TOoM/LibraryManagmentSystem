using LibraryManagmentSystem.Services.Services;
using LibraryManagmentSystem.Services.Services.Base;
using Microsoft.Extensions.DependencyInjection;




namespace LibraryManagmentSystem.Services
{
    public static class ModuleServicesDependcies
    {

        public static IServiceCollection AddServicesDependcies( this IServiceCollection services )
        {
            services.AddAutoMapper( typeof( ModuleServicesDependcies ).Assembly );

            // Register Book Service
            services.AddTransient<IBookService, BookService>();

            return services;
        }

    }
}
