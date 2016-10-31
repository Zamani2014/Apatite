//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MyWebPagesStarterKit.Controls;
using MyWebPagesStarterKit;

public partial class TagResults : PageBaseClass
{
    private string _tag;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _tag = Request.QueryString["tag"];
            lbNoResults.Visible = false;
        }
    }

    protected new void Page_PreRender(object sender, EventArgs e)
    {
        updateViews();
        base.Page_PreRender(sender, e);
    }

    protected string countComments(object EntryComments)
    {
        DataTable comments = (DataTable)EntryComments;
        return comments.Rows.Count.ToString() + " comments";
    }

    protected string getTagsFooter(Object EntryTags)
    {
        string strTags = string.Empty;
        ArrayList tags = (ArrayList)EntryTags;
        if (tags != null && tags.Count > 0)
        {
            strTags = "&nbsp;{Tags: ";
            for (int i = 0; i < tags.Count; i++)
            {
                string guid = tags[i].ToString();
                if (i > 0) strTags += ", ";
                foreach (Blog blog in Blogs)
                {
                    try
                    {
                        strTags += blog.GetBlogTag(guid)["Name"].ToString();
                    }
                    catch { }
                }
            }
            strTags += "}";
        }
        return strTags;
    }

    private void updateViews()
    {
        string guids = string.Empty;
        ArrayList blogs = Blogs;

        //get id of searched tag
        foreach (Blog blog in blogs)
        {
            DataRow[] foundRows = blog.BlogTags.Select(string.Format("Name = '{0}'", _tag));
            if (foundRows.Length > 0)
            {
                guids += foundRows[0]["guid"].ToString() + ";";
            }
        }

        //filtering all blogs with searched tag and merging in new datatable with new attributes sectionId and pageId
        DataTable result = Blog.BlogEntriesSearchedStructure();
        foreach (Blog blog in blogs)
        {
            string blogId = blog.SectionId;
            string pageId = _website.getPageId(blogId);
            foreach (DataRow row in blog.BlogEntries.Rows)
            {
                ArrayList tags = (ArrayList)row["Tags"];
                foreach (string guid in guids.Split(';'))
                {
                    if (tags.Contains(guid))
                    {
                        DataRow rowMerged = result.NewRow();
                        rowMerged["SectionId"] = blogId;
                        rowMerged["PageId"] = pageId;
                        rowMerged["Guid"] = row["Guid"];
                        rowMerged["Title"] = row["Title"];
                        rowMerged["Content"] = row["Content"];
                        rowMerged["Created"] = row["Created"];
                        rowMerged["Comments"] = row["Comments"];
                        rowMerged["Tags"] = row["Tags"];
                        result.Rows.Add(rowMerged);
                        break;
                    }
                }
            }
        }

        if (result.Rows.Count == 0)
        {
            lbNoResults.Text = "Im öffentlichen Bereich wurden keine Einträge gefunden. Bitte melden Sie sich im Member Bereich an.";
            lbNoResults.Visible = true;
        }

        lstBlogEntries.DataSource = new DataView(result, null, "Created desc", DataViewRowState.CurrentRows);
        lstBlogEntries.DataBind();
    }

    private ArrayList Blogs
    {
        get
        {
            ArrayList blogs = new ArrayList();
            //looks for all files in the blog folder
            string[] files = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/App_Data/Blog/"));
            if (files.Length != 0)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    string id = files[i].Substring(files[i].LastIndexOf("\\") + 1);
                    id = id.Substring(0, id.LastIndexOf("."));
                    Blog blog = (Blog)Activator.CreateInstance(typeof(Blog), id);
                    blogs.Add(blog);
                }
            }
            return blogs;
        }
    }

}
