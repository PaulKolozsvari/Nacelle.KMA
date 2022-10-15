using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XF.Material.Forms.Models;
using XF.Material.Forms.UI;
using System.Collections;
using XF.Material.Forms;

namespace Nacelle.KMA.UI.Views
{
    public partial class TextMenuButton : Grid
    {
        public static readonly BindableProperty ChoicesProperty = BindableProperty.Create(
            nameof(XF.Material.Forms.UI.MaterialMenuButton.Choices),
            typeof(IList),
            typeof(TextMenuButton),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is TextMenuButton textMenuButton &&
                    newValue is IList newChoices)
                {
                    textMenuButton.MaterialMenuButton.Choices = newChoices;
                }
            });

        public static BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(TextMenuButton),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is TextMenuButton textMenuButton &&
                    newValue is string title)
                {
                    textMenuButton.Label.Text = title;
                }
            });

        public static readonly BindableProperty MenuBackgroundColorProperty = BindableProperty.Create(
            nameof(XF.Material.Forms.UI.MaterialMenuButton.MenuBackgroundColor),
            typeof(Color),
            typeof(TextMenuButton),
            Material.Color.Surface,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is TextMenuButton textMenuButton &&
                    newValue is Color menuBackgroundColor)
                {
                    textMenuButton.MaterialMenuButton.MenuBackgroundColor = menuBackgroundColor;
                }
            });

        public static readonly BindableProperty MenuCornerRadiusProperty = BindableProperty.Create(
            nameof(XF.Material.Forms.UI.MaterialMenuButton.MenuCornerRadius),
            typeof(float),
            typeof(TextMenuButton),
            4f,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is TextMenuButton textMenuButton &&
                    newValue is float menuCornerRadius)
                {
                    textMenuButton.MaterialMenuButton.MenuCornerRadius = menuCornerRadius;
                }
            });

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(XF.Material.Forms.UI.MaterialMenuButton.CommandParameter),
            typeof(object),
            typeof(TextMenuButton),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is TextMenuButton textMenuButton)
                {
                    textMenuButton.MaterialMenuButton.CommandParameter = newValue;
                }
            });

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(Command<MaterialMenuResult>),
            typeof(TextMenuButton),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is TextMenuButton textMenuButton &&
                    newValue is Command<MaterialMenuResult> command)
                {
                    textMenuButton.MaterialMenuButton.Command = command;
                }
            });

        public static readonly BindableProperty MenuTextColorProperty = BindableProperty.Create(
            nameof(XF.Material.Forms.UI.MaterialMenuButton.MenuTextColor),
            typeof(Color),
            typeof(TextMenuButton),
            Material.Color.OnSurface,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is TextMenuButton textMenuButton &&
                    newValue is Color menuTextColor)
                {
                    textMenuButton.MaterialMenuButton.MenuTextColor = menuTextColor;
                }
            });

        public static readonly BindableProperty MenuTextFontFamilyProperty = BindableProperty.Create(
            nameof(XF.Material.Forms.UI.MaterialMenuButton.MenuTextFontFamily),
            typeof(string),
            typeof(MaterialMenuButton),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is TextMenuButton textMenuButton &&
                    newValue is string menuTextFontFamily)
                {
                    textMenuButton.MaterialMenuButton.MenuTextFontFamily = menuTextFontFamily;
                }
            });


        /// <summary>
        /// Raised when a menu item was selected.
        /// </summary>
        public event EventHandler<MenuSelectedEventArgs> MenuSelected;

        public TextMenuButton()
        {
            InitializeComponent();

            this.MaterialMenuButton.MenuSelected += MaterialMenuButton_MenuSelected;
        }

        public IList Choices
        {
            get => (IList)this.GetValue(ChoicesProperty);
            set => this.SetValue(ChoicesProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public Color MenuBackgroundColor
        {
            get => (Color)this.GetValue(MenuBackgroundColorProperty);
            set => this.SetValue(MenuBackgroundColorProperty, value);
        }

        public float MenuCornerRadius
        {
            get => (float)this.GetValue(MenuCornerRadiusProperty);
            set => this.SetValue(MenuCornerRadiusProperty, value);
        }

        public Command<MaterialMenuResult> Command
        {
            get => (Command<MaterialMenuResult>)this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => this.GetValue(CommandParameterProperty);
            set => this.SetValue(CommandParameterProperty, value);
        }

        public Color MenuTextColor
        {
            get => (Color)this.GetValue(MenuTextColorProperty);
            set => this.SetValue(MenuTextColorProperty, value);
        }

        public string MenuTextFontFamily
        {
            get => (string)this.GetValue(MenuTextFontFamilyProperty);
            set => this.SetValue(MenuTextFontFamilyProperty, value);
        }

        private void MaterialMenuButton_MenuSelected(object sender, MenuSelectedEventArgs e)
        {
            MenuSelected?.Invoke(sender, e);
        }

        private void OnLabelTapped(object sender, System.EventArgs e)
        {
            this.MaterialMenuButton.Click();
        }
    }
}
