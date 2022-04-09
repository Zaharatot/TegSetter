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

namespace TegSetter.Content.Windows
{
    /// <summary>
    /// Логика взаимодействия для TagSelectorWindow.xaml
    /// </summary>
    public partial class TagSelectorWindow : Window
    {
        /// <summary>
        /// Конструктор окна
        /// </summary>
        public TagSelectorWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Обработчик события нажатия на кнопку принятия изменений
        /// </summary>
        private void OkButton_Click(object sender, RoutedEventArgs e) =>
            //Завершаем работу с окном
            this.DialogResult = true;


        /// <summary>
        /// Обработчик события нажатия кнопки в окне
        /// </summary>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //Если нажат энтер
            if(e.Key == Key.Enter)
                //Успешно завершаем работу с окном
                this.DialogResult = true;
            else if (e.Key == Key.Escape)
                //Отменяем работу с окном
                this.DialogResult = false;
        }



        /// <summary>
        /// Проставляем теги в список
        /// </summary>
        /// <param name="tags">Список тегов для добавления</param>
        public void SetTagsToList(List<string> tags)
        {
            //Удаляем все старые элементы из списка
            TagsListBox.Items.Clear();
            //Проходимся по тегам
            foreach (var tag in tags)
                //Добавляем теги на контролл, в виде чекбоксов
                TagsListBox.Items.Add(new CheckBox() {
                    Content = tag
                });
        }

        /// <summary>
        /// Получаем список выбранных тегов
        /// </summary>
        /// <returns>Список выбранных тегов</returns>
        public List<string> GetSelectedTags()
        {
            //Инициализируем выходной список
            List<string> ex = new List<string>();
            //Проходимся по чекбоксам
            foreach (CheckBox checkBox in TagsListBox.Items)
                //Если тег выбран
                if (checkBox.IsChecked.GetValueOrDefault(false))
                    //Добавляем его в список
                    ex.Add((string)checkBox.Content);
            //Возвращаем результат
            return ex;
        }
    }
}
