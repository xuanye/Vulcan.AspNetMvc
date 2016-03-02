using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using Vulcan.AspNetMvc.DefaultImpl;
using Vulcan.AspNetMvc.Interfaces;

namespace Vulcan.AspNetMvc
{
    public class DefaultRegistry: Registry
    {
        public DefaultRegistry()
        {
            For<IJsonSerialize>().Use<DefaultJsonSerialize>().Singleton();
        }
    }
}
