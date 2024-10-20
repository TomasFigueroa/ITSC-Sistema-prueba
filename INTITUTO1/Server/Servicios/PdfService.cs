using INSTITUTO.Bdat.Data.Entity;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

using System.IO;

public class PdfService
{
    public byte[] GenerateCertificado(CertificadoExamen model)
    {
        //Crear un nuevo documento PDF
        PdfDocument document = new PdfDocument();
        document.Info.Title = "Certificado de Examen";

        //Añadir una página al documento
        PdfPage page = document.AddPage();
        XGraphics gfx = XGraphics.FromPdfPage(page);

        // Configurar estilos de texto
        XFont titleFont = new XFont("Verdana", 16);  
        XFont regularFont = new XFont("Verdana", 12);  
        XFont smallFont = new XFont("Verdana", 10);

        //Dibujar el título del documento
        gfx.DrawString("Instituto Técnico Superior Córdoba", titleFont, XBrushes.Black,
            new XRect(0, 40, page.Width, page.Height),
            XStringFormats.TopCenter);

        gfx.DrawString("CONSTANCIA GENERAL", titleFont, XBrushes.Black,
            new XRect(0, 80, page.Width, page.Height),
            XStringFormats.TopCenter);

        //Dibujar el contenido del certificado
        gfx.DrawString($"El/La que suscribe: {model.NombreAdministrador},", regularFont, XBrushes.Black, 50, 150);
        gfx.DrawString($"Hace constar que: {model.NombreAlumno}, DNI: {model.DniAlumno}", regularFont, XBrushes.Black, 50, 180);
        gfx.DrawString($"Participó de la mesa de Examen de la Carrera de: {model.Carrera}", regularFont, XBrushes.Black, 50, 210);
        gfx.DrawString($"A pedido del interesado/a: {model.Interesado}", regularFont, XBrushes.Black, 50, 240);
        gfx.DrawString($"En Córdoba, a {model.DiaNumero} de {model.Mes} del {model.Anio}.", regularFont, XBrushes.Black, 50, 270);

        // Dibujar la firma y sello
        gfx.DrawString("Firma y Sello de la Institución", smallFont, XBrushes.Black, 50, 300);

        // Guardar el documento en un MemoryStream
        using var memoryStream = new MemoryStream();
        document.Save(memoryStream);
        return memoryStream.ToArray(); // Devolver los bytes del PDF
    }
}

