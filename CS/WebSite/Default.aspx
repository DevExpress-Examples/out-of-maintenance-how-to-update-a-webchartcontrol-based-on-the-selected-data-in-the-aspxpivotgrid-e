<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dxwpg" %>
<%@ Register Assembly="DevExpress.XtraCharts.v15.1.Web, Version=15.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v15.1, Version=15.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    
    <script language="javascript" type="text/javascript">
    
    var currentRow = 0;
    var currentColumn = 0;

    function UpdatePivotGridContent()
    {
        pivotGrid.PerformCallback(currentRow.toString() + '|' + currentColumn.toString());
    }
    
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <dxe:ASPxRadioButton ID="ASPxRadioButton1" runat="server" Text="Use column" Checked="True"
                    GroupName="grp1">
                    <ClientSideEvents CheckedChanged="function(s, e) {
	if (s.GetChecked())
	{
		document.getElementById('hfDataSourceType').value = &quot;column&quot;;
		UpdatePivotGridContent();
	}
}" />
                </dxe:ASPxRadioButton>
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxRadioButton ID="ASPxRadioButton2" runat="server" Text="Use row" GroupName="grp1">
                    <ClientSideEvents CheckedChanged="function(s, e) {
	if (s.GetChecked())
	{
		document.getElementById('hfDataSourceType').value = &quot;row&quot;;
		UpdatePivotGridContent();
	}
}" />
                </dxe:ASPxRadioButton>
            </td>
        </tr>
    </table>

                <dxwpg:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server" 
                    OnCustomCellStyle="ASPxPivotGrid1_CustomCellStyle" CellStyle-Cursor="pointer" 
                    ClientInstanceName="pivotGrid" 
        oncustomcallback="ASPxPivotGrid1_CustomCallback">
                    <Styles>
                        <CellStyle Cursor="pointer">
                        </CellStyle>
                    </Styles>
                    <ClientSideEvents CellClick="function(s, e) {
	currentRow = e.RowIndex;
	currentColumn = e.ColumnIndex;

	UpdatePivotGridContent();
}" AfterCallback="function(s, e) {
	    chart.PerformCallback('update');
}" />
                </dxwpg:ASPxPivotGrid>
          
            <br />
                
                <dxchartsui:WebChartControl ID="WebChartControl1" runat="server" ClientInstanceName="chart"
                     Height="261px" Width="580px" 
         EnableViewState="False" 
        SaveStateOnCallbacks="False">
                    <DiagramSerializable>
<cc1:XYDiagram>
                        <axisx visibleinpanesserializable="-1">
<range sidemarginsenabled="True"></range>
</axisx>
                        <axisy visibleinpanesserializable="-1">
<range sidemarginsenabled="True"></range>
</axisy>
                    </cc1:XYDiagram>
</DiagramSerializable>
                    <FillStyle >
                        <OptionsSerializable>
<cc1:SolidFillOptions HiddenSerializableString="to be serialized" />
</OptionsSerializable>
                    </FillStyle>
                    <SeriesSerializable>
                        <cc1:Series  Name="Series 1"
                             >
                            <Points>
<cc1:SeriesPoint Values="0.2" ArgumentSerializable="1"></cc1:SeriesPoint>
<cc1:SeriesPoint Values="0.6" ArgumentSerializable="2"></cc1:SeriesPoint>
<cc1:SeriesPoint Values="0.4" ArgumentSerializable="3"></cc1:SeriesPoint>
<cc1:SeriesPoint Values="0.5" ArgumentSerializable="4"></cc1:SeriesPoint>
</Points>
                            <ViewSerializable>
<cc1:LineSeriesView HiddenSerializableString="to be serialized"></cc1:LineSeriesView>
</ViewSerializable>
                            <LabelSerializable>
<cc1:PointSeriesLabel HiddenSerializableString="to be serialized" LineVisible="True">
                            </cc1:PointSeriesLabel>
</LabelSerializable>
                            <PointOptionsSerializable>
<cc1:PointOptions HiddenSerializableString="to be serialized"></cc1:PointOptions>
</PointOptionsSerializable>
                            <LegendPointOptionsSerializable>
<cc1:PointOptions HiddenSerializableString="to be serialized"></cc1:PointOptions>
</LegendPointOptionsSerializable>
                        </cc1:Series>
                    </SeriesSerializable>
                    <SeriesTemplate  
                        >
                        <ViewSerializable>
<cc1:SideBySideBarSeriesView HiddenSerializableString="to be serialized">
                        </cc1:SideBySideBarSeriesView>
</ViewSerializable>
                        <LabelSerializable>
<cc1:SideBySideBarSeriesLabel HiddenSerializableString="to be serialized" LineVisible="True">
                        </cc1:SideBySideBarSeriesLabel>
</LabelSerializable>
                        <PointOptionsSerializable>
<cc1:PointOptions HiddenSerializableString="to be serialized">
                        </cc1:PointOptions>
</PointOptionsSerializable>
                        <LegendPointOptionsSerializable>
<cc1:PointOptions HiddenSerializableString="to be serialized">
                        </cc1:PointOptions>
</LegendPointOptionsSerializable>
                    </SeriesTemplate>
                </dxchartsui:WebChartControl>
                
    <asp:HiddenField ID="hfDataSourceType" runat="server" Value="column" />
    <%--<asp:HiddenField ID="hfShouldUpdateChart" runat="server" Value="false" />--%>
    </form>
</body>
</html>
