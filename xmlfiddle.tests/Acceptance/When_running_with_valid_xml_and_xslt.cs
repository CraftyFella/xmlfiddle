using Machine.Specifications;
using Nancy;
using Nancy.Hosting.Self;
using Nancy.Json;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using xmlfiddle.web;

namespace xmlfiddle.tests.Acceptance
{
//    [Subject("Running")]
//    public class When_running_with_valid_xml_and_xslt
//    {
//        Establish context = () =>
//        {
//            // Given
//            var bootstrapper = new DefaultNancyBootstrapper();
//            browser = new Browser(bootstrapper);            
//        };

//        Because of = () => {
//            var xml = @"
//<animals>
//  <crazy-mad-badger>
//    <name>Steve</name>
//    <age>3</age>
//  </crazy-mad-badger>
//</animals>";

//            var xslt = @"
//<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
//<xsl:template match='/'>
//  <html/>
//</xsl:template>
//</xsl:stylesheet>";

//            browser.Post("/", with => {
//                with.JsonBody<RunRequest>(new RunRequest(xml, xslt));
//                });
//        };

//        static Browser browser;
//    }

    [TestFixture]
    public class When_running_with_valid_xml_and_xslt
    {
        private BrowserResponse result;
        private string xml;

        [TestFixtureSetUp]
        public void Context()
        {
            // Given
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper);

            xml = @"
<animals>
  <crazy-mad-badger>
    <name>Steve</name>
    <age>3</age>
  </crazy-mad-badger>
</animals>";

            var xslt = @"
<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
<xsl:template match='/'>
  <html/>
</xsl:template>
</xsl:stylesheet>";

            result = browser.Post("/", with =>
            {
                with.JsonBody<RunRequest>(new RunRequest(xml, xslt));
            });

        }

        [Test]
        public void Then_transform_is_returned()
        {
            var body = result.BodyAsJson<RunResponse>();

            body.Transform.Should().Be("<html></html>");
        }

        [Test]
        public void Then_200_is_returned()
        {
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }

    public static class BrowserResponseExtensions
    {
        public static TModel BodyAsJson<TModel>(this BrowserResponse response)
        {
            using (var contentsStream = new MemoryStream())
            {
                response.Context.Response.Contents.Invoke(contentsStream);
                contentsStream.Position = 0;
                var serializer = new JavaScriptSerializer();
                return serializer.Deserialize<TModel>(new StreamReader(contentsStream).ReadToEnd());
            }

            

        }
    }
}
