using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Web.Mvc;
using System.Linq;
using System.Web.Security;

namespace Pruefung_Praktisch_Musterloesung.Controllers
{
    public class Lab1Controller : Controller
    {
        /**
         * 
         * ANTWORTEN BITTE HIER
         * 
         * 1: Der Name der Datenbankdatei wird in der URL mitgegeben. Dies kann von einem Hacker unter Umständen benutzt werden, um eine falsche Datenbank anzubinden.
         * 1: Der Name des Bildes wird per URL mitgegeben.
         * 
         * 2: http://localhost:50374/Lab1/index?file="hacked_database"
         * 2: http://localhost:50374/Lab1/index?file="yourprivatedata.txt"
         * 
         * 3: Die URL kann dazu verwendet werden, eine Datenbankdatei anzusprechen. Dies kann dazu führen, dass ein unauthorisierter Benutzer plötzlich Zugriff auf die Daten hat.
         * 3: Die URL kann dazu verwendet werden, eine andere Datei anzusprechen.
         *
         * 4: 
         * 
         * */


        public ActionResult Index()
        {
            var type = Request.QueryString["type"];

            if (string.IsNullOrEmpty(type))
            {
                type = "lions";
            }

            var path = "~/Content/images/" + type;

            List<List<string>> fileUriList = new List<List<string>>();

            if (Directory.Exists(Server.MapPath(path)))
            {
                var scheme = Request.Url.Scheme;
                var host = Request.Url.Host;
                var port = Request.Url.Port;

                string[] fileEntries = Directory.GetFiles(Server.MapPath(path));
                foreach (var filepath in fileEntries)
                {
                    var filename = Path.GetFileName(filepath);
                    var imageuri = scheme + "://" + host + ":" + port + path.Replace("~", "") + "/" + filename;

                    var urilistelement = new List<string>();
                    urilistelement.Add(FormsAuthentication.HashPasswordForStoringInConfigFile(filename.Trim(), "md5")); // Added hash functionality
                    urilistelement.Add(imageuri);
                    urilistelement.Add(type);

                    fileUriList.Add(urilistelement);
                }
            }

            return View(fileUriList);
        }

        public ActionResult Detail()
        {
            // Don't do unsecure URL parameter thing
            //var file = Request.QueryString["file"];
            var filehash = Request.QueryString["hashed_file"]; // Hashed filename

            List<string> folder = new List<string>();

            foreach (var file1 in folder)
            {

                // Check, which file is equal the hash
                if (FormsAuthentication.HashPasswordForStoringInConfigFile(file1.Trim(), "md5") == filehash)
                {
                    var file = "";

                    var type = Request.QueryString["type"];

                    if (string.IsNullOrEmpty(file))
                    {
                        file = "Lion1.jpg";
                    }
                    if (string.IsNullOrEmpty(type))
                    {
                        file = "lions";
                    }

                    var relpath = "~/Content/images/" + type + "/" + file;

                    List<List<string>> fileUriItem = new List<List<string>>();
                    var path = Server.MapPath(relpath);

                    if (System.IO.File.Exists(path))
                    {
                        var scheme = Request.Url.Scheme;
                        var host = Request.Url.Host;
                        var port = Request.Url.Port;
                        var absolutepath = Request.Url.AbsolutePath;

                        var filename = Path.GetFileName(file);
                        var imageuri = scheme + "://" + host + ":" + port + "/Content/images/" + type + "/" + filename;

                        var urilistelement = new List<string>();
                        urilistelement.Add(filename);
                        urilistelement.Add(imageuri);
                        urilistelement.Add(type);

                        fileUriItem.Add(urilistelement);
                    }


                    return View(fileUriItem);
                }
            }

            return View();
        }

        // OLD

        public ActionResult IndexUnsecure()
        {
            var type = Request.QueryString["type"];

            if (string.IsNullOrEmpty(type))
            {
                type = "lions";
            }

            var path = "~/Content/images/" + type;

            List<List<string>> fileUriList = new List<List<string>>();

            if (Directory.Exists(Server.MapPath(path)))
            {
                var scheme = Request.Url.Scheme;
                var host = Request.Url.Host;
                var port = Request.Url.Port;

                string[] fileEntries = Directory.GetFiles(Server.MapPath(path));
                foreach (var filepath in fileEntries)
                {
                    var filename = Path.GetFileName(filepath);
                    var imageuri = scheme + "://" + host + ":" + port + path.Replace("~", "") + "/" + filename;

                    var urilistelement = new List<string>();
                    urilistelement.Add(filename);
                    urilistelement.Add(imageuri);
                    urilistelement.Add(type);

                    fileUriList.Add(urilistelement);
                }
            }

            return View(fileUriList);
        }

        public ActionResult DetailUnsecure()
        {
            var file = Request.QueryString["file"];
            var type = Request.QueryString["type"];

            if (string.IsNullOrEmpty(file))
            {
                file = "Lion1.jpg";
            }
            if (string.IsNullOrEmpty(type))
            {
                file = "lions";
            }

            var relpath = "~/Content/images/" + type + "/" + file;

            List<List<string>> fileUriItem = new List<List<string>>();
            var path = Server.MapPath(relpath);

            if (System.IO.File.Exists(path))
            {
                var scheme = Request.Url.Scheme;
                var host = Request.Url.Host;
                var port = Request.Url.Port;
                var absolutepath = Request.Url.AbsolutePath;

                var filename = Path.GetFileName(file);
                var imageuri = scheme + "://" + host + ":" + port + "/Content/images/" + type + "/" + filename;

                var urilistelement = new List<string>();
                urilistelement.Add(filename);
                urilistelement.Add(imageuri);
                urilistelement.Add(type);

                fileUriItem.Add(urilistelement);
            }

            return View(fileUriItem);
        }
    }
}