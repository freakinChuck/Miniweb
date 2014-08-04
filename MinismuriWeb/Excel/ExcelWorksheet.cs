using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ExcelExportHelper
{
    /// <summary>
    /// symbolizes an excelWorksheet
    /// </summary>
    /// <typeparam name="T">the DataType of the MainData</typeparam>
    /// <typeparam name="TValue">the Datatype of the DisplayData</typeparam>
    public class ExcelWorksheet<T, TValue> : ExcelWorksheetBase<T>
    {
        /// <summary>
        /// initializes a new ExcelWorksheet
        /// </summary>
        /// <param name="name">the Name of the Worksheet</param>
        /// <param name="workbook">the Workbook</param>
        /// <param name="selector">der selektor der ChildElemente</param>
        internal ExcelWorksheet(string name, ExcelWorkbook<T> workbook, Func<IEnumerable<T>, IEnumerable<TValue>> selector)
            : base(name, workbook)
        {
            Columns = new List<ExcelColumn<TValue, T>>();
            this.Selector = selector;
        }

        /// <summary>
        /// die Columns
        /// </summary>
        public List<ExcelColumn<TValue, T>> Columns { get; private set; }

        /// <summary>
        /// adds a new Column to the Worksheet
        /// </summary>
        /// <param name="headertext">the Headertext of the new Column</param>
        /// <param name="valueFunction">the function to recieve the cellContent from the Item</param>
        /// <returns>the added Cell</returns>
        public ExcelColumn<TValue, T> AddColumn(string headertext, Func<TValue, object> valueFunction)
        {
            var column = new ExcelColumn<TValue, T>(headertext, valueFunction, this);
            Columns.Add(column);
            return column;
        }

        /// <summary>
        /// der Selektor der anzuzeigenden Daten
        /// </summary>
        public Func<IEnumerable<T>, IEnumerable<TValue>> Selector { get; private set; }

        /// <summary>
        /// exportiert die Daten in das Excel
        /// </summary>
        /// <param name="excelWorkbook">das Workbook</param>
        /// <param name="data">die Daten</param>
        public override void Export(OfficeOpenXml.ExcelWorkbook excelWorkbook, IEnumerable<T> data)
        {
            int initialindex = 1;
            var excelSheet = excelWorkbook.Worksheets.Add(this.Name);

            int xIndex = initialindex;
            int yIndex = initialindex;
            foreach (var column in this.Columns)
            {
                var cell = excelSheet.Cells[yIndex, xIndex];
                cell.Value = column.HeaderText;
                CellFormat.Transfer(column.HeaderFormat, cell.Style);
                xIndex++;
            }
            // no columns when xIndex = initalIndex
            if (xIndex > initialindex)
            {
                var headerCellRange = excelSheet.Cells[yIndex, initialindex, yIndex, xIndex - 1];
                // strangly sets autofilter to true, even if you do 'cells.AutoFilter = false'
                if (this.AutoFilter)
                {
                    headerCellRange.AutoFilter = true;
                }
            }            

            yIndex++;
            xIndex = initialindex;
            foreach (var item in Selector(data))
            {
                foreach (var column in this.Columns)
                {
                    var cell = excelSheet.Cells[yIndex, xIndex];
                    cell.Value = column.ValueFunction(item);
                    CellFormat.Transfer(column.CellFormat, cell.Style);
                    xIndex++;
                }
                yIndex++;
                xIndex = initialindex;
            }
            if (this.AutoAdjustAllColumns)
            {
                excelSheet.Cells[excelSheet.Dimension.Address].AutoFitColumns();
            }
        }
    }

    /// <summary>
    /// symbolizes an ExcelWorksheet
    /// </summary>
    /// <typeparam name="T">the Type of the WorksheetData</typeparam>
    public class ExcelWorksheet<T> : ExcelWorksheet<T, T>
    {
        internal ExcelWorksheet(string name, ExcelWorkbook<T> workbook)
            : base(name, workbook, x => x)
        {

        }
        ///// <summary>
        ///// initializes a new ExcelWorksheet
        ///// </summary>
        ///// <param name="name">the Name of the Worksheet</param>
        ///// <param name="workbook">the Workbook</param>
        //internal ExcelWorksheet(string name, ExcelWorkbook<T> workbook)
        //    : base(name, workbook)
        //{
        //    Columns = new List<ExcelColumn<T>>();
            
        //}

        ///// <summary>
        ///// die Columns
        ///// </summary>
        //public List<ExcelColumn<T>> Columns { get; private set; }
        
        ///// <summary>
        ///// adds a new Column to the Worksheet
        ///// </summary>
        ///// <param name="headertext">the Headertext of the new Column</param>
        ///// <param name="valueFunction">the function to recieve the cellContent from the Item</param>
        ///// <returns>the added Cell</returns>
        //public ExcelColumn<T> AddColumn(string headertext, Func<T, object> valueFunction)
        //{
        //    var column = new ExcelColumn<T>(headertext, valueFunction, this);
        //    Columns.Add(column);
        //    return column;
        //}

        //public override void Export(OfficeOpenXml.ExcelWorkbook excelWorkbook, IEnumerable<object> data)
        //{
        //    int initialindex = 1;
        //    var excelSheet = excelWorkbook.Worksheets.Add(this.Name);

        //    int xIndex = initialindex;
        //    int yIndex = initialindex;
        //    foreach (var column in this.Columns)
        //    {
        //        var cell = excelSheet.Cells[yIndex, xIndex];
        //        cell.Value = column.HeaderText;
        //        CellFormat.Transfer(column.HeaderFormat, cell.Style);
        //        xIndex++;
        //    }

        //    var headerCellRange = excelSheet.Cells[yIndex, initialindex, yIndex, xIndex - 1];
        //    // strangly sets autofilter to true, even if you do 'cells.AutoFilter = false'
        //    if (this.AutoFilter)
        //    {
        //        headerCellRange.AutoFilter = true;
        //    }

        //    yIndex++;
        //    xIndex = initialindex;
        //    foreach (var item in data.OfType<T>())
        //    {
        //        foreach (var column in this.Columns)
        //        {
        //            var cell = excelSheet.Cells[yIndex, xIndex];
        //            cell.Value = column.ValueFunction(item);
        //            CellFormat.Transfer(column.CellFormat, cell.Style);
        //            xIndex++;
        //        }
        //        yIndex++;
        //        xIndex = initialindex;
        //    }
        //    if (this.AutoAdjustAllColumns)
        //    {
        //        excelSheet.Cells[excelSheet.Dimension.Address].AutoFitColumns();
        //    }
        //}
    }
}
