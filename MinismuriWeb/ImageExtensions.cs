using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace MinismuriWeb
{
    public static class ImageExtensions
    {
        public static Image Resize(this Image img, int maxWidth, int maxHeight)
        {
            double faktorHeight = (double)maxHeight / img.Height;
            double faktorWidth = (double)maxWidth / img.Width;

            double faktor = faktorWidth < faktorHeight ? faktorWidth : faktorHeight;
            if (faktor > 1)
            {
                faktor = 1;
            }

            Size newSize = new Size((int)(img.Width * faktor), (int)(img.Height * faktor));

            return new Bitmap(img, newSize);
        }
        public static void ResizeAndSave(this Image img, int maxWidth, int maxHeight, string filePfad)
        {
            double faktorHeight = (double)maxHeight / img.Height;
            double faktorWidth = (double)maxWidth / img.Width;

            double faktor = faktorWidth < faktorHeight ? faktorWidth : faktorHeight;
            if (faktor > 1)
            {
                faktor = 1;
            }

            Size newSize = new Size((int)(img.Width * faktor), (int)(img.Height * faktor));

            var thumbnail = new Bitmap(img, newSize);
            thumbnail.Save(filePfad);
        }
    }
}