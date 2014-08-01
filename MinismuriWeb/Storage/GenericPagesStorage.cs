using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MinismuriWeb.Storage
{
    public class GenericPageStorage
    {
        private static string storageFilePath = "~/Storage/Pages.xml";

        private GenericPageStorage(GenericPagesDataSet dataSet) 
        { 
            DataSet = dataSet;
        }
        public static GenericPageStorage LoadStorage()
        {
            GenericPagesDataSet dataSet = new GenericPagesDataSet();
            string rootPath = HttpContext.Current.Server.MapPath(storageFilePath);
            if (!File.Exists(rootPath))
            {
                dataSet.WriteXml(rootPath);
            }
            dataSet.ReadXml(HttpContext.Current.Server.MapPath(storageFilePath));

            //var row = dataSet.Videos.NewVideosRow();
            //row.Anzeigename = "Sakristei mit Roman";
            //row.FileName = "01 Ministrantensakristei_qtp.mp4";
            //dataSet.Videos.AddVideosRow(row);
            //var x = new DataStorage(dataSet);
            //x.Save();
            //return x;
            return new GenericPageStorage(dataSet);
        }
        public GenericPagesDataSet DataSet { get; private set; }

        public void Save()
        {
            DataSet.WriteXml(HttpContext.Current.Server.MapPath(storageFilePath));
        }
    }
}