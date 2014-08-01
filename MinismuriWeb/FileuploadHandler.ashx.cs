using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;
using System.Drawing;

namespace MinismuriWeb
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für FileuploadHandler
    /// </summary>
    public class FileuploadHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var ziel = context.Request["Ziel"];
            bool isBildergalerie = ziel.ToLower().StartsWith("bildergalerie".ToLower());
            context.Response.ContentType = "text/plain";//"application/json";
            var r = new System.Collections.Generic.List<ViewDataUploadFilesResult>();
            JavaScriptSerializer js = new JavaScriptSerializer();
            foreach (string file in context.Request.Files)
            {
                HttpPostedFile hpf = context.Request.Files[file] as HttpPostedFile;
                string fileName = string.Empty;

                string[] files = hpf.FileName.Split(new char[] { '\\' });
                fileName = files[files.Length - 1];

                if (hpf.ContentLength == 0)
                    continue;


                if (!Directory.Exists(context.Server.MapPath(string.Format("~/UploadData/{0}", ziel))))
                {
                    Directory.CreateDirectory(context.Server.MapPath(string.Format("~/UploadData/{0}", ziel)));
                }

                string savedFileName;

                if (isBildergalerie)
                {
                    var folderPath = context.Server.MapPath(string.Format("~/UploadData/{0}/thumb/", ziel));

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var stream = hpf.InputStream;
                    Image img = Image.FromStream(stream);

                    const int dateTakenMetadata = 0x9003;
                    if (img.PropertyIdList.Contains(dateTakenMetadata))
                    {
                        try
                        {
                            var createDateProp = System.Text.Encoding.ASCII.GetString(img.GetPropertyItem(dateTakenMetadata).Value); // DateTaken

                            string[] parts = createDateProp.Split(':', ' ');
                            int year = int.Parse(parts[0]);
                            int month = int.Parse(parts[1]);
                            int day = int.Parse(parts[2]);
                            int hour = int.Parse(parts[3]);
                            int minute = int.Parse(parts[4]);
                            int second = int.Parse(parts[5]);

                            fileName = string.Format("{0:0000}{1:00}{2:00}_{3:00}{4:00}{5:00}_{6}",
                                year, month, day, hour, minute, second,
                                fileName
                                );
                        }
                        catch { }
                    }

                    savedFileName = context.Server.MapPath(string.Format("~/UploadData/{0}/{1}", ziel, fileName));

                    img.ResizeAndSave(1000, 1000, savedFileName);

                    string savePath = context.Server.MapPath(string.Format("~/UploadData/{0}/thumb/{1}", ziel, fileName));
                    //var thumbnail = img.Resize(300, 300);
                    //thumbnail.Save(savePath);
                    img.ResizeAndSave(300, 300, savePath);
                }
                else
                {
                    savedFileName = context.Server.MapPath(string.Format("~/UploadData/{0}/{1}", ziel, fileName));
                    hpf.SaveAs(savedFileName);
                }

                r.Add(new ViewDataUploadFilesResult()
                {
                    //Thumbnail_url = savedFileName,
                    Name = fileName,
                    Length = hpf.ContentLength,
                    Type = hpf.ContentType
                });
                var uploadedFiles = new
                {
                    files = r.ToArray()
                };
                var jsonObj = js.Serialize(uploadedFiles);
                //jsonObj.ContentEncoding = System.Text.Encoding.UTF8;
                //jsonObj.ContentType = "application/json;";
                context.Response.Write(jsonObj.ToString());
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public class ViewDataUploadFilesResult
        {
            public string Thumbnail_url { get; set; }
            public string Name { get; set; }
            public int Length { get; set; }
            public string Type { get; set; }
        }
    }
}