using Microsoft.AspNetCore.Hosting;

namespace Inventario.API.Extensions
{
    public static class EnvironmentExtensions
    {
        public static bool IsTesting(this IWebHostEnvironment webHostEnvironment)
        {
            return webHostEnvironment.EnvironmentName.ToLower().Equals("testing");
        }

        public static bool IsIntegration(this IWebHostEnvironment webHostEnvironment)
        {
            return webHostEnvironment.EnvironmentName.ToLower().Equals("integration");
        }
    }
}