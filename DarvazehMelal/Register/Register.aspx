<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register_Register" MasterPageFile="~/Site.master" %>
<%@ Register Assembly="Artem.Google" Namespace="Artem.Google.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="GoogleMap" Namespace="GoogleMap" TagPrefix="GoogleMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
    <script src="../App_Themes/DarvazehMelal/JS/jQuery/jquery-2.1.0.min.js"></script>
    <script language="javascript" type="text/javascript">
        function _HandlePositionChanged(sender, e) {
            var position = e.latLng || sender.markers[e.index].getPosition();
            document.getElementById('_latitude').innerHTML = position.lat();
            document.getElementById('_lng').innerHTML = position.lng();
        }

        $(document).ready(function () {
            $('#step3Btn').on('click', function () {
                window.open("TerminateRegistration.aspx?" + "lat=" + document.getElementById('_latitude').textContent + "&lng=" + document.getElementById('_lng').textContent + "&bid=" + '<%=BuildingIDLbl.Text %> ' + "");
                window.close();
            })
        });
</script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Image ID="Image11" runat="server" ImageUrl="~/Images/addfile_step1.png" />
        <fieldset>
        <legend>مشخصات مالک</legend>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="1" DisplayMode="List" HeaderText="لطفا به پیغام های زیر توجه فرمائید :"/>
                    <table style="width: 100%; height: 251px;">
                <tr>
                    <td>
                        &nbsp;<span style="color: #FF0000">*</span>
                        <asp:Label ID="Label1" runat="server" Text="کشور :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList5" runat="server" CssClass="aspcontrols">
                            <asp:ListItem>جمهوری اسلامی ایران</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td><span style="color: #FF0000">*</span>
                        <asp:Label ID="Label2" runat="server" Text="استان :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="aspcontrols" 
                            DataSourceID="SqlDataSource1" DataTextField="ProvinceName" 
                            DataValueField="ProvinceName">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                            SelectCommand="SELECT [ProvinceName] FROM [ProvinceTbl]">
                        </asp:SqlDataSource>
                    </td>
                    <td><span style="color: #FF0000">*</span>
                        <asp:Label ID="Label4" runat="server" Text="شهر :"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;<span style="color: #FF0000">*</span>
                        <asp:Label ID="Label5" runat="server" Text="منطقه :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                    <td><span style="color: #FF0000">*</span>
                        <asp:Label ID="Label6" runat="server" Text="نوع معامله :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="aspcontrols" 
                            DataSourceID="SqlDataSource3" DataTextField="TransactionName" 
                            DataValueField="TransactionName">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                            SelectCommand="SELECT [TransactionName] FROM [TransactionTypes]">
                        </asp:SqlDataSource>
                    </td>
                    <td><span style="color: #FF0000">*</span>
                        <asp:Label ID="Label7" runat="server" Text="نوع ملک :"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="aspcontrols" 
                            DataSourceID="SqlDataSource2" DataTextField="BuildingName" 
                            DataValueField="BuildingName">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                            SelectCommand="SELECT [BuildingName] FROM [BuildingTypes]">
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;<span style="color: #FF0000">*</span>
                        <asp:Label ID="Label8" runat="server" Text="نوع سند :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="aspcontrols" 
                            DataSourceID="SqlDataSource4" DataTextField="DocName" DataValueField="DocName">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                            SelectCommand="SELECT [DocName] FROM [DocTypes]"></asp:SqlDataSource>
                    </td>
                    <td><span style="color: #FF0000">*</span>
                        <asp:Label ID="Label9" runat="server" Text="نام مالک / ثبت کننده :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <span style="color: #FF0000">*</span>
                        <asp:Label ID="Label10" runat="server" Text="پست الکترونیکی :"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                        <asp:TextBox ID="TextBox5" runat="server" style="direction: ltr"></asp:TextBox>
                    </td>
                </tr>
                        <tr>
                            <td>
                            &nbsp;<span style="color: #FF0000">*</span>
                                <asp:Label ID="Label11" runat="server" Text="نشانی ملک :"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="TextBox6" runat="server" Width="425px"></asp:TextBox>
                            </td>
                            <td><span style="color: #FF0000">*</span>
                                <asp:Label ID="Label12" runat="server" Text="پلاک ملک :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox7" runat="server" style="direction: ltr"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            &nbsp;<span style="color: #FF0000">*</span>
                                <asp:Label ID="Label13" runat="server" Text="شماره تماس 1 :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox8" runat="server" style="direction: ltr"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="شماره تماس 2 :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox9" runat="server" style="direction: ltr"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label15" runat="server" Text="شماره همراه :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox10" runat="server" style="direction: ltr"></asp:TextBox>
                            </td>
                        </tr>
            </table>
            <hr />
             <br />
            <asp:Button ID="step1Btn" runat="server" Text="مرحله بعد" 
                style=" float:left" ValidationGroup="1" onclick="step1Btn_Click"/>
        </fieldset>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="وارد کردن نام شهر ضروری است ." ControlToValidate="TextBox2" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="وارد کردن نام منطقه ضروری است ." ControlToValidate="TextBox3" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="وارد کردن نام مالک / ثبت کننده ضروری است ." ControlToValidate="TextBox4" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="وارد کردن پست الکترونیکی ضروری است ." ControlToValidate="TextBox5" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="وارد کردن نشانی ملک ضروری است ." ControlToValidate="TextBox6" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="وارد کردن شماره پلاک ملک ضروری است ." ControlToValidate="TextBox7" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="وارد کردن شماره تماس 1 ضروری است ." ControlToValidate="TextBox8" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="لطفا نشانی پست الکترونیکی خود را بطور صحیح وارد نمائید ." ControlToValidate="TextBox5" ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$" ValidationGroup="1" Display="None"></asp:RegularExpressionValidator>
    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TextBox7" FilterType="Numbers" >
    </asp:FilteredTextBoxExtender>
    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TextBox8" FilterType="Numbers">
    </asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="TextBox10" FilterType="Numbers">
            </asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="TextBox9" FilterType="Numbers">
            </asp:FilteredTextBoxExtender>
        </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:Image ID="Image10" runat="server" ImageUrl="~/Images/addfile_step2.png" />
        <fieldset>
        <legend>مشخصات ملک و طبقات</legend>
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="2" DisplayMode="List" HeaderText="لطفا به پیغام های زیر توجه فرمائید :"/>
            <table style="width: 100%; height: 333px;">
                <tr>
                    <td>
                        &nbsp;<span style="color: #FF0000">*</span>
                        <asp:Label ID="Label16" runat="server" Text="متراژ بنا :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox15" runat="server" ValidationGroup="2" style="direction: ltr; text-align:center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label19" runat="server" Text="مساحت زمین (متر) :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox16" runat="server" ValidationGroup="2" style="direction: ltr; text-align:center"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;<span style="color: #FF0000">*</span>
                        <asp:Label ID="Label20" runat="server" Text="تعداد اتاق :"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                        <asp:DropDownList ID="DropDownList10" runat="server" CssClass="aspcontrols" style=" text-align:center">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;<span style="color: #FF0000">*</span>
                        <asp:Label ID="Label21" runat="server" Text="طبقه :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox17" runat="server" ValidationGroup="2" style="direction: ltr; text-align:center"></asp:TextBox>
                    </td>
                    <td><span style="color: #FF0000">*</span>
                        <asp:Label ID="Label22" runat="server" Text="تعداد طبقات :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox18" runat="server" ValidationGroup="2" style="direction: ltr; text-align:center"></asp:TextBox>
                    </td>
                    <td>
                        <span style="color: #FF0000">*</span>
                        <asp:Label ID="Label23" runat="server" Text="تعداد واحد در طبقه :"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                        <asp:TextBox ID="TextBox19" runat="server" ValidationGroup="2" style="direction: ltr; text-align:center"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                        <asp:Label ID="Label24" runat="server" Text="جمع واحدها :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox20" runat="server" style="direction: ltr; text-align:center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label25" runat="server" Text="نما :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList11" runat="server" 
                            DataSourceID="SqlDataSource9" DataTextField="ViewName" 
                            DataValueField="ViewName" CssClass="aspcontrols">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource9" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                            SelectCommand="SELECT [ViewName] FROM [ViewTypes]"></asp:SqlDataSource>
                    </td>
                    <td>
                        &nbsp;
                        <asp:Label ID="Label26" runat="server" Text="وضعیت سکونت :"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                        <asp:DropDownList ID="DropDownList12" runat="server" 
                            DataSourceID="SqlDataSource10" DataTextField="ResidenceName" 
                            DataValueField="ResidenceName" CssClass="aspcontrols">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource10" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                            SelectCommand="SELECT [ResidenceName] FROM [ResidenceTypes]">
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td> &nbsp;<span style="color: #FF0000">*</span>
                        <asp:Label ID="Label27" runat="server" Text="سن بنا :"></asp:Label>
                    </td>
                    <td >
                        <asp:RadioButton ID="RadioButton1" runat="server"  Text="  نوساز" GroupName="1" 
                            Checked="true" AutoPostBack="True" oncheckedchanged="RadioBtn1_CheckedChanged"/>
                        <asp:RadioButton ID="RadioButton2" runat="server" Text="  قدیمی"  
                            AutoPostBack="true" GroupName="1" oncheckedchanged="RadioBtn2_CheckedChanged"/>
                        </td>
                    <td colspan="1">
                        <asp:Label ID="Label3" runat="server" Text="چند ساله ؟" style=" padding-top:10px;"></asp:Label><br />
                        <asp:TextBox ID="TextBox1" runat="server" Enabled="false" Width="70" style="direction: ltr; text-align:center"></asp:TextBox><br /><br />
                        <asp:CheckBox ID="CheckBox5" runat="server" Text="  بازسازی شده" Enabled="false" AutoPostBack="true" />
                        </td>
                    <td>
                        <asp:Label ID="Label28" runat="server" Text="موقعیت جغرافیائی :"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:CheckBox ID="CheckBox6" runat="server" Text="  شمالی"/>
                        <asp:CheckBox ID="CheckBox7" runat="server" Text="  جنوبی"/>
                        <asp:CheckBox ID="CheckBox8" runat="server" Text="  شرقی"/>
                        <asp:CheckBox ID="CheckBox9" runat="server" Text="  غربی"/>
                    </td>
                </tr>
                <tr>
                    <td> &nbsp;
                        <asp:Label ID="Label29" runat="server" Text="توضیحات :"></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="TextBox21" runat="server" Width="690"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 100%;">
                <tr>
                    <td style="background-color: #FFCC66; padding:5px; text-align:center">
                        &nbsp;
                        <asp:Label ID="Label30" runat="server" Text=" کابینت آشپزخانه"></asp:Label>
                    </td>
                    <td style="background-color: #FFCC66; padding:5px; text-align:center">
                        <asp:Label ID="Label31" runat="server" Text=" سرویس بهداشتی"></asp:Label>
                    </td>
                    <td style="background-color: #FFCC66; padding:5px; text-align:center">
                        <asp:Label ID="Label32" runat="server" Text=" کف پوش"></asp:Label>
                    </td>
                    <td style="background-color: #FFCC66; padding:5px; text-align:center"><span style="color: #FF0000">*</span>
                        <asp:Label ID="Label33" runat="server" Text="قیمت متری"></asp:Label>
                    </td>
                    <td style="background-color: #FFCC66; padding:5px; text-align:center"><span style="color: #FF0000">*</span>
                        &nbsp;<asp:Label ID="Label34" runat="server" Text="قیمت کل"></asp:Label>
&nbsp;</td>
                    <td style="background-color: #FFCC66; padding:5px; text-align:center">
                        &nbsp;<asp:Label ID="Label35" runat="server" Text=" واحد پول"></asp:Label>
&nbsp;</td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF; padding:5px">
                        <asp:DropDownList ID="DropDownList6" runat="server" 
                            DataSourceID="SqlDataSource5" DataTextField="CabinetName" 
                            DataValueField="CabinetName" CssClass="aspcontrols" Width="120px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                            SelectCommand="SELECT [CabinetName] FROM [CabinetTypes]">
                        </asp:SqlDataSource>
                    </td>
                    <td style="background-color: #FFFFFF; padding:5px">
                        <asp:DropDownList ID="DropDownList7" runat="server" 
                            DataSourceID="SqlDataSource6" DataTextField="SanitaryName" 
                            DataValueField="SanitaryName" CssClass="aspcontrols" Width="120px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource6" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                            SelectCommand="SELECT [SanitaryName] FROM [SanitaryServicesTypes]">
                        </asp:SqlDataSource>
                    </td>
                    <td style="background-color: #FFFFFF; padding:5px">
                        <asp:DropDownList ID="DropDownList8" runat="server" 
                            DataSourceID="SqlDataSource7" DataTextField="FlooringName" 
                            DataValueField="FlooringName" CssClass="aspcontrols" Width="120px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource7" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                            SelectCommand="SELECT [FlooringName] FROM [FlooringTypes]">
                        </asp:SqlDataSource>
                    </td>
                    <td style="background-color: #FFFFFF; padding:5px">
                        <asp:TextBox ID="TextBox11" runat="server" Width="100px" ValidationGroup="2" style="direction: ltr; text-align:center"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF; padding:5px">
                        <asp:TextBox ID="TextBox12" runat="server" Width="100px" ValidationGroup="2" style="direction: ltr; text-align:center"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF; padding:5px">
                        <asp:DropDownList ID="DropDownList9" runat="server" 
                            DataSourceID="SqlDataSource8" DataTextField="CurrencyName" 
                            DataValueField="CurrencyName" CssClass="aspcontrols" Width="120px" style="text-align:center">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource8" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                            SelectCommand="SELECT [CurrencyName] FROM [CurrencyTypes]">
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFF99" colspan="1">
                        &nbsp;
                        <asp:Label ID="Label36" runat="server" Text="تعداد پارکینگ :"></asp:Label><br />
                        <asp:TextBox ID="TextBox13" runat="server" style="direction: ltr; text-align:center" Width="100px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFF99" colspan="1">
                        <asp:Label ID="Label37" runat="server" Text="تعداد تلفن :"></asp:Label><br />
                        <asp:TextBox ID="TextBox14" runat="server" style="direction: ltr; text-align:center" Width="100px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFF99;" colspan="1">
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="  انباری"/>
                        </td>
                    <td style="background-color: #FFFF99" colspan="1">
                        <asp:CheckBox ID="CheckBox2" runat="server" Text="  بالکن" />
                        </td>
                    <td style="background-color: #FFFF99" colspan="1">
                        <asp:CheckBox ID="CheckBox3" runat="server" Text="  شومینه" />
                        </td>
                    <td style="background-color: #FFFF99" colspan="1">
                        <asp:CheckBox ID="CheckBox4" runat="server" Text="  Open" />
                    </td>
                </tr>
            </table>
            </fieldset>
            <fieldset>
            <legend>امکانات ملک</legend>
                <table style="width: 100%; height: 203px;">
                    <tr>
                        <td>
                            &nbsp;
                            <asp:CheckBox ID="CheckBox10" runat="server" Text="  شوفاژ" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox11" runat="server" Text="  استخر" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox12" runat="server" Text="  آیفون تصویری"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox13" runat="server" Text="  حیاط خلوت"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox14" runat="server" Text="  پاسیو"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                            <asp:CheckBox ID="CheckBox15" runat="server" Text="  چیلر"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox16" runat="server" Text="  سونا"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox17" runat="server" Text="  دوربین مدار بسته"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox18" runat="server" Text="  فضای سبز"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox19" runat="server" Text="  نیمه مبله"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        &nbsp;
                            <asp:CheckBox ID="CheckBox20" runat="server" Text="  فن کوئل"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox21" runat="server" Text="  جکوزی"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox22" runat="server" Text="  درب ریموت"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox23" runat="server" Text="  لابی"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox24" runat="server" Text="  مبله"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                            <asp:CheckBox ID="CheckBox25" runat="server" Text="  پکیج"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox26" runat="server" Text="  آسانسور" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox27" runat="server" Text="  آنتن مرکزی"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox28" runat="server" Text="  سالن اجتماعات"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox29" runat="server" Text="  اطفاء حریق" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                            <asp:CheckBox ID="CheckBox30" runat="server" Text="  کولر" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox31" runat="server" Text="  باربیکیو" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox32" runat="server" Text="  اینترنت مرکزی" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox33" runat="server"  Text="  سرایداری"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox34" runat="server" Text="  شوتینگ زباله" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
            <legend>تصویر ضد بات</legend>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp;<span style="color: #FF0000">*</span>
                            <asp:Label ID="Label17" runat="server" Text="تصویر ضد بات :"></asp:Label>
                        </td>
                        <td>
                <asp:Image ID="imgAntiBotImage" runat="server" ImageUrl="~/antibotimage.ashx" GenerateEmptyAlternateText="true"/><br /><br />
                <asp:TextBox runat="server" ID="txtAntiBotImage" ValidationGroup="2" MaxLength="4" 
                    CssClass="textbox" Width="75px" style="direction: ltr; text-align:center;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:ImageButton ID="CAPTCHARefresh" runat="server" ImageUrl="~/Images/Button-Refresh32.png" OnClick="CAPTCHARefresh_Click"/>
                        </td>
                    </tr>
                </table>
                <br />
                <hr />
             <br />
                            <asp:Button ID="step2Btn" runat="server" ValidationGroup="2" 
                    Text="مرحله بعد" style=" float:left; " onclick="step2Btn_Click"/>
            </fieldset>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="وارد کردن قیمت متری ضروری است ." Display="None" ValidationGroup="2" ControlToValidate="TextBox11"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="وارد کردن قیمت کل ضروری است ." Display="None" ValidationGroup="2" ControlToValidate="TextBox12"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="وارد کردن تعداد واحد در طبقه ضروری است ." Display="None" ValidationGroup="2" ControlToValidate="TextBox19"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="وارد کردن تعداد طبقه ضروری است ." Display="None" ValidationGroup="2" ControlToValidate="TextBox18"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="وارد کردن شماره طبقه ضروری است ." Display="None" ValidationGroup="2" ControlToValidate="TextBox17"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="وارد کردن متراژ بنا ضروری است ." Display="None" ValidationGroup="2" ControlToValidate="TextBox15"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="وارد کردن تصویر ضد بات ضروری است ." Display="None" ValidationGroup="2" ControlToValidate="txtAntiBotImage"></asp:RequiredFieldValidator>

            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="TextBox15" FilterType="Numbers">
            </asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="TextBox17" FilterType="Numbers">
            </asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="TextBox18" FilterType="Numbers">
            </asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="TextBox19" FilterType="Numbers">
            </asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="TextBox16" FilterType="Numbers">
            </asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="TextBox20" FilterType="Numbers">
            </asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="TextBox1" FilterType="Numbers">
            </asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="TextBox11" FilterType="Numbers">
            </asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="TextBox12" FilterType="Numbers">
            </asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="TextBox13" FilterType="Numbers">
            </asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="TextBox14" FilterType="Numbers">
            </asp:FilteredTextBoxExtender>
        </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Image ID="Image12" runat="server" ImageUrl="~/Images/addfile_step3.png" />
        <fieldset>
        <legend>تصاویر ملک</legend>
            <table style="width: 100%;">
                <tr>
                    <td>
                        &nbsp;
                        <asp:Label ID="Label38" runat="server" Text="تصویر مورد نظر :"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="600px"/>
                        <br /><br /><asp:Label ID="Label40" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="UploadBtn" runat="server" Text="بالاگذاری" 
                            onclick="UploadBtn_Click"/>
                    </td>
                </tr>
            </table>
            <table style="width: 100%;">
                <tr>
                    <td>
                        &nbsp;
                        <asp:Image ID="Image1" runat="server" Width="100px" Height="100px"/>
                    </td>
                    <td>
                        &nbsp;
                        <asp:Image ID="Image2" runat="server" Width="100px" Height="100px"/>
                    </td>
                    <td>
                        &nbsp;
                        <asp:Image ID="Image3" runat="server" Width="100px" Height="100px"/>
                    </td>
                    <td>
                        &nbsp;
                        <asp:Image ID="Image4" runat="server" Width="100px" Height="100px"/>
                    </td>
                    <td>
                        &nbsp;
                        <asp:Image ID="Image5" runat="server" Width="100px" Height="100px"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                        <asp:Image ID="Image6" runat="server" Width="100px" Height="100px"/>
                    </td>
                    <td>
                        &nbsp;
                        <asp:Image ID="Image7" runat="server" Width="100px" Height="100px"/>
                    </td>
                    <td>
                        &nbsp;
                        <asp:Image ID="Image8" runat="server" Width="100px" Height="100px"/>
                    </td>
                    <td>
                        &nbsp;
                        <asp:Image ID="Image9" runat="server" Width="100px" Height="100px"/>
                    </td>
                </tr>
            </table>
            <br />
            <ul style=" list-style-type:disc; padding-right:20px">
            <li>جهت افزودن تصویر کافیست تصویر مورد نظر را انتخاب نموده و بر روی دکمه بالاگذاری کلیک کنید، تا تصویر به لیست اضافه شود.</li>
                <li>بهتر است تصویر اول نمای اصلی ساختمان باشد.</li>
            <li>شما امکان ارسال 9 عکس برای هر فایل را دارید.</li>
            <li>اندازه تصاویر ارسالی نباید از 800 در 600 بیشتر باشد.</li>
            <li>حجم تصاویر ارسالی نباید از 150 کیلو بایت بیشتر باشد.</li>
            </ul>
            <br />
            <div style="text-align:left">
                <asp:Button ID="ImagesReset" runat="server" Text="حذف کلیه تصاویر و بالاگذاری مجدد" OnClick="ImagesReset_Click"  />
            </div>
        </fieldset>
            <br />
        <fieldset>
        <legend>موقعیت جغرافیایی ملک</legend>
        <div style=" padding-left:10px; padding-right:10px">
        <cc1:GoogleMap ID="GoogleMap1" runat="server" Address="Iran" Zoom="5" MapType="Roadmap" Width="100%" EnableScrollWheelZoom="true"></cc1:GoogleMap>
        <cc1:GoogleMarkers ID="GoogleMarkers1" runat="server" TargetControlID="GoogleMap1" OnClientDragEnd="_HandlePositionChanged">
        <Markers>
        <cc1:Marker Address="Tehran" Title="! محل ملک خود را مشخص کنید"></cc1:Marker>
        </Markers>
        <MarkerOptions Draggable="true"></MarkerOptions>
        </cc1:GoogleMarkers>
            </div><br />
           <div style="text-align:center"> <table>
            <tr>
                   <td> &nbsp;<asp:Label ID="Label18" runat="server" Text="عرض جغرافیائی :  "> </asp:Label></td>
                   <td><span id="_latitude" class="geoPosition"  style=" text-align:left"></span></td>
                   <td>&nbsp;&nbsp<asp:Label ID="Label39" runat="server" Text="طول جغرافیائی :  "></asp:Label></td>
                   <td><span id="_lng" class="geoPosition"  style=" text-align:left"></span></td>
                   <td></td>
            </tr>
            </table></div>
            <asp:Label runat="server" Visible="false" ID="BuildingIDLbl"  ></asp:Label>
            <br />
            <ul style=" list-style-type:disc; padding-right:20px">
            <li>جهت افزودن موقعیت ملک خود کافیست ابتدا از ابزار بزرگنمایی نقشه استفاده نموده و سپس علامت بالون را بر روی مکانی که ملک شما قرار دارد درگ کنید.</li>
            <li>برای حرکت بر روی نقشه باید روی نقشه کلیک ماوس را نگه دارید و نقشه را به هر طرف که می خواهید بکشید.</li>
            <li>برای بزرگنمائی نقشه کافیست بر روی نقشه دوبار کلیک کنید و یا از ابزار بزرگنمائی در سمت چپ نقشه استفاده نمائید.</li>
            </ul>
            <br />
            <hr />
             <br />
            <input type="button" title="ثبت نقشه و پایان" id="step3Btn"  style=" float:left; " class="button" name="ثبت نقشه و پایان" value="ثبت نقشه و پایان" />
        </fieldset>
        </ContentTemplate>
        </asp:UpdatePanel>
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="step1Btn" />
    <asp:PostBackTrigger ControlID="step2Btn" />
    <asp:AsyncPostBackTrigger ControlID="CAPTCHARefresh" />
    <asp:AsyncPostBackTrigger ControlID="RadioButton1" />
    <asp:AsyncPostBackTrigger ControlID="RadioButton1" />
    <asp:PostBackTrigger ControlID="UploadBtn" />
    <asp:PostBackTrigger ControlID="ImagesReset" />
    </Triggers>
   </asp:UpdatePanel>
</asp:Content>