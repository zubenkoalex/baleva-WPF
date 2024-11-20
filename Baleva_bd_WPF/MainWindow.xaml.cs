using Microsoft.Data.SqlClient;
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
using static Baleva_bd_WPF.database;

namespace Baleva_bd_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        database RPM = new database();
        public MainWindow()
        {
            InitializeComponent();
            LoadDataHeaders();
        }

        private void LoadDataHeaders()
        {
            List<Автомобиль> autos = new List<Автомобиль>(); // лист типа Book для сохранения значений из БД
            try
            {
                RPM.OpenConnection();
                
                string query = "SELECT \r\n    a.ID AS 'Автомобиль_ID',\r\n    m.Название_марки AS 'Марка',\r\n    mo.Название_модели AS 'Модель',\r\n    p.Название_поколения AS 'Поколение',\r\n    k.Тип_топлива,\r\n    k.Объем_двигателя,\r\n    k.Мощность_двигателя,\r\n    k.Тип_КПП,\r\n    k.Тип_привода,\r\n    k.Тип_кузова,\r\n    k.Цвет_кузова,\r\n    k.Руль,\r\n    k.Название_комплектации,\r\n    a.Пробег,\r\n    a.Цена,\r\n    a.Год_выпуска,\r\n    a.Изображение\r\nFROM \r\n    Автомобиль a\r\nINNER JOIN \r\n    Марка m ON a.Марка_ID = m.ID\r\nINNER JOIN \r\n    Модель mo ON m.Модель_ID = mo.ID\r\nINNER JOIN \r\n    Поколение p ON mo.Поколение_ID = p.ID\r\nINNER JOIN \r\n    Комплектация k on a.ID = k.ID";
                
                SqlCommand cmd = new SqlCommand(query, RPM.GetConnection());
                
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())// пока есть что считывать
                {
                    autos.Add(new Автомобиль // добавляем в список элемент типа книга
                    {
                        Автомобиль_ID = reader.GetInt32(0), // по индексам присваиваем значения в 
                        Марка = reader.GetString(1), // переменные класса Book
                        Модель = reader.GetString(2),
                        Поколение = reader.GetString(3),
                        Тип_топлива = reader.GetString(4),
                        Объем_двигателя = reader.GetDouble(5),
                        Мощность_двигателя = reader.GetInt32(6),
                        Тип_КПП = reader.GetString(7),
                        Тип_привода = reader.GetString(8),
                        Тип_кузова = reader.GetString(9),
                        Цвет_кузова = reader.GetString(10),
                        Руль = reader.GetString(11),
                        Название_комплектации = reader.GetString(12),
                        Пробег = reader.GetInt32(13),
                        Цена = reader.GetInt32(14),
                        Год_выпуска = reader.GetInt32(15),
                        Изображение = reader.GetString(16)
                   
                    });
                }
                autogrid.ItemsSource = autos; // передаем в datagrid получившийся список
                RPM.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
                MessageBox.Show("COCO JAMBO: " + ex.Message);
            }
        }


    }
}
