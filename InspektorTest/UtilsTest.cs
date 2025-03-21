using Common.DTO;
using Common.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace InspektorTest
{
    [TestClass]
    public class UtilsTest
    {
   
        [TestMethod]
        public DataTable ReadExcelTest()
        {            
            var path = @"C:\Users\usuario\source\repos\InspektorBackend\Common\Common.WebApiCore\Files\Excel\pruebaMasiva.xlsx";
            var file = File.ReadAllBytes(path);
            var ds = FilesHelper.ExcelToDataSet(file);
            return ds.Tables[0];
            
        }

        [TestMethod]
        public void ColumnDtToContainsNearMaxtring()
        {
            var strings = new Strings();
            var dt = ReadExcelTest();
            var s= Strings.ColumnDtToStringNearMax(dt, "Nombre");
            Console.WriteLine(s);
            

        }
    }
}