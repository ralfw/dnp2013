rem Aufrufen im lib\curl Verzeichnis

rem Lokaler Test
curl --data "hello, world!" --header " Content-Type: text/plain" localhost:63399/api/1/heckles

rem AppHarbor Test
curl --data "hello from appharbor!" --header " Content-Type: text/plain" heckleservice.apphb.com/api/1/heckles