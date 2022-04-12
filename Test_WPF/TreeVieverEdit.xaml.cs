using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для TreeVieverEdit.xaml
    /// </summary>
    public partial class TreeVieverEdit : UserControl, INotifyPropertyChanged
    {
        // INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public static object rt;
        // Этот флаг указывает, должны ли элементы древовидного представления (если это возможно) открываться в режиме редактирования
        bool isInEditMode = false;
        public bool IsInEditMode
        {
            get { return isInEditMode; }
            set
            {
                isInEditMode = value;
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs("IsInEditMode"));
            }
        }

        public TreeVieverEdit()
        {
            InitializeComponent();
        }
        
        // старый текст в редактируемом элементе
        string oldText;

        // если текстовое поле только что стало видимым, мы фокусируем его на вводе с клавиатуры и выбираем содержимое
        private void editableTextBoxHeader_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.IsVisible)
            {
                tb.Focus();
                tb.SelectAll();
                oldText = tb.Text;      // резервное копирование - для возможной отмены
            }
        }

        // остановить редактирование при Enter или Exs (затем с помощью кнопки отмена)
        private void editableTextBoxHeader_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                IsInEditMode = false;
            if (e.Key == Key.Escape)
            {
                var tb = sender as TextBox;
                tb.Text = oldText;
                IsInEditMode = false;
            }
        }

        // потеря фокуса - стоп редактирование
        private void editableTextBoxHeader_LostFocus(object sender, RoutedEventArgs e)
        {
            IsInEditMode = false;
        }

        // ToDO^^может случиться так, что пользователь нажал клавишу F2, когда был выбран не редактируемый элемент
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            IsInEditMode = false;
             rt = treeView.SelectedItem;
        }

        // мы (возможно) переключаемся в режим редактирования, когда пользователь нажимает клавишу F2
        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
                IsInEditMode = true;
        }

        // пользователь нажал на заголовок - продолжайте редактирование, если он был выбран
        private void textBlockHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (FindTreeItem(e.OriginalSource as DependencyObject).IsSelected)
            {
                IsInEditMode = true;
                e.Handled = true;       // в противном случае вновь активированный элемент управления немедленно потеряет фокус
            }
        }

        // выполняет поиск соответствующего элемента TreeViewItem,
        public static TreeViewItem FindTreeItem(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);
            return source as TreeViewItem;
        }
    }
}
