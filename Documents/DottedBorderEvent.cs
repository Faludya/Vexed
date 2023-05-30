using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents
{
    public class DottedBorderEvent : IPdfPCellEvent
    {
        public void CellLayout(PdfPCell cell, Rectangle position, PdfContentByte[] canvases)
        {
            // Define the dash pattern for the dotted line
            float[] dashPattern = { 2f, 2f };

            // Create a dotted border using the dash pattern
            PdfContentByte canvas = canvases[PdfPTable.LINECANVAS];
            canvas.SetLineDash(dashPattern, 0);
            canvas.Rectangle(position.Left, position.Bottom, position.Width, position.Height);
            canvas.Stroke();
        }
    }
}
