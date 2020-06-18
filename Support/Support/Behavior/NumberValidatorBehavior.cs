using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Support
{
    public class NumberValidationBehavior : Behavior<Entry>

    {
        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(NumberValidationBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        protected override void OnAttachedTo(Entry bindable)

        {

            bindable.TextChanged += OnEntryTextChanged;

            base.OnAttachedTo(bindable);

        }


        protected override void OnDetachingFrom(Entry bindable)

        {

            bindable.TextChanged -= OnEntryTextChanged;

            base.OnDetachingFrom(bindable);

        }


        void OnEntryTextChanged(object sender, TextChangedEventArgs args)

        {
            long result;

            IsValid = long.TryParse(args.NewTextValue, out result);

            ((Entry)sender).BackgroundColor = IsValid ? Color.Default : Color.FromHex("#fdafab");

        }

    }
}
