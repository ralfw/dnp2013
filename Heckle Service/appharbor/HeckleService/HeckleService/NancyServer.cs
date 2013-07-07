using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script;
using System.Web.Script.Serialization;
using Nancy;
using Nancy.ModelBinding;

namespace HeckleService
{
    public class NancyServer : NancyModule
    {
        JavaScriptSerializer _json = new JavaScriptSerializer();

        public NancyServer()
        {
            Get["/"] = x => {
                dynamic model = new ExpandoObject();
                model.Name = "";
                model.Email = "";
                model.IstQuitting = false;
                return View["heckle.html", model];
            };

            Post["/heckled"] = x => {
                dynamic model = new ExpandoObject();
                model.IstQuitting = true;
                model.Name = Request.Form.name;
                model.Email = Request.Form.email;
                model.Zwischenruf = Request.Form.text + ", " + Request.Form.name + " (" + Request.Form.email + ")";

                var po = new Pushover(Credentials.Get("heckle_pushover_apptoken"), Credentials.Get("heckle_pushover_userkey"));
                po.Send(model.Zwischenruf);

                return View["heckle.html", model];
            };

            Post["/api/1/heckles"] = x => {
                try
                {
                    using (var sr = new StreamReader(Request.Body))
                    {
                            var zwischenruf = sr.ReadToEnd();

                            var po = new Pushover(Credentials.Get("heckle_pushover_apptoken"), Credentials.Get("heckle_pushover_userkey"));
                            po.Send(zwischenruf);

                            return HttpStatusCode.OK;
                    }
                }
                catch (Exception ex)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        ContentType = "text/plain",
                        Contents = stream => (new StreamWriter(stream) { AutoFlush = true }).Write("Zwischenruf konnte nicht abgesetzt werden!\n" + ex.ToString())
                    };
                }
            };
        }
    }
}