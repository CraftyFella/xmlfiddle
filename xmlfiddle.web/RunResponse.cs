using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xmlfiddle.web
{
    public class RunResponse
    {
        protected RunResponse()
        {

        }

        public RunResponse(string transform)
        {
            this.Transform = transform;
        }
        public string Transform { get; set; }
    }
}
