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
            TagControl selectedTag = null;
            TagsGroup selectedGroup = null;
            //Получаем выбранный тег
            GetSelectedTag(ref selectedTag, ref selectedGroup);
            //Если есть выбранный тег
            if (selectedTag != null)
                //Удаляем контролл тега из текущей группы
                DeleteTag(selectedTag, selectedGroup);
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
                //Добавляем тег в целевую группу
                AddNewTag(enterTagTextWindow.GetTag(), enterTagTextWindow.TagGroupName);
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку изменения тега
        /// </summary>
        private void EditTagButton_Click(object sender, RoutedEventArgs e)
        {
            TagControl selectedTag = null;
            TagsGroup selectedGroup = null;
            //Получаем выбранный тег
            GetSelectedTag(ref selectedTag, ref selectedGroup);
            //Если найден выбранный тег
            if (selectedTag != null)
            {
                //Инициализируем окно изменения тега
                EnterTagTextWindow enterTagTextWindow = new EnterTagTextWindow();
                //Проставляем текст тега в окно редактирования
                enterTagTextWindow.SetTag(selectedTag.TagValue);
                //ПРоставляем имя группы тега
                enterTagTextWindow.TagGroupName = selectedGroup.GroupName;
                //Вызываем окно добавления тега как диалоговое
                bool? result = enterTagTextWindow.ShowDialog();
                //Если окно успешно закрыто, и если был введён тег
                if (result.GetValueOrDefault(false) && enterTagTextWindow.IsContainTagName)
                    //Выполняем редактирование тега
                    EditTag(
                        enterTagTextWindow.GetTag(), 
                        enterTagTextWindow.TagGroupName, 
                        selectedTag, 
                        selectedGroup);
            }
        }

        /// <summary>
        ///  Обработчик события выбора тега в группе
        /// </summary>
        /// <param name="group">Контролл выбранной группы тегов</param>
        private void Elem_SelectControl(TagsGroup group)
        {
            //Проходимся по группам в панели
            foreach (TagsGroup gr in TagsPanel.Children)
                //Если мы обрабатываем не текущую выбранную группу
                if (gr.GroupName != group.GroupName)
                    //Сбрасываем выделение со всех контроллов
                    gr.SetAllTagsSelectionState(false);
        }


        /// <summary>
        /// Выполняем редактирование тега
        /// </summary>
        /// <param name="tag">Тег для редактирования</param>
        /// <param name="groupName">Имя группы для данного тега</param>
        /// <param name="selectedTag">Контроллл редактируемого тега</param>
        /// <param name="selectedGroup">Группа редактируемого тега</param>
        private void EditTag(TagInfo tag, string groupName, TagControl selectedTag, TagsGroup selectedGroup)
        {
            //Если группа у тега не поменялась
            if (selectedGroup.GroupName.ToLower().Equals(groupName.ToLower()))
                //Просто обновляем информацию о теге
                selectedTag.TagValue = tag;
            //В противном случае
            else
            {
                //Удаляем контролл тега из текущей группы
                DeleteTag(selectedTag, selectedGroup);
                //Добавляем тег в новую группу, как новый контролл
                AddNewTag(tag, groupName);
            }
        }

        /// <summary>
        /// Выполняем удаление тега из группы
        /// </summary>
        /// <param name="tag">Тег для удаления</param>
        /// <param name="group">Группа для удаления тега</param>
        private void DeleteTag(TagControl tag, TagsGroup group)
        {
            //Удаляем контролл тега из текущей группы
            group.DeleteTag(tag);
            //Если в группе тегов не осталось
            if (!group.IsContainChildTags)
                //Удаляем контролл группы с панели
                RemoveGroup(group);
        }

        /// <summary>
        /// Добавляем тег в целевую группу
        /// </summary>
        /// <param name="tag">Тег для добавления</param>
        /// <param name="groupName">Имя группы для данного тега</param>
        private void AddNewTag(TagInfo tag, string groupName)
        {
            //Получаем контролл группы тегов по введённому имени
            TagsGroup group = FindGroupControlFromName(groupName);
            //Если такой группы нет
            if (group == null)
            {
                //Инициализируем группу тегов
                TagGroup tagGroup = new TagGroup(tag, groupName);
                //Формируем контролл группы и добавляем на панель
                TagsPanel.Children.Add(CreateTagGroup(tagGroup));
            }
            //Если такая группа есть
            else
                //Просто добавляем тег в контролл группы
                group.AddTag(tag);
        }

        /// <summary>
        /// Получаем контролл группы тегов по имени
        /// </summary>
        /// <param name="groupName">Имя группы тегов</param>
        /// <returns>Контролл группы тегов</returns>
        private TagsGroup FindGroupControlFromName(string groupName)
        {
            //Проходимся по контроллам групп
            foreach (TagsGroup group in TagsPanel.Children)
                //Если имя контролла группы совпало с переданным
                if (group.GroupName.ToLower().Equals(groupName.ToLower()))
                    //Возвращаем этот контролл
                    return group;
            //Если поиск не дал ничего - возвращаем Null
            return null;
        }

        /// <summary>
        /// Получаем выбранный тег
        /// </summary>
        /// <param name="selectedGroup">Выбранная группа</param>
        /// <param name="selectedTag">Выбранный тег</param>
        /// <returns>Контролл выбранного тега</returns>
        private void GetSelectedTag(ref TagControl selectedTag, ref TagsGroup selectedGroup)
        {
            //Проходимся по контроллам групп
            foreach (TagsGroup group in TagsPanel.Children)
            {
                //Получаем выбранный контролл тега
                selectedTag = group.GetSelectedTagControl();
                //Если такой найден
                if (selectedTag != null)
                {
                    //Запоминаем выбранную группу
                    selectedGroup = group;
                    //Выходим из цикла
                    break;
                }
            }
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
            //Проставляем единичное выделение
            elem.IsOneSelected = true;
            //Вставляем группу в контролл
            elem.SetGroup(group);
            //Удаляем обработчик события выбора тега в группе
            elem.SelectControl += Elem_SelectControl;
            //Возвращаем результат
            return elem;
        }

        /// <summary>
        /// Удаляем все группы тегов с панели
        /// </summary>
        private void RemoveGroups()
        {
            //Проходимся по группам в панели
            foreach (TagsGroup group in TagsPanel.Children)
                //Удаляем обработчик события выбора тега в группе
                group.SelectControl -= Elem_SelectControl;
            //Удаляем все группы с панели
            TagsPanel.Children.Clear();
        }

        /// <summary>
        /// Метод удаления контролла группы тегов
        /// </summary>
        /// <param name="group">Контролл для удаления</param>
        private void RemoveGroup(TagsGroup group)
        {
            //Удаляем обработчик события выбора тега в группе
            group.SelectControl -= Elem_SelectControl;
            //Удаляем группу с панели
            TagsPanel.Children.Remove(group);
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
            RemoveGroups();
            //Проходимся по группам
            foreach (TagGroup group in tags.Groups)
                //Добавляем группы на контролл
                TagsPanel.Children.Add(CreateTagGroup(group));
        }

    }
}
