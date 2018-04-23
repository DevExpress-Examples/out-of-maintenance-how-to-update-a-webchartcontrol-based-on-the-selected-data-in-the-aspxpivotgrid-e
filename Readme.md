# How to update a WebChartControl based on the selected data in the ASPxPivotGrid


<p>This example demonstrates how to display a series in the WebChartControl based on a selected column/row in the ASPxPivotGrid.</p><p>The most important features:</p><p>1) Once a user somehow modified the ASPxPivotGrid (e. g. sorted or filtered its data) a callback is raised. Then the selected data is handled on the server and displayed in the WebChartControl.<br />
2) The color of ASPxPivotGrid cells is customized in the server ASPxPivotGrid.CustomCellStyle event handler.<br />
3) A callback for the ASPxPivotGrid is initiated via the client ASPxClientPivotGrid.PerformCallback() method called from the ASPxClientPivotGrid.CellClick event handler. The e.RowIndex and e.ColumnIndex parameters of this event are persisted in JS variables and then passed to the ASPxClientPivotGrid.PerformCallback() method.<br />
4) A callback for the WebChartControl is initiated via the client ASPxClientWebChartControl.PerformCallback() method called from the ASPxClientPivotGrid.AfterCallback event handler. This will update the WebChartControl each time the ASPxPivotGrid is updated.<br />
5) Both the WebChartControl.EnableViewState and WebChartControl.SaveStateOnCallbacks properties are set to false, because the chart's data is populated at runtime.</p>

<br/>


