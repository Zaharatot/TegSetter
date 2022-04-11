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
using System.Windows.Shapes;
using TegSetter.Content.Clases.DataClases.Info;

namespace TegSetter.Content.Windows
{
    /// <summary>
    /// Логика взаимодействия для EnterTagTextWindow.xaml
    /// </summary>
    public partial class EnterTagTextWindow : Window
    {        

        /// <summary>
        /// Проверка наличия текста тега
        /// </summary>
        public bool IsContainTagName => !string.IsNullOrEmpty(TagNameTextBox.Text);

        /// <summary>
        /// Конструктор окна
        /// </summary>
        public EnterTagTextWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Обработчик события нажатия кнопки в окне
        /// </summary>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //Если нажат энтер
            if (e.Key == Key.Enter)
                //Успешно завершаем работу с окном
                this.DialogResult = true;
            else if (e.Key == Key.Escape)
                //Отменяем работу с окном
                this.DialogResult = false;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку принятия
        /// </summary>
        private void OkButton_Click(object sender, RoutedEventArgs e) =>
            //Успешно завершаем работу с окном
            this.DialogResult = true;


        /// <summary>
        /// Проставляем тег в контролл
        /// </summary>
        /// <param name="tag">Информация о теге</param>
        public void SetTag(TagInfo tag)
        {
            //ПРоставляем значения в контролл
            TagDescriptionTextBox.Text = tag.Description;
            TagNameTextBox.Text = tag.Name;
        }

        /// <summary>
        /// Получаем тег из контролла
        /// </summary>
        /// <returns>Класс тега</returns>
        public TagInfo GetTag() =>
            new TagInfo(
                TagNameTextBox.Text, 
                TagDescriptionTextBox.Text);

    }
}
