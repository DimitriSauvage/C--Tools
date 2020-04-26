using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DimitriSauvageTools.OpenXml.Helpers
{
    public class ExcelHelper
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
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(fs))
                {
                    int rowIndex = 0;
                    // 1. Use the reader methods
                    do
                    {
                        bool hasNoLimit = !takes.HasValue && !skip.HasValue;

                        if (hasNoLimit || (skip.HasValue && rowIndex >= skip.Value))
                        {
                            while (reader.Read())
                            {
                                data.Add(rowIndex, new List<string>());
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var value = reader.GetValue(i);
                                    data[rowIndex].Add(value?.ToString());
                                }

                                rowIndex++;

                                if (takes.HasValue && takes.Value >= rowIndex)
                                    break;
                            }
                        }
                    } while (reader.NextResult());
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
            int rowIndex = 0;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(fs))
                {
                    rowIndex = 0;
                    // 1. Use the reader methods
                    do
                    {
                        while (reader.Read())
                        {
                            rowIndex++;

                        }
                    } while (reader.NextResult());
                }

                return rowIndex;
            }
        }
    }
}
