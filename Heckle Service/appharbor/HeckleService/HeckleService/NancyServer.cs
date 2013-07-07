using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Nancy;

namespace HeckleService
{
    public class NancyServer : NancyModule
    {
        public NancyServer()
        {
            Get["/"] = x => "heckle: " + Credentials.Get("heckle_pushover_apptoken");

        }
    }
}