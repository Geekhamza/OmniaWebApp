using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace OmniaWeb.ExtraTools
{
    public class byteToImage
    {
        public Image byteArrayToImage(byte[] source)
        {
            MemoryStream _ms = new MemoryStream(source);
            Image _ret = Image.FromStream(_ms);
            return _ret;
        }
    }
}