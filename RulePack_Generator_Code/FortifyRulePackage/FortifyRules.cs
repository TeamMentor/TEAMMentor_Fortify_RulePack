using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using FluentSharp.CoreLib;
using FortifyPackageGenerator.Mapping;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace FortifyPackageGenerator
{
    public class FortifyRules
    {
        private const string FilePath = @"C:\Temp\FortifyMapping.xml";
        private static IWebDriver driver;
        private static Dictionary<string, string> mapping;
        private static IList<DataMapping> dataSouce;
        private static RulePackRules rule;

        #region Constructor

        public FortifyRules()
        {
            driver = new FirefoxDriver();

            mapping = new Dictionary<string, string>();
            dataSouce = new List<DataMapping>();
            Assembly assembly = Assembly.GetExecutingAssembly();

            Stream stream = assembly.GetManifestResourceStream("FortifyPackageGenerator.Mapping.XML.FortifyMapping.xml");
            XmlSerializer serializer;
            serializer = new XmlSerializer(typeof (List<DataMapping>), extraTypes: new[]
                                                                                       {
                                                                                           typeof (DataMapping),
                                                                                           typeof (
                                                                                               FortifySubCategory
                                                                                               )
                                                                                       });
            if (stream != null)
                dataSouce = (List<DataMapping>) serializer.Deserialize(stream);
        }

        #endregion

        public RulePack CreateRulePack()
        {
            int totalRules = dataSouce.Count() +
                             dataSouce.Where(n => n.SubCategories.Count > 0).Sum(n => n.SubCategories.count());
            var rulePack = new RulePack
                               {
                                   RulePackID = Guid.NewGuid().ToString(),
                                   Description = "TeamMentor Secure Coding Rules version 2.0",
                                   Name = "TeamMentor Secure Coding Rules version 2.0",
                                   SKU = Guid.NewGuid().ToString(),
                                   Version = "2.0",
                                   Rules = new RulePackRules[1]
                               };
            //Creating rules
            rule = new RulePackRules
                       {
                           version = "3.11",
                           RuleDefinitions = new RulePackRulesRuleDefinitionsCustomDescriptionRule[totalRules]
                       };

            AddRulesWithHtmlContent3(rulePack, totalRules);
            return rulePack;
        }

        //public void Login ()
        //{
        //    var url = "http://checkmarx.teammentor.net";
        //    driver.Url = url;
        //    driver.Navigate();
        //    driver.FindElement(By.Id(""))
        //}

        private static void AddRulesWithHtmlContent(RulePack rulePack)
        {
            ////Creating rules
            //var rule = new RulePackRules
            //{
            //    version = "3.11",
            //    RuleDefinitions = new RulePackRulesRuleDefinitionsCustomDescriptionRule[mapping.count()]
            //};

            int index = 0;
            foreach (var item in mapping)
            {
                var url = new Uri(item.Value);
                string articleUrl = @"<a href=""" + url.hostUrl() + "/article/";

                driver.Url = item.Value;
                driver.Navigate();
                Thread.Sleep(3000);
                ReadOnlyCollection<IWebElement> links = driver.FindElements(By.TagName("a"));

                //UpdateRelativeURl(links);


                rule.RuleDefinitions[index] = new RulePackRulesRuleDefinitionsCustomDescriptionRule
                                                  {
                                                      RuleID = Guid.NewGuid().ToString(),
                                                      RuleMatch =
                                                          new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatch
                                                          [1],
                                                      formatVersion = "3.2"
                                                  };
                rule.RuleDefinitions[index].RuleMatch[0] =
                    new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatch();
                rule.RuleDefinitions[index].RuleMatch[0].Category =
                    new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchCategory[1];
                rule.RuleDefinitions[index].RuleMatch[0].Category[0] =
                    new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchCategory();
                rule.RuleDefinitions[index].RuleMatch[0].Category[0].Value = item.Key;
                rule.RuleDefinitions[index].Description =
                    new RulePackRulesRuleDefinitionsCustomDescriptionRuleDescription[1];
                rule.RuleDefinitions[index].Description[0] =
                    new RulePackRulesRuleDefinitionsCustomDescriptionRuleDescription();
                //var description = driver.FindElement(By.Id("itemContent")).Text.replace("\r\n", "\r\n\r\n");
                var description = new StringBuilder();
                description.Append(driver.FindElement(By.Id("guidanceItem")).GetAttribute("innerHTML"));

                description = description.Replace("<h1>", "");
                description = description.Replace("</h1>", "\r\n\r\n");
                description = description.Replace("<h2>", "");
                description = description.Replace("</h2>", "\r\n\r\n");
                description = description.Replace("<h3>", "");
                description = description.Replace("</h3>", "\r\n\r\n");
                // description = description.Replace("<a href=\"/article/", articleUrl);


                rule.RuleDefinitions[index].Description[0].Explanation = description.ToString();


                rule.RuleDefinitions[index].Description[0].Recommendations = "Please refer to : " + item.Value;

                index++;
            }

            rulePack.Rules[0] = rule;
            //driver.Close();
            //driver.Quit();
        }

        private static void AddRulesWithHtmlContent2(RulePack rulePack)
        {
            //Creating rules
            var rule = new RulePackRules
                           {
                               version = "3.11",
                               RuleDefinitions =
                                   new RulePackRulesRuleDefinitionsCustomDescriptionRule[dataSouce.count()]
                           };

            int index = 0;
            foreach (DataMapping item in dataSouce)
            {
                var url = new Uri(item.ArticleUrl);
                string articleUrl = @"<a href=""" + url.hostUrl() + "/article/";
                int matchIndex = 0;
                if (item.SubCategories.notNull())
                    matchIndex = item.SubCategories.count() + 1;
                else matchIndex = 1;
                driver.Url = item.ArticleUrl;
                driver.Navigate();
                Thread.Sleep(3000);
                ReadOnlyCollection<IWebElement> links = driver.FindElements(By.TagName("a"));

                // UpdateRelativeURl(links);


                rule.RuleDefinitions[index] = new RulePackRulesRuleDefinitionsCustomDescriptionRule
                                                  {
                                                      RuleID = Guid.NewGuid().ToString(),
                                                      RuleMatch =
                                                          new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatch
                                                          [matchIndex],
                                                      formatVersion = "3.2"
                                                  };
                rule.RuleDefinitions[index].RuleMatch[0] =
                    new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatch();
                rule.RuleDefinitions[index].RuleMatch[0].Category =
                    new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchCategory[1];
                rule.RuleDefinitions[index].RuleMatch[0].Category[0] =
                    new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchCategory();
                rule.RuleDefinitions[index].RuleMatch[0].Category[0].Value = item.FortifyCategoryDescription;
                if (item.SubCategories.notNull() && item.SubCategories.count() > 0)
                {
                    rule.RuleDefinitions[index].RuleMatch[0].Subcategory =
                        new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchSubcategory[
                            item.SubCategories.count()];
                    int newIndex = 0;
                    foreach (FortifySubCategory subCat in item.SubCategories)
                    {
                        rule.RuleDefinitions[index].RuleMatch[0].Subcategory[newIndex] =
                            new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchSubcategory();
                        rule.RuleDefinitions[index].RuleMatch[0].Subcategory[newIndex].Value =
                            subCat.SubCategoryDescription;
                        newIndex ++;
                    }
                }
                rule.RuleDefinitions[index].Description =
                    new RulePackRulesRuleDefinitionsCustomDescriptionRuleDescription[1];
                rule.RuleDefinitions[index].Description[0] =
                    new RulePackRulesRuleDefinitionsCustomDescriptionRuleDescription();
                //var description = driver.FindElement(By.Id("itemContent")).Text.replace("\r\n", "\r\n\r\n");
                var description = new StringBuilder();
                description.Append(driver.FindElement(By.Id("guidanceItem")).GetAttribute("innerHTML"));

                description = description.Replace("<h1>", "");
                description = description.Replace("</h1>", "\r\n\r\n");
                description = description.Replace("<h2>", "");
                description = description.Replace("</h2>", "\r\n\r\n");
                description = description.Replace("<h3>", "");
                description = description.Replace("</h3>", "\r\n\r\n");
                // description = description.Replace("<a href=\"/article/", articleUrl);


                rule.RuleDefinitions[index].Description[0].Explanation = description.ToString();


                rule.RuleDefinitions[index].Description[0].Recommendations = "Please refer to : " + item.ArticleUrl;

                index++;
            }

            rulePack.Rules[0] = rule;
            driver.Close();
            driver.Quit();
        }

        private static void AddRulesWithHtmlContent3(RulePack rulePack, int totalrows)
        {
            int index = 0;
            foreach (DataMapping xrule in dataSouce)
            {
                AddCategory(xrule.ArticleUrl, xrule.FortifyCategoryDescription, index);
                if (xrule.SubCategories != null && xrule.SubCategories.count() > 0)
                {
                    index++;
                    foreach (FortifySubCategory subCategory in xrule.SubCategories)
                    {
                        AddSubCategory(xrule.FortifyCategoryDescription, subCategory.SubCategoryDescription,
                                       subCategory.ArticleUrl, index);
                        index++;
                    }
                }
                else index++;
            }
            rulePack.Rules[0] = rule;
            driver.Close();
            driver.Quit();
        }

        private static void AddCategory(string url, string category, int index)
        {
            driver.Url = url;
            driver.Navigate();
            Thread.Sleep(3000);

            //Finding all links
            ReadOnlyCollection<IWebElement> links = driver.FindElements(By.TagName("a"));
            UpdateRelativeLinks(links);
            rule.RuleDefinitions[index] = new RulePackRulesRuleDefinitionsCustomDescriptionRule
                                              {
                                                  RuleID = Guid.NewGuid().ToString(),
                                                  RuleMatch =
                                                      new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatch[1],
                                                  formatVersion = "3.2"
                                              };
            rule.RuleDefinitions[index].RuleMatch[0] =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatch();
            rule.RuleDefinitions[index].RuleMatch[0].Category =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchCategory[1];
            rule.RuleDefinitions[index].RuleMatch[0].Category[0] =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchCategory();
            rule.RuleDefinitions[index].RuleMatch[0].Category[0].Value = category;
            rule.RuleDefinitions[index].RuleMatch[0].Subcategory =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchSubcategory[1];
            rule.RuleDefinitions[index].RuleMatch[0].Subcategory[0] =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchSubcategory();
            rule.RuleDefinitions[index].RuleMatch[0].Subcategory[0].Value = "";
            rule.RuleDefinitions[index].Description =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleDescription[1];
            rule.RuleDefinitions[index].Description[0] =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleDescription();

            var description = new StringBuilder();
            description.Append(driver.FindElement(By.Id("guidanceItem")).GetAttribute("innerHTML"));

            rule.RuleDefinitions[index].Description[0].Explanation = description.CleanUpHtmlMarkUps();

            string urlLink = String.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", url, url);

            rule.RuleDefinitions[index].Description[0].Recommendations =
                String.Format("For further reference visit TeamMentor page: {0}", urlLink);
        }

        private static void AddSubCategory(string category, string subcategory, string url, int index)
        {
            driver.Url = url;
            driver.Navigate();
            Thread.Sleep(3000);

            ReadOnlyCollection<IWebElement> links = driver.FindElements(By.TagName("a"));
            UpdateRelativeLinks(links);
            rule.RuleDefinitions[index] = new RulePackRulesRuleDefinitionsCustomDescriptionRule
                                              {
                                                  RuleID = Guid.NewGuid().ToString(),
                                                  RuleMatch =
                                                      new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatch[1],
                                                  formatVersion = "3.2"
                                              };
            rule.RuleDefinitions[index].RuleMatch[0] =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatch();
            rule.RuleDefinitions[index].RuleMatch[0].Category =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchCategory[1];
            rule.RuleDefinitions[index].RuleMatch[0].Category[0] =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchCategory();
            rule.RuleDefinitions[index].RuleMatch[0].Category[0].Value = category;
            rule.RuleDefinitions[index].RuleMatch[0].Subcategory =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchSubcategory[1];
            rule.RuleDefinitions[index].RuleMatch[0].Subcategory[0] =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchSubcategory();
            rule.RuleDefinitions[index].RuleMatch[0].Subcategory[0].Value = subcategory;

            rule.RuleDefinitions[index].Description =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleDescription[1];
            rule.RuleDefinitions[index].Description[0] =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleDescription();
            var description = new StringBuilder();
            description.Append(driver.FindElement(By.Id("guidanceItem")).GetAttribute("innerHTML"));

            rule.RuleDefinitions[index].Description[0].Explanation = description.CleanUpHtmlMarkUps();


            string urlLink = String.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", url, url);
            rule.RuleDefinitions[index].Description[0].Recommendations =
                String.Format("For further reference visit TeamMentor page: {0}", urlLink);
        }

        private static void UpdateRelativeLinks(IEnumerable<IWebElement> links)
        {
            var linkList = links.Where(x => (x.Text.Length > 0));
            foreach (IWebElement link in linkList)
            {
                string href = link.GetAttribute("href");

                string newUrl = String.Format("<a href=\"{0}\"", href);
                string outerHtml = link.GetAttribute("outerHTML");

                int endIndex = outerHtml.IndexOf(">");

                string newText = newUrl + outerHtml.Substring(endIndex);
                if (!link.Text.contains("("))
                {
                    var xJavaScriptExecutor = driver as IJavaScriptExecutor;
                    xJavaScriptExecutor.ExecuteScript(String.Format("$('a:contains(\"{0}\")')[0].outerHTML='{1}'",
                                                                    link.Text,
                                                                    newText));
                }
                else
                {
                    Console.WriteLine(href);
                }
            }
        }
    }
}