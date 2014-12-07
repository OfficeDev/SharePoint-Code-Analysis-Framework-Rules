using System.Collections.Generic;
using System.Linq;
using SPCAF.Sdk.Helpers;
using SPCAF.Sdk.Model;
using SPCAF.Sdk.Model.Extensions;
using SPCAF.Sdk.Rules;
using SPCAF.Sdk;

namespace SPCAF.Rules.MigrationAssessment
{
    [RuleMetadata(typeof(CollaborationCustomizationsGroup),
        CheckId = "SMA256403",
        DisplayName = "WebParts that use Xslt transforms to display data",
        Description = "In SharePoint 2013, webparts use JSLink instead of XSLT to transform data for display. While there still is support for XSLT in farm solutions, JSLink now superceeds it, and is an integral part of SharePoint 2013",
        DefaultSeverity = Severity.CriticalWarning,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "For the WebPart {0}, consider the use of JSLink for data transformations.")]
    public class WebPartWithXsltTransform : Rule<AssemblyFileReferenceWSP>
    {
        public override void Visit(AssemblyFileReferenceWSP target, NotificationCollection notifications)
        {
            var referencesToMatch = new List<string>
            {
                "System.Xml.Xsl.XslCompiledTransform"
            };
            foreach (var webpart in target.AssemblyFileReference.GetWebParts())
            {
                if (webpart.Methods.ContainsReferences(referencesToMatch))
                {
                    string message = string.Format(this.MessageTemplate(), webpart.BaseType.FullName, target.ReadableElementName);
                    this.Notify(target, message, notifications);
                }
            }
        }
    }
}