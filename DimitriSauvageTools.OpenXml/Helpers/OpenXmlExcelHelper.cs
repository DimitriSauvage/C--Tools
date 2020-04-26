using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DimitriSauvageTools.OpenXml.Helpers
{
    public class OpenXmlExcelHelper
    {
        /// <summary>
        /// Lit les cellules du fichier Xlsx passé en paramètre
        /// </summary>
        /// <param name="filePath">Chemin du fichier Excel (*.xlsx) à lire</param>
        /// <param name="takes">Nombre de lignes à prendre en compte</param>
        /// <param name="skip">Nombre de lignes à éviter</param>
        /// <returns></returns>
        public static IDictionary<int, List<string>> ReadAllRows(string filePath, int? takes = null, int? skip = null)
        {
            Dictionary<int, List<string>> data = new Dictionary<int, List<string>>();

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false))
                {
                    WorkbookPart workbookPart = doc.WorkbookPart;
                    SharedStringTablePart sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                    SharedStringTable sst = sstpart.SharedStringTable;

                    WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                    Worksheet sheet = worksheetPart.Worksheet;

                    var rows = sheet.Descendants<Row>();
                    int rowIndex = 0;
                    foreach (var row in rows)
                    {
                        bool hasNoLimit = !takes.HasValue && !skip.HasValue;

                        if (hasNoLimit || (skip.HasValue && rowIndex >= skip.Value))
                        {
                            data.Add(rowIndex, new List<string>());
                            foreach (Cell c in row.Elements<Cell>())
                            {
                                if ((c.DataType != null) && (c.DataType == CellValues.SharedString))
                                {
                                    int ssid = int.Parse(c.CellValue.Text);
                                    data[rowIndex].Add(sst.ChildElements[ssid].InnerText);
                                }
                                else
                                    data[rowIndex].Add(string.Empty); // Champ avec valeur vide
                            }
                        }

                        rowIndex++;

                        if (takes.HasValue && takes.Value >= rowIndex)
                            break;
                    }
                }


                return data;
            }
        }

        /// <summary>
        /// Lit les cellules du fichier Xlsx passé en paramètre
        /// </summary>
        /// <param name="filePath">Chemin du fichier Excel (*.xlsx) à lire</param>
        /// <param name="takes">Nombre de lignes à prendre en compte</param>
        /// <param name="skip">Nombre de lignes à éviter</param>
        /// <returns></returns>
        public static int CountRows(string filePath)
        {
            Dictionary<int, List<string>> data = new Dictionary<int, List<string>>();

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false))
                {
                    WorkbookPart workbookPart = doc.WorkbookPart;
                    SharedStringTablePart sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                    SharedStringTable sst = sstpart.SharedStringTable;

                    WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                    Worksheet sheet = worksheetPart.Worksheet;

                    return sheet.Descendants<Row>().Count();

                }
            }
        }
    }
}
