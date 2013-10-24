using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using FluentSharp.CoreLib;
using FortifyPackageGenerator.Mapping;

namespace FortifyPackageGenerator
{
    internal class Program
    {
        const string BaseUrl = "https://vulnerabilities.teammentor.net/article/";
        
        private static void Main(string[] args)
        {
            if (!File.Exists(@"C:\Temp\FortifyMapping.xml"))
                GenerateMappingXml();    
            
            
            var rules = new FortifyRules();
            var teamMentorRulePack = rules.CreateRulePack();
            //Saving it to XML
            teamMentorRulePack.ToXml().saveAs(@"C:\Temp\TeamMentorRulePack.xml");
           
        }

        private static void GenerateMappingXml()
        {
            var data = new List<DataMapping>();
            
            //SQL Injection
            var mapping = new DataMapping();
            mapping.FortifyCategoryDescription = "SQL Injection";
            mapping.ArticleUrl = BaseUrl + "c4914e51-2609-4edc-8133-31491f1b03c0";
            mapping.SubCategories = new List<FortifySubCategory>
            {
                new FortifySubCategory
                {
                    ArticleUrl = BaseUrl + "c4914e51-2609-4edc-8133-31491f1b03c0",
                    SubCategoryDescription = "Castle ActiveRecord"
                },
                new FortifySubCategory
                {
                    ArticleUrl = BaseUrl + "c4914e51-2609-4edc-8133-31491f1b03c0",
                    SubCategoryDescription = "Hibernate"
                },
                new FortifySubCategory
                {
                    ArticleUrl = BaseUrl + "c4914e51-2609-4edc-8133-31491f1b03c0",
                    SubCategoryDescription = "iBatis Data Map"
                },
                new FortifySubCategory
                {
                    ArticleUrl = BaseUrl + "c4914e51-2609-4edc-8133-31491f1b03c0",
                    SubCategoryDescription = "JDO"
                },
                new FortifySubCategory
                {
                    ArticleUrl = BaseUrl + "c4914e51-2609-4edc-8133-31491f1b03c0",
                    SubCategoryDescription = "LINQ"
                },
                new FortifySubCategory
                {
                    ArticleUrl = BaseUrl + "c4914e51-2609-4edc-8133-31491f1b03c0",
                    SubCategoryDescription = "NHibernate"
                },
                new FortifySubCategory
                {
                    ArticleUrl = BaseUrl + "c4914e51-2609-4edc-8133-31491f1b03c0",
                    SubCategoryDescription = "Persistence"
                },
                new FortifySubCategory
                {
                    ArticleUrl = BaseUrl + "c4914e51-2609-4edc-8133-31491f1b03c0",
                    SubCategoryDescription = "SubSonic"
                }
            };
            data.add(mapping);

            //Cross-Site Scripting
            mapping = new DataMapping();
            mapping.FortifyCategoryDescription = "Cross-Site Scripting";
            mapping.ArticleUrl = BaseUrl + "683d33a8-7979-4307-8cf0-3a82457f9f47";
            mapping.SubCategories = new List<FortifySubCategory>
            {
                new FortifySubCategory
                {
                    ArticleUrl = BaseUrl + "683d33a8-7979-4307-8cf0-3a82457f9f47",
                    SubCategoryDescription = "DOM"
                },
                new FortifySubCategory
                {
                    ArticleUrl = BaseUrl + "683d33a8-7979-4307-8cf0-3a82457f9f47",
                    SubCategoryDescription = "External Links"
                },
                new FortifySubCategory
                {
                    ArticleUrl = BaseUrl + "683d33a8-7979-4307-8cf0-3a82457f9f47",
                    SubCategoryDescription = "Persistent"
                },
                new FortifySubCategory
                {
                    ArticleUrl = BaseUrl + "683d33a8-7979-4307-8cf0-3a82457f9f47",
                    SubCategoryDescription = "Poor Validation"
                },
                new FortifySubCategory
                {
                    ArticleUrl = BaseUrl + "683d33a8-7979-4307-8cf0-3a82457f9f47",
                    SubCategoryDescription = "Reflected"
                }
            };
            data.add(mapping);


            //Command Injection
            mapping = new DataMapping();
            mapping.FortifyCategoryDescription = "Command Injection";
            mapping.ArticleUrl = BaseUrl + "94e52aca-06b6-4747-9bc9-f0149208f18c";
            data.add(mapping);

            //Cross-Site Request Forgery
            mapping = new DataMapping();
            mapping.FortifyCategoryDescription = "Cross-Site Request Forgery";
            mapping.ArticleUrl = BaseUrl + "62f78eb2-9eba-484c-ade3-7b54c2df9e5a";
            data.add(mapping);

            //Dangerous File Inclusion
            mapping = new DataMapping();
            mapping.FortifyCategoryDescription = "Dangerous File Inclusion";
            mapping.ArticleUrl = BaseUrl + "4a8034cb-e024-4ccb-a5f7-d7397dfc1371";
            data.add(mapping);
            //Dynamic Code Evaluation
            mapping = new DataMapping
            {
                SubCategories = new List<FortifySubCategory>()
                {
                    new FortifySubCategory()
                    {
                        ArticleUrl = BaseUrl + "5869ac6a-0a19-4541-ac8f-0d74e09f3156",
                        SubCategoryDescription = "Code Injection"
                    }
                }
            };
            mapping.FortifyCategoryDescription = "Dynamic Code Evaluation";
            mapping.ArticleUrl = BaseUrl + "5869ac6a-0a19-4541-ac8f-0d74e09f3156";
            data.add(mapping);

            //Key Management
            mapping = new DataMapping
            {
                FortifyCategoryDescription = "Key Management",
                ArticleUrl = BaseUrl + "275c194d-3f0d-46d8-a874-8584bf0fce30",
                SubCategories = new List<FortifySubCategory>()
                {
                    new FortifySubCategory()
                    {
                        ArticleUrl = BaseUrl + "275c194d-3f0d-46d8-a874-8584bf0fce30",
                        SubCategoryDescription = "Hardcoded Encryption Key"
                    }
                }
            };
            data.add(mapping);

            //LDAP Injection
            mapping = new DataMapping
            {
                FortifyCategoryDescription = "LDAP Injection",
                ArticleUrl = BaseUrl + "8eba1b70-1b1a-4810-a819-1c212cf33099",
            };
            data.add(mapping);

            //Log Forging
            mapping = new DataMapping
            {
                FortifyCategoryDescription = "Log Forging",
                ArticleUrl = BaseUrl + "4f84a5c0-b513-4054-8f1a-1ff1367de03b",
            };
            data.add(mapping);

            //Missing XML Validation
            mapping = new DataMapping
            {
                FortifyCategoryDescription = "Missing XML Validation",
                ArticleUrl = BaseUrl + "907797a3-3e9c-4cd8-ac5d-261209092414",
            };
            data.add(mapping);

            //Open Redirect
            mapping = new DataMapping
            {
                FortifyCategoryDescription = "Open Redirect",
                ArticleUrl = BaseUrl + "90608236-847e-47ff-bb8f-b551297a25af",
            };
            data.add(mapping);


            //Password Management
            mapping = new DataMapping
            {
                FortifyCategoryDescription = "Password Management",
                ArticleUrl = BaseUrl + "57829386-5df5-4f7f-993a-3552ab3a292c",
                SubCategories = new List<FortifySubCategory>()
                {
                    new FortifySubCategory()
                    {
                        ArticleUrl = BaseUrl + "57829386-5df5-4f7f-993a-3552ab3a292c",
                        SubCategoryDescription = "Empty Password"
                    },
                     new FortifySubCategory()
                    {
                        ArticleUrl = BaseUrl + "57829386-5df5-4f7f-993a-3552ab3a292c",
                        SubCategoryDescription = "Empty Password in Configuration File"
                    },
                     new FortifySubCategory()
                    {
                        ArticleUrl = BaseUrl + "57829386-5df5-4f7f-993a-3552ab3a292c",
                        SubCategoryDescription = "Hardcoded Password"
                    },
                    new FortifySubCategory()
                    {
                        ArticleUrl = BaseUrl + "57829386-5df5-4f7f-993a-3552ab3a292c",
                        SubCategoryDescription = "Null Password"
                    },

                    new FortifySubCategory()
                    {
                        ArticleUrl = BaseUrl + "57829386-5df5-4f7f-993a-3552ab3a292c",
                        SubCategoryDescription = "Password in Comment"
                    },
                    new FortifySubCategory()
                    {
                        ArticleUrl = BaseUrl + "6c6f2433-222d-4568-8de6-9cbdd198dbc0",
                        SubCategoryDescription = "Password in Configuration File"
                    },
                     new FortifySubCategory()
                    {
                        ArticleUrl = BaseUrl + "57829386-5df5-4f7f-993a-3552ab3a292c",
                        SubCategoryDescription = "Password in Redirect"
                    },
                     new FortifySubCategory()
                    {
                        ArticleUrl = BaseUrl + "57829386-5df5-4f7f-993a-3552ab3a292c",
                        SubCategoryDescription = "Weak Cryptography"
                    },
                }
            };
            data.add(mapping);
            //Path Manipulation
            mapping = new DataMapping
            {
                FortifyCategoryDescription = "Path Manipulation",
                ArticleUrl = BaseUrl + "b0a8079f-fda0-46d9-bc3b-20ac08334e75",
            };
            data.add(mapping);


            //XPath Injection
            mapping = new DataMapping
            {
                FortifyCategoryDescription = "XPath Injection",
                ArticleUrl = BaseUrl + "5ef5eea8-07c2-41de-80f9-7f62aaf0e3c8",
            };
            data.add(mapping);


            //XPath Injection
            mapping = new DataMapping
            {
                FortifyCategoryDescription = "ASP.NET Misconfiguration",
                ArticleUrl = BaseUrl + "0230d542-313e-4931-9d89-6161ad0ad343",
                SubCategories = new List<FortifySubCategory>()
                {
                    new FortifySubCategory()
                    {
                        ArticleUrl = BaseUrl + "0230d542-313e-4931-9d89-6161ad0ad343",
                        SubCategoryDescription = "Missing Error Handling"
                    }
                }
            };
            data.add(mapping);


            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(typeof(List<DataMapping>), new Type[] { typeof(DataMapping),typeof(FortifySubCategory) });

            var fileName = @"C:\Temp\FortifyMapping.xml";

            using (TextWriter writer = new StreamWriter(fileName))
            {
                // Serialize the object, and close the TextWriter.
                serializer.Serialize(writer,data);
                writer.Close();
            }
        }
    }
}