using SPCAF.Sdk.Model;
using SPCAF.Sdk.Model.Extensions;
using SPCAF.Sdk.Rules;
using SPCAF.Sdk;
using System.Linq;
using System.Xml;

namespace SPCAF.Rules.MigrationAssessment
{

    [RuleMetadata(typeof(AdvancedCustomizationsGroup),
        CheckId = "SMA292202",
        DisplayName = "Consider different implementation of FieldType",
        Description = "FieldTypes have been superceeded by JSLink style reformatting of fields in SharePoint 2013.",
        DefaultSeverity = Severity.CriticalError,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "The FieldType '{1}' in file '{0}' must be replaced with a base type of the field, and reformatted with JSLink.",
        Resolution = "res:SPCAF.Rules.MigrationAssessment.Resources.SMA292202.html", 
        Links = new string[]
        {
            "OfficeDev PnP: Provisioning Sub Site Creation (Includes Lab helper example with JSLink)",
            "https://github.com/OfficeDev/PnP/tree/master/Samples/Provisioning.SubSiteCreationApp",
            "Using JSLink with SharePoint 2013",
            "http://msdn.microsoft.com/en-us/magazine/dn745867.aspx",
            "MSDN Code Sample: Client-side rendering code sample",
            "http://code.msdn.microsoft.com/office/Client-side-rendering-code-0a786cdd"
        })]
    public class FieldTypeRecommendations : Rule<TemplateFileReference>
    {
        public override void Visit(TemplateFileReference target, NotificationCollection notifications)
        {
            if(target.Location.ToLower().StartsWith("xml\\"))
            {
                try
                {
                    //open the file and count the xml tags
                    XmlDocument doc = new XmlDocument();
                    doc.Load(target.ManifestFile);
                    foreach(XmlNode fieldTypeNode in doc.SelectNodes("//FieldTypes/FieldType/Field[@Name='TypeName']"))
                    {
                        string message = string.Format(this.MessageTemplate(), target.ReadableElementName, fieldTypeNode.InnerText);
                        this.Notify(target, message, notifications);
                    }
                }
                catch
                {
                }
            }    
        }
    }
}