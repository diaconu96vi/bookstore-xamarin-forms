using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Bookstore.Converters
{
    public class CardNumberToImageSourceConverter : BaseCardValidator
    {
        public string Visa { get { return "ic_visa.png"; } }
        public string Amex { get; set; }
        public string MasterCard { get { return "ic_mastercard.png"; } }
        public string Dinners { get; set; }
        public string Discover { get; set; }
        public string JCB { get; set; }
        public string NotRecognized { get; set; }

        public object ConvertToImageSource(object value)
        {
            if (value == null)
            {
                return NotRecognized;
            }

            var number = value.ToString();
            var numberNormalized = number.Replace("-", string.Empty);

            if (visaRegex.IsMatch(numberNormalized))
            {
                return Visa;
            }

            if (amexRegex.IsMatch(numberNormalized))
            {
                return Amex;
            }

            if (masterRegex.IsMatch(numberNormalized))
            {
                return MasterCard;
            }

            if (dinnersRegex.IsMatch(numberNormalized))
            {
                return Dinners;
            }

            if (discoverRegex.IsMatch(numberNormalized))
            {
                return Discover;
            }

            if (jcbRegex.IsMatch(numberNormalized))
            {
                return JCB;
            }

            return NotRecognized;
        }
        
        public object ConvertToString(object value)
        {
            if (value == null)
            {
                return "NotRecognized";
            }

            var number = value.ToString();
            var numberNormalized = number.Replace("-", string.Empty);

            if (visaRegex.IsMatch(numberNormalized))
            {
                return "Visa";
            }

            if (amexRegex.IsMatch(numberNormalized))
            {
                return "Amex";
            }

            if (masterRegex.IsMatch(numberNormalized))
            {
                return "MasterCard";
            }

            if (dinnersRegex.IsMatch(numberNormalized))
            {
                return "Dinners";
            }

            if (discoverRegex.IsMatch(numberNormalized))
            {
                return "Discover";
            }

            if (jcbRegex.IsMatch(numberNormalized))
            {
                return "JBC";
            }

            return "Not recognised";
        }
    }
}
