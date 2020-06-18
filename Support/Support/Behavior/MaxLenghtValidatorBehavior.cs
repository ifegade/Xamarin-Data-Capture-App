using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Support
{
    public class MaxLengthValidatorBehavior : Behavior<Entry>

    {

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create("MaxLength", typeof(int), typeof(MaxLengthValidatorBehavior), 0);

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(MaxLengthValidatorBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        public int MaxLength

        {

            get { return (int)GetValue(MaxLengthProperty); }

            set { SetValue(MaxLengthProperty, value); }

        }


        protected override void OnAttachedTo(Entry bindable)

        {

            bindable.TextChanged += bindable_TextChanged;

        }


        private void bindable_TextChanged(object sender, TextChangedEventArgs e)

        {
            IsValid = !(e.NewTextValue.Length > MaxLength);
            if (!IsValid)
                ((Entry)sender).Text = e.NewTextValue.Substring(0, MaxLength);
        }


        protected override void OnDetachingFrom(Entry bindable)

        {
            bindable.TextChanged -= bindable_TextChanged;
        }
    }
}
