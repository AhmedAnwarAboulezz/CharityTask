using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Common.StandardInfrastructure
{
    public static class Extensions
    {
        public static IQueryable<T> OrderByWithDirection<T>(this IQueryable<T> query, string sortDirection, string sortField, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            return orderBy != null ? orderBy(query).ThenBy($"{sortField} {sortDirection}") : query.OrderBy($"{sortField} {sortDirection}");
        }
        public static IQueryable<T> GetPaggedList<T>(this IQueryable<T> query, int offset, int limit)
        {
            offset = offset < 1 ? 1 : offset;
            return query.Skip(--offset * limit)
                        .Take(limit);
        }
        public static Guid ToGuid(this int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
        public static DataTable GetDataTableFromSpreadSheet(this SpreadsheetDocument document)
        {
            var results = new DataTable();
            try
            {
                var sheets = document.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                var relationshipId = sheets.First().Id.Value;
                var part = (WorksheetPart)document.WorkbookPart.GetPartById(relationshipId);
                var sheet = part.Worksheet;
                var data = sheet.GetFirstChild<SheetData>();
                List<Row> rows = data.Descendants<Row>().ToList();
                if (rows.Count != 0)
                {
                    foreach (var cell in rows[0].Cast<Cell>())
                        results.Columns.Add(cell.GetValue(document));

                    foreach (var row in rows)
                        if (row.RowIndex > 1) results.Rows.Add((from cell in row.Cast<Cell>() select cell.GetValue(document)).ToArray());
                }
            }
            catch (Exception)
            {
                results = new DataTable();
            }
            return results;
        }
        public static string GetValue(this Cell cell, SpreadsheetDocument document)
        {
            string result = string.Empty;
            try
            {
                if (cell != null && cell.ChildElements.Count != 0)
                {
                    var part = document.WorkbookPart.SharedStringTablePart;
                    if (cell.DataType != null && cell.DataType == CellValues.SharedString)
                        result = part.SharedStringTable.ChildElements[Int32.Parse(cell.CellValue.InnerText)].InnerText;
                    else
                        result = cell.CellValue.InnerText;
                }
            }
            catch (Exception)
            {
                result = string.Empty;
            }
            return result;
        }
        public static IEnumerable<T> Flatten<T, R>(this IEnumerable<T> source, Func<T, R> recursion) where R : IEnumerable<T>
        {
            return source.SelectMany(x => (recursion(x) != null && recursion(x).Any()) ? recursion(x).Flatten(recursion) : null)
                         .Where(x => x != null);
        }
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        public static (string extension, string data) GetBase64StringContents(this string input)
        {
            input = input.Replace("data:", "");
            var parts = input.Split(';').ToList<string>();
            return (MimeTypeMap.GetExtension(parts[0]), parts[1].Replace("base64,", ""));
        }
        public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T1, T2> f, Func<T2, T3> g)
        {
            return (x) => g(f(x));
        }
    }
    public static class Sha256Helper
    {
        private const string Key = "_Apex";

        public static string Encrypt(this string source)
        {
            using var mySha256 = SHA256.Create();
            var hashValue = mySha256.ComputeHash(Encoding.ASCII.GetBytes(source + Key));
            return PrintByteArray(hashValue);
        }

        private static string PrintByteArray(byte[] array)
        {
            var result = new StringBuilder();
            for (var i = 0; i < array.Length; i++)
            {
                result.Append($"{i:X2}".ToLower());
            }
            Console.WriteLine(result.ToString());
            return result.ToString();
        }
        public static bool IsVerified(this string source, string destination)
        {
            return Encrypt(source) == destination;
        }

    }
}
