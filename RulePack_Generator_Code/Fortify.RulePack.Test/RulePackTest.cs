using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using FortifyPackageGenerator.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Serialization;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Fortify.RulePack.Test
{
    [TestClass]
    public class RulePackTest
    {
        private List<DataMapping> HPFortifyXMLMapping;
        private const string AssemblyName = "FortifyPackageGenerator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        private const string FileName =     "FortifyPackageGenerator.Mapping.XML.FortifyMapping.xml";

        [TestInitialize]
        public void SetUp()
        {
            HPFortifyXMLMapping = new List<DataMapping>();

            using (var stream = System.Reflection.Assembly.Load(AssemblyName).GetManifestResourceStream(FileName))
            {
                Assert.IsTrue(stream != null);
                var serializer = new XmlSerializer(typeof (List<DataMapping>), new[]{typeof (DataMapping),typeof (FortifySubCategory)});
                HPFortifyXMLMapping = (List<DataMapping>) serializer.Deserialize(stream);
            }


        }

        [TestMethod]
        public void UrlGuidsAreValid()
        {
            Assert.IsTrue(HPFortifyXMLMapping!=null);
            Assert.IsTrue(HPFortifyXMLMapping.Count ==42);
            foreach (DataMapping dataMapping in HPFortifyXMLMapping)
            {
                Guid temp;
                string guid;

                if (dataMapping.SubCategories != null && dataMapping.SubCategories.Count > 0)
                {

                    foreach (FortifySubCategory mapping in dataMapping.SubCategories)
                    {
                        guid = mapping.ArticleUrl.Substring(mapping.ArticleUrl.Length - 36);

                        Assert.IsTrue(Guid.TryParse(guid, out temp));
                    }
                }
                guid = dataMapping.ArticleUrl.Substring(dataMapping.ArticleUrl.Length - 36);
                Assert.IsTrue(Guid.TryParse(guid, out temp));
            }

        }
        [TestMethod]
        public void HyperLinksAreOk()
        {
           var urlList = new List<string>();
           //Fetching Parent node Urls
           urlList.AddRange(HPFortifyXMLMapping.Select(x=> x.ArticleUrl));
           //Fetching Child urls
           var subcategoryUrl = (from query in HPFortifyXMLMapping
                                     from subcategory in query.SubCategories
                              select subcategory.ArticleUrl).ToList();

           //Merging both Url list to make processing easier
           urlList.AddRange(subcategoryUrl);
           
           //Verifying Urls are ok (i.e no broken links)
           foreach (var url in urlList)
           {
               var request = (HttpWebRequest) System.Net.WebRequest.Create(url);
               using (var response = (HttpWebResponse) request.GetResponse())
               {
                   //Checking status code
                   Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

                   //Converting binary stream to string.
                   using(var stream = response.GetResponseStream())
                   {
                      Assert.IsTrue(stream!=null);
                      using (var strmReader = new StreamReader(stream))
                      {
                          var responseString = strmReader.ReadToEnd();
                          Assert.IsFalse(String.IsNullOrEmpty(responseString));
                          Assert.IsTrue(responseString.Length > 0);
                      }
                   }
               }

           }
        }
        [TestMethod]
        public void ArticleClassificationIsVulnerabilities()
        {
            var urlList = new List<string>();
            //Fetching Parent node Urls
            urlList.AddRange(HPFortifyXMLMapping.Select(x => x.ArticleUrl));
            //Fetching Child urls
            var subcategoryUrl = (from query in HPFortifyXMLMapping
                                  from subcategory in query.SubCategories
                                  select subcategory.ArticleUrl).ToList();

            //Merging both Url list to make processing easier
            urlList.AddRange(subcategoryUrl);
            using (var driver = new OpenQA.Selenium.Firefox.FirefoxDriver())
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
                foreach (var url in urlList)
                {
                    driver.Url = url;
                    driver.Navigate();
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(x => x.FindElement(By.Id("GuidanceTypeLabel")).Text.Length>0);
                    var text = driver.FindElement(By.Id("GuidanceTypeLabel")).Text;
                    Console.WriteLine(text);
                    Assert.IsTrue(text == "Vulnerability");
                }
            }
        }
        [TestMethod]
        public void UrlIsWellFormedAndPointsToTheCorrectServer()
        {
            var urlList = new List<string>();
            //Fetching Parent node Urls
            urlList.AddRange(HPFortifyXMLMapping.Select(x => x.ArticleUrl));
            //Fetching Child urls
            var subcategoryUrl = (from query in HPFortifyXMLMapping
                                  from subcategory in query.SubCategories
                                  select subcategory.ArticleUrl).ToList();

            //Merging both Url list to make processing easier
            urlList.AddRange(subcategoryUrl);
            foreach(var url in urlList)
            {
                var tempUrl = new System.Uri(url);
                Assert.IsTrue(tempUrl.Scheme =="https");
                Assert.IsTrue(tempUrl.Host=="vulnerabilities.teammentor.net");
                Assert.IsTrue(tempUrl.IsWellFormedOriginalString());
            }
        }
    }
}
