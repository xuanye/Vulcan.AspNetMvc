using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulcan.AspNetMvc.Test.MockObject
{
    public interface IHelloService
    {
        string Hello(string name);
       
    }

    public class HelloService : IHelloService
    {

        public string Hello(string name)
        {
            return string.Format("Hello,{0}!",name);
        }
    }
}
