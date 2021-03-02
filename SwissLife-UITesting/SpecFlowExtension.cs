using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Automated_E2E_Testing_Workshop
{
    public static class SpecFlowExtension
    {
        public static DataTable ToDataTable(this TechTalk.SpecFlow.Table table)
        {
            DataTable resultTable = new DataTable();

            foreach (var col in table.Header)
            {
                resultTable.Columns.Add(col, typeof(string));
            }

            foreach (var row in table.Rows)
            {
                var resRow = resultTable.NewRow();
                for (int i = 0; i < row.Count; i++)
                {
                    resRow[i] = row[i];
                }
                resultTable.Rows.Add(resRow);
            }

            return resultTable;
        }

        /// <summary>
        /// Converts to Dictionary to a proper string.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToProperString(this IOrderedDictionary dict)
        {
            string ret = "";
            foreach (DictionaryEntry item in dict)
            {
                ret += $"{item.Key}: {item.Value}; ";
            }
            return ret;
        }
    }
}
