using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace HeckleService
{
    public class NancyServer : NancyModule
    {
        public NancyServer()
        {
            Get["/"] = x => "hello, world - " + DateTime.Now.ToString();
        }
    }
}