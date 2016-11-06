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

namespace HermitePresentation
{
    using Hermite;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void AddDataGridColumn()
        {
            this.DataGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            for (var i = 0; i < this.DataGrid.RowDefinitions.Count; i++)
            {
                var textBox = new TextBox
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(2),
                    MinWidth = 20
                };
                this.DataGrid.Children.Add(textBox);
                Grid.SetRow(textBox, i);
                Grid.SetColumn(textBox, this.DataGrid.ColumnDefinitions.Count - 1);
            }
        }

        private void AddDataGridRow()
        {
            this.DataGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            var label = new Label();
            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
            stackPanel.Children.Add(new TextBlock { Text = "f" });
            stackPanel.Children.Add(new TextBlock { Text = "(" + (this.DataGrid.RowDefinitions.Count - 1) + ")", FontSize = 8, VerticalAlignment = VerticalAlignment.Top });
            stackPanel.Children.Add(new TextBlock { Text = "(x" });
            stackPanel.Children.Add(new TextBlock { Text = "i", FontSize = 8, VerticalAlignment = VerticalAlignment.Bottom });
            stackPanel.Children.Add(new TextBlock { Text = "):" });
            label.Content = stackPanel;
            this.DataGrid.Children.Add(label);
            Grid.SetRow(label, this.DataGrid.RowDefinitions.Count - 1);
            Grid.SetColumn(label, 0);
            for (var i = 1; i < this.DataGrid.ColumnDefinitions.Count; i++)
            {
                var textBox = new TextBox
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(2),
                    MinWidth = 20
                };
                this.DataGrid.Children.Add(textBox);
                Grid.SetRow(textBox, this.DataGrid.RowDefinitions.Count - 1);
                Grid.SetColumn(textBox, i);
            }
        }

        private void RemoveDataGridColumn()
        {
            if (this.DataGrid.ColumnDefinitions.Count <= 2) return;
            this.DataGrid.ColumnDefinitions.Remove(this.DataGrid.ColumnDefinitions.Last());
            var boxestoRemove = this.DataGrid.Children.Cast<object>().OfType<TextBox>().Where(box => Grid.GetColumn(box) == this.DataGrid.ColumnDefinitions.Count).ToList();
            foreach (var box in boxestoRemove)
            {
                this.DataGrid.Children.Remove(box);
            }
        }

        private void RemoveDataGridRow()
        {
            if (this.DataGrid.RowDefinitions.Count <= 3) return;
            this.DataGrid.RowDefinitions.Remove(this.DataGrid.RowDefinitions.Last());
            var boxestoRemove = this.DataGrid.Children.Cast<object>().OfType<TextBox>().Where(box => Grid.GetRow(box) == this.DataGrid.RowDefinitions.Count).ToList();
            var label = this.DataGrid.Children.Cast<object>().OfType<Label>().FirstOrDefault(lab => Grid.GetRow(lab) == this.DataGrid.RowDefinitions.Count);
            this.DataGrid.Children.Remove(label);
            foreach (var box in boxestoRemove)
            {
                this.DataGrid.Children.Remove(box);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.AddDataGridColumn();
        }

        private void AddRow_Click(object sender, RoutedEventArgs e)
        {
            this.AddDataGridRow();
        }

        private void AddColumnRow_Click(object sender, RoutedEventArgs e)
        {
            this.AddDataGridRow();
            this.AddDataGridColumn();
        }

        private void RemoveColumn_Click(object sender, RoutedEventArgs e)
        {
            this.RemoveDataGridColumn();
        }

        private void RemoveRow_Click(object sender, RoutedEventArgs e)
        {
            this.RemoveDataGridRow();
        }

        private void RemoveRowColumn_Click(object sender, RoutedEventArgs e)
        {
            this.RemoveDataGridColumn();
            this.RemoveDataGridRow();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = new Fraction[this.DataGrid.RowDefinitions.Count, this.DataGrid.ColumnDefinitions.Count - 1];
                foreach (var box in this.DataGrid.Children)
                {
                    var tbox = box as TextBox;
                    if (tbox == null) continue;
                    var column = Grid.GetColumn(tbox) - 1;
                    var row = Grid.GetRow(tbox);
                    data[row, column] = new Fraction { Counter = int.Parse(tbox.Text), Denominator = 1 };
                }

                var hermite = new Hermite(data);
                this.polynominal = hermite.Calculate();
                this.Polynominal.Content = this.polynominal.PolynominalToString();
                this.PolynominalStack.Visibility = Visibility.Visible;
                this.IntegralStack.Visibility = Visibility.Visible;
            }
            catch
            {              
            }         
        }

        private List<PolynominalElement> polynominal = new List<PolynominalElement>();

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                var from = int.Parse(this.From.Text);
                var to = int.Parse(this.To.Text);
                var unsingnedintegral = PolynominalIntegral.UnsignedIntegral(this.polynominal);
                this.UnsignedIntegral.Content = unsingnedintegral.PolynominalToString();
                var signedIntegral = PolynominalIntegral.SignedIntegral(unsingnedintegral, from, to);
                this.SignedResult.Content = signedIntegral.ToString();
                this.IntegralResultStack.Visibility = Visibility.Visible;
            }
            catch
            {
            }          
        }
    }
}
