//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MyWebPagesStarterKit;
using MyWebPagesStarterKit.Controls;
using System.Web.Security;

public partial class keywords : PageBaseClass
{
    private Blog _blog;
    protected string _title = Resources.StringsRes.pge_Keywords_Title;
    protected string _direction = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        lbSaved.Visible = false;

        if (!this.Page.IsPostBack)
        {
            tbNewTag.Visible = false;

            load();

            //bind tag to CheckBoxList
            cblTags.DataSource = _blog.GetBlogTagsSorted();
            cblTags.DataBind();

            //select entry tags
            DataRow entry = null;
            if (Request.QueryString["entry"] != "new")
            {
                entry = _blog.GetBlogEntry(Request.QueryString["entry"]);
                ArrayList tags = (ArrayList)entry["Tags"];
                for (int i = 0; i < cblTags.Items.Count; i++)
                {
                    if (tags.Contains(cblTags.Items[i].Value))
                    {
                        cblTags.Items[i].Selected = true;
                    }
                }
            }
        }
    }

    protected new void Page_PreRender(object sender, EventArgs e)
    {
        if (_website.Theme == "Arabic")
        {
            _direction = "rtl";
        }
        else
        {
            _direction = "ltr";
        }
    }

    protected void btNewTag_Click(object sender, EventArgs e)
    {
        tbNewTag.Visible = true;
        tbNewTag.Focus();
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        DataRow blogTag = null;
        DataRow blogEntry = null;

        load();

        this.Page.Validate();
        if (this.Page.IsValid)
        {
            //check if there is a new tag:
            if (tbNewTag.Text.Trim() != string.Empty)
            {
                //save new tag to blog tags collection
                blogTag = null;
                blogTag = _blog.BlogTags.NewRow();
                blogTag["Guid"] = Guid.NewGuid();
                _blog.BlogTags.Rows.Add(blogTag);
                blogTag["Name"] = tbNewTag.Text.Trim();
                blogTag["Description"] = "";
                blogTag["Number"] = 0;
                blogTag.AcceptChanges();

                //add new tag to CheckBoxList
                cblTags.Items.Add(new ListItem(tbNewTag.Text.Trim(), blogTag["Guid"].ToString()));
                cblTags.Items.FindByValue(blogTag["Guid"].ToString()).Selected = true;
            }

            //save tag number information to the tag collection (for new entries, the number information 
            //is safed from Session["EntryTags"] below, when safing the entry
            if (Request.QueryString["entry"] != "new")
            {
                blogEntry = _blog.GetBlogEntry(Request.QueryString["entry"]);
                ArrayList entryTags = (ArrayList)blogEntry["Tags"];
                for (int i = 0; i < cblTags.Items.Count; i++)
                {
                    blogTag = _blog.GetBlogTag(cblTags.Items[i].Value);
                    int intNumber = (int)blogTag["Number"];

                    //new tag added
                    if (cblTags.Items[i].Text == tbNewTag.Text.Trim())
                    {
                        blogTag["Number"] = intNumber + 1;
                    }
                    //existing tag added
                    if (cblTags.Items[i].Selected == true && !entryTags.Contains(cblTags.Items[i].Value))
                    {
                        blogTag["Number"] = intNumber + 1;
                    }
                    //existing tag removed
                    if (cblTags.Items[i].Selected == false && entryTags.Contains(cblTags.Items[i].Value))
                    {
                        if (intNumber > 0)
                        {
                            blogTag["Number"] = intNumber - 1;
                        }
                    }
                }
                if (cblTags.Items.Count > 0) blogTag.AcceptChanges();
            }

            //store selected tags in Arraylist
            ArrayList tagsSelection = new ArrayList();
            for (int i = 0; i < cblTags.Items.Count; i++)
            {
                if (cblTags.Items[i].Selected == true)
                {
                    tagsSelection.Add(cblTags.Items[i].Value);
                }
            }

            //safe selected tags to blog entry (or session object if blog entry is new)
            if (Request.QueryString["entry"] == "new")
            {
                Session["EntryTags"] = tagsSelection;
            }
            else
            {
                blogTag = null;
                blogEntry = _blog.GetBlogEntry(Request.QueryString["entry"]);
                blogEntry["Tags"] = tagsSelection;
                blogEntry.AcceptChanges();

                //Reload TagCloud
                MyWebPagesStarterKit.TagCloud.reloadTagCloud();
            }

            ///blog updated
            _blog.ConfigurationData.Rows[0]["Updated"] = DateTime.Now;
            _blog.ConfigurationData.AcceptChanges();
           
            _blog.SaveData();

            //confirmation message
            lbSaved.Visible = true;
            if (tbNewTag.Text.Trim() != string.Empty)
            {
                lbSaved.Text = Resources.StringsRes.pge_Keywords_TagCreated;
            }
            else
            {
                lbSaved.Text = Resources.StringsRes.pge_Keywords_SelectionSafed;
            }

            //clean up and hide new tag textbox
            tbNewTag.Text = string.Empty;
            tbNewTag.Visible = false;
        }
    }

    private void load()
    {
        if (User.Identity.IsAuthenticated == false)
        {
            Response.Write("<script type=\"text/javascript\">window.close();</script>");
            Response.End();
        }

        WebPage page = new WebPage(Request.QueryString["pg"]);

        //load blog for tag management
        SectionLoader oSection = SectionLoader.GetInstance();
        _blog = (Blog)oSection.LoadSection(Request.QueryString["section"]);
    }

    protected void valTags_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        //check if the new tag does already exist for this blog
        if (tbNewTag.Text.Trim() != string.Empty)
        {
            for (int i = 0; i < cblTags.Items.Count; i++)
            {
                if (cblTags.Items[i].Text ==  tbNewTag.Text.Trim())
                {
                    args.IsValid = false;
                }
            }
        }
    }
}
