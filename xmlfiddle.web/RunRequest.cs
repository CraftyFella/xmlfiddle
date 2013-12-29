using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xmlfiddle.web
{
    public class RunRequest
    {
        public  RunRequest()
        {

        }

        public string Xml { get; set; }
        public string Xslt { get; set; }


        public RunRequest(string xml, string xslt)
        {
            this.Xml = xml;
            this.Xslt = xslt;
        }
    }
}
