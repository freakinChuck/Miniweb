using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MinismuriWeb.Storage
{
    public class StatisticStorage
    {
        public static Func<string, string> MapPath { get; set; }

        private static string storageFilePath = "~/Storage/Statistic.xml";

        private StatisticStorage(StatisticDataSet dataSet) 
        {
            DataSet = dataSet;
        }
        public StatisticDataSet DataSet { get; set; }
        public static StatisticStorage LoadStorage()
        {
            StatisticDataSet dataSet = new StatisticDataSet();
            string rootPath = MapPath(storageFilePath);
            if (!File.Exists(rootPath))
            {
                dataSet.WriteXml(rootPath);
            }
            dataSet.ReadXml(MapPath(storageFilePath));

            return new StatisticStorage(dataSet);
        }

        public void Save()
        {
            DataSet.WriteXml(MapPath(storageFilePath));
        }
    }
}