using System;
using System.ComponentModel;
using SPCAF.Sdk;

namespace SPCAF.Rules.MigrationAssessment
{
    [System.Reflection.ObfuscationAttribute(Exclude = true)]
    [Category("Collaboration Customizations")]
    [Description("In a Migration Assessment this category covers customizations which are focussed on SharePoint collaboration functionality, like Webparts etc.")]
    [VisitorGroupMetadata(
        Category = "Collaboration Customizations",
        Description = "In a Migration Assessment this category covers customizations which are focussed on SharePoint collaboration functionality, like Webparts etc.")]
    public class CollaborationCustomizationsGroup : IVisitorGroup
    {
    }

    [System.Reflection.ObfuscationAttribute(Exclude = true)]
    [Category("Deployment and Provisioning")]
    [Description("In a Migration Assessment this category evaluates migration options for customizations which are mainly used during the deployment and provisionig process of customizations.")]
    [VisitorGroupMetadata(
        Category = "Deployment and Provisioning",
        Description = "In a Migration Assessment this category evaluates migration options for customizations which are mainly used during the deployment and provisionig process of customizations.")]
    public class DeploymentAndProvisioningGroup : IVisitorGroup
    {
    }    

    [System.Reflection.ObfuscationAttribute(Exclude = true)]
    [Category("Processes and Workflows")]
    [Description("In a Migration Assessment this category covers customizations which are focussed on process integration, like Event Receivers, Workflows etc.")]
    [VisitorGroupMetadata(
        Category = "Processes and Workflows",
        Description = "In a Migration Assessment this category covers customizations which are focussed on SharePoint collaboration functionality, like Event Receivers, Workflows etc.")]
    public class ProcessAndWorkflowsGroup : IVisitorGroup
    {
    }

    [System.Reflection.ObfuscationAttribute(Exclude = true)]
    [Category("Branding and UI")]
    [Description("In a Migration Assessment this category covers customizations which are focussed on branding the SharePoint UI.")]
    [VisitorGroupMetadata(
        Category = "Branding and UI",
        Description = "In a Migration Assessment this category contains customizations which are focussed on branding the SharePoint UI.")]
    public class BrandingCustomizationsGroup : IVisitorGroup
    {
    }

    [System.Reflection.ObfuscationAttribute(Exclude=true)]
    [Category("Advanced Customizations")]
    [Description("In a Migration Assessment this category contains more advanced SharePoint customizations like Timer Jobs, HttpModules etc.")]
    [VisitorGroupMetadata(
        Category = "Advanced Customizations",
        Description = "In a Migration Assessment this category evaluates migration options of more advanced SharePoint customizations like Timer Jobs, HttpModules etc.")]
    public class AdvancedCustomizationsGroup : IVisitorGroup
    {
    }

    
}