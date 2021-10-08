using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_App
{
    public interface ITimeService
    {
        string Time { get; }
    }
    public class SimpleTimeService : ITimeService
    {
        public SimpleTimeService()
        {
            Time = DateTime.Now.ToString("hh:mm:ss");
        }
        public string Time { get; }
    }
}
