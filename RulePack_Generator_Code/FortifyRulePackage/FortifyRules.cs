using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using FluentSharp.CoreLib;
using FortifyPackageGenerator.Mapping;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace FortifyPackageGenerator
{

    public class FortifyRules
    {
        private static IWebDriver driver;
        private static Dictionary<string, string> mapping;
        private static IList<DataMapping> dataSouce;
        private const string FilePath = @"C:\Temp\FortifyMapping.xml";
        private static RulePackRules rule;
        #region Constructor

        public FortifyRules()
        {
            driver = new FirefoxDriver();

            //mapping = new Dictionary<string, string>
            //{
            //    {
            //        "Cross-Site Request Forgery",
            //        "http://checkmarx.teammentor.net/article/8ea9bdd9-de4f-46d7-9fd2-b5be2a50a6eb"
            //    },
            //    {"Cross-Site Scripting", "http://checkmarx.teammentor.net/article/2afe59f4-b498-48f8-ab28-6d449a267371"},
            //    {"SQL Injection", "http://checkmarx.teammentor.net/article/7f3c6601-439f-4e9d-8365-be9985d68315"},
            //    {"XPath Injection", "http://checkmarx.teammentor.net/article/9da2fccb-d20c-4e74-9f59-349d799e25cf"},
            //    {"Race Condition", "http://checkmarx.teammentor.net/article/9da2fccb-d20c-4e74-9f59-349d799e25cf"},
            //    {"Command Injection", "http://checkmarx.teammentor.net/article/8698cde2-0fdf-4676-a043-ae90e62f55b8"}
            //};

            dataSouce = new List<DataMapping>();
            using (var fReader = new FileStream(FilePath, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(List<DataMapping>), extraTypes: new Type[] { typeof(DataMapping),
                                                   typeof(FortifySubCategory) });

                dataSouce = (List<DataMapping>) serializer.Deserialize(fReader);
            }
        }

        #endregion

        public RulePack CreateRulePack()
        {
            var totalRules = dataSouce.Count() +
                             dataSouce.Where(n => n.SubCategories.Count > 0).Sum(n => n.SubCategories.count());
            var rulePack = new RulePack
            {
                RulePackID = Guid.NewGuid().ToString(),
                Description = "TeamMentor Secure Coding Rules version 1.0",
                Name = "TeamMentor Secure Coding Rules version 1.0",
                SKU = Guid.NewGuid().ToString(),
                Version = "1.0",
                Rules = new RulePackRules[1]
            };
            //Creating rules
            rule = new RulePackRules
            {
                version = "3.11",
                RuleDefinitions = new RulePackRulesRuleDefinitionsCustomDescriptionRule[totalRules]
            };
            
            AddRulesWithHtmlContent3(rulePack,totalRules);
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
                var articleUrl = @"<a href=""" + url.hostUrl() + "/article/";

                driver.Url = item.Value;
                driver.Navigate();
                System.Threading.Thread.Sleep(3000);
                ReadOnlyCollection<IWebElement> links = driver.FindElements(By.TagName("a"));

                //UpdateRelativeURl(links);


                rule.RuleDefinitions[index] = new RulePackRulesRuleDefinitionsCustomDescriptionRule
                {
                    RuleID = Guid.NewGuid().ToString(),
                    RuleMatch = new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatch[1],
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
                RuleDefinitions = new RulePackRulesRuleDefinitionsCustomDescriptionRule[dataSouce.count()]
            };

            int index = 0;
            foreach (var item in dataSouce)
            {
                var url = new Uri(item.ArticleUrl);
                var articleUrl = @"<a href=""" + url.hostUrl() + "/article/";
                var matchIndex = 0;
                if (item.SubCategories.notNull())
                    matchIndex = item.SubCategories.count() + 1;
                else matchIndex = 1;
                driver.Url = item.ArticleUrl;
                driver.Navigate();
                System.Threading.Thread.Sleep(3000);
                ReadOnlyCollection<IWebElement> links = driver.FindElements(By.TagName("a"));

               // UpdateRelativeURl(links);


                rule.RuleDefinitions[index] = new RulePackRulesRuleDefinitionsCustomDescriptionRule
                {
                    RuleID = Guid.NewGuid().ToString(),
                    RuleMatch = new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatch[matchIndex],
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
                   rule.RuleDefinitions[index].RuleMatch[0].Subcategory = new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchSubcategory[item.SubCategories.count()];
                    var newIndex = 0;
                    foreach (var subCat in item.SubCategories)
                    {
                        rule.RuleDefinitions[index].RuleMatch[0].Subcategory [newIndex] = new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchSubcategory();
                        rule.RuleDefinitions[index].RuleMatch[0].Subcategory[newIndex].Value =
                            subCat.SubCategoryDescription;
                        newIndex ++;
                    }
                }
                 rule.RuleDefinitions[index].Description = new RulePackRulesRuleDefinitionsCustomDescriptionRuleDescription[1];
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

        private static void AddRulesWithHtmlContent3(RulePack rulePack,int totalrows)
        {
            var index = 0;
            foreach (var xrule in dataSouce)
            {
                AddCategory(xrule.ArticleUrl,xrule.FortifyCategoryDescription,index);
                if (xrule.SubCategories != null && xrule.SubCategories.count() > 0)
                {
                    index++;
                    foreach (var subCategory in xrule.SubCategories)
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

        private static void AddCategory(string url, string category,int index)
        {
            driver.Url = url;
            driver.Navigate();
            System.Threading.Thread.Sleep(3000);

            //Finding all links
            ReadOnlyCollection<IWebElement> links = driver.FindElements(By.TagName("a"));
            UpdateRelativeLinks(links);
            rule.RuleDefinitions[index] = new RulePackRulesRuleDefinitionsCustomDescriptionRule
            {
                RuleID = Guid.NewGuid().ToString(),
                RuleMatch = new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatch[1],
                formatVersion = "3.2"
            };
            rule.RuleDefinitions[index].RuleMatch[0] =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatch();
            rule.RuleDefinitions[index].RuleMatch[0].Category =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchCategory[1];
            rule.RuleDefinitions[index].RuleMatch[0].Category[0] =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchCategory();
            rule.RuleDefinitions[index].RuleMatch[0].Category[0].Value = category;
            rule.RuleDefinitions[index].RuleMatch[0].Subcategory = new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchSubcategory[1];
            rule.RuleDefinitions[index].RuleMatch[0].Subcategory[0] = new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchSubcategory();
            rule.RuleDefinitions[index].RuleMatch[0].Subcategory[0].Value = "";
            rule.RuleDefinitions[index].Description = new RulePackRulesRuleDefinitionsCustomDescriptionRuleDescription[1];
            rule.RuleDefinitions[index].Description[0] =new RulePackRulesRuleDefinitionsCustomDescriptionRuleDescription();

            var description = new StringBuilder();
            description.Append(driver.FindElement(By.Id("guidanceItem")).GetAttribute("innerHTML"));

            rule.RuleDefinitions[index].Description[0].Explanation =  description.CleanUpHtmlMarkUps();

            var urlLink = String.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", url, url);

            rule.RuleDefinitions[index].Description[0].Recommendations = String.Format("For further reference visit TeamMentor page: {0}",urlLink);
        }

        private static void AddSubCategory(string category, string subcategory, string url, int index)
        {
            driver.Url = url;
            driver.Navigate();
            System.Threading.Thread.Sleep(3000);

            ReadOnlyCollection<IWebElement> links = driver.FindElements(By.TagName("a"));
            UpdateRelativeLinks(links);
            rule.RuleDefinitions[index] = new RulePackRulesRuleDefinitionsCustomDescriptionRule
            {
                RuleID = Guid.NewGuid().ToString(),
                RuleMatch = new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatch[1],
                formatVersion = "3.2"
            };
            rule.RuleDefinitions[index].RuleMatch[0] =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatch();
            rule.RuleDefinitions[index].RuleMatch[0].Category =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchCategory[1];
            rule.RuleDefinitions[index].RuleMatch[0].Category[0] =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchCategory();
            rule.RuleDefinitions[index].RuleMatch[0].Category[0].Value = category;
            rule.RuleDefinitions[index].RuleMatch[0].Subcategory = new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchSubcategory[1];
            rule.RuleDefinitions[index].RuleMatch[0].Subcategory[0] = new RulePackRulesRuleDefinitionsCustomDescriptionRuleRuleMatchSubcategory();
            rule.RuleDefinitions[index].RuleMatch[0].Subcategory[0].Value = subcategory;

            rule.RuleDefinitions[index].Description = new RulePackRulesRuleDefinitionsCustomDescriptionRuleDescription[1];
            rule.RuleDefinitions[index].Description[0] =
                new RulePackRulesRuleDefinitionsCustomDescriptionRuleDescription();
            var description = new StringBuilder();
            description.Append(driver.FindElement(By.Id("guidanceItem")).GetAttribute("innerHTML"));

            rule.RuleDefinitions[index].Description[0].Explanation = description.CleanUpHtmlMarkUps();

           
            var urlLink = String.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", url, url);
            rule.RuleDefinitions[index].Description[0].Recommendations = String.Format("For further reference visit TeamMentor page: {0}", urlLink);
        }

        static void UpdateRelativeLinks(IEnumerable<IWebElement> links)
        {
            foreach (var link in links.Where(x => x.Text.Length > 0))
            {
                var href = link.GetAttribute("href");

                var newUrl = String.Format("<a href=\"{0}\"", href);
                var outerHtml = link.GetAttribute("outerHTML");

                int endIndex = outerHtml.IndexOf(">");

                var newText = newUrl + outerHtml.Substring(endIndex);

                var xJavaScriptExecutor = driver as IJavaScriptExecutor;
                xJavaScriptExecutor.ExecuteScript(String.Format("$('a:contains(\"{0}\")')[0].outerHTML='{1}'", link.Text, newText));


            }
        }
    }

}

    

