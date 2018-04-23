using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.XtraCharts;
using System.Drawing;
using System.Collections.Generic;

public partial class _Default : System.Web.UI.Page {

    #region Currently selected row and column in the ASPxPivotGrid
    public int SelectedRowIndex {
        get {
            if(Session["SelectedRowIndex"] == null) {
                Session["SelectedRowIndex"] = 0;
            }

            if (Convert.ToInt32(Session["SelectedRowIndex"]) >= ASPxPivotGrid1.RowCount)
                Session["SelectedRowIndex"] = ASPxPivotGrid1.RowCount - 1;

            return Convert.ToInt32(Session["SelectedRowIndex"]);
        }
        set {
            int result = value;

            if(result >= ASPxPivotGrid1.RowCount)
                result = ASPxPivotGrid1.RowCount - 1;

            Session["SelectedRowIndex"] = result;
        }
    }

    public int SelectedColumnIndex {
        get {
            if(Session["SelectedColumnIndex"] == null) {
                Session["SelectedColumnIndex"] = 0;
            }

            if (Convert.ToInt32(Session["SelectedColumnIndex"]) >= ASPxPivotGrid1.ColumnCount)
                Session["SelectedColumnIndex"] = ASPxPivotGrid1.ColumnCount - 1;

            return Convert.ToInt32(Session["SelectedColumnIndex"]);
        }
        set {
            int result = value;

            if(result >= ASPxPivotGrid1.ColumnCount)
                result = ASPxPivotGrid1.ColumnCount - 1;

            Session["SelectedColumnIndex"] = result;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e) {
        ASPxPivotGrid1.DataSource = ManualDataSet.CreateData().Tables[0];
        ASPxPivotGrid1.DataBind();

        if(!IsPostBack) {
            Session["selectedRowIndex"] = 0;
            Session["selectedColIndex"] = 0;

            ASPxPivotGrid1.RetrieveFields();
            ASPxPivotGrid1.Fields["MyData"].Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            ASPxPivotGrid1.Fields["MyRow"].Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            ASPxPivotGrid1.Fields["MyDateTime"].Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;

            ASPxPivotGrid1.Fields["MyDateTime"].ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            ASPxPivotGrid1.Fields["MyDateTime"].ValueFormat.FormatString = "d";
            ((XYDiagram)WebChartControl1.Diagram).AxisX.DateTimeOptions.Format = DateTimeFormat.ShortDate;
        }

        PrepareChartData();
        FillChart();
    }

    protected void ASPxPivotGrid1_CustomCallback(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomCallbackEventArgs e) {
        string[] parameters = e.Parameters.Split(new char[] { '|' });

        this.SelectedRowIndex = Convert.ToInt32(parameters[0]);
        this.SelectedColumnIndex = Convert.ToInt32(parameters[1]);

        PrepareChartData();
    }

    protected void ASPxPivotGrid1_CustomCellStyle(object sender, DevExpress.Web.ASPxPivotGrid.PivotCustomCellStyleEventArgs e) {
        if(hfDataSourceType.Value == "column") {
            if(e.ColumnIndex == this.SelectedColumnIndex)
                e.CellStyle.BackColor = Color.Yellow;
        }
        else if(hfDataSourceType.Value == "row") {
            if(e.RowIndex == this.SelectedRowIndex)
                e.CellStyle.BackColor = Color.Yellow;
        }

        if(e.RowIndex == this.SelectedRowIndex && e.ColumnIndex == this.SelectedColumnIndex)
            e.CellStyle.BackColor = Color.Red;
    }

    private void PrepareChartData() {
        if(Session["points"] == null)
            Session["points"] = new List<MySeriesPoint>();

        ((List<MySeriesPoint>)Session["points"]).Clear();

        if(hfDataSourceType.Value == "column") {

            for(int i = 0; i < ASPxPivotGrid1.RowCount; i++) {
                string argument = Convert.ToString(ASPxPivotGrid1.GetFieldValue(ASPxPivotGrid1.Fields["MyRow"], i));

                if(string.IsNullOrEmpty(argument))
                    argument = "Grand Total";

                decimal value = Convert.ToDecimal(ASPxPivotGrid1.GetCellValue(this.SelectedColumnIndex, i));

                ((List<MySeriesPoint>)Session["points"]).Add(new MySeriesPoint(argument, Convert.ToDouble(value)));
            }
        }
        else {

            for(int i = 0; i < ASPxPivotGrid1.ColumnCount; i++) {
                string argument = Convert.ToString(ASPxPivotGrid1.GetFieldValue(ASPxPivotGrid1.Fields["MyDateTime"], i));

                if(string.IsNullOrEmpty(argument))
                    argument = "Grand Total";

                decimal value = Convert.ToDecimal(ASPxPivotGrid1.GetCellValue(i, this.SelectedRowIndex));

                ((List<MySeriesPoint>)Session["points"]).Add(new MySeriesPoint(argument, Convert.ToDouble(value)));
            }
        }
    }

    private void FillChart() {
        List<MySeriesPoint> points = (List<MySeriesPoint>)Session["points"];

        WebChartControl1.Series[0].Points.Clear();
        for(int i = 0; i < points.Count; i++)
            WebChartControl1.Series[0].Points.Add(new SeriesPoint(points[i].Arg, new double[] { points[i].Val }));
    }

}


public class MySeriesPoint {
    private string arg;

    public string Arg {
        get { return arg; }
        set { arg = value; }
    }

    private double val;

    public double Val {
        get { return val; }
        set { val = value; }
    }

    public MySeriesPoint(string arg, double val) {
        this.Arg = arg;
        this.Val = val;
    }
}
