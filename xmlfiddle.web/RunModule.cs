using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.ModelBinding;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.Xml;
using System.IO;

namespace xmlfiddle.web
{
    public class RunModule : NancyModule
    {
        public RunModule()
        {

            Post["/"] = _ => {
                var request = this.Bind<RunRequest>();

                var xDoc = XDocument.Parse(request.Xml);
                var transformedDoc = new XDocument();
                using (var writer = new StringWriter())
                {
                    var transform = new XslCompiledTransform();
                    transform.Load(new XmlTextReader(request.Xslt, XmlNodeType.Document, null));
                    transform.Transform(xDoc.CreateReader(), null, writer);
                    return Response.AsJson<RunResponse>(new RunResponse(writer.ToString()));
                }

            };
        }
    }
}