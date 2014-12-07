using SPCAF.Sdk.Model;
using SPCAF.Sdk.Model.Extensions;
using SPCAF.Sdk.Rules;
using SPCAF.Sdk;
using System.Linq;

namespace SPCAF.Rules.MigrationAssessment
{

    [RuleMetadata(typeof(AdvancedCustomizationsGroup),
        CheckId = "SMA290110",
        DisplayName = "Consider different implementation of TimerJob",
        Description = "There is no direct replacment for TimerJobs in the App Model. Alternative approach performs regular running operations e.g. via CSOM, console application and Azure Web jobs.",
        DefaultSeverity = Severity.CriticalWarning,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "The TimerJob '{0}' in Assembly '{1}' use full trust implementation approach. Consider the implementation with an alternative approach.",
        Links = new string[]
        {
            "OfficeDev PnP: See sample Core.SimpleTimerJob",
            "https://github.com/OfficeDev/PnP/tree/master/Samples/Core.SimpleTimerJob",
            "Richard diZerega's Blog: SharePoint Timer Jobs running as Windows Azure Web Jobs",
            "http://blogs.msdn.com/b/richard_dizeregas_blog/archive/2014/04/07/sharepoint-timer-jobs-running-as-windows-azure-web-jobs.aspx",
            "Shariq Siddiqui's Blog: Simulate Timer Job Solution for SharePoint 2013/Online using App Model & CSOM",
            "http://blogs.msdn.com/b/shariq/archive/2013/12/09/simulate-timer-job-solution-for-sharepoint-2013-online-using-csom.aspx",
            "Kirk Evans Blog: Building a SharePoint App as a Timer Job",
            "http://blogs.msdn.com/b/kaevans/archive/2014/03/02/building-a-sharepoint-app-as-a-timer-job.aspx"
            
        })]
    public class TimerJobRecommendations : Rule<AssemblyFileReferenceWSP>
    {
        public override void Visit(AssemblyFileReferenceWSP target, NotificationCollection notifications)
        {
            foreach(Mono.Cecil.TypeDefinition timerJob in target.AssemblyFileReference.TypesThatDerivesFromType("Microsoft.SharePoint.Administration.SPJobDefinition"))
            {
                string message = string.Format(this.MessageTemplate(), timerJob.BaseType.FullName, target.ReadableElementName);
                this.Notify(target, message, notifications);
            }          
        }
    }
}