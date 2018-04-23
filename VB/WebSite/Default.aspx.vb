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
Imports DevExpress.XtraCharts
Imports System.Drawing
Imports System.Collections.Generic

Partial Public Class _Default
	Inherits System.Web.UI.Page

	#Region "Currently selected row and column in the ASPxPivotGrid"
	Public Property SelectedRowIndex() As Integer
		Get
			If Session("SelectedRowIndex") Is Nothing Then
				Session("SelectedRowIndex") = 0
			End If

			If Convert.ToInt32(Session("SelectedRowIndex")) >= ASPxPivotGrid1.RowCount Then
				Session("SelectedRowIndex") = ASPxPivotGrid1.RowCount - 1
			End If

			Return Convert.ToInt32(Session("SelectedRowIndex"))
		End Get
		Set(ByVal value As Integer)
			Dim result As Integer = value

			If result >= ASPxPivotGrid1.RowCount Then
				result = ASPxPivotGrid1.RowCount - 1
			End If

			Session("SelectedRowIndex") = result
		End Set
	End Property

	Public Property SelectedColumnIndex() As Integer
		Get
			If Session("SelectedColumnIndex") Is Nothing Then
				Session("SelectedColumnIndex") = 0
			End If

			If Convert.ToInt32(Session("SelectedColumnIndex")) >= ASPxPivotGrid1.ColumnCount Then
				Session("SelectedColumnIndex") = ASPxPivotGrid1.ColumnCount - 1
			End If

			Return Convert.ToInt32(Session("SelectedColumnIndex"))
		End Get
		Set(ByVal value As Integer)
			Dim result As Integer = value

			If result >= ASPxPivotGrid1.ColumnCount Then
				result = ASPxPivotGrid1.ColumnCount - 1
			End If

			Session("SelectedColumnIndex") = result
		End Set
	End Property
	#End Region

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		ASPxPivotGrid1.DataSource = ManualDataSet.CreateData().Tables(0)
		ASPxPivotGrid1.DataBind()

		If (Not IsPostBack) Then
			Session("selectedRowIndex") = 0
			Session("selectedColIndex") = 0

			ASPxPivotGrid1.RetrieveFields()
			ASPxPivotGrid1.Fields("MyData").Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
			ASPxPivotGrid1.Fields("MyRow").Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
			ASPxPivotGrid1.Fields("MyDateTime").Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea

			ASPxPivotGrid1.Fields("MyDateTime").ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime
			ASPxPivotGrid1.Fields("MyDateTime").ValueFormat.FormatString = "d"
			CType(WebChartControl1.Diagram, XYDiagram).AxisX.DateTimeOptions.Format = DateTimeFormat.ShortDate
		End If

		PrepareChartData()
		FillChart()
	End Sub

	Protected Sub ASPxPivotGrid1_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxPivotGrid.PivotGridCustomCallbackEventArgs)
		Dim parameters() As String = e.Parameters.Split(New Char() { "|"c })

		Me.SelectedRowIndex = Convert.ToInt32(parameters(0))
		Me.SelectedColumnIndex = Convert.ToInt32(parameters(1))

		PrepareChartData()
	End Sub

	Protected Sub ASPxPivotGrid1_CustomCellStyle(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxPivotGrid.PivotCustomCellStyleEventArgs)
		If hfDataSourceType.Value = "column" Then
			If e.ColumnIndex = Me.SelectedColumnIndex Then
				e.CellStyle.BackColor = Color.Yellow
			End If
		ElseIf hfDataSourceType.Value = "row" Then
			If e.RowIndex = Me.SelectedRowIndex Then
				e.CellStyle.BackColor = Color.Yellow
			End If
		End If

		If e.RowIndex = Me.SelectedRowIndex AndAlso e.ColumnIndex = Me.SelectedColumnIndex Then
			e.CellStyle.BackColor = Color.Red
		End If
	End Sub

	Private Sub PrepareChartData()
		If Session("points") Is Nothing Then
			Session("points") = New List(Of MySeriesPoint)()
		End If

		CType(Session("points"), List(Of MySeriesPoint)).Clear()

		If hfDataSourceType.Value = "column" Then

			For i As Integer = 0 To ASPxPivotGrid1.RowCount - 1
				Dim argument As String = Convert.ToString(ASPxPivotGrid1.GetFieldValue(ASPxPivotGrid1.Fields("MyRow"), i))

				If String.IsNullOrEmpty(argument) Then
					argument = "Grand Total"
				End If

				Dim value As Decimal = Convert.ToDecimal(ASPxPivotGrid1.GetCellValue(Me.SelectedColumnIndex, i))

				CType(Session("points"), List(Of MySeriesPoint)).Add(New MySeriesPoint(argument, Convert.ToDouble(value)))
			Next i
		Else

			For i As Integer = 0 To ASPxPivotGrid1.ColumnCount - 1
				Dim argument As String = Convert.ToString(ASPxPivotGrid1.GetFieldValue(ASPxPivotGrid1.Fields("MyDateTime"), i))

				If String.IsNullOrEmpty(argument) Then
					argument = "Grand Total"
				End If

				Dim value As Decimal = Convert.ToDecimal(ASPxPivotGrid1.GetCellValue(i, Me.SelectedRowIndex))

				CType(Session("points"), List(Of MySeriesPoint)).Add(New MySeriesPoint(argument, Convert.ToDouble(value)))
			Next i
		End If
	End Sub

	Private Sub FillChart()
		Dim points As List(Of MySeriesPoint) = CType(Session("points"), List(Of MySeriesPoint))

		WebChartControl1.Series(0).Points.Clear()
		For i As Integer = 0 To points.Count - 1
			WebChartControl1.Series(0).Points.Add(New SeriesPoint(points(i).Arg, New Double() { points(i).Val }))
		Next i
	End Sub

End Class


Public Class MySeriesPoint
	Private arg_Renamed As String

	Public Property Arg() As String
		Get
			Return arg_Renamed
		End Get
		Set(ByVal value As String)
			arg_Renamed = value
		End Set
	End Property

	Private val_Renamed As Double

	Public Property Val() As Double
		Get
			Return val_Renamed
		End Get
		Set(ByVal value As Double)
			val_Renamed = value
		End Set
	End Property

	Public Sub New(ByVal arg As String, ByVal val As Double)
		Me.Arg = arg
		Me.Val = val
	End Sub
End Class
