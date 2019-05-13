using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leo.Native.Tests
{
   public abstract class TestBase
    {
      public   TestBase()
        {
             ServiceProvider = Leo.Native.Startup.ServiceProvider;
        }
        protected  IServiceProvider ServiceProvider { get; set; }
    }
}
