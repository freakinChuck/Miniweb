using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelExportHelper
{
    /// <summary>
    /// represents a column in the ExcelSheet
    /// </summary>
    /// <typeparam name="TData">the type of the Item</typeparam>
    /// <typeparam name="TWorksheet">the typeparam of the Worksheet</typeparam>
    public class ExcelColumn<TData, TWorksheet>
    {
        /// <summary>
        /// initializes a new item of ExcelColumn
        /// </summary>
        /// <param name="headertext">the HeaderText of the Column</param>
        /// <param name="valueFunction">the Function of the Value</param>
        /// <param name="worksheet">the Worksheet</param>
        public ExcelColumn(string headertext, Func<TData, object> valueFunction, ExcelWorksheetBase<TWorksheet> worksheet)
        {
            this.Worksheet = worksheet;
            this.HeaderText = headertext;
            this.ValueFunction = valueFunction;
        }
        /// <summary>
        /// the ExcelWorksheet
        /// </summary>
        public ExcelWorksheetBase<TWorksheet> Worksheet { get; private set; }
        /// <summary>
        /// the HeaderText
        /// </summary>
        public string HeaderText { get; private set; }
        /// <summary>
        /// the function to recieve the Value out of an Item
        /// </summary>
        public Func<TData, object> ValueFunction { get; private set; }

        private CellFormat headerFormat;
        /// <summary>
        /// the Format of the headerCell
        /// </summary>
        public CellFormat HeaderFormat
        {
            get
            {
                if (headerFormat == null)
                {
                    headerFormat = Worksheet.Workbook.DefaultHeaderFormat;
                }
                return headerFormat;
            }
        }

        private CellFormat cellFormat;  
        /// <summary>
        /// the Format of the ContentCells
        /// </summary>
        public CellFormat CellFormat
        { 
            get
            {
                if (cellFormat == null)
                {
                    cellFormat = Worksheet.Workbook.DefaultFormat;
                }
                return cellFormat;
            }
        }
    }

    /// <summary>
    /// represents a column in the ExcelSheet with both Types identical
    /// </summary>
    /// <typeparam name="T">the Type</typeparam>
    public class ExcelColumn<T> : ExcelColumn<T, T> // where T : class
    {
        /// <summary>
        /// initializes a new item of ExcelColumn
        /// </summary>
        /// <param name="headertext">the HeaderText of the Column</param>
        /// <param name="valueFunction">the Function of the Value</param>
        /// <param name="worksheet">the Worksheet</param>
        public ExcelColumn(string headertext, Func<T, object> valueFunction, ExcelWorksheetBase<T> worksheet)
            : base(headertext, valueFunction, worksheet)
        {

        }
    }
}
