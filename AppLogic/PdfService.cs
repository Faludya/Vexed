using Abstractions.Services;
using DataModels;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Shared;
using Vexed.Repositories.Abstractions;
using Documents;
using Microsoft.AspNetCore.Hosting;

namespace AppLogic
{
    public class PdfService : IPdfService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PdfService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public byte[] GenerateSalaryPdf(Salary salary)
        {
            // Create a new PDF document
            Document document = new Document();

            // Create a memory stream to hold the PDF bytes
            using (MemoryStream stream = new MemoryStream())
            {
                // Create a PDF writer to write the document to the memory stream
                PdfWriter writer = PdfWriter.GetInstance(document, stream);

                // Set the custom page event for the header
                writer.PageEvent = new PdfHeaderEvent(salary);

                // Open the document for writing
                document.Open();

                // Add content to the document
                AddCompanyLogo(document);

                // Close the document
                document.Close();

                // Return the PDF bytes
                return stream.ToArray();
            }
        }

        private void AddCompanyLogo(Document document)
        {
            string imagePath = _webHostEnvironment.WebRootPath + "/images/logoDark.png";

            // Create an Image object
            Image image = Image.GetInstance(imagePath);
            float x = document.PageSize.Width - 220f;
            float y = document.PageSize.Height - 210f;


            // Set the position and size of the image
            image.SetAbsolutePosition(x, y); // Replace x and y with the desired coordinates
            image.ScaleToFit(100, 100); // Replace width and height with the desired size

            // Add the image to the document
            document.Add(image);

        }
    }
}
