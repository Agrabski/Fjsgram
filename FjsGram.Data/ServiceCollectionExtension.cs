using FjsGram.Data.Passwords;
using Microsoft.Extensions.DependencyInjection;

namespace FjsGram.Data;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFjsGramData(
        this IServiceCollection services
    ) =>
        services
        .AddScoped<IPasswordManager, ArgonPasswordManager>()
        .AddOptions<ArgonOptions>()
        .Services
    ;
}
