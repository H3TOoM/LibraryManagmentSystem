using LibraryManagmentSystem.Infrasturcture.Repoistories;
using LibraryManagmentSystem.Infrasturcture.Repoistories.Base;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagmentSystem.Infrasturcture
{
    public static class ModuleInfrastuctreDependcies
    {
        public static IServiceCollection AddInfrastuctreDependcies( this IServiceCollection services )
        {

            // Register Main Repoistory
            services.AddTransient(typeof(IMainRepoistory<>), typeof(MainRepoistory<>));

            // Register Unit of Work
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
