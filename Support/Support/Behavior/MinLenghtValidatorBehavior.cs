using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Support
{
    public class MinLengthValidatorBehavior : Behavior<Entry>

    {

        public static readonly BindableProperty MinLengthProperty = BindableProperty.Create("MinLength", typeof(int), typeof(MinLengthValidatorBehavior), 0);

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(MinLengthValidatorBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        public int MinLength

        {

            get { return (int)GetValue(MinLengthProperty); }

            set { SetValue(MinLengthProperty, value); }

        }

        protected override void OnAttachedTo(Entry bindable)

        {

            bindable.Unfocused += bindable_LostFocus;
            bindable.TextChanged += bindable_TextChanged;

            base.OnAttachedTo(bindable);

        }


        private void bindable_LostFocus(object sender, FocusEventArgs e)

        {
            var entry = ((Entry)sender);
            IsValid = !(entry.Text == null || entry.Text.Length < MinLength);
            if (!IsValid)
                entry.BackgroundColor = Color.FromHex("#fdafab");
            else
            {
                entry.BackgroundColor = Color.Default;
            }
        }
        private void bindable_TextChanged(object sender, TextChangedEventArgs e)

        {

            var entry = ((Entry)sender);
            IsValid = !(entry.Text == null || entry.Text.Length < MinLength);
            if (!IsValid)
                entry.BackgroundColor = Color.FromHex("#fdafab");
            else
            {
                entry.BackgroundColor = Color.Default;
            }
        }


        protected override void OnDetachingFrom(Entry bindable)

        {

            bindable.Unfocused -= bindable_LostFocus;
            bindable.TextChanged -= bindable_TextChanged;

            base.OnDetachingFrom(bindable);

        }

    }
}
