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
            var service =ServiceProvider.GetService<IGroupMessageService>();
            Assert.IsTrue(service.Add(new GroupMessage()));
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
