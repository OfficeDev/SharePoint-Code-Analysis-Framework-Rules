using SPCAF.Sdk.Model;
using SPCAF.Sdk.Model.Extensions;
using SPCAF.Sdk.Rules;
using SPCAF.Sdk;
using System.Linq;

namespace SPCAF.Rules.MigrationAssessment
{
    [RuleMetadata(typeof(ProcessAndWorkflowsGroup),
        CheckId = "SMA286001",
        DisplayName = "Change implementation of ItemEventReceiver to RemoteEventReceiver",
        Description = "Event Receivers are deployed to the farm, causing slowdown in other components, and decreasing flexibility when migrating, updating, and in disaster recovery.",
        DefaultSeverity = Severity.CriticalWarning,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "ItemEventReceiver '{0}' should be changed into a Remote Event Receiver",
        Links = new string[]
        {
            "Kirk Evans Blog: Attaching Remote Event Receivers to Lists in the Host Web",
            "http://blogs.msdn.com/b/kaevans/archive/2014/02/26/attaching-remote-event-receivers-to-lists-in-the-host-web.aspx", 
            "OfficeDev PnP: See sample Core.EventReceivers",
            "https://github.com/OfficeDev/PnP/tree/master/Samples/Core.EventReceivers"            
        })]
    public class ItemEventReceiver : Rule<ReceiverDefinition>
    {
        public override void Visit(ReceiverDefinition target, NotificationCollection notifications)
        {
            if (target.Type.ToString().StartsWith("Item", System.StringComparison.OrdinalIgnoreCase) 
                || target.Type.ToString().StartsWith("Context", System.StringComparison.OrdinalIgnoreCase) )
            {
                string message = string.Format(this.MessageTemplate(), target.ReadableElementName);
                this.Notify(target, message, notifications);
            }
        }
    }
}