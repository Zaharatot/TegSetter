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
using TegSetter.Content.Controls.Tags;

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
        /// Создаём контролл чекбокса для тега
        /// </summary>
        /// <param name="tag">Инфомрация о теге</param>
        /// <returns>Контролл чекбокса</returns>
        private CheckBox CreateCheckBox(TagInfo tag)
        {
            //Инициализируем контролл тега
            TagControl elem = new TagControl() {
                IsRemoveButtonVisible = false,
                TagLetter = null
            };
            //Проставляем тег в контролл
            elem.SetTag(tag);
            //СОздаём чекбокс с контентом в виде контролла тега
            return new CheckBox() { Content = elem };
        }


        /// <summary>
        /// Проставляем теги в список
        /// </summary>
        /// <param name="tags">Список тегов для добавления</param>
        public void SetTagsToList(List<TagInfo> tags)
        {
            //Удаляем все старые элементы из списка
            TagsListBox.Items.Clear();
            //Проходимся по тегам
            foreach (var tag in tags.OrderBy(tag => tag.Name))
                //Добавляем теги на контролл, в виде чекбоксов
                TagsListBox.Items.Add(CreateCheckBox(tag));
        }

        /// <summary>
        /// Получаем список выбранных тегов
        /// </summary>
        /// <returns>Список выбранных тегов</returns>
        public List<TagInfo> GetSelectedTags()
        {
            TagControl elem;
            //Инициализируем выходной список
            List<TagInfo> ex = new List<TagInfo>();
            //Проходимся по чекбоксам
            foreach (CheckBox checkBox in TagsListBox.Items)
                //Если тег выбран
                if (checkBox.IsChecked.GetValueOrDefault(false))
                {
                    //Получаем контролл тега
                    elem = (TagControl)checkBox.Content;
                    //Добавляем его в список
                    ex.Add(elem.GetTag());
                }
            //Возвращаем результат
            return ex;
        }
    }
}
