using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;

namespace BloodTransfusionStationApp.Models
{
    public static class PrintedForm
    {
        private static readonly BloodTransfusionStationDBEntities db = new BloodTransfusionStationDBEntities();

        public static byte[] FullTakingBloodExls()
        {
            Excel._Workbook oWB = null;
            Excel._Worksheet oSheet = null;
            byte[] data = null;

            try
            {
                var full = db.FullTakingBloodInf().ToList();

                Excel.Application oXL = new Excel.Application() { Visible = false };

                //Get a new workbook.
                oWB = oXL.Workbooks.Add(Missing.Value);
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "№";
                oSheet.Cells[1, 2] = "Дата приёма";
                oSheet.Cells[1, 3] = "Вид донорства";
                oSheet.Cells[1, 4] = "Группа крови";
                oSheet.Cells[1, 5] = "Имя";
                oSheet.Cells[1, 6] = "Объем";
                oSheet.Cells[1, 7] = "Врач";

                oSheet.get_Range("A1", "G1").Font.Bold = true;
                oSheet.get_Range("A1", "G1").VerticalAlignment =
                    Excel.XlVAlign.xlVAlignCenter;
                oSheet.get_Range("A1", "G1").HorizontalAlignment =
                    Excel.XlHAlign.xlHAlignCenter;

                int rowNum = 2;

                foreach (var take in full)
                {
                    object[] row = new object[7];
                    row[0] = rowNum - 1;
                    row[1] = take.Дата_приема;
                    row[2] = take.Вид_донорства;
                    row[3] = take.Группа_крови;
                    row[4] = $"{take.Имя} {take.Фамилия}";
                    row[5] = take.Объем;
                    row[6] = take.Фамилия_врача;

                    Excel.Range range = oSheet.get_Range($"A{rowNum}", $"G{rowNum}");
                    range.Value2 = row;
                    oSheet.get_Range($"B{rowNum}").EntireColumn.NumberFormat = "MM.DD.YYYY";
                    range.EntireColumn.AutoFit();
                    range.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    oSheet.get_Range($"F{rowNum}").EntireColumn.NumberFormat = "0.00";

                    rowNum++;
                }

                string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.xlsx");
                oWB.SaveAs(path);
                oWB.Close(false);
                oXL.Application.Quit();
                oXL.Quit();
                
                FinalReleaseComObject(oXL);
                FinalReleaseComObject(oWB);
                FinalReleaseComObject(oSheet);
                data = System.IO.File.ReadAllBytes(path);
                System.IO.File.Delete(path);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            return data;
        }

        private static void FinalReleaseComObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    int finalized = Marshal.FinalReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
    }
}