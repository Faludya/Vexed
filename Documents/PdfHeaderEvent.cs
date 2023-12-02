using DataModels;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Documents
{
    public class PdfHeaderEvent : PdfPageEventHelper
    {
        private readonly Salary _salary;

        public PdfHeaderEvent(Salary salary)
        {
            _salary = salary;
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfGenerator pdfGenerator = new PdfGenerator(_salary, document, writer);
            pdfGenerator.Generate();
        }
    }

}
