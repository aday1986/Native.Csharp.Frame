using System;
using System.Linq;
using Leo.Data;
using Leo.Native.Message;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Leo.Native.Tests
{
    [TestClass]
    public class GroupMessageServiceTests : TestBase
    {
        [TestMethod]
        public void Add()
        {
            var db = ServiceProvider.GetService<IDbProvider>().CreateConnection();
            Console.WriteLine(db.ConnectionString);
            var service =ServiceProvider.GetService<IGroupMessageService>();
            for (int i = 0; i < 1000; i++)
            {
                service.AddAsync(new GroupMessage());
            }
         
        }

        [TestMethod]
        public void GetTopMessageCounts()
        {
            var service = ServiceProvider.GetService<IGroupMessageService>();
            var results = service.GetTopMessageCounts(10, 1025447761, new DateTime(1900, 1, 1), DateTime.Now);
            Assert.IsTrue(results.Any());
            Console.WriteLine();
        }
    }
}
