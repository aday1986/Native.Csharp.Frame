using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Leo.Native;
using Leo.Native.Message;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = Leo.Native.Startup.ServiceProvider.GetService<IGroupMessageService>();
            Random random = new Random();
            while (true)
            {
                Console.WriteLine("输入命令：1.add");
                switch (Console.ReadLine())
                {
                    case "add":
                        for (int t = 0; t < 1000; t++)
                        {
                           Task.Run(() =>
                            {
                                for (int i = 0; i < 1000; i++)
                                {
                                    service.AddAsync(new GroupMessage()
                                    {
                                        FromGroup = random.Next(),
                                        FromQQ = random.Next(),
                                        Msg = Guid.NewGuid().ToString(),
                                        MsgDate = DateTime.Now
                                    });
                                }
                            });
                        }


                        break;
                    default:
                        break;
                }


            }


        }
    }
}
