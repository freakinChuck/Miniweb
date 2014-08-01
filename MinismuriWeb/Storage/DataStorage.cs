using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MinismuriWeb.Storage
{
    public class DataStorage
    {
        private static string storageFilePath = "~/Storage/DataSet.xml";

        private DataStorage(DataStorageDataSet dataSet) 
        { 
            DataSet = dataSet;
        }
        public static DataStorage LoadStorage()
        {
            DataStorageDataSet dataSet = new DataStorageDataSet();
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
            return new DataStorage(dataSet);
        }
        public DataStorageDataSet DataSet { get; private set; }

        public void Save()
        {
            DataSet.WriteXml(HttpContext.Current.Server.MapPath(storageFilePath));
        }
    }
}