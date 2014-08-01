using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MinismuriWeb.Storage
{
    public class EventStorage
    {
        public static Func<string, string> MapPath { get; set; }

        private static string storageFilePath = "~/Storage/Event.xml";

        private EventStorage(EventStorageDataSet dataSet) 
        {
            DataSet = dataSet;
        }
        public EventStorageDataSet DataSet { get; set; }
        public static EventStorage LoadStorage()
        {
            EventStorageDataSet dataSet = new EventStorageDataSet();
            string rootPath = MapPath(storageFilePath);
            if (!File.Exists(rootPath))
            {
                dataSet.WriteXml(rootPath);
            }
            dataSet.ReadXml(MapPath(storageFilePath));

            return new EventStorage(dataSet);
        }

        public void Save()
        {
            DataSet.WriteXml(MapPath(storageFilePath));
        }
    }
}