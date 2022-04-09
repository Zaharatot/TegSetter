using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TegSetter.Content.Windows
{
    /// <summary>
    /// Логика взаимодействия для OpenFolderWinodw.xaml
    /// </summary>
    public partial class OpenFolderWinodw : Window
    {
        /// <summary>
        /// Выбранный путь
        /// </summary>
        public string SelectedPath
        {
            get => SelectedPathTaxtBox.Text;
            set => SelectedPathTaxtBox.Text = value;
        }

        /// <summary>
        /// Флаг рекурсивной загрузки изображений
        /// </summary>
        public bool IsRecurse
        {
            get => IsRecurseCheckBox.IsChecked.GetValueOrDefault(false);
            set => IsRecurseCheckBox.IsChecked = value;
        }

        /// <summary>
        /// Флаг загрузки только изображений без тегов
        /// </summary>
        public bool IsOnlyEmpty
        {
            get => IsOnlyEmptyCheckBox.IsChecked.GetValueOrDefault(false);
            set => IsOnlyEmptyCheckBox.IsChecked = value;
        }


        /// <summary>
        /// Конструктор окна
        /// </summary>
        public OpenFolderWinodw()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Обработчик события нажатия кнопки в окне
        /// </summary>
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //Если нажат эскейп
            if (e.Key == Key.Escape)
                //Отменяем работу с окном
                this.DialogResult = false;
        }


        /// <summary>
        /// Обработчик события нажатия на кнопку выбора пути
        /// </summary>
        private void BroawseButton_Click(object sender, RoutedEventArgs e)
        {
            //Инициализируем диалоговое окно выбора папки для загрузки
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            //Вызываем окно выбора папки и возвращаем результат
            DialogResult result = folderBrowserDialog.ShowDialog();
            //Если выбор был успешен
            if (result == System.Windows.Forms.DialogResult.OK)
                //Получаем выбранный путь
                SelectedPath = folderBrowserDialog.SelectedPath;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку принятия изменений
        /// </summary>
        private void OkButton_Click(object sender, RoutedEventArgs e) =>
            //Завершаем работу с окном
            this.DialogResult = true;
    }
}
