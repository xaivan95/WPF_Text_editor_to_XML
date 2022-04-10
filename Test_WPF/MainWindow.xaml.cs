using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void txtRichText_KeyDown(object sender, KeyEventArgs e)
        {
            ModifierKeys combCtrSh = ModifierKeys.Control | ModifierKeys.Shift;
            if (e.Key == Key.V)
            {
                if ((e.KeyboardDevice.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                    txtRichText.Paste();

            }
        }

        private void txtRichText_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = txtRichText.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btn_Bold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            temp = txtRichText.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btn_Italic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
            temp = txtRichText.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btn_underline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));
            //ToDO обновление состояния следующих кнопок
            temp = txtRichText.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;
            temp = txtRichText.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cmbFontSize.Text = temp.ToString();
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null)

                txtRichText.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
        }

        private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtRichText.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.Text);
        }

        private void btn_Paste_Click(object sender, RoutedEventArgs e)
        {
            txtRichText.Paste();
        }

        private void btn_Cyte_Click(object sender, RoutedEventArgs e)
        {
            txtRichText.Copy();
        }

        private void btn_Copy_Click(object sender, RoutedEventArgs e)
        {
            txtRichText.Cut();
        }
    }
}
