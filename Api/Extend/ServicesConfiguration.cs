namespace DisneyApi.Extend
{
    public static class ServicesConfiguration
    {
        public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.WithOrigins("https://disneyfinder.azurewebsites.net/")
            .AllowAnyMethod()
            .AllowAnyHeader());
        });

    }
}
