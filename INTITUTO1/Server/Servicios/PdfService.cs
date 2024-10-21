using INSTITUTO.Bdat.Data.Entity;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

using System.IO;

public class PdfService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    // Constructor con IWebHostEnvironment para obtener la ruta física de wwwroot
    public PdfService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public byte[] GenerateCertificado(CertificadoExamen model)
    {
        // Crear un nuevo documento PDF
        PdfDocument document = new PdfDocument();
        document.Info.Title = "Certificado de Examen";

        // Añadir una página al documento
        PdfPage page = document.AddPage();
        XGraphics gfx = XGraphics.FromPdfPage(page);

        // Configurar estilos de texto
        XFont titleFont = new XFont("Verdana", 14, XFontStyleEx.Bold);
        XFont headerFont = new XFont("Verdana", 12, XFontStyleEx.Bold);
        XFont regularFont = new XFont("Verdana", 10);
        XFont smallFont = new XFont("Verdana", 9);
        XFont boldFont = new XFont("Verdana", 10, XFontStyleEx.Bold);
  

        // Dibujar el título del documento
        gfx.DrawString("Instituto Técnico Superior Córdoba", titleFont, XBrushes.Black,
            new XRect(120, 30, page.Width, 50), XStringFormats.TopLeft);

        gfx.DrawString("CONSTANCIA GENERAL", headerFont, XBrushes.Black,
            new XRect(0, 100, page.Width, 20), XStringFormats.TopCenter);

        // Dibujar el contenido del certificado
        gfx.DrawString("Establecimiento:", regularFont, XBrushes.Black, 50, 150);
        gfx.DrawString("INSTITUTO TÉCNICO SUPERIOR CÓRDOBA", boldFont, XBrushes.Black, 200, 150);

        gfx.DrawString($"El/La que suscribe: {model.NombreAdministrador}", regularFont, XBrushes.Black, 50, 180);
        gfx.DrawString("de este Instituto", regularFont, XBrushes.Black, 300, 180);

        gfx.DrawString("Hace constar que:", regularFont, XBrushes.Black, 50, 210);
        gfx.DrawString($"{model.NombreAlumno}", regularFont, XBrushes.Black, 150, 210);
        gfx.DrawString($"DNI: {model.DniAlumno}", regularFont, XBrushes.Black, 350, 210);

        gfx.DrawString("Participó de la mesa de Examen de la Carrera de", regularFont, XBrushes.Black, 50, 240);
        gfx.DrawString($"{model.Carrera}", regularFont, XBrushes.Black, 300, 240);

        // Línea separadora
        gfx.DrawLine(XPens.Black, 50, 270, page.Width - 50, 270);

        gfx.DrawString("A pedido del interesado/a y al solo efecto de ser presentada ante:", regularFont, XBrushes.Black, 50, 290);
        gfx.DrawString($"{model.Interesado}", regularFont, XBrushes.Black, 400, 290);

        // Otra línea separadora
        gfx.DrawLine(XPens.Black, 50, 320, page.Width - 50, 320);

        gfx.DrawString("En Córdoba, a", regularFont, XBrushes.Black, 50, 340);
        gfx.DrawString($"{model.DiaNumero}", regularFont, XBrushes.Black, 130, 340);
        gfx.DrawString("días del mes de", regularFont, XBrushes.Black, 170, 340);
        gfx.DrawString($"{model.Mes}", regularFont, XBrushes.Black, 270, 340);
        gfx.DrawString("del año", regularFont, XBrushes.Black, 350, 340);
        gfx.DrawString($"{model.Anio}", regularFont, XBrushes.Black, 400, 340);

        // Dibujar la firma y sello
        gfx.DrawString("Firma y Sello de la Institución", smallFont, XBrushes.Black, 50, 370);

        // Guardar el documento en un MemoryStream
        using var memoryStream = new MemoryStream();
        document.Save(memoryStream);
        return memoryStream.ToArray(); // Devolver los bytes del PDF
    }
}
