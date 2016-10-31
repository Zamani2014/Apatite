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
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MyWebPagesStarterKit.Controls;
using MyWebPagesStarterKit;

public partial class SectionControls_EasyControl : SectionControlBaseClass
{
	private EasyControl _section;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			btnSave.Text = Resources.StringsRes.glb__Save;

			string easyControlSourceDir = Server.MapPath("~/EasyControls");

			if (!Directory.Exists(easyControlSourceDir))
				Directory.CreateDirectory(easyControlSourceDir);

			foreach (string control in Directory.GetFiles(easyControlSourceDir, "*.ascx"))
			{
				cmbControls.Items.Add(new ListItem(Path.GetFileName(control), "~/EasyControls/" + control.Substring(easyControlSourceDir.Length + 1)));
			}
			cmbControls.Items.Insert(0, new ListItem("-- choose --", string.Empty));

			EnsureID();
			valControlsRequired.ValidationGroup = ID;
			btnSave.ValidationGroup = ID;
		}
		addEasyControl();
	}

	protected void Page_PreRender(object sender, EventArgs e)
	{
		updateViews();
	}

	protected void btnSave_Click(object sender, EventArgs e)
	{
		_section.ControlName = cmbControls.SelectedValue;
		_section.SaveData();
	}

	protected void btnCancel_Click(object sender, EventArgs e)
	{
		cmbControls.Items[0].Selected = true;
		_section.ControlName = string.Empty;
		_section.SaveData();
	}

	public override ISection Section
	{
		set
		{
			if (value is EasyControl)
				_section = (EasyControl)value;
			else
				throw new ArgumentException("Section must be of type EasyControl");
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
			return "Documentation/" + lang + "/quick_guide.html#easy-control";
		}
	}

	private void updateViews()
	{
		if (ViewMode == ViewMode.Edit)
		{
			multiview.SetActiveView(editView);
			if (cmbControls.Items.FindByValue(_section.ControlName) != null)
				cmbControls.SelectedValue = _section.ControlName;
		}
		else
		{
			multiview.SetActiveView(readonlyView);
		}
	}

	private void addEasyControl()
	{
		phControlPlaceholder.Controls.Clear();

		if (_section.ControlName != string.Empty)
		{
			try
			{
				phControlPlaceholder.Controls.Add(LoadControl(_section.ControlName));
			}
			catch (Exception ex)
			{
				LiteralControl myLiteral = new LiteralControl("<p>The following error was thrown when trying to load an EasyControl: '" + ex.ToString().Trim() + "'.</p>");
				phControlPlaceholder.Controls.Add(myLiteral);
			}
		}
	}


}