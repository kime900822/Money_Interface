using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Money_Interface.DBHelp
{ 
    public static class Conn
    {
        public static string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        /// <summary>
        /// 返回JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static HttpResponseMessage toJson(Object obj)
        {
            String str;
            if (obj is String || obj is Char)
            {
                str = obj.ToString();
            }
            else
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                str = serializer.Serialize(obj);
            }
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }


        /// <summary>
        /// 二进制转图片
        /// </summary>
        /// <param name="streamByte"></param>
        /// <returns></returns>
        public static System.Drawing.Image ReturnPhoto(byte[] streamByte)
        {
            //System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);
            //System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            //return img;
            //将内存流格式化为位图
            if (streamByte.Length == 0)
            {
                return null;
            }
            else
            {
                MemoryStream stream = new MemoryStream(streamByte); //内存流
                Bitmap bitmap = new Bitmap(stream);
                stream.Close();
                return bitmap;
            }

        }


        /// <summary>
        /// 图片转流
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] ReturnByte(Image image)
        {
            byte[] bt = null;

            if (!image.Equals(null))

            {

                using (MemoryStream mostream = new MemoryStream())

                {

                    Bitmap bmp = new Bitmap(image);

                    bmp.Save(mostream, System.Drawing.Imaging.ImageFormat.Bmp);//将图像以指定的格式存入缓存内存流

                    bt = new byte[mostream.Length];

                    mostream.Position = 0;//设置留的初始位置

                    mostream.Read(bt, 0, Convert.ToInt32(bt.Length));

                }

            }

            return bt;

            //FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);//可读

            ////将文件流中的数据存入内存字节组中
            //byte[] buffer = new byte[stream.Length];
            //stream.Read(buffer, 0, (int)stream.Length);
            //stream.Close();
            //return buffer;
        }



    }


}