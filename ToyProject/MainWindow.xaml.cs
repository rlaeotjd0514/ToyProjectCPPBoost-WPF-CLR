using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace ToyProject
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            for(int i=1;i<=9;i++)
            {
                for(int j=1;j<=9;j++)
                {
                    var textBlock = new TextBlock();                    
                    textBlock.Text = "-1";
                    textBlock.Name = string.Format("_{0}_{1}", i, j);
                    textBlock.FontSize = 24;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    textBlock.DataContext = (MainWindowViewModel)this.DataContext;

                    Binding tbi = new Binding();
                    tbi.Source = i;
                    Binding tbj = new Binding();
                    tbj.Source = j;
                    Binding tbp = new Binding();
                    tbp.Path = new PropertyPath("Sudoku");                    
                                        
                    MultiBinding mtb = new MultiBinding();
                    mtb.NotifyOnSourceUpdated = true;
                    mtb.Converter = new MultiDimensionalCoverter();                            
                    mtb.Bindings.Add(tbp);
                    mtb.Bindings.Add(tbi);
                    mtb.Bindings.Add(tbj);

                    textBlock.SetBinding(TextBlock.TextProperty, mtb);

                    Grid.SetRow(textBlock, i - 1);
                    Grid.SetColumn(textBlock, j - 1);                    
                    SudokuGrid.Children.Add(textBlock);
                }
            }
        }

        private void IDontKnowFlag(object sender, ExecutedRoutedEventArgs e)
        {
            //byte[] bt = Encoding.UTF8.GetBytes("ㅇㄴ ㅋㄹㅇㅍㅌㄹ ㅅㅅㅇㅅ ㅈㅇㄱㄴ ㅍㅌㅇㅌㅊ!");
            //string msg = "";
            //foreach(byte b in bt)
            //{
            //    msg += b.ToString();
            //}
            //MessageBox.Show(msg);
            var Instance = this.DataContext as MainWindowViewModel;
            ObservableCollection<ObservableCollection<int>> tt = new ObservableCollection<ObservableCollection<int>>();            
            Instance.Create(1);
        }
    }

    public class MultiDimensionalCoverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return (values[0] as ObservableCollection<ObservableCollection<int>>)[(int)values[1]][(int)values[2]].ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FlagCommand
    {
        public static RoutedCommand ShowFlag = new RoutedCommand();        
    }
}
