using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace MonyeManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Dictionary<string, MaterialDesignThemes.Wpf.PackIconKind> categoryList = new Dictionary<string, MaterialDesignThemes.Wpf.PackIconKind>()
        {
            {"Путешествие", MaterialDesignThemes.Wpf.PackIconKind.Travel },
            {"Медецина", MaterialDesignThemes.Wpf.PackIconKind.Medicine },
            {"Шопинг", MaterialDesignThemes.Wpf.PackIconKind.Shopping },
            {"Отдых", MaterialDesignThemes.Wpf.PackIconKind.RecreationalVehicle },
            {"Продукты", MaterialDesignThemes.Wpf.PackIconKind.Food },
            {"Техника", MaterialDesignThemes.Wpf.PackIconKind.ElectronicStabilityProgram },
            {"Долг", MaterialDesignThemes.Wpf.PackIconKind.Debian },
            {"Зарплата", MaterialDesignThemes.Wpf.PackIconKind.SailBoat },
            {"Прочее", MaterialDesignThemes.Wpf.PackIconKind.Payment},
        };

        public string Balance;

        public static List<Pays> listPay = new List<Pays>();
        XmlSerializer serializerPay = new XmlSerializer(typeof(List<Pays>), new XmlRootAttribute() { ElementName = "Pays" });
        public static List<PaysStonksChildren> listPayStonks = new List<PaysStonksChildren>();
        XmlSerializer serializer = new XmlSerializer(typeof(List<PaysStonks>), new XmlRootAttribute() { ElementName = "PaysStonks" });
        public MainWindow()
        {
            InitializeComponent();
            DeserealizedSerealizedPays(false);
            DeserealizedSerealizedPaysStonks(false);
            GetBalance(true);
        }

        private void DeserealizedSerealizedPays(bool trueSerealize)
        {
            switch (trueSerealize)
            {
                case true:
                    try
                    {
                        using (FileStream fs = new FileStream("Pays.xml", FileMode.Open))
                        {
                            serializerPay.Serialize(fs, listPay);
                        }
                    }
                    catch
                    {

                    }
                    break;
                case false:
                    try
                    {
                        using (FileStream fs = new FileStream("Pays.xml", FileMode.Open))
                        {
                            listPay = (List<Pays>)serializerPay.Deserialize(fs);
                        }
                    }
                    catch
                    {

                    }
                    break;
            }
        }

        private void DeserealizedSerealizedPaysStonks(bool trueSerealize)
        {
            switch (trueSerealize)
            {
                case true:
                    try
                    {
                        var @listPayStonksCopy = new List<PaysStonks>();
                        foreach (var item in listPayStonks)
                        {
                            @listPayStonksCopy.Add(new PaysStonks()
                            {
                                Category = item.Category,
                                Date = item.Date,
                                Pay = item.Pay,
                                Price = item.Price,
                                ShopName = item.ShopName
                            });
                        }
                        using (FileStream fs = new FileStream("PaysStonks.xml", FileMode.Open))
                        {
                            serializer.Serialize(fs, @listPayStonksCopy);
                        }
                    }
                    catch
                    {

                    }
                    break;
                case false:
                    try
                    {
                        using (FileStream fs = new FileStream("PaysStonks.xml", FileMode.Open))
                        {
                            var listPayStonksCopy = (List<PaysStonks>)serializer.Deserialize(fs);
                            listPayStonks.Clear();
                            foreach (var item in listPayStonksCopy)
                            {
                                listPayStonks.Add(new PaysStonksChildren()
                                {
                                    Category = item.Category,
                                    Date = item.Date,
                                    Kind = item.Kind,
                                    Pay = item.Pay,
                                    Price = item.Price.Contains("+") || item.Price.Contains("-") ? item.Price : item.Pay == "+" ? $"+{item.Price}" : $"-{item.Price}",
                                    ShopName = item.ShopName
                                });
                            }
                        }
                    }
                    catch
                    {

                    }
                    break;
            }
        }
        private void TicketButton_Click(object sender, RoutedEventArgs e)
        {
            if ((((MaterialDesignThemes.Wpf.PackIcon)((Button)sender).Content).Foreground) == (Brush)FindResource("ClickButton"))
                return;
            ClearButton();
            MainFrame.Navigate(new CategoryPays());
            ((MaterialDesignThemes.Wpf.PackIcon)((Button)sender).Content).Foreground = (Brush)FindResource("ClickButton");
            ClosedAddedButton();
        }
        private void GetBalance(bool trueRead)
        {
            if (trueRead)
                using (StreamReader sr = new StreamReader("Balance.txt"))
                {
                    Balance = sr.ReadToEnd().Replace("\r\n", "");
                    textBlockBalance.Text = Balance.Replace("\r\n", "");
                    textBlockBalance.Foreground = int.Parse(Balance.Replace("-", "")) >= 0 ? Brushes.White : Brushes.IndianRed;
                }
            else
                using (StreamWriter sw = new StreamWriter("Balance.txt"))
                {
                    sw.WriteLine(Balance);
                }
        }
        private void StonksButton_Click(object sender, RoutedEventArgs e)
        {
            if ((((MaterialDesignThemes.Wpf.PackIcon)((Button)sender).Content).Foreground) == (Brush)FindResource("ClickButton"))
                return;
            ClearButton();
            MainFrame.Navigate(new StonksPays());
            ((MaterialDesignThemes.Wpf.PackIcon)((Button)sender).Content).Foreground = (Brush)FindResource("ClickButton");
            ShowAddedButton();
        }
        private void ClearButton()
        {
            foreach (Button btn in GridFooterMenuBar.Children)
                ((MaterialDesignThemes.Wpf.PackIcon)btn.Content).Foreground = (Brush)FindResource("DefaultButton");
        }

        private void ShowAddedButton()
        {
            MinusButton.Visibility = Visibility.Visible;
            PlusButton.Visibility = Visibility.Visible;
        }
        private void ClosedAddedButton()
        {
            MinusButton.Visibility = Visibility.Hidden;
            PlusButton.Visibility = Visibility.Hidden;
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            NameOperation.Text = "Добавление дохода";
            CategoryComboBox.SelectedIndex = -1;
            CategoryComboBox.ItemsSource = categoryList.Keys;
            _numValue = 1;
            txtNum.Text = _numValue.ToString();
            TextForAddePrice.Text = string.Empty;
        }

        #region
        private int _numValue = 1;

        public int NumValue
        {
            get { return _numValue; }
            set
            {
                _numValue = value;
                txtNum.Text = value.ToString();
            }
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            NumValue++;
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            NumValue--;
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNum == null)
            {
                return;
            }

            if (!int.TryParse(txtNum.Text, out _numValue))
                txtNum.Text = _numValue.ToString();
        }
        #endregion


        private void SaveShop_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryComboBox.SelectedIndex == -1 || TextForAddePrice.Text == string.Empty)
                return;
            switch (NameOperation.Text)
            {
                case "Добавление дохода":
                    var stonksPlus = new PaysStonksChildren()
                    {
                        Category = CategoryComboBox.SelectedItem.ToString(),
                        Date = DateTime.Now.ToShortDateString() + "." + DateTime.Now.ToShortTimeString(),
                        Pay = "+",
                        Price = txtNum.Text,
                        ShopName = TextForAddePrice.Text
                    };
                    listPayStonks.Add(stonksPlus);
                    DeserealizedSerealizedPaysStonks(true);
                    DeserealizedSerealizedPaysStonks(false);
                    int balans = int.Parse(Balance.Replace("+", "").Replace("-", ""));
                    if (Balance.Contains("-"))
                    {
                        int tempBalanse = balans;
                        balans = tempBalanse - tempBalanse;
                    }
                    balans += int.Parse(txtNum.Text);
                    Balance = balans.ToString();
                    GetBalance(false);
                    GetBalance(true);
                    MainFrame.Navigate(new StonksPays());
                    break;
                case "Добавление расхода":
                    var stonksMinus = new PaysStonksChildren()
                    {
                        Category = CategoryComboBox.SelectedItem.ToString(),
                        Date = DateTime.Now.ToShortDateString()+"." + DateTime.Now.ToShortTimeString(),
                        Pay = "-",
                        Price = txtNum.Text,
                        ShopName = TextForAddePrice.Text
                    };
                    listPayStonks.Add(stonksMinus);
                    DeserealizedSerealizedPaysStonks(true);
                    DeserealizedSerealizedPaysStonks(false);
                    int balansMinus = int.Parse(Balance.Replace("+", "").Replace("-", ""));
                    if (Balance.Contains("-"))
                    {
                        int tempBalanse = balansMinus;
                        balansMinus = tempBalanse - tempBalanse;
                    }
                    balansMinus -= int.Parse(txtNum.Text);
                    Balance = balansMinus.ToString();
                    GetBalance(false);
                    GetBalance(true);
                    MainFrame.Navigate(new StonksPays());
                    break;
            }
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            NameOperation.Text = "Добавление расхода";
            CategoryComboBox.SelectedIndex = -1;
            CategoryComboBox.ItemsSource = categoryList.Keys;
            _numValue = 1;
            txtNum.Text = _numValue.ToString();
            TextForAddePrice.Text = string.Empty;
        }
    }
}
