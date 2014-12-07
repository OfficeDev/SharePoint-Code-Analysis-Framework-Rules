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
        CheckId = "SMA256402",
        DisplayName = "WebParts that use search",
        Description = "The new Search Results WebPart, and Content Search WebPart will be able to replace almost any search based result webpart currently in use.",
        DefaultSeverity = Severity.CriticalWarning,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "For the WebPart {0}, consider use of either the Search Results WebPart, or the Content Search WebPart.")]
    public class WebPartsThatImplementSearch : Rule<AssemblyFileReferenceWSP>
    {
        public override void Visit(AssemblyFileReferenceWSP target, NotificationCollection notifications)
        {
            var referencesToMatch = new List<string>
            {
                "Microsoft.Office.Server.Search.Query.KeywordQuery"
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