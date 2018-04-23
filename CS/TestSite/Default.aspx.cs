using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Collections;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Text;

public partial class _Default : System.Web.UI.Page 
{
    Record[] list;

    protected void Page_Load(object sender, EventArgs e)
    {
        list = new Record[ASPxGridView1.SettingsPager.PageSize];
        for (int i = 0; i < ASPxGridView1.SettingsPager.PageSize; i++)
        {
            list[i].id = -1;
            int vi = ASPxGridView1.VisibleStartIndex + i;
            ASPxTextBox txtBox1 = (ASPxTextBox)ASPxGridView1.FindRowCellTemplateControl(vi, ASPxGridView1.Columns["Num"] as GridViewDataColumn, "txtBox");
            if (txtBox1 == null) continue;
            try
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < Codes.Length; j++)
                {
                    string code = Codes.Substring(j, 1);
                    GridViewDataColumn col = ASPxGridView1.Columns[code] as GridViewDataColumn;
                    ASPxComboBox cbx = (ASPxComboBox)ASPxGridView1.FindRowCellTemplateControl(vi, col, "cbx_" + code);
                    if (Convert.ToBoolean(cbx.Value))
                    {
                        sb.Append(code);
                    }
                }
                list[i].id = Convert.ToInt32(ASPxGridView1.GetRowValues(vi, ASPxGridView1.KeyFieldName));
                list[i].num = Convert.ToInt32(txtBox1.Text);
                list[i].code = sb.ToString();
            }
            catch { }

        }
        BindGridToData(ASPxGridView1);
        ASPxGridView1.JSProperties["cpCodes"] = Codes;
    }

    private void BindGridToData(ASPxGridView grid)
    {
        DataTable dt = Session["DT"] as DataTable;
        if (dt == null)
        {
            dt = new DataTable();
            DataColumn col = dt.Columns.Add("ID", typeof(int));
            col.AutoIncrement = true;
            dt.Columns.Add("Num", typeof(int)).AllowDBNull= false;
            dt.Columns.Add("Code", typeof(string)).AllowDBNull = false;
            dt.PrimaryKey = new DataColumn[] { col };
            dt.Rows.Add(new object[] { null, 2, "BC" });
            dt.Rows.Add(new object[] { null, 2, "AD" });
            dt.Rows.Add(new object[] { null, 0, "" });
            dt.Rows.Add(new object[] { null, 0, "" });
            dt.Rows.Add(new object[] { null, 0, "" });
            Session["DT"] = dt;
        }
        grid.DataSource = dt;
        grid.DataBind();
    }

    const string Codes = "ABCD";

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        for (int i = 0; i < Codes.Length; i++)
        {
            string code = Codes.Substring(i,1);
            GridViewDataColumn col = ASPxGridView1.Columns[code] as GridViewDataColumn;
            if (col == null)
            {
                col = new GridViewDataColumn(code);
                ASPxGridView1.Columns.Add(col);
                col.DataItemTemplate = new ComboTemplate();
            }
        }
    }

    protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "post")
        {
            DataTable dt = Session["DT"] as DataTable;
            for (int i = 0; i < list.Length; i++)
            {
                DataRow row = dt.Rows.Find(list[i].id);
                if (row != null)
                {
                    row["Num"] = list[i].num;
                    row["Code"] = list[i].code;
                }
            }
            ASPxGridView1.DataBind();
        }
    }

    class ComboTemplate : ITemplate
    {
        public ComboTemplate()
        {

        }

        #region #ITemplateMembers

        public void InstantiateIn(Control container)
        {
            GridViewDataItemTemplateContainer gcontainer = (GridViewDataItemTemplateContainer)container;
            ASPxComboBox cbx = new ASPxComboBox();
            cbx.ID = "cbx_" + gcontainer.Column.FieldName;
            cbx.ValueType = typeof(bool);
            cbx.Items.Add("Yes", true);
            cbx.Items.Add("Blank", false);
            string code = DataBinder.Eval(gcontainer.DataItem, "Code") as string;
            bool value = string.IsNullOrEmpty(code) ? false : code.Contains(gcontainer.Column.FieldName);
            cbx.Value = value;
            cbx.Width = Unit.Pixel(80);
            int num = Convert.ToInt32(DataBinder.Eval(gcontainer.DataItem, "Num"));
            cbx.ClientEnabled = value || code.Length < num;
            cbx.ClientSideEvents.SelectedIndexChanged = "function(s,e){RefreshRow("+gcontainer.VisibleIndex+");}";
            cbx.ClientInstanceName = "cbx_" + gcontainer.Column.FieldName + gcontainer.VisibleIndex.ToString();
            gcontainer.Controls.Add(cbx);
        }

        #endregion #ITemplateMembers
    }

    struct Record
    {
        public int id;
        public int num;
        public string code;
    }
}
