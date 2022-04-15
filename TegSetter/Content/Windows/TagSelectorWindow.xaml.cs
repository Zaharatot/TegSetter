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
            StackPanel panel;
            //Проходимся по экспандерам
            foreach (Expander expander in TagsPanel.Children)
            {
                //Получаем панель экспандера
                panel = expander.Content as StackPanel;
                //Проходимся по тегам в панели
                foreach (TagControl tag in panel.Children)
                    //Проставляем всем нужный статус
                    tag.IsSelected = state;
            }
        }

        /// <summary>
        /// Создаём контролл чекбокса для тега
        /// </summary>
        /// <param name="tag">Инфомрация о теге</param>
        /// <returns>Контролл чекбокса</returns>
        private TagControl CreateTagControl(TagInfo tag)
        {
            //Инициализируем контролл тега
            TagControl elem = new TagControl() {
                IsRemoveButtonVisible = false,
                IsAllowSelected = true,
                IsTagLetterVisible = false,
                TagValue = tag,
            };
            //Добавляем обработчик события выбора тега
            elem.SelectControl += Elem_SelectControl;
            //Возвращаем результат
            return elem;
        }


        /// <summary>
        /// Обработчик события выбора тега
        /// </summary>
        /// <param name="tag">Контролл с выбранным тегом</param>
        private void Elem_SelectControl(TagControl tag) =>
            //Инвертируем5 текущему тэгу выделение
            tag.IsSelected = !tag.IsSelected;

        /// <summary>
        /// Удаляем все теги с панели
        /// </summary>
        private void RemoveTags()
        {
            StackPanel panel;
            //Проходимся по экспандерам
            foreach (Expander expander in TagsPanel.Children)
            {
                //Получаем панель экспандера
                panel = expander.Content as StackPanel;
                //Проходимся по тегам в панели
                foreach(TagControl tag in panel.Children)
                    //Удаляем обработчик события выбора тега
                    tag.SelectControl -= Elem_SelectControl;            
            }
            //Удаляем все группы с панели
            TagsPanel.Children.Clear();
        }

        /// <summary>
        /// Получаем группы тегов, отсортирвоанные по именам
        /// </summary>
        /// <param name="tags">Список тегов для разделения</param>
        /// <returns>Список групп тегов</returns>
        private List<IGrouping<string, TagInfo>> GetTagGroups(List<TagInfo> tags) =>
             tags.GroupBy(tag => tag.Group).OrderBy(group => group.Key).ToList();

        /// <summary>
        /// Метод получения заголовка для группы тегов
        /// </summary>
        /// <param name="group">Содержимое группы тегов</param>
        /// <returns>Заголовок группы</returns>
        private string GetGroupHeader(IGrouping<string, TagInfo> group) =>
            string.IsNullOrEmpty(group.Key) ? "Не распределённые теги" : group.Key;

        /// <summary>
        /// Создаём контролл группы тегов
        /// </summary>
        /// <param name="group">Содержимое группы тегов</param>
        /// <returns>Контролл группы тегов</returns>
        private Expander CreateTagGroup(IGrouping<string, TagInfo> group)
        {
            //Возвращаем экспандер
            Expander elem = new Expander();
            //Инициализируем панель для тегов
            StackPanel panel = new StackPanel();
            //Получаем заголовок группы
            elem.Header = GetGroupHeader(group);
            //Проставляем панель в экспандер
            elem.Content = panel;
            //Разворачиваем панель с неотгруппированными тэгами
            elem.IsExpanded = string.IsNullOrEmpty(group.Key);
            //Проходимся по группе, отсортированной по имени
            foreach (TagInfo tag in group.OrderBy(tag => tag.Name))
                //Добавляем теги на панель
                panel.Children.Add(CreateTagControl(tag));
            //ВОзвращаем контролл
            return elem;
        }


        /// <summary>
        /// Проставляем теги в список
        /// </summary>
        /// <param name="tags">Список тегов для добавления</param>
        public void SetTagsToList(List<TagInfo> tags)
        {
            //Удаляем все теги с панели
            RemoveTags();
            //Получаем группы тегов
            List<IGrouping<string, TagInfo>> groups = GetTagGroups(tags);
            //Проходимся по группам
            foreach (IGrouping<string, TagInfo> group in groups)
                //Добавляем группы на контролл
                TagsPanel.Children.Add(CreateTagGroup(group));
        }

        /// <summary>
        /// Проставляем выбранные теги
        /// </summary>
        /// <param name="tags">Список имён выбранных тегов</param>
        public void SetSelectedTags(List<string> tags)
        {
            StackPanel panel;
            //Проходимся по экспандерам
            foreach (Expander expander in TagsPanel.Children)
            {
                //Получаем панель экспандера
                panel = expander.Content as StackPanel;
                //Проходимся по тегам в панели
                foreach (TagControl tag in panel.Children)
                    //Если имя тега есть в списке - выделяем тег
                    tag.IsSelected = tags.Contains(tag.TagValue.Name);
            }
        }

        /// <summary>
        /// Получаем список выбранных тегов
        /// </summary>
        /// <returns>Список выбранных тегов</returns>
        public List<TagInfo> GetSelectedTags()
        {
            StackPanel panel;
            //Инициализируем выходной список
            List<TagInfo> ex = new List<TagInfo>();
            //Проходимся по экспандерам
            foreach (Expander expander in TagsPanel.Children)
            {
                //Получаем панель экспандера
                panel = expander.Content as StackPanel;
                //Проходимся по тегам в панели
                foreach (TagControl tag in panel.Children)
                    //Если тег выбран
                    if (tag.IsSelected)
                        //Добавляем его в список
                        ex.Add(tag.TagValue);
            }      
            //Возвращаем результат
            return ex;
        }

    }
}
