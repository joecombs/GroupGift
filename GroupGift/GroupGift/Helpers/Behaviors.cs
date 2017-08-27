using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace GroupGift.Helpers
{
    public class RequiredFieldBehavior : Behavior<Entry>
    {
        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(RequiredFieldBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            set { base.SetValue(IsValidPropertyKey, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.Unfocused += HandleFocusChanged;
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        private void HandleFocusChanged(object sender, FocusEventArgs e)
        {
            IsValid = !string.IsNullOrWhiteSpace(((Entry)sender).Text);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.Unfocused -= HandleFocusChanged;
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = !string.IsNullOrWhiteSpace(((Entry)sender).Text);
        }
    }

    public class EmailValidationBehavior : Behavior<Entry>
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public bool IsValid { get; set; }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += HandleTextChanged;
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= HandleTextChanged;

        }
    }
}
