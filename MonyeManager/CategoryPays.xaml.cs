using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace MonyeManager
{
    /// <summary>
    /// Логика взаимодействия для CategoryPays.xaml
    /// </summary>
    public partial class CategoryPays : Page
    {
        public CategoryPays()
        {
            InitializeComponent();
            ItemsControlPays.ItemsSource = MainWindow.listPay;
        }
    }
}
