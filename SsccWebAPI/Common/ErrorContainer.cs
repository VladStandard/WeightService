using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SsccWebAPI.Common
{
    public class ErrorContainer
    {

        private List<string> ErrorList = new List<string>();

        public void Add (string error)
        {
            ErrorList.Add(error);
        }

        public void Clear()
        {
            ErrorList.Clear();
        }

        public XElement GetXElement()
        {
            XElement errMessages;
            if (ErrorList.Count > 0)
            {
                errMessages = new XElement("Errors",
                    new XElement("Error", from msg in ErrorList select msg)
                );
            }
            else
            {
                errMessages = new XElement("Errors");
            }
            return errMessages;
        }

    }
}