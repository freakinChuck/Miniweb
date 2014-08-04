using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelExportHelper
{
    /// <summary>
    /// die Basisklasse der Worksheets
    /// </summary>
    /// <typeparam name="T">dr Typ der DataItems</typeparam>
    public abstract class ExcelWorksheetBase<T>
    {
        /// <summary>
        /// initialisiert ein neues objekt der Basisklasse
        /// </summary>
        /// <param name="name">der Name</param>
        /// <param name="workbook">das Workbook</param>
        public ExcelWorksheetBase(string name, ExcelWorkbook<T> workbook)
        {
            this.Workbook = workbook;
            this.Name = name;
        }
        /// <summary>
        /// the Workbook containing this WorkSheet
        /// </summary>
        public ExcelWorkbook<T> Workbook { get; private set; }
        /// <summary>
        /// the Name of the Worksheet
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// enabled the Autofilter
        /// </summary>
        public bool AutoFilter { get; set; }
        /// <summary>
        /// AutoFit whole Worksheet
        /// </summary>
        public bool AutoAdjustAllColumns { get; set; }

        /// <summary>
        /// exportiert die Daten in das Excel
        /// </summary>
        /// <param name="excelWorkbook">das Workbook</param>
        /// <param name="data">die Daten</param>
        public abstract void Export(OfficeOpenXml.ExcelWorkbook excelWorkbook, IEnumerable<T> data);
    }
}
