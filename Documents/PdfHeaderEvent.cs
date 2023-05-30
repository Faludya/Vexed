using DataModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Rectangle = iTextSharp.text.Rectangle;

namespace Documents
{
    public class PdfHeaderEvent : PdfPageEventHelper
    {
        private readonly Salary _salary;
        private readonly Font _HeaderFont;
        private readonly Font _TextFont;

        public PdfHeaderEvent(Salary salary)
        {
            _salary = salary;
            _HeaderFont = new Font(Font.FontFamily.TIMES_ROMAN, 30f, Font.BOLD);
            _TextFont = new Font(Font.FontFamily.TIMES_ROMAN, 14f, Font.NORMAL);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfGenerator pdfGenerator = new PdfGenerator(_salary, document, writer);
            pdfGenerator.Generate();
        }
    }

}
