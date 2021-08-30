using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using TurntableLottery.Utils.Check;
using TurntableLottery.Utils.Extensions;

namespace TurntableLottery.NetCore
{
    public static class ServiceExtension
    {
        public static void RegisterAssembly(IServiceCollection services, string assemblyName, ServiceLifetime injection = ServiceLifetime.Scoped)
        {
            CheckNull.ArgumentIsNullException(services, nameof(services));
            CheckNull.ArgumentIsNullException(assemblyName, nameof(assemblyName));
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
            if (assembly.IsNullT())
            {
                throw new DllNotFoundException($"\"{assemblyName}\".dll不存在");
            }
            var types = assembly.GetTypes().Where(o =>
            (typeof(IDependency).IsAssignableFrom(o)) && !o.IsInterface).ToList();
            //&& !o.Name.Contains("Base")
            foreach (var type in types)
            {
                var faces = type.GetInterfaces().Where(o => o.Name != "IDependency" && !o.Name.Contains("Base")).ToArray();
                if (faces.Any())
                {
                    var interfaceType = faces.FirstOrDefault();
                    switch (injection)
                    {
                        case ServiceLifetime.Scoped:
                            services.AddScoped(interfaceType, type);
                            break;

                        case ServiceLifetime.Singleton:
                            services.AddSingleton(interfaceType, type);
                            break;

                        case ServiceLifetime.Transient:
                            services.AddTransient(interfaceType, type);
                            break;
                    }
                }
            }
        }
    }
}
