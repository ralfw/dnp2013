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
using Nancy.Responses.Negotiation;

namespace HeckleService
{
    public class NancyServer : NancyModule
    {
        public NancyServer()
        {
            Post["/api/1/heckles"] = x =>
            {
                try
                {
                    var nachricht = Nachricht_aus_Request_extrahieren(Request);
                    Versenden(nachricht);
                    return HttpStatusCode.OK;
                }
                catch (Exception ex)
                {
                    return this.AsError(ex);
                }
            };


            Get["/"] = x => Zwischenruf_Formular_bauen("");


            Post["/"] = x => {
                var nachricht = Nachricht_aus_Formular_extrahieren(Request);
                Versenden(nachricht);
                return Zwischenruf_Formular_bauen(nachricht);
            };
        }


        private static void Versenden(string nachricht)
        {
            var credentials = Konfiguration.Mehrere_laden("heckle_pushover_apptoken", "heckle_pushover_userkey");
            new Pushover(credentials["heckle_pushover_apptoken"], credentials["heckle_pushover_userkey"])
                .Send(nachricht);
        }


        private Negotiator Zwischenruf_Formular_bauen(string nachricht)
        {
            dynamic viewModel = new ExpandoObject();
            viewModel.IstQuitting = !string.IsNullOrEmpty(nachricht);
            viewModel.Zwischenruf = nachricht;
            viewModel.Name = viewModel.IstQuitting ? Request.Form.Name : "";
            viewModel.Email = viewModel.IstQuitting ? Request.Form.Email : "";
            return View["heckle.html", viewModel];
        }


        private string Nachricht_aus_Request_extrahieren(Request request)
        {
            using (var sr = new StreamReader(Request.Body))
            {
                return sr.ReadToEnd();
            }
        }

        
        private string Nachricht_aus_Formular_extrahieren(Request request)
        {
            return Request.Form.text + ", " + Request.Form.name + " (" + Request.Form.email + ")";
        }
    }
}