//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

namespace MyWebPagesStarterKit.Controls
{
    [Designer(typeof(TemplatedContentDesigner)), ToolboxData("<{0}:TemplatedPanel runat=server></{0}:TemplatedPanel>")] 
    public class TemplatedContent : WebControl, INamingContainer
    {
        private ITemplate _template;

        public TemplatedContent() { }

        [Browsable(false), PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate ContentTemplate
        {
            get
            {
                return _template;
            }
            set
            {
                _template = value;
            }
        }

        protected override void CreateChildControls()
        {
            if (_template != null)
            {
                PlaceHolder ctl = new PlaceHolder();
                _template.InstantiateIn(ctl);
                Controls.Add(ctl);
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            //base.RenderBeginTag(writer);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            //base.RenderEndTag(writer);
        }
    }

    public class TemplatedContentDesigner : ControlDesigner
    {
        TemplateGroupCollection col = null;

        public override void Initialize(IComponent component)
        {
            // Initialize the base
            base.Initialize(component);
            // Turn on template editing
            SetViewFlags(ViewFlags.TemplateEditing, true);
        }

        // Add instructions to the placeholder view of the control
        public override string GetDesignTimeHtml()
        {
            return CreatePlaceHolderDesignTimeHtml("Click here and use the task menu to edit the template.");
        }

        public override TemplateGroupCollection TemplateGroups
        {
            get
            {
                if (col == null)
                {
                    // Get the base collection
                    col = base.TemplateGroups;

                    // Create variables
                    TemplateGroup tempGroup;
                    TemplateDefinition tempDef;
                    TemplatedContent ctl;

                    // Get reference to the component as TemplateGroupsSample
                    ctl = (TemplatedContent)Component;

                    // Create a TemplateGroup
                    tempGroup = new TemplateGroup("Content");

                    // Create a TemplateDefinition
                    tempDef = new TemplateDefinition(this, "Content", ctl, "ContentTemplate", false);

                    // Add the TemplateDefinition to the TemplateGroup
                    tempGroup.AddTemplateDefinition(tempDef);

                    // Add the TemplateGroup to the TemplateGroupCollection
                    col.Add(tempGroup);
                }
                return col;
            }
        }

        // Do not allow direct resizing unless in TemplateMode
        public override bool AllowResize
        {
            get
            {
                if (this.InTemplateMode)
                    return true;
                else
                    return false;
            }
        }
    }
}