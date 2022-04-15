using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для EditTagsWindow.xaml
    /// </summary>
    public partial class EditTagsWindow : Window
    {      


        /// <summary>
        /// Конструктор окна
        /// </summary>
        public EditTagsWindow()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Инициализатор окна
        /// </summary>
        private void Init()
        {

        }

        /// <summary>
        /// Обработчик события нажатия кнопки в окне
        /// </summary>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //Если нажат эскейп
            if (e.Key == Key.Escape)
                //Успешно завершаем работу с окном
                this.DialogResult = true;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку удаления тега
        /// </summary>
        private void DeleteTagButton_Click(object sender, RoutedEventArgs e)
        {
            //Получаем выбранный тег
            TagControl elem = GetSelectedTag();
            //Если есть выбранный тег
            if (elem != null)
                //Удаляем его из списка
                TagsPanel.Children.Remove(elem);
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку добавления тега
        /// </summary>
        private void AddTagButton_Click(object sender, RoutedEventArgs e)
        {
            //Инициализируем окно добавления тега
            EnterTagTextWindow enterTagTextWindow = new EnterTagTextWindow();
            //Вызываем окно добавления тега как диалоговое
            bool? result = enterTagTextWindow.ShowDialog();
            //Если окно успешно закрыто, и если был введён тег
            if (result.GetValueOrDefault(false) && enterTagTextWindow.IsContainTagName)
                //Добавляем тег на контролл
                TagsPanel.Children.Add(CreateTagControl(enterTagTextWindow.GetTag()));
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку изменения тега
        /// </summary>
        private void EditTagButton_Click(object sender, RoutedEventArgs e)
        {
            //Получаем выбранный тег
            TagControl selected = GetSelectedTag();
            //Если такой есть
            if (selected != null)
            {
                //Инициализируем окно изменения тега
                EnterTagTextWindow enterTagTextWindow = new EnterTagTextWindow();
                //Проставляем текст тега в окно редактирования
                enterTagTextWindow.SetTag(selected.TagValue);
                //Вызываем окно добавления тега как диалоговое
                bool? result = enterTagTextWindow.ShowDialog();
                //Если окно успешно закрыто, и если был введён тег
                if (result.GetValueOrDefault(false) && enterTagTextWindow.IsContainTagName)
                    //Обновляем текст тега в таблице
                    selected.TagValue = enterTagTextWindow.GetTag();
            }
        }

        /// <summary>
        /// Получаем выбранный тег
        /// </summary>
        /// <returns>Контролл выбранного тега</returns>
        private TagControl GetSelectedTag()
        {
            //Проходимся по тегам
            foreach (TagControl elem in TagsPanel.Children)
                //Если тэг выбран 
                if (elem.IsSelected)
                    //Возвращаем его
                    return elem;
            //Возвращаем результат
            return null;
        }

        /// <summary>
        /// Формируем контролл текстового блока
        /// </summary>
        /// <param name="tag">Текст тега для блока</param>
        /// <returns>Созданный контролл текстового блока</returns>
        private TagControl CreateTagControl(TagInfo tag)
        {
            //Инициализируем контролл тега
            TagControl elem = new TagControl() {
                IsRemoveButtonVisible = false,
                IsAllowSelected = true,
                IsTagLetterVisible = false,
                TagValue = tag
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
        private void Elem_SelectControl(TagControl tag)
        {
            //Проходимся по тегам
            foreach (TagControl elem in TagsPanel.Children)
                //Сбрасываем им выделение
                elem.IsSelected = false;
            //Проставляем текущему тэгу выделение
            tag.IsSelected = true;
        }

        /// <summary>
        /// Удаляем все теги с панели
        /// </summary>
        private void RemoveTags()
        {
            //Проходимся по тегам
            foreach (TagControl tag in TagsPanel.Children)
                //Удаляем обработчик события выбора тега
                tag.SelectControl -= Elem_SelectControl;
            //Удаляем все теги с панели
            TagsPanel.Children.Clear();
        }



        /// <summary>
        /// Выполняем получение списка тегов
        /// </summary>
        /// <returns>Список тегшов для получения</returns>
        public List<TagInfo> GetTags()
        {
            //Инициализируем выходной список
            List<TagInfo> ex = new List<TagInfo>();
            //Проходимся по тегам
            foreach (TagControl tag in TagsPanel.Children)
                //Добавляем текст из них в список
                ex.Add(tag.TagValue);
            //Возвращаем результат
            return ex;
        }

        /// <summary>
        /// Выполняем загрузку списка тегов
        /// </summary>
        /// <param name="tags">Список тегов для загрузки</param>
        public void SetTags(List<TagInfo> tags)
        {
            //Удаляем все теги с панели
            RemoveTags();
            //Проходимся по тегам, отсортированным по имени
            foreach (var tag in tags.OrderBy(tag => tag.Name))
                //Добавляем теги на контролл
                TagsPanel.Children.Add(CreateTagControl(tag));
        }
    }
}
