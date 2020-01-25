﻿using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Bookstore.Droid.Renderers;
using Bookstore.Renderers;
using DatePicker = Xamarin.Forms.DatePicker;
[assembly:ExportRenderer(typeof(BorderlessDatePicker),typeof(BorderlessDatePickerRenderer))]
namespace Bookstore.Droid.Renderers
{
    public class BorderlessDatePickerRenderer : DatePickerRenderer
    {
        public BorderlessDatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
            }
        }
    }
}