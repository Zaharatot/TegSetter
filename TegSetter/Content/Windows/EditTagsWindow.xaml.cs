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
        /// Флаг наличия изменений в списке тегов
        /// </summary>
        private bool _isChanged;

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
            //Проставляем дефолтные знечения
            _isChanged = false;
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
            //Если есть выбранный тег
            if (TagsListBox.SelectedItem != null)
            {
                //Удаляем его из списка
                TagsListBox.Items.Remove(TagsListBox.SelectedItem);
                //Указываем на изменение списка
                _isChanged = true;
            }
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
            {
                //Добавляем тег на контролл
                TagsListBox.Items.Add(CreateTagControl(enterTagTextWindow.GetTag()));
                //Указываем на изменение списка
                _isChanged = true;
            }
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
                enterTagTextWindow.SetTag(selected.GetTag());
                //Вызываем окно добавления тега как диалоговое
                bool? result = enterTagTextWindow.ShowDialog();
                //Если окно успешно закрыто, и если был введён тег
                if (result.GetValueOrDefault(false) && enterTagTextWindow.IsContainTagName)
                {
                    //Обновляем текст тега в таблице
                    selected.SetTag(enterTagTextWindow.GetTag());
                    //Указываем на изменение списка
                    _isChanged = true;
                }
            }
        }

        /// <summary>
        /// Получаем выбранный тег
        /// </summary>
        /// <returns>Контролл выбранного тега</returns>
        private TagControl GetSelectedTag()
        {
            TagControl ex = null;
            //Если есть выбранный тег
            if(TagsListBox.SelectedItem != null)
                //Возвращаем контролл выбранного тега
                ex = (TagControl)TagsListBox.SelectedItem;
            //Возвращаем результат
            return ex;
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
                TagLetter = null
            };
            //Проставляем тег в контролл
            elem.SetTag(tag);
            //Возвращаем результат
            return elem;
        }
            



        /// <summary>
        /// Выполняем получение списка тегов
        /// </summary>
        /// <returns>Список тегшов для получения</returns>
        public List<TagInfo> GetTags()
        {
            //Инициализируем выходной список
            List<TagInfo> ex = new List<TagInfo>();
            //Проходимся по текстовым блокам
            foreach (TagControl textBlock in TagsListBox.Items)
                //Добавляем текст из них в список
                ex.Add(textBlock.GetTag());
            //Возвращаем результат
            return ex;
        }

        /// <summary>
        /// Выполняем загрузку списка тегов
        /// </summary>
        /// <param name="tags">Список тегов для загрузки</param>
        public void SetTags(List<TagInfo> tags)
        {
            //Удаляем все старые элементы из списка
            TagsListBox.Items.Clear();
            //Проходимся по тегам, отсортированным по имени
            foreach (var tag in tags.OrderBy(tag => tag))
                //Добавляем теги на контролл
                TagsListBox.Items.Add(CreateTagControl(tag));
            //Указываем, что изменений нет
            _isChanged = false;
        }
    }
}
