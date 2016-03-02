using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulcan.AspNetMvc.Test.MockObject
{
    public interface ISampleService
    {
        string SayHi();
    }
    public class SampleService : ISampleService
    {
        private string _id;
        public SampleService()
        {
            this._id = Guid.NewGuid().ToString("N");
        }

        public string SayHi()
        {
            return string.Format("Hello,My id is {0}",this._id);
        }
    }
}
