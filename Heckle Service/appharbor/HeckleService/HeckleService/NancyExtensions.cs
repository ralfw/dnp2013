using System;
using System.IO;
using Nancy;

namespace HeckleService
{
    static class NancyExtensions
    {
        public static Response AsError(this NancyModule module, Exception ex)
        {
            return new Response
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ContentType = "text/plain",
                    Contents = stream => (new StreamWriter(stream) { AutoFlush = true }).Write("Zwischenruf konnte nicht abgesetzt werden!\n" + ex.ToString())
                };
        }
    }
}