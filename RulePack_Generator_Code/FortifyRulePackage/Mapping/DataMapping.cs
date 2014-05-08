using System.Collections.Generic;
using System.Xml.Serialization;

namespace FortifyPackageGenerator.Mapping
{
    public class DataMapping
    {
        [XmlAttribute]
        public string FortifyCategoryDescription { get; set; }

        [XmlAttribute]
        public string ArticleUrl { get; set; }

        [XmlArray("Lists")]
        [XmlArrayItem("List")]
        public List<FortifySubCategory> SubCategories { get; set; }
    }

    public class FortifySubCategory
    {
        [XmlAttribute]
        public string SubCategoryDescription { get; set; }

        [XmlAttribute]
        public string ArticleUrl { get; set; }
    }
}