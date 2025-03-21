using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Utils
{
    public class Strings
    {
        private static Random random = new Random();
        public static List<string> ColumnDtToStringNearMax(DataTable dataTable, String ColumnName)
        {
            var lsContains = new List<string>();
            var contains = string.Empty;
            foreach (DataRow row in dataTable.Rows)
            {
                if (contains.Length < 7000)
                {
                    var mName = row[ColumnName].ToString().Trim();
                    mName = Regex.Replace(mName, @"\s{2,}", " ");
                    if (mName.Contains(' '))
                    {
                        mName = mName.Replace(' ', ',');
                        contains += String.IsNullOrEmpty(contains) ? "NEAR((" + mName + "), MAX)" : " OR NEAR((" + mName + "), MAX)";
                    }
                    else
                    {
                        contains += String.IsNullOrEmpty(contains) ? mName : " OR " + mName;
                    }
                }
                else
                {
                    lsContains.Add(contains);
                    contains = string.Empty;
                }


            }
            if (lsContains.Count() == 0 && !String.IsNullOrEmpty(contains))
                lsContains.Add(contains);

            return lsContains;
        }

        /// <summary>
        /// Normaliza un string, quitando caracteres especiales,tildes y espacions en blanco al inicio y fin
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns> string normalizado</returns>
        public static string StringNormalizado(string strIn)
        {
            try
            {

                String strIn2 = Regex.Replace(strIn, @"[^\w -]", "", RegexOptions.None).PadLeft(0).PadRight(0);
                String strOut = new String(strIn2.Normalize(NormalizationForm.FormD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray()).Normalize(NormalizationForm.FormC);
                if (strIn.Contains("  "))
                {
                    strOut = strOut.Replace("  ", " ");

                }
                return strOut;
            }
            catch (Exception ex)
            {                
                return String.Empty;
            }
        }
        /// <summary>
        /// Ordena alfabeticamente string por palabras
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns>string</returns>
        public static string NombreLogico(string strIn)
        {
            string[] palabras = strIn.Split(' ');
            Array.Sort(palabras);
            return String.Join(" ", palabras);
        }
        /// <summary>
        /// Retorna valor random con una cantidad de caracteres indicada
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns>string</returns>
        public static string CreateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.$@&#/+¡!{}[]:-_¿?=|";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
