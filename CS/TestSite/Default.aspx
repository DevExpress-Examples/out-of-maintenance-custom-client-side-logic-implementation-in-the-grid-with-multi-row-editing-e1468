<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Custom client-side logic implementation in the grid with multi-row editing</title>
    <script type="text/javascript">
        function RefreshRow(vi) {
            var tbNum = eval('tbNum' + vi);
            var num = tbNum.GetText();
            var codes = grid.cpCodes;
            var used = 0;
            var cboxes = new Array(codes.length);
            for (var i = 0; i < codes.length; i++) {
                var cbx = eval('cbx_' + codes.charAt(i) + vi);
                cboxes[i] = cbx;
                if (cbx.GetValue()) used++;
            }
            for (var i = 0; i < codes.length; i++) {
                var cbx = cboxes[i];
                cbx.SetEnabled(cbx.GetValue() || (used < num));
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False"
                KeyFieldName="ID" Width="599px" ClientInstanceName="grid" OnCustomCallback="ASPxGridView1_CustomCallback">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="ID" ReadOnly="True" VisibleIndex="0">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Num" VisibleIndex="1">
                        <DataItemTemplate>
                            <dx:ASPxTextBox ID="txtBox" Width="100px" runat="server" OnInit="txtBox_Init"></dx:ASPxTextBox>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
            &nbsp;&nbsp;&nbsp;
        <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" Text="Post Modifications" Width="217px">
            <ClientSideEvents Click="function(s, e) {
	grid.PerformCallback('post');
}" />
        </dx:ASPxButton>
        </div>
    </form>
</body>
</html>
