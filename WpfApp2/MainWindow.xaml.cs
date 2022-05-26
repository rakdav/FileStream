using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SaveFileDialog saveFileDialog;
        private OpenFileDialog openFileDialog;
        public MainWindow()
        {
            InitializeComponent();
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files(*.txt)|*.txt";
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files(*.txt)|*.txt";
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (saveFileDialog.ShowDialog() == false) return;
            string path=saveFileDialog.FileName;
            SaveAsync(path);
        }
        private async void SaveAsync(string path)
        {
            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] buffer = Encoding.Default.GetBytes(TextInput.Text);
                // запись массива байтов в файл
                await fstream.WriteAsync(buffer, 0, buffer.Length);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == false) return;
            string path = openFileDialog.FileName;
            ReadAsync(path);
        }
        private async void ReadAsync(string path)
        {
            using (FileStream fstream = File.OpenRead(path))
            {
                // выделяем массив для считывания данных из файла
                byte[] buffer = new byte[fstream.Length];
                // считываем данные
                await fstream.ReadAsync(buffer, 0, buffer.Length);
                // декодируем байты в строку
                TextInput.Text = Encoding.Default.GetString(buffer);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuItem_Click_1(sender, e);
        }
    }
}
