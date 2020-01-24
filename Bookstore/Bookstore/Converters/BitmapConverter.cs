using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Bookstore.Converters
{
    public static class BitmapConverter
    {
        public static ImageSource ByteToImageSource(Byte[] bytemap)
        {
            return ImageSource.FromStream(() =>
            {
                var stream = new MemoryStream(bytemap);
                return stream;
            });
        }

    }
}
