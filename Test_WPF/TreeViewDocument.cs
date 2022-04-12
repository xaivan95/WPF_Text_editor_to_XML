using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Test_WPF
{
class TreeViewDocument : ObservableCollection<TreeViewParentItem> //коллекция элементов меню
{
    public TreeViewDocument() // при создании добавляем одну главу
    {
        Add(new TreeViewParentItem("Новая глава"));
    }

}

public class TreeViewParentItem : NotifyPropertyChanged // элемент меню
{
    // имя, которое будет выведено в меню
    string name;
    public string Name //возврат и установка имени
    {
        get { return name; }
        set { SetField(ref name, value); }          // через обновление компонента
    }

    // подразделы, каждый имеет такую же структуру и может содержать подэлементы 3-го уровня и тд...
    public ObservableCollection<TreeViewParentItem> TreeViewChildrenItems { get ; set; }
    // создаем новый элемент и объявляем коллекцию подэлементов
    public TreeViewParentItem(string name)
    {
            //TODO::при создании генерировать случайный GUID
        Name = name;
        TreeViewChildrenItems = new ObservableCollection<TreeViewParentItem>();

    }
}
}
