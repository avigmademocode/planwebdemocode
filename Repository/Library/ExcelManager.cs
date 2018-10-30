using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using MyProject.Models;
using OfficeOpenXml.Style;

namespace MyProject.Repository.Library
{
    public class ExcelManager
    {

        public ExcelPackage ExportDataToExcel(ExcelDTO excelDTO)
        {
            byte[] result = null;
            ExcelPackage excel = new ExcelPackage();
            try
            {
                
                #region CreateExcel
              
                var workSheet = excel.Workbook.Worksheets.Add("resg676"); // Cust

                workSheet.Column(5).Width = 50;

                workSheet.Cells[1, 1, 1, 8].Merge = true;
                workSheet.Cells[1, 1, 1, 8].Value = excelDTO.CustomerName;
                workSheet.Cells[1, 1, 1, 8].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 1, 1, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 1, 1, 8].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                workSheet.Cells[1, 1, 1, 8].Style.Font.Size = 19;
                workSheet.Cells[1, 1, 1, 8].Style.Font.Bold = true;
                workSheet.Cells[1, 1, 1, 8].Style.Font.Name = "Arial";
                workSheet.Cells[1, 1, 1, 8].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[1, 1, 1, 8].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[1, 1, 1, 8].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[1, 1, 1, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[1, 9, 1, 12].Merge = true;
                workSheet.Cells[1, 9, 1, 12].Value = "PURCHASE ORDER";
                workSheet.Cells[1, 9, 1, 12].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 9, 1, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 9, 1, 12].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                workSheet.Cells[1, 9, 1, 12].Style.Font.Size = 19;
                workSheet.Cells[1, 9, 1, 12].Style.Font.Bold = true;
                workSheet.Cells[1, 9, 1, 12].Style.Font.Name = "Arial";
                workSheet.Cells[1, 9, 1, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[1, 9, 1, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[1, 9, 1, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[1, 9, 1, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[2, 12, 2, 12].Value = "This Purchase Order is issued subject to the terms and conditions on the reverse of this document.";
                workSheet.Cells[2, 12, 2, 12].Merge = true;
                workSheet.Cells[2, 12, 2, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[2, 12, 2, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[2, 12, 2, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[2, 12, 2, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[3, 1, 3, 2].Merge = true;
                workSheet.Cells[3, 1, 3, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[3, 1, 3, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[3, 1, 3, 2].Value = "Date:";
                workSheet.Cells[3, 1, 3, 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[3, 1, 3, 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[3, 1, 3, 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[3, 1, 3, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[3, 3, 3, 5].Merge = true;
                workSheet.Cells[3, 3, 3, 5].Value = excelDTO.OrderDate;
                workSheet.Cells[3, 3, 3, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[3, 3, 3, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[3, 3, 3, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[3, 3, 3, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;


                workSheet.Cells[4, 1, 4, 4].Merge = true;
                workSheet.Cells[4, 1, 4, 4].Value = "Reference to framework agreement:(if relevant)";
                workSheet.Cells[4, 1, 4, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[4, 1, 4, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[4, 1, 4, 4].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[4, 1, 4, 4].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[4, 1, 4, 4].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[4, 1, 4, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[3, 6, 4, 7].Merge = true;
                workSheet.Cells[3, 6, 4, 7].Value = "PO No:";
                workSheet.Cells[3, 6, 4, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[3, 6, 4, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[3, 6, 4, 7].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[3, 6, 4, 7].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[3, 6, 4, 7].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[3, 6, 4, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[3, 8, 4, 12].Merge = true;
                workSheet.Cells[3, 8, 4, 12].Value = excelDTO.PONumber;

                workSheet.Cells[3, 8, 4, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[3, 8, 4, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[3, 8, 4, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[3, 8, 4, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;


                workSheet.Cells[6, 1, 6, 5].Merge = true;
                workSheet.Cells[6, 1, 6, 5].Value = "SUPPLIER";
                workSheet.Cells[6, 1, 6, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[6, 1, 6, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[6, 1, 6, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[6, 1, 6, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[6, 1, 6, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[6, 1, 6, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[6, 6, 6, 12].Merge = true;
                workSheet.Cells[6, 6, 6, 12].Value = "DELIVERY / COLLECTION ADDRESS";
                workSheet.Cells[6, 6, 6, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[6, 6, 6, 12].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[6, 6, 6, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[6, 6, 6, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[6, 6, 6, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[6, 6, 6, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[7, 1, 7, 2].Merge = true;
                workSheet.Cells[7, 1, 7, 2].Value = "Company name:";
                workSheet.Cells[7, 1, 7, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[7, 1, 7, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[7, 1, 7, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[7, 1, 7, 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[7, 1, 7, 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[7, 1, 7, 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[7, 1, 7, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[7, 3, 7, 5].Merge = true;
                workSheet.Cells[7, 3, 7, 5].Value = excelDTO.CompanyName;

                workSheet.Cells[7, 3, 7, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[7, 3, 7, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[7, 3, 7, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[7, 3, 7, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[7, 6, 7, 7].Merge = true;
                workSheet.Cells[7, 6, 7, 7].Value = "Contact Name:";
                workSheet.Cells[7, 6, 7, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[7, 6, 7, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[7, 6, 7, 7].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[7, 6, 7, 7].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[7, 6, 7, 7].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[7, 6, 7, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;


                workSheet.Cells[7, 8, 7, 12].Merge = true;
                workSheet.Cells[7, 8, 7, 12].Value = excelDTO.ShipContactName;

                workSheet.Cells[7, 8, 7, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[7, 8, 7, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[7, 8, 7, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[7, 8, 7, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[8, 1, 8, 2].Merge = true;
                workSheet.Cells[8, 1, 8, 2].Value = "Contact Name:";
                workSheet.Cells[8, 1, 8, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[8, 1, 8, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[8, 1, 8, 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[8, 1, 8, 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[8, 1, 8, 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[8, 1, 8, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[8, 3, 8, 5].Merge = true;
                workSheet.Cells[8, 3, 8, 5].Value = excelDTO.SuppContactName;
                workSheet.Cells[8, 3, 8, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[8, 3, 8, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[8, 3, 8, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[8, 3, 8, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;


                workSheet.Cells[8, 6, 8, 7].Merge = true;
                workSheet.Cells[8, 6, 8, 7].Value = "E-mail:";
                workSheet.Cells[8, 6, 8, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[8, 6, 8, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[8, 6, 8, 7].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[8, 6, 8, 7].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[8, 6, 8, 7].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[8, 6, 8, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;


                workSheet.Cells[7, 8, 7, 12].Merge = true;
                workSheet.Cells[7, 8, 7, 12].Value = excelDTO.ShipEmailAdd;
                workSheet.Cells[7, 8, 7, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[7, 8, 7, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[7, 8, 7, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[7, 8, 7, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;



                workSheet.Cells[9, 1, 12, 2].Merge = true;
                workSheet.Cells[9, 1, 12, 2].Value = "E-mail::";
                workSheet.Cells[9, 1, 12, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[9, 1, 12, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[9, 1, 12, 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[9, 1, 12, 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[9, 1, 12, 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[9, 1, 12, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[9, 3, 12, 5].Merge = true;
                workSheet.Cells[9, 3, 12, 5].Value = excelDTO.SuppEmailAdd;
                workSheet.Cells[9, 3, 12, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[9, 3, 12, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[9, 3, 12, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[9, 3, 12, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;


                workSheet.Cells[9, 6, 12, 7].Merge = true;
                workSheet.Cells[9, 6, 12, 7].Value = "Address::";
                workSheet.Cells[9, 6, 12, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[9, 6, 12, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[9, 6, 12, 7].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[9, 6, 12, 7].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[9, 6, 12, 7].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[9, 6, 12, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;


                workSheet.Cells[9, 8, 9, 12].Merge = true;
                workSheet.Cells[9, 8, 9, 12].Value = excelDTO.ShipAdd1;
                workSheet.Cells[9, 8, 9, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[9, 8, 9, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[9, 8, 9, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[9, 8, 9, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[10, 8, 10, 12].Merge = true;
                workSheet.Cells[10, 8, 10, 12].Value = excelDTO.ShipAdd2;
                workSheet.Cells[10, 8, 10, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[10, 8, 10, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[10, 8, 10, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[10, 8, 10, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[11, 8, 11, 12].Merge = true;
                workSheet.Cells[11, 8, 11, 12].Value = excelDTO.ShipAdd3;
                workSheet.Cells[11, 8, 11, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[11, 8, 11, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[11, 8, 11, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[11, 8, 11, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[12, 8, 12, 12].Merge = true;
                workSheet.Cells[12, 8, 12, 12].Value = excelDTO.ShipCity + "   " + excelDTO.ShipState + "   " + excelDTO.ShipPin;
                workSheet.Cells[12, 8, 12, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[12, 8, 12, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[12, 8, 12, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[12, 8, 12, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[13, 8, 13, 12].Merge = true;
                workSheet.Cells[13, 8, 13, 12].Value = excelDTO.ShipCountry;
                workSheet.Cells[13, 8, 13, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[13, 8, 13, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[13, 8, 13, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[13, 8, 13, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[13, 1, 13, 2].Merge = true;
                workSheet.Cells[13, 1, 13, 2].Value = "Phone:";
                workSheet.Cells[13, 1, 13, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[13, 1, 13, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[13, 1, 13, 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[13, 1, 13, 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[13, 1, 13, 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[13, 1, 13, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[13, 3, 13, 5].Merge = true;
                workSheet.Cells[13, 3, 13, 5].Value = excelDTO.SuppPhone;
                workSheet.Cells[13, 3, 13, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[13, 3, 13, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[13, 3, 13, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[13, 3, 13, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[14, 1, 14, 2].Merge = true;
                workSheet.Cells[14, 1, 14, 2].Value = "Fax:";
                workSheet.Cells[14, 1, 14, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[14, 1, 14, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[14, 1, 14, 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[14, 1, 14, 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[14, 1, 14, 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[14, 1, 14, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[14, 3, 14, 5].Merge = true;
                workSheet.Cells[14, 3, 14, 5].Value = excelDTO.SuppFax;
                workSheet.Cells[14, 3, 14, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[14, 3, 14, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[14, 3, 14, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[14, 3, 14, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;


                workSheet.Cells[14, 6, 14, 12].Merge = true;
                workSheet.Cells[14, 6, 14, 12].Value = excelDTO.CustomerName + "    " + "INVOICING ADDRESS";
                workSheet.Cells[14, 6, 14, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[14, 6, 14, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[14, 6, 14, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[14, 6, 14, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[15, 1, 15, 2].Merge = true;
                workSheet.Cells[15, 1, 15, 2].Value = "Mobile:";
                workSheet.Cells[15, 1, 15, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[15, 1, 15, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[15, 1, 15, 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[15, 1, 15, 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[15, 1, 15, 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[15, 1, 15, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[15, 3, 15, 5].Merge = true;
                workSheet.Cells[15, 3, 15, 5].Value = excelDTO.SuppMobile;
                workSheet.Cells[15, 3, 15, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[15, 3, 15, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[15, 3, 15, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[15, 3, 15, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[15, 6, 15, 7].Merge = true;
                workSheet.Cells[15, 6, 15, 7].Value = "Contact Name:";
                workSheet.Cells[15, 6, 15, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[15, 6, 15, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[15, 6, 15, 7].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[15, 6, 15, 7].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[15, 6, 15, 7].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[15, 6, 15, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;


                workSheet.Cells[15, 8, 15, 12].Merge = true;
                workSheet.Cells[15, 8, 15, 12].Value = excelDTO.BillContactName;
                workSheet.Cells[15, 8, 15, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[15, 8, 15, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[15, 8, 15, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[15, 8, 15, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;


                workSheet.Cells[16, 1, 21, 2].Merge = true;
                workSheet.Cells[16, 1, 21, 2].Value = "Address:";
                workSheet.Cells[16, 1, 21, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[16, 1, 21, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[16, 1, 21, 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[16, 1, 21, 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[16, 1, 21, 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[16, 1, 21, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[16, 3, 21, 5].Merge = true;
                workSheet.Cells[16, 3, 21, 5].Value = excelDTO.SuppAddress;
                workSheet.Cells[16, 3, 21, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[16, 3, 21, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[16, 3, 21, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[16, 3, 21, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;


                workSheet.Cells[16, 6, 16, 7].Merge = true;
                workSheet.Cells[16, 6, 16, 7].Value = "E-mail:";
                workSheet.Cells[16, 6, 16, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[16, 6, 16, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[16, 6, 16, 7].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[15, 6, 15, 7].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[15, 6, 15, 7].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[15, 6, 15, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;


                workSheet.Cells[16, 8, 16, 12].Merge = true;
                workSheet.Cells[16, 8, 16, 12].Value = excelDTO.BillEmail;
                workSheet.Cells[16, 8, 16, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[16, 8, 16, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[16, 8, 16, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[16, 8, 16, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[17, 6, 21, 7].Merge = true;
                workSheet.Cells[17, 6, 21, 7].Value = "Address:";
                workSheet.Cells[17, 6, 21, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[17, 6, 21, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[17, 6, 21, 7].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[17, 6, 21, 7].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[17, 6, 21, 7].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[17, 6, 21, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;


                workSheet.Cells[17, 8, 17, 12].Merge = true;
                workSheet.Cells[17, 8, 17, 12].Value = excelDTO.BillAdd1;
                workSheet.Cells[17, 8, 17, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[17, 8, 17, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[17, 8, 17, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[17, 8, 17, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[18, 8, 18, 12].Merge = true;
                workSheet.Cells[18, 8, 18, 12].Value = excelDTO.BillAdd2;
                workSheet.Cells[18, 8, 18, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[18, 8, 18, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[18, 8, 18, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[18, 8, 18, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[19, 8, 19, 12].Merge = true;
                workSheet.Cells[19, 8, 19, 12].Value = excelDTO.BillAdd3;
                workSheet.Cells[19, 8, 19, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[19, 8, 19, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[19, 8, 19, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[19, 8, 19, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[20, 8, 20, 12].Merge = true;
                workSheet.Cells[20, 8, 20, 12].Value = excelDTO.BillCity + "   " + excelDTO.BillState + "   " + excelDTO.BillPin;
                workSheet.Cells[20, 8, 20, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[20, 8, 20, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[20, 8, 20, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[20, 8, 20, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[21, 8, 21, 12].Merge = true;
                workSheet.Cells[21, 8, 21, 12].Value = excelDTO.BillCountry;
                workSheet.Cells[21, 8, 21, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[21, 8, 21, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[21, 8, 21, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[21, 8, 21, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[23, 1, 23, 2].Merge = true;
                workSheet.Cells[23, 1, 23, 2].Value = "Delivery method:";
                workSheet.Cells[24, 1, 24, 2].Merge = true;
                workSheet.Cells[24, 1, 24, 2].Value = "(if applicable)";
                workSheet.Cells[23, 1, 24, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[23, 1, 24, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[23, 1, 24, 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 1, 24, 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 1, 24, 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 1, 24, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[23, 3, 24, 5].Merge = true;
                workSheet.Cells[23, 3, 24, 5].Value = excelDTO.DeliveryMethod;
                workSheet.Cells[23, 3, 24, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 3, 24, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 3, 24, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 3, 24, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;


                workSheet.Cells[23, 6, 24, 7].Merge = true;
                workSheet.Cells[23, 6, 24, 7].Value = "Required delivery date";
                workSheet.Cells[23, 6, 24, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[23, 6, 24, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[23, 6, 24, 7].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 6, 24, 7].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 6, 24, 7].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 6, 24, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[23, 8, 24, 9].Merge = true;
                workSheet.Cells[23, 8, 24, 9].Value = excelDTO.ReqDelDateMethod;

                workSheet.Cells[23, 8, 24, 9].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 8, 24, 9].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 8, 24, 9].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 8, 24, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[23, 11, 24, 11].Merge = true;
                workSheet.Cells[23, 11, 24, 11].Value = "Payment terms:";
                workSheet.Cells[23, 11, 24, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[23, 11, 24, 11].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[23, 11, 24, 11].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 11, 24, 11].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 11, 24, 11].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 11, 24, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[23, 12, 24, 12].Merge = true;
                workSheet.Cells[23, 12, 24, 12].Value = excelDTO.Paymentterms;

                workSheet.Cells[23, 12, 24, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 12, 24, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 12, 24, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[23, 12, 24, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[26, 1, 27, 1].Merge = true;
                workSheet.Cells[26, 1, 27, 1].Value = "Project code";
                workSheet.Cells[26, 1, 27, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[26, 1, 27, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[26, 1, 27, 1].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 1, 27, 1].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 1, 27, 1].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 1, 27, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[26, 2, 27, 2].Merge = true;
                workSheet.Cells[26, 2, 27, 2].Value = "SOF code";
                workSheet.Cells[26, 2, 27, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[26, 2, 27, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[26, 2, 27, 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 2, 27, 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 2, 27, 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 2, 27, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[26, 3, 27, 3].Merge = true;
                workSheet.Cells[26, 3, 27, 3].Value = "PR no.";
                workSheet.Cells[26, 3, 27, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[26, 3, 27, 3].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[26, 3, 27, 3].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 3, 27, 3].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 3, 27, 3].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 3, 27, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[26, 4, 27, 4].Merge = true;
                workSheet.Cells[26, 4, 27, 4].Value = "Line Item No.";
                workSheet.Cells[26, 4, 27, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[26, 4, 27, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[26, 4, 27, 4].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 4, 27, 4].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 4, 27, 4].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 4, 27, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[26, 5, 26, 5].Merge = true;
                workSheet.Cells[26, 5, 26, 5].Value = "Description of Goods / Services";
                workSheet.Cells[26, 5, 26, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[26, 5, 26, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[26, 5, 26, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 5, 26, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 5, 26, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 5, 26, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[27, 5, 27, 5].Merge = true;

                workSheet.Cells[27, 5, 27, 5].Value = "(add technical specification as attachment if very detailed)";
                workSheet.Cells[26, 5, 27, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[27, 5, 27, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[27, 5, 27, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[27, 5, 27, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[27, 5, 27, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[27, 5, 27, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[26, 6, 27, 7].Merge = true;
                workSheet.Cells[26, 6, 27, 7].Value = "Unit / Form";
                workSheet.Cells[26, 6, 27, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[26, 6, 27, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[26, 6, 27, 7].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 6, 27, 7].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 6, 27, 7].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 6, 27, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[26, 8, 27, 8].Merge = true;
                workSheet.Cells[26, 8, 27, 8].Value = "Quantity required";
                workSheet.Cells[26, 8, 27, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[26, 8, 27, 8].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[26, 8, 27, 8].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 8, 27, 8].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 8, 27, 8].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 8, 27, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[26, 9, 27, 9].Merge = true;
                workSheet.Cells[26, 9, 27, 9].Value = "Currency";
                workSheet.Cells[26, 9, 27, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[26, 9, 27, 9].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[26, 9, 27, 9].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 9, 27, 9].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 9, 27, 9].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 9, 27, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[26, 10, 27, 10].Merge = true;
                workSheet.Cells[26, 10, 27, 10].Value = "Unit Price";
                workSheet.Cells[26, 10, 27, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[26, 10, 27, 10].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[26, 10, 27, 10].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 10, 27, 10].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 10, 27, 10].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 10, 27, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[26, 11, 27, 12].Merge = true;
                workSheet.Cells[26, 11, 27, 12].Value = "Total Price";
                workSheet.Cells[26, 11, 27, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[26, 11, 27, 12].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[26, 11, 27, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 11, 27, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 11, 27, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[26, 11, 27, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                int i = 28;
                // Need to add the Code for Product
                workSheet.Cells[(i = i + 1), 1, i, 9].Merge = true;
                workSheet.Cells[i, 1, i, 9].Value = "Subtotal";
                workSheet.Cells[i, 1, i, 9].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 9].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 9].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
 

                workSheet.Cells[(i = i + 1), 1, i, 9].Merge = true;
                workSheet.Cells[i, 1, i, 9].Value = "Sales tax (if applicable)";
                workSheet.Cells[i, 1, i, 9].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 9].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 9].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

        

                workSheet.Cells[(i = i + 1), 1, i, 9].Merge = true;
                workSheet.Cells[i, 1, i, 9].Value = "Delivery charge (if applicable)";
                workSheet.Cells[i, 1, i, 9].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 9].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 9].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
 


                workSheet.Cells[(i = i + 1), 1, i, 9].Merge = true;
                workSheet.Cells[i, 1, i, 9].Value = "Other charges (if applicable)";
                workSheet.Cells[i, 1, i, 9].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 9].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 9].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[(i = i + 1), 1, i, 9].Merge = true;
                workSheet.Cells[i, 1, i, 9].Value = "TOTAL";
                workSheet.Cells[i, 1, i, 9].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 9].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 9].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;


                workSheet.Cells[(i = i + 1), 1, i, 12].Merge = true;
                workSheet.Cells[i, 1, i, 12].Value = "The Purchase Order number must be quoted on all correspondance and documents including delivery note and invoice";
                workSheet.Cells[i, 1, i, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

         
                workSheet.Cells[(i = i + 1), 1, i, 5].Merge = true;
                workSheet.Cells[i, 1, i, 5].Value = "Prepared by: ";
                workSheet.Cells[i, 1, i, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[i, 1, i, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[i, 1, i, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;


                workSheet.Cells[i, 6, i, 12].Merge = true;
                workSheet.Cells[i, 6, i, 12].Value = "Authorised by Budget Holder (authorised under SoD):";
                workSheet.Cells[i, 6, i, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[i, 6, i, 12].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[i, 6, i, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;



                workSheet.Cells[(i = i + 1), 1,i, 2].Merge = true;
                workSheet.Cells[i, 1, i , 2].Value = "Name:";
                workSheet.Cells[i, 1, i, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[i, 1, i, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[i, 1, i, 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, i, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[i, 3, i, 5].Merge = true;
                workSheet.Cells[i, 3, i, 5].Value = null;

                workSheet.Cells[i, 3, i, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[i, 6, i, 7].Merge = true;
                workSheet.Cells[i, 6, i, 7].Value = "Name:";
                workSheet.Cells[i, 6, i, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[i, 6, i, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[i, 6, i, 7].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 7].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 7].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[i, 8, i, 12].Merge = true;
                workSheet.Cells[i, 8, i, 12].Value = null;

                workSheet.Cells[i, 8, i, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 8, i, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 8, i, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 8, i, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;


                workSheet.Cells[(i = i + 1), 1, i , 2].Merge = true;
                workSheet.Cells[i , 1, i , 2].Value = "Title:";
                workSheet.Cells[i , 1, i , 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[i , 1, i , 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[i , 1, i , 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[i, 3, i, 5].Merge = true;
                workSheet.Cells[i, 3, i, 5].Value = null;

                workSheet.Cells[i, 3, i, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[i, 6, i, 7].Merge = true;
                workSheet.Cells[i, 6, i, 7].Value = "Title:";
                workSheet.Cells[i, 6, i, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[i, 6, i, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[i, 6, i, 7].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 7].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 7].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[i, 8, i, 12].Merge = true;
                workSheet.Cells[i, 8, i, 12].Value = null;

                workSheet.Cells[i, 8, i, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 8, i, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 8, i, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 8, i, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[(i = i + 1), 1, (i = i + 1), 2].Merge = true;
                workSheet.Cells[i , 1, i , 2].Value = "Signature:";
                workSheet.Cells[i , 1, i , 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[i , 1, i , 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[i , 1, i , 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[i, 3, i, 5].Merge = true;
                workSheet.Cells[i, 3, i, 5].Value = null;

                workSheet.Cells[i, 3, i, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[i, 6, i, 7].Merge = true;
                workSheet.Cells[i, 6, i, 7].Value = "Signature:";
                workSheet.Cells[i, 6, i, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[i, 6, i, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[i, 6, i, 7].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 7].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 7].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[i, 8, i, 12].Merge = true;
                workSheet.Cells[i, 8, i, 12].Value = null;

                workSheet.Cells[i, 8, i, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 8, i, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 8, i, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 8, i, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[(i = i + 1), 1, i , 2].Merge = true;
                workSheet.Cells[i , 1, i , 2].Value = "Date:";
                workSheet.Cells[i , 1, i , 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[i , 1, i , 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[i , 1, i , 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[i, 3, i, 5].Merge = true;
                workSheet.Cells[i, 3, i, 5].Value = null;

                workSheet.Cells[i, 3, i, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[i, 6, i, 7].Merge = true;
                workSheet.Cells[i, 6, i, 7].Value = "Date:";
                workSheet.Cells[i, 6, i, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[i, 6, i, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[i, 6, i, 7].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 7].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 7].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[i, 8, i, 12].Merge = true;
                workSheet.Cells[i, 8, i, 12].Value = null;

                workSheet.Cells[i, 8, i, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 8, i, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 8, i, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 8, i, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[(i = i + 2), 1, i , 5].Merge = true;
                workSheet.Cells[i , 1, i , 5].Value = "Supplier acceptance:";
                workSheet.Cells[i , 1, i , 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[i , 1, i , 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[i , 1, i , 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[i, 6, i, 12].Merge = true;
                workSheet.Cells[i, 6, i, 12].Value = "Supplier Stamp";
                workSheet.Cells[i, 6, i, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[i, 6, i, 12].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[i, 6, i, 12].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 12].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 12].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 6, i, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[(i = i + 2), 1, i , 2].Merge = true;
                workSheet.Cells[i , 1, i , 2].Value = "Name:";
                workSheet.Cells[i , 1, i , 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[i , 1, i , 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[i , 1, i , 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[i, 3, i, 5].Merge = true;
                workSheet.Cells[i, 3, i, 5].Value = null;

                workSheet.Cells[i, 3, i, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[(i = i + 1), 1, i , 2].Merge = true;
                workSheet.Cells[i , 1, i , 2].Value = "Title:";
                workSheet.Cells[i , 1, i , 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[i , 1, i , 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[i , 1, i , 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[i, 3, i, 5].Merge = true;
                workSheet.Cells[i, 3, i, 5].Value = null;

                workSheet.Cells[i, 3, i, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[(i = i + 1), 1, i , 2].Merge = true;
                workSheet.Cells[i , 1, i , 2].Value = "Signature:";
                workSheet.Cells[i , 1, i , 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[i , 1, i , 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[i , 1, i , 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[i, 3, i, 5].Merge = true;
                workSheet.Cells[i, 3, i, 5].Value = null;

                workSheet.Cells[i, 3, i, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[(i = i + 1), 1, i , 2].Merge = true;
                workSheet.Cells[i , 1, i , 2].Value = "Date:";
                workSheet.Cells[i , 1, i , 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[i , 1, i , 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                workSheet.Cells[i , 1, i , 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i , 1, i , 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                workSheet.Cells[i, 3, i, 5].Merge = true;
                workSheet.Cells[i, 3, i, 5].Value = null;


                workSheet.Cells[i, 3, i, 5].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 3, i, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                i = i - 3;
                int j = i + 4;
                workSheet.Cells[i, 1, j, 2].Merge = true;
                workSheet.Cells[i, 1, j, 2].Value = "null";



                workSheet.Cells[i, 1, j, 2].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, j, 2].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, j, 2].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                workSheet.Cells[i, 1, j, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                #endregion
                //result = excel.GetAsByteArray();

                //using (var memoryStream = new MemoryStream())
                //{
                //    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=Contact.xlsx");
                //    excel.SaveAs(memoryStream);
                //    memoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                //    HttpContext.Current.Response.Flush();
                //    HttpContext.Current.Response.End();
                //}
                string fileName = string.Concat("Contacts.xls");
                string strpath = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];
                excel.SaveAs(new FileInfo(strpath + "/"+fileName));
                return excel;
            }
            catch (Exception ex)
            {
                return excel;
            }
        }



    }
} 