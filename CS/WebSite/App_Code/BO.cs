using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

public class ManualDataSet : DataSet {
    public ManualDataSet()
        : base() {
        DataTable table = new DataTable("table");

        DataSetName = "ManualDataSet";

        table.Columns.Add("ID", typeof(Int32));
        table.Columns.Add("MyDateTime", typeof(DateTime));
        table.Columns.Add("MyRow", typeof(string));
        table.Columns.Add("MyData", typeof(double));
        table.Constraints.Add("IDPK", table.Columns["ID"], true);

        Tables.AddRange(new DataTable[] { table });
    }

    public static ManualDataSet CreateData() {
        ManualDataSet ds = new ManualDataSet();
        DataTable table = ds.Tables["table"];

        table.Rows.Add(new object[] { 0, DateTime.Today, "A", 103 });
        table.Rows.Add(new object[] { 1, DateTime.Today, "B", 200 });
        table.Rows.Add(new object[] { 2, DateTime.Today, "C", 446 });
        table.Rows.Add(new object[] { 3, DateTime.Today, "D", 323 });
        table.Rows.Add(new object[] { 4, DateTime.Today.AddDays(1), "A", 788 });
        table.Rows.Add(new object[] { 5, DateTime.Today.AddDays(1), "B", 787 });
        table.Rows.Add(new object[] { 6, DateTime.Today.AddDays(1), "C", 452 });
        table.Rows.Add(new object[] { 7, DateTime.Today.AddDays(1), "D", 137 });
        table.Rows.Add(new object[] { 8, DateTime.Today.AddDays(2), "A", 152 });
        table.Rows.Add(new object[] { 9, DateTime.Today.AddDays(2), "B", 565 });
        table.Rows.Add(new object[] { 10, DateTime.Today.AddDays(2), "C", 452 });
        table.Rows.Add(new object[] { 11, DateTime.Today.AddDays(2), "D", 125 });

        return ds;
    }

    #region Disable Serialization for Tables and Relations
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataTableCollection Tables {
        get { return base.Tables; }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataRelationCollection Relations {
        get { return base.Relations; }
    }
    #endregion Disable Serialization for Tables and Relations
}
