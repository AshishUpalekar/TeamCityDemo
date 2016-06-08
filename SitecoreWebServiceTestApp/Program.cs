using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SitecoreWebServiceTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Webservice use
            //var visualSitecoreService = new SitecoreWS.VisualSitecoreServiceSoapClient();
            //string database = "master";
            //SitecoreWS.Credentials credentials = new SitecoreWS.Credentials();
            //credentials.UserName = @"sitecore\admin";
            //credentials.Password = "b";

            //string homeItem = "{641783AA-0EF2-4117-9503-C5CEA36E32F3}";
            //string homeItem2 = "{180A9978-A7F5-4AF3-AEA3-7E4062177DAA}";
            ////string parentItem = "{C5384188-D4B4-4C09-A053-094271353190}";


            //var children = visualSitecoreService.GetChildren(homeItem2, database, credentials);

            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.LoadXml(children.ToString());

            //string xpath = "sitecore/item/@id";
            //var nodes = xmlDoc.SelectNodes(xpath);

            //foreach (XmlNode childrenNode in nodes)
            //{
            //    //childrenNode.Value
            //    var item = visualSitecoreService.GetItemFields(childrenNode.Value, "en", "1", true, database, credentials);
            //    //var getItem = sitecoreService.GetItemFields(getChildren.ChildNodes[i].Attributes[0].Value, "en", "1", true, database, myCred);

            //}
            #endregion

            #region fetch Core database installed pkg info

            var visualSitecoreService = new SitecoreWS.VisualSitecoreServiceSoapClient();
            string database = "core";
            SitecoreWS.Credentials credentials = new SitecoreWS.Credentials();
            credentials.UserName = @"sitecore\admin";
            credentials.Password = "b";

            string homeItem = "/sitecore/system/Packages/Installation history";
            //string homeItem2 = "{180A9978-A7F5-4AF3-AEA3-7E4062177DAA}";
            //string parentItem = "{C5384188-D4B4-4C09-A053-094271353190}";


            var children = visualSitecoreService.GetChildren(homeItem, database, credentials);

            

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(children.ToString());

            XmlDocument xmlGrandChildDoc = new XmlDocument();
            

            string xpath = "sitecore/item/@id";
            var nodes = xmlDoc.SelectNodes(xpath);
            

            foreach (XmlNode childrenNode in nodes)
            {
                var grandChildNode = visualSitecoreService.GetChildren(childrenNode.Value, database, credentials);

                xmlGrandChildDoc.LoadXml(grandChildNode.ToString());
                var grandChildNodes = xmlGrandChildDoc.SelectNodes(xpath);

                foreach (XmlNode grandChildrenNode in grandChildNodes)
                {
                    var item = visualSitecoreService.GetItemFields(grandChildrenNode.Value, "en", "1", true, database, credentials);

                    //field[@title='Package name']
                    //field[@title='Package version']

                    //var getItem = sitecoreService.GetItemFields(getChildren.ChildNodes[i].Attributes[0].Value, "en", "1", true, database, myCred);
                }
            }


            #endregion
        }
    }
}
