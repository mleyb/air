using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueZero.Air
{
    public class ImageResult : ActionResult
    {
        public Image Image { get; set; }

        public ImageFormat ImageFormat { get; set; }

        private static Dictionary<ImageFormat, string> FormatMap { get; set; }

        static ImageResult()
        {
            CreateContentTypeMap();
        }

        public ImageResult(Image image, ImageFormat format)
        {
            Image = image;
            ImageFormat = format;
        }

        public ImageResult(byte[] imageData, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                Image = Image.FromStream(ms);
            }

            ImageFormat = format;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (Image == null) throw new ArgumentNullException("Image");
            if (ImageFormat == null) throw new ArgumentNullException("ImageFormat");

            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = FormatMap[ImageFormat];
            
            Image.Save(context.HttpContext.Response.OutputStream, ImageFormat);
        }

        private static void CreateContentTypeMap()
        {
            FormatMap = new Dictionary<ImageFormat, string>
            {
                { ImageFormat.Bmp,  "image/bmp"                },
                { ImageFormat.Gif,  "image/gif"                },
                { ImageFormat.Icon, "image/vnd.microsoft.icon" },
                { ImageFormat.Jpeg, "image/Jpeg"               },
                { ImageFormat.Png,  "image/png"                },
                { ImageFormat.Tiff, "image/tiff"               },
                { ImageFormat.Wmf,  "image/wmf"                }
            };
        }
    }
}