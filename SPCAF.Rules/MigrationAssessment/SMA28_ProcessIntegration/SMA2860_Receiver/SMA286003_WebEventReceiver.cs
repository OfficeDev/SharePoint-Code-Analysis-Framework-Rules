using SPCAF.Sdk.Model;
using SPCAF.Sdk.Model.Extensions;
using SPCAF.Sdk.Rules;
using SPCAF.Sdk;
using System.Linq;

namespace SPCAF.Rules.MigrationAssessment
{
    [RuleMetadata(typeof(ProcessAndWorkflowsGroup),
        CheckId = "SMA286003",
        DisplayName = "Remove WebEventReceiver",
        Description = "Event Receivers are deployed to the farm, causing slowdown in other components, and decreasing flexibility when migrating, updating, and in disaster recovery.",
        DefaultSeverity = Severity.CriticalError,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "WebEventReceiver '{0}'  should be changed into a Remote Event Receiver",
        Links = new string[]
        {
            "OfficeDev PnP: See sample Core.EventReceivers",
            "https://github.com/OfficeDev/PnP/tree/master/Samples/Core.EventReceivers"            
        })]
    public class WebEventReceiver : Rule<ReceiverDefinition>
    {
        public override void Visit(ReceiverDefinition target, NotificationCollection notifications)
        {
            if (target.Type.ToString().StartsWith("Web", System.StringComparison.OrdinalIgnoreCase)
                || target.Type.ToString().StartsWith("Site", System.StringComparison.OrdinalIgnoreCase))
            {
                string message = string.Format(this.MessageTemplate(), target.ReadableElementName);
                this.Notify(target, message, notifications);
            }
        }
    }
}