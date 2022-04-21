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
using TegSetter.Content.Clases.DataClases.Info.Tag;
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
        /// Обработчик события нажатия на кнопку выбора всех строк
        /// </summary>
        private void SelectAllButton_Click(object sender, RoutedEventArgs e) =>
            //Проставляем статус всем тегам
            SetAllTagsSelectionState(true);

        /// <summary>
        /// Обработчик события нажатия на кнопку сброса всех строк
        /// </summary>
        private void UnSelectAllButton_Click(object sender, RoutedEventArgs e) =>
            //Проставляем статус всем тегам
            SetAllTagsSelectionState(false);

        /// <summary>
        /// Обработчик события изменения текста для поиска
        /// </summary>
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Проходимся по группам в панели
            foreach (TagsGroup group in TagsPanel.Children)
                //Если в группе отображается хоть один тег
                group.Visibility = group.SearchTags(SearchTextBox.Text)
                    //Мы группу отобюражаем, а в противном случае - скрываем
                    ? Visibility.Visible : Visibility.Collapsed;
        }


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
        /// Метод простановки статуса выделения всем тегам
        /// </summary>
        /// <param name="state">Статус для простановки</param>
        private void SetAllTagsSelectionState(bool state)
        {
            //Проходимся по контроллам групп
            foreach (TagsGroup group in TagsPanel.Children)
                //Проставляем статус выделения
                group.SetAllTagsSelectionState(state);
        }

        /// <summary>
        /// Создаём контролл группы тегов
        /// </summary>
        /// <param name="group">Содержимое группы тегов</param>
        /// <returns>Контролл группы тегов</returns>
        private TagsGroup CreateTagGroup(TagGroup group)
        {
            //Инициализируем контролл группы тегов
            TagsGroup elem = new TagsGroup();
            //Вставляем группу в контролл
            elem.SetGroup(group);
            //Возвращаем результат
            return elem;
        }





        /// <summary>
        /// Проставляем теги в список
        /// </summary>
        /// <param name="tags">Коллекция тегов для добавления</param>
        public void SetTagsToList(TagsCollection tags)
        {
            //Удаляем все группы с панели
            TagsPanel.Children.Clear();
            //Проходимся по группам
            foreach (TagGroup group in tags.Groups)
                //Добавляем группы на контролл
                TagsPanel.Children.Add(CreateTagGroup(group));
        }

        /// <summary>
        /// Проставляем выбранные теги
        /// </summary>
        /// <param name="tags">Список имён выбранных тегов</param>
        public void SetSelectedTags(List<string> tags)
        {
            //Проходимся по контроллам групп
            foreach (TagsGroup group in TagsPanel.Children)
                //Проставляем статус выделения
                group.SetSelectedTags(tags);
        }

        /// <summary>
        /// Получаем список выбранных тегов
        /// </summary>
        /// <returns>Список выбранных тегов</returns>
        public List<TagInfo> GetSelectedTags()
        {
            //Инициализируем выходной список
            List<TagInfo> ex = new List<TagInfo>();
            //Проходимся по контроллам групп
            foreach (TagsGroup group in TagsPanel.Children)
                //Добавляем выбранные из группы элементы в списки
                ex.AddRange(group.GetSelectedTags());
            //Возвращаем результат
            return ex;
        }

    }
}
