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
using System.IO;

public partial class SectionControls_EventList : SectionControlBaseClass
{
    private EventList _section;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EnsureID();
            valEventDateRequired.ValidationGroup = ID;
            valEventLocationRequired.ValidationGroup = ID;
            valEventNameRequired.ValidationGroup = ID;
            btnSaveDetails.ValidationGroup = ID;

            int i = 0;
            for (i = 0; i < 24; i++)
            {
                txtEventHours.Items.Add(new ListItem(i.ToString("00"), i.ToString()));
            }
            for (i = 0; i < 60; i++)
            {
                txtEventMinutes.Items.Add(new ListItem(i.ToString("00"), i.ToString()));
            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        updateViews();
        if (RssCapable(this._section.GetType()) && _section.EventEntries.Rows.Count > 0)
        {
            WebPage page = new WebPage(Request.QueryString["pg"]);
            this.DisplayRssButton(page);

            //adding link in html-head for rss detection in the browswer
            HtmlLink link = this.MetaLink(page);
            this.Page.Header.Controls.Add(link);
        }
    }

    protected void gvEventList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView row = (DataRowView)e.Row.DataItem;
            if (((DateTime)row["EventDate"]).Hour.Equals(0) && ((DateTime)row["EventDate"]).Minute.Equals(0))
                e.Row.Cells[0].Text = ((DateTime)row["EventDate"]).ToString("d");
            else
                e.Row.Cells[0].Text = ((DateTime)row["EventDate"]).ToString("g");
            if ((DateTime)row["EventDate"] < DateTime.Today)
                e.Row.CssClass = "pastevent";
            e.Row.Cells[0].Attributes.Add("nowrap", "nowrap");
        }
    }

    protected void gvEventListEdit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView row = (DataRowView)e.Row.DataItem;
            if (((DateTime)row["EventDate"]).Hour.Equals(0) && ((DateTime)row["EventDate"]).Minute.Equals(0))
                e.Row.Cells[0].Text = ((DateTime)row["EventDate"]).ToString("d");
            else
                e.Row.Cells[0].Text = ((DateTime)row["EventDate"]).ToString("g");
            e.Row.Cells[0].Attributes.Add("nowrap", "nowrap");
        }
    }

    protected void gvEventListEdit_RowCommand(object source, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "edit_entry":
                ViewState["Edit"] = gvEventListEdit.DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
                break;
            case "delete_entry":
                _section.EventEntries.Rows.Remove(_section.GetEventEntry(gvEventListEdit.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString()));
                _section.EventEntries.AcceptChanges();
                _section.SaveData();
                break;
        }
    }

    protected void gvEventList_RowCommand(object source, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "show_details")
        {
            ViewState["detail"] = gvEventList.DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
        }
    }

    protected void btnNewEntry_Click(object sender, EventArgs e)
    {
        ViewState["Edit"] = "new";
        dteEventDate.DateValue = DateTime.MinValue;
        dteDisplayDate.DateValue = DateTime.MinValue;
    }

    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        DataRow row = null;
        if (ViewState["Edit"].ToString() == "new")
        {
            row = _section.EventEntries.NewRow();
            row["Guid"] = Guid.NewGuid();
            _section.EventEntries.Rows.Add(row);
        }
        else
        {
            row = _section.GetEventEntry(ViewState["Edit"].ToString());
        }

        if (row != null)
        {
            row["EventName"] = txtEventName.Text.Trim();
            row["EventLocation"] = txtEventLocation.Text.Trim();
            row["EventText"] = txtEventDescriptionHtml.Text.Trim();
			row["EventDate"] = DateTime.Parse(txtEventDate.Text).AddHours(Convert.ToInt32(txtEventHours.SelectedValue)).AddMinutes(Convert.ToInt32(txtEventMinutes.SelectedValue)); //dteEventDate.DateValue.AddHours(Convert.ToInt32(txtEventHours.SelectedValue)).AddMinutes(Convert.ToInt32(txtEventMinutes.SelectedValue));
            row["DisplayDate"] = dteDisplayDate.DateValue;
            row.AcceptChanges();

            _section.SaveData();
        }

        ViewState["Edit"] = null;
    }

    protected void btnCancelDetails_Click(object sender, EventArgs e)
    {
        ViewState["Edit"] = null;
    }

    public override ISection Section
    {
        set
        {
            if (value is EventList)
                _section = (EventList)value;
            else
                throw new ArgumentException("Section must be of type EventList");
        }
        get { return _section; }
    }

    public override bool HasAdminView
    {
        get { return true; }
    }

    public override string InfoUrl
    {
        get
        {
            string lang = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (File.Exists(HttpContext.Current.Server.MapPath(string.Format("Documentation/" + lang + "/quick_guide.html"))))
            {
                return "Documentation/" + lang + "/quick_guide.html#event-list";
            }
            else
            { return "Documentation/en/quick_guide.html#event-list"; }
        }
    }

    private void updateViews()
    {
        if (ViewMode == ViewMode.Edit)
        {
            if (ViewState["Edit"] != null)
            {
                multiview.SetActiveView(editDetailsView);

                txtEventHours.SelectedItem.Selected = false;
                txtEventMinutes.SelectedItem.Selected = false;
                if (ViewState["Edit"].ToString() == "new")
                {
                    dteDisplayDate.DateValue = DateTime.Today;
                    txtEventName.Text = string.Empty;
                    txtEventLocation.Text = string.Empty;
                    txtEventDescriptionHtml.Text = string.Empty;
                }
                else
                {
                    DataRow entry = _section.GetEventEntry(ViewState["Edit"].ToString());
					dteEventDate.DateValue = (DateTime)entry["EventDate"];
                    txtEventHours.Items.FindByValue(((DateTime)entry["EventDate"]).Hour.ToString()).Selected = true;
                    txtEventMinutes.Items.FindByValue(((DateTime)entry["EventDate"]).Minute.ToString()).Selected = true;
                    dteDisplayDate.DateValue = (DateTime)entry["DisplayDate"];
                    txtEventName.Text = (string)entry["EventName"];
                    txtEventLocation.Text = (string)entry["EventLocation"];
                    txtEventDescriptionHtml.Text = (string)entry["EventText"];
                }
            }
            else
            {
                gvEventListEdit.DataSource = _section.GetEventEntriesEdit();
                gvEventListEdit.DataBind();
                multiview.SetActiveView(editView);
            }
        }
        else
        {
            multiview.SetActiveView(readonlyView);

            gvEventList.DataSource = _section.GetEventEntriesReadonly(chkShowPastEvents.Checked); ; 
            gvEventList.DataBind();

            DataRow entry = null;

            if ((!IsPostBack) && (Request.QueryString["detail"] != null))
            {
                entry = _section.GetEventEntry(Request.QueryString["detail"]);
                if (entry != null)
                {
                    ViewState["detail"] = Request.QueryString["detail"];
                }
            }

            if ((entry == null) && (ViewState["detail"] != null))
            {
                entry = _section.GetEventEntry(ViewState["detail"].ToString());
            }

            if (entry != null)
            {
                DateTime eventDate = (DateTime)entry["EventDate"];
                string dateFormat = "g";
                if (eventDate.Hour.Equals(0) && eventDate.Minute.Equals(0))
                    dateFormat = "d";
                else
                    dateFormat = "g";
                litEventDate.Text = eventDate.ToString(dateFormat);
                litEventName.Text = string.Format("{0}, " + Resources.StringsRes.ctl_EventList_Location + ": {1}", entry["EventName"], entry["EventLocation"]);
                litEventText.Text = (string)entry["EventText"];
                divEventDetails.Visible = true;
            }
            else
            {
                litEventDate.Text = string.Empty;
                litEventName.Text = string.Empty;
                litEventText.Text = string.Empty;
                divEventDetails.Visible = false;
            }
        }
    }
}