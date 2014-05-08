using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using FluentSharp.CoreLib;
using FortifyPackageGenerator.Mapping;

namespace FortifyPackageGenerator
{
    internal class Program
    {
        private const string BaseUrl = "https://vulnerabilities.teammentor.net/article/";

        private static void Main(string[] args)
        {
            if (!File.Exists(@"C:\Temp\FortifyMapping.xml"))
                GenerateMappingXml();    


            var rules = new FortifyRules();
            RulePack teamMentorRulePack = rules.CreateRulePack();
            //Saving it to XML
            teamMentorRulePack.ToXml().saveAs(@"C:\Temp\TeamMentorRulePack.xml");
            System.Console.ReadKey();
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

            //Code Correctness
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Code Correctness",
                              ArticleUrl = BaseUrl + "132eb2b6-2d46-4fda-8d18-773ba1a1a919",
                              SubCategories = new List<FortifySubCategory>
                                                  {
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "132eb2b6-2d46-4fda-8d18-773ba1a1a919",
                                                              SubCategoryDescription =
                                                                  "Regular Expressions Denial of Service"
                                                          }
                                                  }
                          };
            data.add(mapping);

            //Out-of-Bounds Read
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Out-of-Bounds Read",
                              ArticleUrl = BaseUrl + "857f6392-0870-4fec-8d58-1572e3bbb6d7",
                              SubCategories = new List<FortifySubCategory>
                                                  {
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "857f6392-0870-4fec-8d58-1572e3bbb6d7",
                                                              SubCategoryDescription = "Off-by-One"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "857f6392-0870-4fec-8d58-1572e3bbb6d7",
                                                              SubCategoryDescription = "Signed Comparison"
                                                          }
                                                  }
                          };
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
                              SubCategories = new List<FortifySubCategory>
                                                  {
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "5869ac6a-0a19-4541-ac8f-0d74e09f3156",
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
                              SubCategories = new List<FortifySubCategory>
                                                  {
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "275c194d-3f0d-46d8-a874-8584bf0fce30",
                                                              SubCategoryDescription = "Hardcoded Encryption Key"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "161382ce-e097-492d-aae1-90dac335aa66",
                                                              SubCategoryDescription = "Empty Encryption Key"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "161382ce-e097-492d-aae1-90dac335aa66",
                                                              SubCategoryDescription = "Null Encryption Key"
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

            //Missing XML Validation(validating_reader)
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Missing XML Validation(validating_reader)",
                              ArticleUrl = BaseUrl + "907797a3-3e9c-4cd8-ac5d-261209092414",
                          };
            data.add(mapping);

            //XML External Entity Injection
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "XML External Entity Injection",
                              ArticleUrl = BaseUrl + "6f0d3861-d0b8-4cb1-9fc3-92bb23d738e5",
                          };
            data.add(mapping);
            //Error Fixation
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Session Fixation",
                              ArticleUrl = BaseUrl + "fd3db9ad-33b8-41af-aba9-fc5fb12bb562",
                          };
            data.add(mapping);
            //Open Redirect
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Open Redirect",
                              ArticleUrl = BaseUrl + "90608236-847e-47ff-bb8f-b551297a25af",
                          };
            data.add(mapping);
            //Missing Check against Null
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Missing Check against Null",
                              ArticleUrl = BaseUrl + "ec38fb55-80b0-45ab-aa08-e0f681744234",
                          };
            data.add(mapping);

            //Password Management
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Password Management",
                              ArticleUrl = BaseUrl + "17e3dc04-f31e-4f9b-9ab7-de34ad7088ac",
                              SubCategories = new List<FortifySubCategory>
                                                  {
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "17e3dc04-f31e-4f9b-9ab7-de34ad7088ac",
                                                              SubCategoryDescription = "Empty Password"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "17e3dc04-f31e-4f9b-9ab7-de34ad7088ac",
                                                              SubCategoryDescription =
                                                                  "Empty Password in Configuration File"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "17e3dc04-f31e-4f9b-9ab7-de34ad7088ac",
                                                              SubCategoryDescription = "Hardcoded Password"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "17e3dc04-f31e-4f9b-9ab7-de34ad7088ac",
                                                              SubCategoryDescription = "Null Password"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "17e3dc04-f31e-4f9b-9ab7-de34ad7088ac",
                                                              SubCategoryDescription = "Password in Comment"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "17e3dc04-f31e-4f9b-9ab7-de34ad7088ac",
                                                              SubCategoryDescription = "Password in Configuration File"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "17e3dc04-f31e-4f9b-9ab7-de34ad7088ac",
                                                              SubCategoryDescription = "Password in Redirect"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "17e3dc04-f31e-4f9b-9ab7-de34ad7088ac",
                                                              SubCategoryDescription = "Weak Cryptography"
                                                          },
                                                  }
                          };
            data.add(mapping);
            //Path Manipulation
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Path Manipulation",
                              ArticleUrl = BaseUrl + "b0a8079f-fda0-46d9-bc3b-20ac08334e75"
                          };
            data.add(mapping);


            //XPath Injection
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "XPath Injection",
                              ArticleUrl = BaseUrl + "5ef5eea8-07c2-41de-80f9-7f62aaf0e3c8"
                          };
            data.add(mapping);

            //XQuery Injection
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "XQuery Injection",
                              ArticleUrl = BaseUrl + "33f09ecd-0d56-4609-a49b-5337461a13f9"
                          };
            data.add(mapping);

            //XML Injection
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "XML Injection",
                              ArticleUrl = BaseUrl + "c54b70d7-11bc-4102-a4e0-f3e468d00cb0"
                          };
            data.add(mapping);
            //XSLT Injection
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "XSLT Injection",
                              ArticleUrl = BaseUrl + "b9e5c990-5b4e-46cd-bb63-72d625fb75db"
                          };
            data.add(mapping);

            //Null Dereference
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Null Dereference",
                              ArticleUrl = BaseUrl + "bf332de3-8258-4549-aa5c-c7b846ab355d"
                          };
            data.add(mapping);

            //Double Free
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Double Free",
                              ArticleUrl = BaseUrl + "edd65ef2-30f0-45bb-b51c-7cf5a08b6bc1"
                          };
            data.add(mapping);

            //Unreleased Resource 
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Unreleased Resource",
                              ArticleUrl = BaseUrl + "f5f78214-e44f-4147-aed3-32bf78fb20db"
                          };
            data.add(mapping);

            //USE After Free
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Use After Free",
                              ArticleUrl = BaseUrl + "aca8fc61-4b62-48a0-843a-1bdf8f19e34c"
                          };
            data.add(mapping);
            //Buffer Overflow 
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Buffer Overflow",
                              ArticleUrl = BaseUrl + "157c6e57-629d-4002-899a-1be6f2f18a1e",
                              SubCategories = new List<FortifySubCategory>
                                                  {
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "157c6e57-629d-4002-899a-1be6f2f18a1e",
                                                              SubCategoryDescription = "Format String"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "157c6e57-629d-4002-899a-1be6f2f18a1e",
                                                              SubCategoryDescription = "Format String (%f/%F)"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "bb7f7b3a-a3bc-498a-b4b4-88e7e8b02855",
                                                              SubCategoryDescription = "Off-by-One"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "bb7f7b3a-a3bc-498a-b4b4-88e7e8b02855",
                                                              SubCategoryDescription = "Signed Comparison"
                                                          }
                                                  }
                          };
            data.add(mapping);

            //ASP.NET Misconfiguration
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "ASP.NET Misconfiguration",
                              ArticleUrl = BaseUrl + "0230d542-313e-4931-9d89-6161ad0ad343",
                              SubCategories = new List<FortifySubCategory>
                                                  {
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "0230d542-313e-4931-9d89-6161ad0ad343",
                                                              SubCategoryDescription = "Missing Error Handling"
                                                          }
                                                  }
                          };
            data.add(mapping);

            //Weak Encryption 
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Weak Encryption",
                              ArticleUrl = BaseUrl + "161382ce-e097-492d-aae1-90dac335aa66",
                              SubCategories = new List<FortifySubCategory>
                                                  {
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "161382ce-e097-492d-aae1-90dac335aa66",
                                                              SubCategoryDescription = "Inadequate RSA Padding"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "161382ce-e097-492d-aae1-90dac335aa66",
                                                              SubCategoryDescription = "Insufficient Key Size"
                                                          }
                                                  }
                          };
            data.add(mapping);
            //Weak Cryptographic Hash 
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Weak Cryptographic Hash",
                              ArticleUrl = BaseUrl + "1c886700-46cb-4dfb-9e3b-8c95164aa4d4",
                              SubCategories = new List<FortifySubCategory>
                                                  {
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "1c886700-46cb-4dfb-9e3b-8c95164aa4d4",
                                                              SubCategoryDescription = "Hardcoded Salt"
                                                          }
                                                  }
                          };
            data.add(mapping);

            //String Termination
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "String Termination Error",
                              ArticleUrl = BaseUrl + "86123ed3-c89a-469a-9438-3feaa21577b3",
                          };
            data.add(mapping);

            //Uninitialized Variable
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Uninitialized Variable",
                              ArticleUrl = BaseUrl + "168741c0-e42b-44af-9e22-30d5388c6b43",
                          };
            data.add(mapping);

            //Insecure Transport
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Insecure Transport",
                              ArticleUrl = BaseUrl + "17ecc589-9be2-4429-8dc4-9f61556f63b1",
                          };
            data.add(mapping);
            //Integer Overflow
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Integer Overflow",
                              ArticleUrl = BaseUrl + "abc12d73-61ae-4736-b123-144aaea5254b",
                          };
            data.add(mapping);

            //String Termination Error(truncate) 
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "String Termination Error(truncate)",
                              ArticleUrl = BaseUrl + "86123ed3-c89a-469a-9438-3feaa21577b3",
                          };
            data.add(mapping);
            //Format String
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Format String",
                              ArticleUrl = BaseUrl + "157c6e57-629d-4002-899a-1be6f2f18a1e",
                              SubCategories = new List<FortifySubCategory>
                                                  {
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "157c6e57-629d-4002-899a-1be6f2f18a1e",
                                                              SubCategoryDescription = "Argument Number Mismatch"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "157c6e57-629d-4002-899a-1be6f2f18a1e",
                                                              SubCategoryDescription = "Argument Type Mismatch"
                                                          }
                                                  }
                          };
            data.add(mapping);
            //Poor Error Handling
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Poor Error Handling",
                              ArticleUrl = BaseUrl + "ac14838d-5bb2-45bf-bd03-60913b1b0b80",
                              SubCategories = new List<FortifySubCategory>
                                                  {
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl = BaseUrl + "ac14838d-5bb2-45bf-bd03-60913b1b0b80",
                                                              SubCategoryDescription =
                                                                  "Empty Catch Block"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl = BaseUrl + "ac14838d-5bb2-45bf-bd03-60913b1b0b80",
                                                              SubCategoryDescription =
                                                                  "Overly Broad Catch"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "ac14838d-5bb2-45bf-bd03-60913b1b0b80",
                                                              SubCategoryDescription =
                                                                  "Program Catches NullPointerException"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl = BaseUrl + "ac14838d-5bb2-45bf-bd03-60913b1b0b80",
                                                              SubCategoryDescription =
                                                                  "Return Inside Finally"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl = BaseUrl + "ac14838d-5bb2-45bf-bd03-60913b1b0b80",
                                                              SubCategoryDescription =
                                                                  "Swallowed ThreadDeath"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl = BaseUrl + "ac14838d-5bb2-45bf-bd03-60913b1b0b80",
                                                              SubCategoryDescription =
                                                                  "Throw Inside Finally"
                                                          }
                                                  }
                          };
            data.add(mapping);

            //Insecure Randomness 
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Insecure Randomness",
                              ArticleUrl = BaseUrl + "79e3c551-71ae-4120-8542-a6f90823ede9",
                              SubCategories = new List<FortifySubCategory>
                                                  {
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "79e3c551-71ae-4120-8542-a6f90823ede9",
                                                              SubCategoryDescription = "Poor Seeding"
                                                          }
                                                  }
                          };
            data.add(mapping);

            //Often Misused
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Often Misused",
                              ArticleUrl = BaseUrl + "1d3c6862-1cde-4764-bef2-b12bdac2fb8a",
                              SubCategories = new List<FortifySubCategory>
                                                  {
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "1d3c6862-1cde-4764-bef2-b12bdac2fb8a",
                                                              SubCategoryDescription = "File Upload"
                                                          }
                                                  }
                          };
            data.add(mapping);
            //System Information Leak
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "System Information Leak",
                              ArticleUrl = BaseUrl + "b2211d6a-c90a-4a42-8586-b364c3630d2c",
                              SubCategories = new List<FortifySubCategory>
                                                  {
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "b2211d6a-c90a-4a42-8586-b364c3630d2c",
                                                              SubCategoryDescription = "PHP Errors"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "b2211d6a-c90a-4a42-8586-b364c3630d2c",
                                                              SubCategoryDescription = "PHP Version"
                                                          }
                                                  }
                          };
            data.add(mapping);
            //Cookie Security
            mapping = new DataMapping
                          {
                              FortifyCategoryDescription = "Cookie Security",
                              ArticleUrl = BaseUrl + "874d5f27-5b42-41e9-accb-141719b1a681",
                              SubCategories = new List<FortifySubCategory>
                                                  {
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "874d5f27-5b42-41e9-accb-141719b1a681",
                                                              SubCategoryDescription = "Cookie not Sent Over SSL"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "874d5f27-5b42-41e9-accb-141719b1a681",
                                                              SubCategoryDescription = "HTTPOnly not Set"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "874d5f27-5b42-41e9-accb-141719b1a681",
                                                              SubCategoryDescription =
                                                                  "HTTPOnly not Set on Application Cookie"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "874d5f27-5b42-41e9-accb-141719b1a681",
                                                              SubCategoryDescription =
                                                                  "HTTPOnly not Set on Session Cookie"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "874d5f27-5b42-41e9-accb-141719b1a681",
                                                              SubCategoryDescription = "Overly Broad Domain"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "874d5f27-5b42-41e9-accb-141719b1a681",
                                                              SubCategoryDescription = "Overly Broad Path"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "874d5f27-5b42-41e9-accb-141719b1a681",
                                                              SubCategoryDescription =
                                                                  "Overly Broad Session Cookie Domain"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "874d5f27-5b42-41e9-accb-141719b1a681",
                                                              SubCategoryDescription = "Persistent Cookie"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "874d5f27-5b42-41e9-accb-141719b1a681",
                                                              SubCategoryDescription = "Persistent Session Cookie"
                                                          },
                                                      new FortifySubCategory
                                                          {
                                                              ArticleUrl =
                                                                  BaseUrl + "874d5f27-5b42-41e9-accb-141719b1a681",
                                                              SubCategoryDescription =
                                                                  "Session Cookie not Sent Over SSL"
                                                          }
                                                  }
                          };
            data.add(mapping);

            var serializer = new XmlSerializer(typeof (List<DataMapping>),
                                               new[] {typeof (DataMapping), typeof (FortifySubCategory)});

            string fileName = @"C:\Temp\FortifyMapping.xml";

            using (TextWriter writer = new StreamWriter(fileName))
            {
                // Serialize the object, and close the TextWriter.
                serializer.Serialize(writer, data);
                writer.Close();
            }
        }
    }
}