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
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MyWebPagesStarterKit.Controls;
using MyWebPagesStarterKit;

public partial class Search : PageBaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            locNoresults.Visible = false;
            resultsPager.Visible = false;
        }
        txtSearch.Focus();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvResults.Visible = true;
    }

    protected new void Page_PreRender(object sender, EventArgs e)
    {
        if (gvResults.Visible)
        {
            gvResults.DataSource = _website.Search(txtSearch.Text.Trim());
            gvResults.DataBind();
            locNoresults.Visible = (gvResults.Rows.Count == 0);
        }
        base.Page_PreRender(sender,e);
    }
}
