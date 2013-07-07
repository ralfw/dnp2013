using System;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace HeckleService
{
    public class Pushover
    {
        private const string PUSHOVER_REST_API_URL = "https://api.pushover.net/1/messages.xml";


        private readonly string _apptoken;
        private readonly string _userkey;

        public Pushover(string apptoken, string userkey)
        {
            _apptoken = apptoken;
            _userkey = userkey;
        }


        public void Send(string title, string message, string url)
        {
            var payload = string.Format("token={0}&user={1}&title={2}&message={3}&url={4}",
                                        _apptoken, _userkey,
                                        HttpUtility.UrlEncode(title),
                                        HttpUtility.UrlEncode(message),
                                        HttpUtility.UrlEncode(url));
            var payloadBytes = Encoding.ASCII.GetBytes(payload);

            var wr = WebRequest.Create(PUSHOVER_REST_API_URL);
            wr.Method = "POST";
            wr.ContentType = "application/x-www-form-urlencoded";
            wr.ContentLength = payloadBytes.Length;

            wr.GetRequestStream().Write(payloadBytes, 0, payloadBytes.Length);

            using (var sResp = wr.GetResponse().GetResponseStream())
            {
                /* Response message format:
                        <?xml version="1.0" encoding="UTF-8"?>
                        <hash>
                            <status type="integer">1</status>
                            <request>5e21dc90ac3c4013e07114b48be78cb7</request>
                        </hash>
                */
                var xmlResp = new XmlDocument();
                xmlResp.Load(sResp);

                if (xmlResp.SelectSingleNode("/hash/status").InnerText != "1")
                    throw new InvalidOperationException(string.Format("Sending Pushover notification failed with status: {0}",
                                                                        xmlResp.SelectSingleNode("/hash/status").InnerText));
            }
        }
    }
}