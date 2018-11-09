Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic
Imports System.Collections
Imports DevExpress.Web
Imports System.Text

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Private list() As Record

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		list = New Record(ASPxGridView1.SettingsPager.PageSize - 1){}
		For i As Integer = 0 To ASPxGridView1.SettingsPager.PageSize - 1
			list(i).id = -1
			Dim vi As Integer = ASPxGridView1.VisibleStartIndex + i
			Dim txtBox1 As ASPxTextBox = CType(ASPxGridView1.FindRowCellTemplateControl(vi, TryCast(ASPxGridView1.Columns("Num"), GridViewDataColumn), "txtBox"), ASPxTextBox)
			If txtBox1 Is Nothing Then
				Continue For
			End If
			Try
				Dim sb As New StringBuilder()
				For j As Integer = 0 To Codes.Length - 1
					Dim code As String = Codes.Substring(j, 1)
					Dim col As GridViewDataColumn = TryCast(ASPxGridView1.Columns(code), GridViewDataColumn)
					Dim cbx As ASPxComboBox = CType(ASPxGridView1.FindRowCellTemplateControl(vi, col, "cbx_" & code), ASPxComboBox)
					If Convert.ToBoolean(cbx.Value) Then
						sb.Append(code)
					End If
				Next j
				list(i).id = Convert.ToInt32(ASPxGridView1.GetRowValues(vi, ASPxGridView1.KeyFieldName))
				list(i).num = Convert.ToInt32(txtBox1.Text)
				list(i).code = sb.ToString()
			Catch
			End Try

		Next i
		BindGridToData(ASPxGridView1)
		ASPxGridView1.JSProperties("cpCodes") = Codes
	End Sub

	Private Sub BindGridToData(ByVal grid As ASPxGridView)
		Dim dt As DataTable = TryCast(Session("DT"), DataTable)
		If dt Is Nothing Then
			dt = New DataTable()
			Dim col As DataColumn = dt.Columns.Add("ID", GetType(Integer))
			col.AutoIncrement = True
			dt.Columns.Add("Num", GetType(Integer)).AllowDBNull= False
			dt.Columns.Add("Code", GetType(String)).AllowDBNull = False
			dt.PrimaryKey = New DataColumn() { col }
			dt.Rows.Add(New Object() { Nothing, 2, "BC" })
			dt.Rows.Add(New Object() { Nothing, 2, "AD" })
			dt.Rows.Add(New Object() { Nothing, 0, "" })
			dt.Rows.Add(New Object() { Nothing, 0, "" })
			dt.Rows.Add(New Object() { Nothing, 0, "" })
			Session("DT") = dt
		End If
		grid.DataSource = dt
		grid.DataBind()
	End Sub

	Private Const Codes As String = "ABCD"

	Protected Overrides Sub OnInit(ByVal e As EventArgs)
		MyBase.OnInit(e)
		For i As Integer = 0 To Codes.Length - 1
			Dim code As String = Codes.Substring(i,1)
			Dim col As GridViewDataColumn = TryCast(ASPxGridView1.Columns(code), GridViewDataColumn)
			If col Is Nothing Then
				col = New GridViewDataColumn(code)
				ASPxGridView1.Columns.Add(col)
				col.DataItemTemplate = New ComboTemplate()
			End If
		Next i
	End Sub

	Protected Sub ASPxGridView1_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
		If e.Parameters = "post" Then
			Dim dt As DataTable = TryCast(Session("DT"), DataTable)
			For i As Integer = 0 To list.Length - 1
				Dim row As DataRow = dt.Rows.Find(list(i).id)
				If row IsNot Nothing Then
					row("Num") = list(i).num
					row("Code") = list(i).code
				End If
			Next i
			ASPxGridView1.DataBind()
		End If
	End Sub

	Private Class ComboTemplate
		Implements ITemplate
		Public Sub New()

		End Sub

		#Region "ITemplate Members"

		Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
			Dim gcontainer As GridViewDataItemTemplateContainer = CType(container, GridViewDataItemTemplateContainer)
			Dim cbx As New ASPxComboBox()
			cbx.ID = "cbx_" & gcontainer.Column.FieldName
			cbx.ValueType = GetType(Boolean)
			cbx.Items.Add("Yes", True)
			cbx.Items.Add("Blank", False)
			Dim code As String = TryCast(DataBinder.Eval(gcontainer.DataItem, "Code"), String)
			Dim value As Boolean
			If String.IsNullOrEmpty(code) Then
				value = False
			Else
				value = code.Contains(gcontainer.Column.FieldName)
			End If
			cbx.Value = value
			cbx.Width = Unit.Pixel(80)
			Dim num As Integer = Convert.ToInt32(DataBinder.Eval(gcontainer.DataItem, "Num"))
			cbx.ClientEnabled = value OrElse code.Length < num
			cbx.ClientSideEvents.SelectedIndexChanged = "function(s,e){RefreshRow(" & gcontainer.VisibleIndex & ");}"
			cbx.ClientInstanceName = "cbx_" & gcontainer.Column.FieldName & gcontainer.VisibleIndex.ToString()
			gcontainer.Controls.Add(cbx)
		End Sub

		#End Region
	End Class

	Private Structure Record
		Public id As Integer
		Public num As Integer
		Public code As String
	End Structure
End Class
