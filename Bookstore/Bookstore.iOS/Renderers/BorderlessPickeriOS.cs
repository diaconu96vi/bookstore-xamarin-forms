using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Bookstore.iOS.Rednerers;
using Bookstore.Renderers;

[assembly: ExportRenderer(typeof(BorderlessPicker), typeof(BorderlessPickeriOS))]

namespace Bookstore.iOS.Rednerers
{
   public class BorderlessPickeriOS : PickerRenderer
    {

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control==null)return;
           
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}