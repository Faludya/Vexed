using DataModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Rectangle = iTextSharp.text.Rectangle;

namespace Documents
{
    public class PdfGenerator
    {
        private readonly Document document;
        private readonly PdfWriter writer;
        private readonly Salary salary;
        private readonly Font _HeaderFont;
        private readonly Font _TextFont;
        private readonly Font _TextBoldFont;

        public PdfGenerator(Salary salary, Document document, PdfWriter writer)
        {
            this.salary = salary;
            this.document = document;
            this.writer = writer;
            _HeaderFont = new Font(Font.FontFamily.TIMES_ROMAN, 30f, Font.BOLD);
            _TextFont = new Font(Font.FontFamily.TIMES_ROMAN, 14f, Font.NORMAL);
            _TextBoldFont = new Font(Font.FontFamily.TIMES_ROMAN, 14f, Font.BOLD);
        }

        public void Generate()
        {
            float margin = 36f; // Adjust the margin as needed (in points)
            float pageWidth = document.PageSize.Width;
            float pageHeight = document.PageSize.Height;
            float borderOffset = 10f; // Adjust the border offset as needed (in points)

            // Add the company name cell with a light purple background and margins
            PdfPTable outerTable = new PdfPTable(1);
            outerTable.TotalWidth = pageWidth - 2 * (margin + borderOffset);

            // Create the cell for the company name
            PdfPCell companyName = new PdfPCell(new Phrase(salary.Company, _HeaderFont));
            companyName.Border = Rectangle.NO_BORDER;
            companyName.HorizontalAlignment = Element.ALIGN_LEFT;
            companyName.Padding = 10f; // Adjust the padding as needed (in points)
            companyName.BackgroundColor = new BaseColor(200, 191, 231); // Light purple

            outerTable.AddCell(companyName);

            // Calculate the Y position for the outer table
            float outerTableY = pageHeight - margin - borderOffset - outerTable.TotalHeight / 6;

            // Set the Y position of the outer table
            outerTable.WriteSelectedRows(0, -1, margin + borderOffset, outerTableY, writer.DirectContent);

            // Add the dotted border to the entire document
            PdfContentByte content = writer.DirectContent;
            content.SetLineDash(3, 3);
            content.Rectangle(margin, margin, pageWidth - 2 * margin, pageHeight - 2 * margin);
            content.Stroke();

            // Add the salary slip details below the company name
            float detailsY = outerTableY - 60f; // Adjust the vertical position of the details as needed
            float detailsX = margin + borderOffset + 10f; // Adjust the horizontal position of the details as needed

            ColumnText ct = new ColumnText(writer.DirectContent);
            ct.SetSimpleColumn(detailsX, detailsY, pageWidth - margin - borderOffset, margin + borderOffset);

            //trust me
            ct.AddElement(new Paragraph($"Serial Number:          S-{salary.Id}", _TextFont));
            ct.AddElement(new Paragraph($"Generated Date:        {salary.GeneratedDate.ToString("dd/MM/yyyy")}", _TextFont));
            ct.AddElement(new Paragraph($"Employee Name:      {salary.FullName}", _TextFont));
            ct.AddElement(new Paragraph($"Function:                   {salary.Function}", _TextFont));
            ct.AddElement(new Paragraph($"Department:              {salary.Department}", _TextFont));

            ct.Go();

            GenerateBody(margin, borderOffset, outerTable.TotalHeight + margin + borderOffset);
        }

        public void GenerateBody(float margin, float borderOffset, float startY)
        {
            // Create a table with two columns
            PdfPTable mainTable = new PdfPTable(4);
            mainTable.TotalWidth = document.PageSize.Width - 2 * (margin + borderOffset);
            mainTable.LockedWidth = true;
            mainTable.SpacingBefore = 20f;
            mainTable.HorizontalAlignment = Element.ALIGN_CENTER;

            // Create the merged cell for the title
            PdfPCell titleCell = new PdfPCell(new Phrase("Salary Slip", new Font(Font.FontFamily.TIMES_ROMAN, 16f, Font.BOLD)));
            titleCell.Colspan = 4;
            titleCell.HorizontalAlignment = Element.ALIGN_CENTER;
            titleCell.Padding = 10f;
            titleCell.BackgroundColor = new BaseColor(200, 191, 231); // Light purple

            // Add the title cell to the main table
            mainTable.AddCell(titleCell);

            // row 1
            mainTable.AddCell(new PdfPCell(new Phrase("Gross Salary:", _TextBoldFont)));
            mainTable.AddCell(new PdfPCell(new Phrase(salary.GrossSalary.ToString() + "$", _TextFont)));

            mainTable.AddCell(new PdfPCell(new Phrase("Health Insurance Tax (" + salary.HealthInsuranceTax + "%) :", _TextFont))); ;
            mainTable.AddCell(new PdfPCell(new Phrase(salary.HealthInsuranceValue.ToString() + "$", _TextFont)));

            //row 2
            mainTable.AddCell(new PdfPCell(new Phrase("Worked Hours:", _TextBoldFont)));
            mainTable.AddCell(new PdfPCell(new Phrase(salary.TotalWorkedHours.ToString() + " hours", _TextFont)));

            mainTable.AddCell(new PdfPCell(new Phrase("Social Insurance Tax (" + salary.SocialInsuranceTax + "%) :", _TextFont))); ;
            mainTable.AddCell(new PdfPCell(new Phrase(salary.SocialInsuranceValue.ToString() + "$", _TextFont)));

            //row 3
            mainTable.AddCell(new PdfPCell(new Phrase("Worked Days:", _TextBoldFont)));
            mainTable.AddCell(new PdfPCell(new Phrase(salary.TotalWorkedDays.ToString() + " days", _TextFont)));

            mainTable.AddCell(new PdfPCell(new Phrase("Personal Income Tax (" + salary.PersonalIncomeTax + "%) :", _TextFont))); ;
            mainTable.AddCell(new PdfPCell(new Phrase(salary.PersonalIncomeValue.ToString() + "$", _TextFont)));


            //row 4
            mainTable.AddCell("");
            mainTable.AddCell("");

            mainTable.AddCell(new PdfPCell(new Phrase("Work Insurance Tax (" + salary.WorkInsuranceTax + "%) :", _TextFont))); ;
            mainTable.AddCell(new PdfPCell(new Phrase(salary.WorkInsuranceValue.ToString() + "$", _TextFont)));

            //row 6
            mainTable.AddCell(new PdfPCell(new Phrase("Meal ticket:", _TextBoldFont)));
            mainTable.AddCell(new PdfPCell(new Phrase(salary.MealTicketValue.ToString() + "$", _TextFont)));

            mainTable.AddCell("");
            mainTable.AddCell("");

            //row 7
            mainTable.AddCell(" ");
            mainTable.AddCell(" ");
            mainTable.AddCell(" ");
            mainTable.AddCell(" ");

            //row 8
            mainTable.AddCell(new PdfPCell(new Phrase("Meal tickets total:", _TextBoldFont)));
            mainTable.AddCell(new PdfPCell(new Phrase(salary.MealTicketTotal.ToString() + "$", _TextBoldFont)));

            mainTable.AddCell(new PdfPCell(new Phrase("Net salary:", _TextBoldFont)));
            mainTable.AddCell(new PdfPCell(new Phrase(salary.NetSalary.ToString() + "$", _TextBoldFont)));

            // Calculate the Y position for the main table
            float mainTableY = startY + 2 * mainTable.TotalHeight;

            // Set the Y position of the main table
            mainTable.WriteSelectedRows(0, -1, margin + borderOffset, mainTableY, writer.DirectContent);
        }

    }
}
