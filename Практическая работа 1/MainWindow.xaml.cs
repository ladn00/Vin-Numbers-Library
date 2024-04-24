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
using System.IO;
using CarVin;
using CarNumberLibrary;

namespace Практическая_работа_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }


        //Нажатие на кнопку
        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            lb1.Items.Clear();
            CarVinClass car = new CarVinClass();
            string vin = tb1.Text;
            if (!car.VinCheck(vin))
            {
                MessageBox.Show("Введено неверное значение");
                lb1.Items.Clear();
                return;
            }

            lb1.Items.Add(car.CodeCountry(vin));
            lb1.Items.Add($"\nГод автомобиля: {car.Year(vin)}");
        }

        private void bt2_Click(object sender, RoutedEventArgs e)
        {
            lb1.Items.Clear();
            string number = tb4.Text;
            CarNumber car = new CarNumber();
            if (!car.NumberCheck(number))
            {
                MessageBox.Show("Введено неверное значение");
                lb1.Items.Clear();
                return;
            }
            lb1.Items.Add("Следующий номер:" + car.NumberGenerate(number));

        }
        char[] symbols = { 'А', 'В', 'Е', 'К', 'М', 'Н', 'О', 'Р', 'С', 'Т', 'У', 'Х' };
        public string GenerateInRange(string firstNumber, string secondNumber)
        {
            var ran = new Random();
            string newNumber = "";
            string regionSub = firstNumber.Substring(6, 3);
            int ranChar, ranDigit;
            string digits = "";
            string symbolsString = "";

            while (symbolsString.Length < 1)
            {
                ranChar = ran.Next(0, symbols.Length);
                if (ranChar >= Array.IndexOf(symbols, firstNumber[0]) && ranChar <= Array.IndexOf(symbols, secondNumber[0]))
                    symbolsString += symbols[ranChar];
            }
            while (symbolsString.Length < 2)
            {
                ranChar = ran.Next(0, symbols.Length);
                if (ranChar >= Array.IndexOf(symbols, firstNumber[4]) && ranChar <= Array.IndexOf(symbols, secondNumber[4]))
                    symbolsString += symbols[ranChar];
            }
            while (symbolsString.Length < 3)
            {
                ranChar = ran.Next(0, symbols.Length);
                if (ranChar >= Array.IndexOf(symbols, firstNumber[5]) && ranChar <= Array.IndexOf(symbols, secondNumber[5]))
                    symbolsString += symbols[ranChar];
            }
            
            
            while (digits.Length < 3)
            {
                ranDigit = ran.Next(100, 1000);

                if (symbolsString[0] != firstNumber[0] || symbolsString[1] != firstNumber[4] || symbolsString[2] != firstNumber[5])
                    digits += ranDigit;
                if(symbolsString[0] == firstNumber[0] && symbolsString[1] == firstNumber[4] && symbolsString[2] == firstNumber[5] && ranDigit >= Int32.Parse(firstNumber.Substring(1, 3)) && ranDigit <= Int32.Parse(secondNumber.Substring(1, 3)))
                    digits += ranDigit;
            }
            

            
            newNumber = String.Concat(symbolsString[0], digits, symbolsString.Substring(0, 2), regionSub);
            return newNumber;
        }
        private void bt3_Click(object sender, RoutedEventArgs e)
        {
            lb1.Items.Clear();
            CarNumber car = new CarNumber();
            string firstNumber = tb2.Text;
            string secondNumber = tb3.Text;
            if (!car.NumberCheck(firstNumber) || !car.NumberCheck(secondNumber))
            {
                MessageBox.Show("Введено неверное значение");
                lb1.Items.Clear();
                return;
            }
            lb1.Items.Add(GenerateInRange(firstNumber, secondNumber));
        }
    }
}
