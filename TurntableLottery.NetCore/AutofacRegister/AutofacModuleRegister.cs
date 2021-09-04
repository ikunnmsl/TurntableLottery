using Autofac;
using Autofac.Extras.DynamicProxy;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TurntableLottery.NetCore.AutofacRegister
{
    public class AutofacModuleRegister : Autofac.Module
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AutofacModuleRegister));
        protected override void Load(ContainerBuilder builder) 
        {
            var basePath = AppContext.BaseDirectory;

            var serviceDllFile = Path.Combine(basePath, "TurntableLottery.Service.dll");

            if (!File.Exists(serviceDllFile))
            {
                var msg = "service.dll 丢失，因为项目解耦了，所以需要先F6编译，再F5运行，请检查 bin 文件夹，并拷贝。";
                log.Error(msg);
                throw new Exception(msg);
            }
            var cacheType = new List<Type>();
            // 获取 Service.dll 程序集服务，并注册
            var assemblysServices = Assembly.LoadFrom(serviceDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerDependency()
                      .PropertiesAutowired()
                      .EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy;
                      .InterceptedBy(cacheType.ToArray());//允许将拦截器服务的列表分配给注册。

            // 获取 Repository.dll 程序集服务，并注册
            //var assemblysRepository = Assembly.LoadFrom(serviceDllFile);
            //builder.RegisterAssemblyTypes(assemblysServices)
            //       .AsImplementedInterfaces()
            //       .PropertiesAutowired()
            //       .InstancePerDependency();
        }
    }
}
