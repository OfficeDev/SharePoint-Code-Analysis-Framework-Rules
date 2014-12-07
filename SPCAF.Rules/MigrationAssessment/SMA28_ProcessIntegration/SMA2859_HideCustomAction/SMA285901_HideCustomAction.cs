using SPCAF.Sdk.Model;
using SPCAF.Sdk.Model.Extensions;
using SPCAF.Sdk.Rules;
using SPCAF.Sdk;
using System.Linq;

namespace SPCAF.Rules.MigrationAssessment
{

    [RuleMetadata(typeof(ProcessAndWorkflowsGroup),
        CheckId = "SMA285901",
        DisplayName = "HideCustomActions are not migratable",
        Description = "Custom Actions are Deployable through the App Model, however the Schema is limited and some customisations such as HideCustomActions through this will not be viable.",
        DefaultSeverity = Severity.CriticalWarning,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "HideCustomAction '{0}' needs to be reworked to match the new remote provisioning model.",
        Links = new string[]
        {
            "OfficeDev PnP",
            "https://github.com/OfficeDev/PnP"  ,
            "Apps for SharePoint compared with SharePoint solutions - Doing things the App way",
            "http://msdn.microsoft.com/en-us/library/office/jj163114%28v=office.15%29.aspx#Questions"
        })]
    public class HideCustomAction : Rule<HideCustomActionDefinition>
    {
        public override void Visit(HideCustomActionDefinition target, NotificationCollection notifications)
        {
            string message = string.Format(this.MessageTemplate(), target.ReadableElementName);
            this.Notify(target, message, notifications);
        }
    }
}