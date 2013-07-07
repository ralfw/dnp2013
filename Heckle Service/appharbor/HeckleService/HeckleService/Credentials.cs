using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace HeckleService
{
    public static class Credentials
    {
        public static string Get(string key)
        {
            /*
                Idee für die Speicherung der Credentials im Environment aus:
                http://codermike.com/sensitive-configuration-and-appharbor
                  
                Setzen der Environmentvariablen mit einem Batch z.B. so:
                 
                setx heckle_pushover_apptoken "ChAo..." /M
                setx heckle_pushover_userkey "2xvM..." /M
                 
                Ausführen mit Admin-Rechten. Anschließend Visual Studio neu starten. 
                 
                In der web.config stehen Einträge mit den selben Namen. Solange dort "{ENV}" als Wert eingetragen ist,
                werden die Werte aber aus dem Environment geladen.
                 
                    <appSettings>
                        <add key="heckle_pushover_apptoken" value="{ENV}"/>
                        <add key="heckle_pushover_userkey" value="{ENV}"/>
                    </appSettings>
                  
                Batch kann in ein Verzeichnis "unversioned" gelegt werden, das in .gitignore ausgeschlossen wird.
            */
            var value = ConfigurationManager.AppSettings[key];
            if (value.ToLower() == "{env}")
                value = Environment.GetEnvironmentVariable(key);
            return value;
        }
    }
}