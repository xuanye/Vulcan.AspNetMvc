using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;

namespace Vulcan.AspNetMvc.DependencyInjection
{
    public static class ConfigureDependencies
    {
        private static IContainer _container;

        private static object _lockObject = new object();

        public static IContainer GetContainer()
        {
            if (_container == null)
            {
                throw new Exception("请先初始化容器");
            }          
            return _container;
        }
        private static void CreateContainer()
        {
            if (_container == null)
            {
                lock (_lockObject)
                {
                    if (_container == null)
                    {
                        _container = new Container();
                    }
                }
            }
        }
        public static void DisposeContainer()
        {
            if (_container != null)
            {
                _container.Dispose();
                _container = null;
            }
        }
        public static IContainer InitContainer(Registry registry)
        {
            CreateContainer();
          
            _container.Configure(
                x => {
                    x.AddRegistry(registry);
                }
            );

            return _container;
        }
    }
}
