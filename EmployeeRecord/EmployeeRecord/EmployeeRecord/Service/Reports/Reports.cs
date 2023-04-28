using EmployeeRecord.Models.Register;
using EmployeeRecord.Utilities;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Xamarin.Essentials;

namespace EmployeeRecord.Service.Reports
{
    public class Reports
    {
        public static string ToPdf(string fileName, RegisterEventModel eventModel)
        {
            string AppFolder = $"{FileSystem.CacheDirectory}/Reports";

            #region Ruta de archivo
            string logoChulula = "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";
            string logoApp = "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";
                if (Directory.Exists(AppFolder))
            {

                //Borra archivos preexistentes
                string[] files = Directory.GetFiles(AppFolder);
                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }
            else
            {

                Directory.CreateDirectory(AppFolder);
            }
            var filePath = System.IO.Path.Combine(AppFolder, $"{fileName}.pdf");
            #endregion

            #region Crear Pdf

            PdfWriter pdfWriter = new PdfWriter(filePath);

            PdfDocument pdfDocument = new PdfDocument(pdfWriter);

            pdfDocument.SetDefaultPageSize(new PageSize(PageSize.LETTER));

            Document document = new Document(pdfDocument);

            document.SetMargins(57, 57, 57, 57);

            #endregion

            #region Header

            float[] ColumnWidths = { 100F, 298F, 100F };
            Table HeaderTable = new Table(ColumnWidths).SetBorderCollapse(BorderCollapsePropertyValue.SEPARATE);
            
            Style styleHeder = new Style()
                .SetBold()
                .SetFontSize(14)
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.BOTTOM);

            Image appChulula = new Image(ImageDataFactory.CreateJpeg(logoChulula.Base64StringToBytes()));
            HeaderTable.AddCell(new Cell(1, 1).Add(appChulula).AddStyle(styleHeder));

            var titulo = new Cell(1,1).Add(new Paragraph($"REPORTE DEL SITE"));
            HeaderTable.AddCell(titulo.AddStyle(styleHeder));

            Image appImage = new Image(ImageDataFactory.CreateJpeg(logoApp.Base64StringToBytes()));
            HeaderTable.AddCell(new Cell(1, 1).Add(appImage).AddStyle(styleHeder));

            
            document.Add(HeaderTable);
            #endregion
            
            document.Add(new Cell().SetHeight(14F));

            #region Body

            #region Create Colum
            float[] ColumnWidth = { 124.5F, 124.5F, 124.5F, 124.5F };

            Table BodyTable = new Table(ColumnWidth).SetBorderCollapse(BorderCollapsePropertyValue.SEPARATE);

            Style styleBold = new Style()
                .SetBold()
                .SetBackgroundColor(new DeviceRgb(242, 242, 242))
                .SetBorderBottom(Border.NO_BORDER)
                .SetBorderLeft(Border.NO_BORDER)
                .SetBorderRight(new SolidBorder(ColorConstants.BLACK, 1))
                .SetBorderTop(Border.NO_BORDER)
                .SetFontSize(12)
                .SetTextAlignment(TextAlignment.CENTER);

            Style styleNormal = new Style()
                .SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1))
                .SetBackgroundColor(new DeviceRgb(242, 242, 242))
                .SetBorderLeft(Border.NO_BORDER)
                .SetBorderRight(new SolidBorder(ColorConstants.BLACK, 1))
                .SetBorderTop(Border.NO_BORDER)
                .SetFontSize(12)
                .SetTextAlignment(TextAlignment.CENTER);
            #endregion

            #region Row1
            var cellTitle = new Cell(1, 1).Add(new Paragraph("Nombre del Usuario")).AddStyle(styleBold);
            BodyTable.AddCell(cellTitle);

            cellTitle = new Cell(1, 1).Add(new Paragraph("Empresa")).AddStyle(styleBold);
            BodyTable.AddCell(cellTitle);

            cellTitle = new Cell(1, 1).Add(new Paragraph("Puesto")).AddStyle(styleBold);
            BodyTable.AddCell(cellTitle);

            cellTitle = new Cell(1, 1).Add(new Paragraph()).AddStyle(styleBold);
            BodyTable.AddCell(cellTitle);

            cellTitle = new Cell(1, 1).Add(new Paragraph($"{eventModel.nombreCompleto}")).AddStyle(styleNormal);
            BodyTable.AddCell(cellTitle);

            cellTitle = new Cell(1, 1).Add(new Paragraph(eventModel.empresa)).AddStyle(styleNormal);
            BodyTable.AddCell(cellTitle);

            cellTitle = new Cell(1, 1).Add(new Paragraph(eventModel.puesto)).AddStyle(styleNormal);
            BodyTable.AddCell(cellTitle);

            cellTitle = new Cell(1, 1).Add(new Paragraph()).AddStyle(styleNormal);
            BodyTable.AddCell(cellTitle);

            #endregion

            document.Add(new LineSeparator(new SolidLine()));

            #region Row2
            cellTitle = new Cell(1, 1).Add(new Paragraph("Hora de Entrada")).AddStyle(styleBold.SetBorderBottom(Border.NO_BORDER));
            BodyTable.AddCell(cellTitle);

            cellTitle = new Cell(1, 1).Add(new Paragraph("Hora de Salida")).AddStyle(styleBold).SetBackgroundColor(ColorConstants.WHITE);
            BodyTable.AddCell(cellTitle);

            cellTitle = new Cell(1, 1).Add(new Paragraph("Fecha de Entrada")).AddStyle(styleBold);
            BodyTable.AddCell(cellTitle);

            cellTitle = new Cell(1, 1).Add(new Paragraph("Fecha de Salida")).AddStyle(styleBold).SetBackgroundColor(ColorConstants.WHITE);
            BodyTable.AddCell(cellTitle);

            cellTitle = new Cell(1, 1).Add(new Paragraph(eventModel.hora_entra.ToString("hh:mm:ss"))).AddStyle(styleNormal).SetBorderBottom(Border.NO_BORDER);
            BodyTable.AddCell(cellTitle);

            cellTitle = new Cell(1, 1).Add(new Paragraph(eventModel.hora_sali.ToString("hh:mm:ss"))).AddStyle(styleBold).SetBackgroundColor(ColorConstants.WHITE).SetBorderBottom(Border.NO_BORDER);
            BodyTable.AddCell(cellTitle);

            cellTitle = new Cell(1, 1).Add(new Paragraph(eventModel.hora_entra.ToString("dd/MM/yyyy"))).AddStyle(styleNormal).SetBorderBottom(Border.NO_BORDER);
            BodyTable.AddCell(cellTitle);

            cellTitle = new Cell(1, 1).Add(new Paragraph(eventModel.hora_sali.ToString("dd/MM/yyyy"))).AddStyle(styleBold).SetBackgroundColor(ColorConstants.WHITE).SetBorderBottom(Border.NO_BORDER);
            BodyTable.AddCell(cellTitle);

            #endregion

            BodyTable.SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 1))
                .SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1));
            document.Add(BodyTable);
            #endregion

            document.Add(new Cell().SetHeight(14F));

            #region Footer

            #region Create Table
            Table FooterTable = new Table(1, true)
                .SetBackgroundColor(new DeviceRgb(242, 242, 242))
                .SetBorderCollapse(BorderCollapsePropertyValue.SEPARATE)
                .SetHeight(160);

            styleBold = new Style()
                .SetBold()
                .SetBorderBottom(Border.NO_BORDER)
                .SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 1))
                .SetBorderRight(new SolidBorder(ColorConstants.BLACK, 1))
                .SetBorderTop(new SolidBorder(ColorConstants.BLACK, 1))
                .SetFontSize(12);

            styleNormal = new Style().SetFontSize(12)
                .SetBorderBottom(Border.NO_BORDER)
                .SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 1))
                .SetBorderRight(new SolidBorder(ColorConstants.BLACK, 1))
                .SetBorderTop(new SolidBorder(ColorConstants.BLACK, 1));
            #endregion

            var footerName = new Cell(1, 1).Add(new Paragraph("Motivo de la Entrada")).AddStyle(styleBold).SetHeight(26.6F);
            FooterTable.AddCell(footerName);

            footerName = new Cell(1, 1).Add(new Paragraph(eventModel.motivo)).AddStyle(styleNormal).SetHeight(26.6F);
            FooterTable.AddCell(footerName);

            FooterTable.AddCell(new Cell(1, 1).Add(new Paragraph()).AddStyle(styleNormal));
            FooterTable.AddCell(new Cell(1, 1).Add(new Paragraph()).AddStyle(styleNormal));
            FooterTable.AddCell(new Cell(1, 1).Add(new Paragraph()).AddStyle(styleNormal));

            FooterTable.AddCell(new Cell(1, 1).Add(new Paragraph()).AddStyle(styleNormal));

            FooterTable.SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1))
                 .SetBorderLeft(Border.NO_BORDER)
                 .SetBorderRight(Border.NO_BORDER)
                 .SetBorderTop(Border.NO_BORDER);

            document.Add(FooterTable);

            #endregion

            document.Close();

            return filePath;

        }
    }
}
