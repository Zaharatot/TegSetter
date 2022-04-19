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
using TegSetter.Content.Clases.DataClases.Info.Tag;
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
            TagControl ex = null;
            //Проходимся по контроллам групп
            foreach (TagsGroup group in TagsPanel.Children)
            {
                //Получаем выбранный контролл тега
                ex = group.GetSelectedTagControl();
                //Если такой найден
                if (ex != null)
                    //Выходим из цикла
                    break;
            }
            //Возвращаем результат
            return ex;
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
        /// Выполняем получение списка тегов
        /// </summary>
        /// <returns>Коллекция тегшов для получения</returns>
        public TagsCollection GetTagsCollection()
        {
            //Инициализируем выходную коллекцию тегов
            TagsCollection ex = new TagsCollection();
            //Проходимся по контроллам групп
            foreach (TagsGroup group in TagsPanel.Children)
                //Добавляем группу с него в коллекцию
                ex.Groups.Add(group.GetGroup());
            //Возвращаем результат
            return ex;
        }

        /// <summary>
        /// Выполняем загрузку списка тегов
        /// </summary>
        /// <param name="tags">Коллекция тегов для загрузки</param>
        public void SetTags(TagsCollection tags)
        {
            //Удаляем все группы с панели
            TagsPanel.Children.Clear();
            //Проходимся по группам
            foreach (TagGroup group in tags.Groups)
                //Добавляем группы на контролл
                TagsPanel.Children.Add(CreateTagGroup(group));
        }
    }
}
