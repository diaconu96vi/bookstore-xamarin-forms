using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Bookstore.Droid.Renderers;
using Bookstore.Renderers;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessBorderlessEntryRenderer))]
namespace Bookstore.Droid.Renderers
{
    public class BorderlessBorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessBorderlessEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
            }
        }
    }
}