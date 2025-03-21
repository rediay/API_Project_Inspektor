using ClosedXML.Excel;
using Common.DTO;
using Common.DTO.Queries;
using ExcelDataReader;
using FastMember;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;

namespace Common.Utils
{
    public class FilesHelper
    {
        public static Boolean saveIndividualQueryFile(IndividualQueryResponseDTO responseDTO)
        {
            try
            {
                var jsondata = JsonConvert.SerializeObject(responseDTO);

                string path = Path.Combine(Directory.GetCurrentDirectory(), $"Files/querys/{responseDTO.query.Id}.json");
                System.IO.File.WriteAllText(path, jsondata);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public static IndividualQueryResponseDTO getIndividualQuery(int idQuery)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"Files/querys/{idQuery}.json");
            try
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    IndividualQueryResponseDTO responseDTO = JsonConvert.DeserializeObject<IndividualQueryResponseDTO>(json);

                    return responseDTO;

                }
            }
            catch (Exception e)
            {
                return new IndividualQueryResponseDTO();
            }
            
        }
        public static DataSet ExcelToDataSet(byte[] archivo)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = new MemoryStream(archivo))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var headers = new List<string>();
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                            ReadHeaderRow = rowReader =>
                            {
                                for (var i = 0; i < rowReader.FieldCount; i++)
                                    headers.Add(Convert.ToString(rowReader.GetValue(i)).Trim());
                            },
                            FilterColumn = (columnReader, columnIndex) =>
                                headers.IndexOf("string") != columnIndex,

                        }
                    });
                   

                
                    // Ejemplos de acceso a datos
                    //DataSet dt = result
                    //var a = dt.AsEnumerable().MapTo<List<DTOPR>>();
                    return result;
                }
            }
        }
        public  byte[] TableToExcel(List<dynamic> data, List<string> names, List<Dictionary<int, string>> headers)
        {
            try
            {
                
                using (var workbook = new XLWorkbook())
                {
                    DataSet dataSet = new DataSet();
                    
                    int n = 0;
                    foreach (var item in data)
                    {
                        DataTable table = new DataTable();
                        table.TableName = names[n];
                        var header = headers != null ? headers[n] : null;
                        List<string> columnNames = new List<string>();
                        var worksheet = workbook.Worksheets.Add(names[n]);
                        using (var reader = ObjectReader.Create(item))
                        {
                            table.Load(reader);
                        }
                        // Obtener los nombres de las columnas del DataTable
                        //var columnNames = table.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();
                        if (header != null && header.Count.Equals(table.Columns.Count))
                        {
                            for (int i = 0; i < header.Count; i++)
                            {
                                //table.Columns[i].ColumnName = headers[i];
                                columnNames.Add(header[i]);
                            }
                        }
                        else
                        {
                            columnNames = table.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();
                        }
                        // Agregar los encabezados de columna a la hoja de Excel
                        for (int i = 0; i < columnNames.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = columnNames[i];
                        }
                        // Agregar los datos al cuerpo de la hoja de Excel
                        for (int row = 0; row < table.Rows.Count; row++)
                        {
                            for (int col = 0; col < table.Columns.Count; col++)
                            {
                                var cellValue = table.Rows[row][col];
                                worksheet.Cell(row + 2, col + 1).Value = cellValue;
                            }
                        }
                        dataSet.Tables.Add(table);
                        n++;
                    }
                    //for (int i = 0; i < dataSet.Tables.Count; i++)
                    //{
                    //    workbook.AddWorksheet(dataSet.Tables[i]);
                    //}
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        return stream.ToArray();
                    }
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public static byte[] IFormFileToByteArray(IFormFile file)
        {
            
            long length = file.Length;
            if (length < 0)
                return null;

            byte[] bytes = new byte[length];
            using (var fileStream = file.OpenReadStream())
            {                
                fileStream.Read(bytes, 0, (int)file.Length);
            }
            return bytes;

        }
        public static Boolean saveBulkQueryFile(BulkQueryResponseDTO responseDTO)
        {
            try
            {
                var jsondata = JsonConvert.SerializeObject(responseDTO);

                string path = Path.Combine(Directory.GetCurrentDirectory(), $"Files/querys/{responseDTO.query.Id}.json");
               File.WriteAllText(path, jsondata);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public static Boolean saveBulkQueryServiceAdditionalFile(BulkQueryServicesAdditionalResponseDTO responseDTO)
        {
            try
            {
                var jsondata = JsonConvert.SerializeObject(responseDTO);

                string path = Path.Combine(Directory.GetCurrentDirectory(), $"Files/bulkquerys/{responseDTO.QueryServiceAdditional.Id}.json");
               File.WriteAllText(path, jsondata);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public static QueryJsonFileDTO getQuery(int idQuery)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"Files/querys/{idQuery}.json");

            try
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    QueryJsonFileDTO responseDTO = JsonConvert.DeserializeObject<QueryJsonFileDTO>(json);

                    return responseDTO;
                }
            }
            catch (Exception e)
            {
                return new QueryJsonFileDTO();                
            }
            
        }
        public static bool deleteQuery(int idQuery)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"Files/querys/{idQuery}.json");

            if(File.Exists(path))
            { 
                File.Delete(path);
                return true;
            }    
            return false;

        }
        public static BulkQueryResponseDTO getBulkQuery(int idQuery)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"Files/querys/{idQuery}.json");

            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                BulkQueryResponseDTO responseDTO = JsonConvert.DeserializeObject<BulkQueryResponseDTO>(json);

                return responseDTO;

            }
        }
        public static BulkQueryServicesAdditionalResponseDTO getBulkQueryServicesAddiotional(int idQuery)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"Files/bulkquerys/{idQuery}.json");

            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                BulkQueryServicesAdditionalResponseDTO responseDTO = JsonConvert.DeserializeObject<BulkQueryServicesAdditionalResponseDTO>(json);

                return responseDTO;

            }
        }
        public static string StringBetween(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }
        public bool ValidateExcel(IFormFile file, string counFile)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            int numRegister = 0;

            using (var package = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                numRegister = worksheet.Dimension.Rows - 1;
            }

            var validateExcel = numRegister < int.Parse(counFile) ? true : false;

            return validateExcel;
        }
    }
}
