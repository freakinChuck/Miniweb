using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelExportHelper
{
    /// <summary>
    /// represents an ExcelWorkbook
    /// </summary>
    /// <typeparam name="T">the Type of the Item</typeparam>
    public class ExcelWorkbook<T>
    {
        /// <summary>
        /// initializes a new ExcelWorkbook
        /// </summary>
        public ExcelWorkbook()
        {
            Worksheets = new List<ExcelWorksheetBase<T>>();
        }

        private CellFormat defaultFormat;
        /// <summary>
        /// the Default Format of the Document
        /// </summary>
        public CellFormat DefaultFormat
        {
            get
            {
                if (defaultFormat == null)
                {
                    defaultFormat = new CellFormat
                    {
                        Bold = false,
                        Color = Color.Black,
                        Italic = false,
                        Size = 11,
                        Underline = false
                    };
                }
                return defaultFormat;
            }
        }

        private CellFormat defaultHeaderFormat;
        /// <summary>
        /// the defaultFormat of the HeaderColumns
        /// </summary>
        public CellFormat DefaultHeaderFormat
        {
            get
            {
                if (defaultHeaderFormat == null)
                {
                    defaultHeaderFormat = new CellFormat
                    {
                        Bold = true,
                        Color = Color.Black,
                        Italic = false,
                        Size = 11,
                        Underline = false
                    };
                }
                return defaultHeaderFormat;
            }
        }

        /// <summary>
        /// the Worksheets
        /// </summary>
        public List<ExcelWorksheetBase<T>> Worksheets { get; private set; }

        /// <summary>
        /// adds a new Worksheet tho the document
        /// </summary>
        /// <param name="name">the Name of the Worksheet</param>
        /// <returns>the added worksheet object</returns>
        public ExcelWorksheet<T> AddWorksheet(string name)
        {
            ExcelWorksheet<T> worksheet = new ExcelWorksheet<T>(name, this);
            Worksheets.Add(worksheet);
            return worksheet;
        }

        /// <summary>
        /// adds a new Worksheet tho the document
        /// </summary>
        /// <param name="name">the Name of the Worksheet</param>
        /// <param name="selector">der selector der Values</param>
        /// <returns>the added worksheet object</returns>
        public ExcelWorksheet<T, TValue> AddWorksheet<TValue>(string name, Func<IEnumerable<T>, IEnumerable<TValue>> selector) where TValue : class
        {
            ExcelWorksheet<T, TValue> worksheet = new ExcelWorksheet<T, TValue>(name, this, selector);
            Worksheets.Add(worksheet);
            return worksheet;
        }
        

        /// <summary>
        /// exports the Workbook to an EPPlus-ExcelPackage with the Data from the datalist filled
        /// </summary>
        /// <param name="data">the DataLost</param>
        /// <returns>the Excelpackage</returns>
        public ExcelPackage Export(IEnumerable<T> data)
        {
            ExcelPackage package = new ExcelPackage();

            foreach (var worksheet in Worksheets)
            {
                worksheet.Export(package.Workbook, data);
            }

            return package;
        }
    }
}
