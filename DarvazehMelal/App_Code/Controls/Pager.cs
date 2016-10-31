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
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Web.UI.Design;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace MyWebPagesStarterKit.Controls
{
    [ToolboxData("<{0}:Pager runat=server></{0}:Pager>")]
    public class Pager : WebControl, INamingContainer
    {
        private ITemplate _separatorTemplate;
        private Control _controlToPage;
        private string _controlToPageName;
        private PagedDataSource _pagedDataSource;
        private int _pageSize;
        private string _firstImageUrl;
        private string _previousImageUrl;
        private string _nextImageUrl;
        private string _lastImageUrl;
        private PagerMode _mode;
        private bool _showPageNumbers;
        private Table _table;

        public Pager()
        {
            _pageSize = 20;
            _firstImageUrl = string.Empty;
            _previousImageUrl = string.Empty;
            _nextImageUrl = string.Empty;
            _lastImageUrl = string.Empty;
            _mode = PagerMode.FirstPreviousNextLast;
            _showPageNumbers = true;
            _table = new Table();
        }

        [Browsable(false), PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate SeparatorTemplate
        {
            get { return _separatorTemplate; }
            set { _separatorTemplate = value; }
        }

        public string ControlToPage
        {
            get { return _controlToPageName; }
            set { _controlToPageName = value; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public bool ShowPageNumbers
        {
            get { return _showPageNumbers; }
            set { _showPageNumbers = value; }
        }

        public override Unit BorderWidth
        {
            get { return _table.BorderWidth; }
            set { _table.BorderWidth = value; }
        }

        public override Color BorderColor
        {
            get { return _table.BorderColor; }
            set { _table.BorderColor = value; }
        }

        public override BorderStyle BorderStyle
        {
            get { return _table.BorderStyle; }
            set { _table.BorderStyle = value; }
        }

        public override Color BackColor
        {
            get { return _table.BackColor; }
            set { _table.BackColor = value; }
        }

        public override string CssClass
        {
            get { return _table.CssClass; }
            set { _table.CssClass = value; }
        }

        [UrlProperty]
        public string FirstImageUrl
        {
            get { return _firstImageUrl; }
            set { _firstImageUrl = ResolveUrl(value); }
        }

        [UrlProperty]
        public string PreviousImageUrl
        {
            get { return _previousImageUrl; }
            set { _previousImageUrl = ResolveUrl(value); }
        }

        [UrlProperty]
        public string NextImageUrl
        {
            get { return _nextImageUrl; }
            set { _nextImageUrl = ResolveUrl(value); }
        }

        [UrlProperty]
        public string LastImageUrl
        {
            get { return _lastImageUrl; }
            set { _lastImageUrl = ResolveUrl(value); }
        }

        public PagerMode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        public int CurrentPageIndex
        {
            get { return (ViewState["CurrentPageIndex"] == null) ? 0 : (int)ViewState["CurrentPageIndex"]; }
            set { ViewState["CurrentPageIndex"] = value; }
        }

        private int TotalPages
        {
            get { return (ViewState["TotalPages"] == null) ? 0 : (int)ViewState["TotalPages"]; }
            set { ViewState["TotalPages"] = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _controlToPage = Parent.FindControl(_controlToPageName);
            if (_controlToPage == null)
                throw new Exception(string.Format("'{0}' is not a valid value for ControlToPage (The control does not exist).", _controlToPageName));

            if (
                    ! (
                        (_controlToPage.GetType().IsSubclassOf(typeof(BaseDataBoundControl)))
                        ||
                        (_controlToPage.GetType().IsSubclassOf(typeof(BaseDataList)))
                    )
                )
                throw new Exception("Works only for Controls deriving from BaseDataBoundControl or BaseDataList.");

            if (_controlToPage is BaseDataBoundControl)
                ((BaseDataBoundControl)_controlToPage).DataBinding += new EventHandler(ControlToPage_DataBinding);
            else if (_controlToPage is BaseDataList)
                ((BaseDataList)_controlToPage).DataBinding += new EventHandler(ControlToPage_DataBinding);
        }

        private void ControlToPage_DataBinding(object sender, EventArgs e)
        {
            ChildControlsCreated = false;

            object o = null;

            if (_controlToPage is BaseDataBoundControl)
                o = ((BaseDataBoundControl)_controlToPage).DataSource;
            else if (_controlToPage is BaseDataList)
                o = ((BaseDataList)_controlToPage).DataSource;

            IEnumerable enumerable;

            if (o.GetType().Name == typeof(DataTable).Name)
                enumerable = ((DataTable)o).DefaultView;
            else
                enumerable = (IEnumerable)o;

            _pagedDataSource = new PagedDataSource();
            _pagedDataSource.AllowPaging = true;
            _pagedDataSource.DataSource = enumerable;
            _pagedDataSource.PageSize = PageSize;
            _pagedDataSource.CurrentPageIndex = CurrentPageIndex;

            if (_controlToPage is BaseDataBoundControl)
                ((BaseDataBoundControl)_controlToPage).DataSource = _pagedDataSource;
            else if (_controlToPage is BaseDataList)
                ((BaseDataList)_controlToPage).DataSource = _pagedDataSource;
            
            TotalPages = _pagedDataSource.PageCount;

            Visible = (TotalPages > 1);
        }

        protected override void CreateChildControls()
        {
            _table.Rows.Clear();
            Controls.Clear();
            ClearChildViewState();

            //for Valid XHTML 1.0 Transitional the table-tag cannot be placed directly within span-tag
            //span-tag is created automatically by the .net control
            //HtmlGenericControl obj = new HtmlGenericControl("object");
            //Controls.Add(obj);

            Controls.Add(_table);
            //obj.Controls.Add(_table);
            _table.EnableViewState = false;
            _table.Width = Unit.Percentage(100);
            _table.Rows.Add(new TableRow());

            TableCell cell = null;

            cell = new TableCell();
            _table.Rows[0].Cells.Add(cell);
            cell.Width = Unit.Percentage(20);
            cell.HorizontalAlign = HorizontalAlign.Left;

            // first button
            if ((CurrentPageIndex > 0) && ((Mode == PagerMode.FirstLast) || (Mode == PagerMode.FirstPreviousNextLast)))
            {
                LinkButton btn = new LinkButton();
                cell.Controls.Add(btn);
                if (FirstImageUrl != string.Empty)
                    btn.Text = string.Format("<img src=\"{0}\" border=\"0\" alt=\"\" />", FirstImageUrl);
                else
                    btn.Text = "|&lt;&lt;";
                btn.CommandArgument = "0";
                btn.CommandName = "Page";
                btn.Command += new CommandEventHandler(btn_Command);
                btn.CausesValidation = false;
            }

            // previous button
            if ((CurrentPageIndex > 0) && ((Mode == PagerMode.PreviousNext) || (Mode == PagerMode.FirstPreviousNextLast)))
            {
                if (cell.Controls.Count > 0)
                {
                    Literal lit = new Literal();
                    lit.Text = "&nbsp;";
                    cell.Controls.Add(lit);
                }

                LinkButton btn = new LinkButton();
                cell.Controls.Add(btn);
                if (PreviousImageUrl != string.Empty)
                    btn.Text = string.Format("<img src=\"{0}\" border=\"0\" alt=\"\" />", PreviousImageUrl);
                else
                    btn.Text = "&lt;";
                btn.CommandArgument = (CurrentPageIndex - 1).ToString();
                btn.CommandName = "Page";
                btn.Command += new CommandEventHandler(btn_Command);
                btn.CausesValidation = false;
            }

            // page numbers
            cell = new TableCell();
            _table.Rows[0].Cells.Add(cell);
            cell.HorizontalAlign = HorizontalAlign.Center;
            cell.Width = Unit.Percentage(60);

            if (_showPageNumbers)
            {
                for (int i = 0; i < TotalPages; i++)
                {
                    if (cell.Controls.Count > 0)
                    {
                        Literal lit = new Literal();
                        lit.Text = "&nbsp;";
                        cell.Controls.Add(lit);
                    }

                    if (i != CurrentPageIndex)
                    {
                        LinkButton btn = new LinkButton();
                        cell.Controls.Add(btn);
                        btn.Text = (i + 1).ToString();
                        btn.CommandName = "Page";
                        btn.CommandArgument = i.ToString();
                        btn.Command += new CommandEventHandler(btn_Command);
                        btn.CausesValidation = false;
                    }
                    else
                    {
                        Literal lit = new Literal();
                        cell.Controls.Add(lit);
                        lit.Text = (i + 1).ToString();
                    }
                }
            }

            cell = new TableCell();
            _table.Rows[0].Cells.Add(cell);
            cell.Width = Unit.Percentage(20);
            cell.HorizontalAlign = HorizontalAlign.Right;

            // next button
            if ((CurrentPageIndex < TotalPages - 1) && ((Mode == PagerMode.PreviousNext) || (Mode == PagerMode.FirstPreviousNextLast)))
            {
                LinkButton btn = new LinkButton();
                cell.Controls.Add(btn);
                if (NextImageUrl != string.Empty)
                    btn.Text = string.Format("<img src=\"{0}\" border=\"0\" alt=\"\" />", NextImageUrl);
                else
                    btn.Text = "&gt;";
                btn.CommandArgument = (CurrentPageIndex + 1).ToString();
                btn.CommandName = "Page";
                btn.Command += new CommandEventHandler(btn_Command);
                btn.CausesValidation = false;
            }

            // last button
            if ((CurrentPageIndex < TotalPages - 1) && ((Mode == PagerMode.FirstLast) || (Mode == PagerMode.FirstPreviousNextLast)))
            {
                if (cell.Controls.Count > 0)
                {
                    Literal lit = new Literal();
                    lit.Text = "&nbsp;";
                    cell.Controls.Add(lit);
                }

                LinkButton btn = new LinkButton();
                cell.Controls.Add(btn);
                if (LastImageUrl != string.Empty)
                    btn.Text = string.Format("<img src=\"{0}\" border=\"0\" alt=\"\" />", LastImageUrl);
                else
                    btn.Text = "&gt;&gt;|";
                btn.CommandArgument = (TotalPages - 1).ToString();
                btn.CommandName = "Page";
                btn.Command += new CommandEventHandler(btn_Command);
                btn.CausesValidation = false;
            }
        }

        void btn_Command(object sender, CommandEventArgs e)
        {
            CurrentPageIndex = int.Parse(e.CommandArgument.ToString());
        }
    }

    public enum PagerMode 
    {
        FirstLast,
        PreviousNext,
        FirstPreviousNextLast,
        None
    }
}