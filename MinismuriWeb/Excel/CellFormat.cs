using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelExportHelper
{
    /// <summary>
    /// the format of an excel cell
    /// </summary>
    public class CellFormat
    {
        /// <summary>
        /// Font Weight Bold
        /// </summary>
        public bool Bold { get; set; }
        /// <summary>
        /// Font Style Italic
        /// </summary>
        public bool Italic { get; set; }
        /// <summary>
        /// Text underlined
        /// </summary>
        public bool Underline { get; set; }
        /// <summary>
        /// font size
        /// </summary>
        public float Size { get; set; }
        /// <summary>
        /// font color
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// transfers the CellFormat to the ExcelFormat
        /// </summary>
        /// <param name="format">the Format</param>
        /// <param name="cellStyle">the ExcelFormat</param>
        internal static void Transfer(CellFormat format, ExcelStyle cellStyle)
        {
            cellStyle.Font.Bold = format.Bold;
            cellStyle.Font.Italic = format.Italic;
            cellStyle.Font.UnderLine = format.Underline;
            cellStyle.Font.Size = format.Size;
            cellStyle.Font.Color.SetColor(format.Color);
        }
    }
}
