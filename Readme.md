<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128575775/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1618)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [BO.cs](./CS/WebSite/App_Code/BO.cs) (VB: [BO.vb](./VB/WebSite/App_Code/BO.vb))
* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
<!-- default file list end -->
# How to update a WebChartControl based on the selected data in the ASPxPivotGrid
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e1618/)**
<!-- run online end -->


<p>This example demonstrates how to display a series in the WebChartControl based on a selected column/row in the ASPxPivotGrid.</p><p>The most important features:</p><p>1) Once a user somehow modified the ASPxPivotGrid (e. g. sorted or filtered its data) a callback is raised. Then the selected data is handled on the server and displayed in the WebChartControl.<br />
2) The color of ASPxPivotGrid cells is customized in the server ASPxPivotGrid.CustomCellStyle event handler.<br />
3) A callback for the ASPxPivotGrid is initiated via the client ASPxClientPivotGrid.PerformCallback() method called from the ASPxClientPivotGrid.CellClick event handler. The e.RowIndex and e.ColumnIndex parameters of this event are persisted in JS variables and then passed to the ASPxClientPivotGrid.PerformCallback() method.<br />
4) A callback for the WebChartControl is initiated via the client ASPxClientWebChartControl.PerformCallback() method called from the ASPxClientPivotGrid.AfterCallback event handler. This will update the WebChartControl each time the ASPxPivotGrid is updated.<br />
5) Both the WebChartControl.EnableViewState and WebChartControl.SaveStateOnCallbacks properties are set to false, because the chart's data is populated at runtime.</p>

<br/>


