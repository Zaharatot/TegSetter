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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TegSetter.Content.Clases.DataClases.Info.Tag;

namespace TegSetter.Content.Controls.Tags
{
    /// <summary>
    /// Логика взаимодействия для TagsGroup.xaml
    /// </summary>
    public partial class TagsGroup : UserControl
    {
        /// <summary>
        /// Флаг одиночного выделения
        /// </summary>
        public bool IsOneSelected { get; set; }


        /// <summary>
        /// Конструктор контролла
        /// </summary>
        public TagsGroup()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Инициализатор класса
        /// </summary>
        private void Init()
        {
            //Проставляем дефолтные значения
            IsOneSelected = false;
        }


        /// <summary>
        /// Обработчик события выбора тега
        /// </summary>
        /// <param name="tag">Контролл с выбранным тегом</param>
        private void Elem_SelectControl(TagControl tag)
        {
            //Если может быть выделен только один элемент
            if(IsOneSelected)
            {
                //Проходимся по тегам
                foreach (TagControl elem in TagsPanel.Children)
                    //Сбрасываем им выделение
                    elem.IsSelected = false;
                //Проставляем текущему тэгу выделение
                tag.IsSelected = true;
            }
            //Если может быть выделено несколько элементов
            else
                //Инвертируем5 текущему тэгу выделение
                tag.IsSelected = !tag.IsSelected;
            
        }

        /// <summary>
        /// Создаём контролл для тега
        /// </summary>
        /// <param name="tag">Инфомрация о теге</param>
        /// <returns>Контролл тега</returns>
        private TagControl CreateTagControl(TagInfo tag)
        {
            //Инициализируем контролл тега
            TagControl elem = new TagControl()
            {
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
        /// Удаляем все теги с панели
        /// </summary>
        private void RemoveTags()
        {
            //Проходимся по тегам в панели
            foreach (TagControl tag in TagsPanel.Children)
                //Удаляем обработчик события выбора тега
                tag.SelectControl -= Elem_SelectControl;
            //Удаляем все группы с панели
            TagsPanel.Children.Clear();
        }

        /// <summary>
        /// Получаем список тегов контролла
        /// </summary>
        /// <returns>Список тегов контролла</returns>
        public List<TagInfo> GetTags()
        {
            //Инициализируем выходной список
            List<TagInfo> ex = new List<TagInfo>();
            //Проходимся по тегам в панели
            foreach (TagControl tag in TagsPanel.Children)
                //Добавляем его в список
                ex.Add(tag.TagValue);
            //Возвращаем результат
            return ex;
        }




        /// <summary>
        /// Проставляем выбранные теги
        /// </summary>
        /// <param name="tags">Список имён выбранных тегов</param>
        public void SetSelectedTags(List<string> tags)
        {
            //Проходимся по тегам в панели
            foreach (TagControl tag in TagsPanel.Children)
                //Если имя тега есть в списке - выделяем тег
                tag.IsSelected = tags.Contains(tag.TagValue.Name);            
        }

        /// <summary>
        /// Метод простановки статуса выделения всем тегам
        /// </summary>
        /// <param name="state">Статус для простановки</param>
        public void SetAllTagsSelectionState(bool state)
        {
            //Проходимся по тегам в панели
            foreach (TagControl tag in TagsPanel.Children)
                //Проставляем всем нужный статус
                tag.IsSelected = state;
        }

        /// <summary>
        /// Метод простановки группы тегов вв контролл
        /// </summary>
        /// <param name="group">Группа тегов для простановки</param>
        public void SetGroup(TagGroup group)
        {
            //Проставляем имя группы в заголоввок контролла
            GroupExpander.Header = group.GetGroupHeader();
            //Удаляем все теги с панели
            RemoveTags();
            //Проходимся по группе, отсортированной по имени
            foreach (TagInfo tag in group.GetOrderedTags())
                //Добавляем теги на панель
                TagsPanel.Children.Add(CreateTagControl(tag));
        }

        /// <summary>
        /// Получаем список выбранных тегов
        /// </summary>
        /// <returns>Список выбранных тегов</returns>
        public List<TagInfo> GetSelectedTags()
        {
            //Инициализируем выходной список
            List<TagInfo> ex = new List<TagInfo>();
            //Проходимся по тегам в панели
            foreach (TagControl tag in TagsPanel.Children)
                //Если тег выбран
                if (tag.IsSelected)
                    //Добавляем его в список
                    ex.Add(tag.TagValue);
            //Возвращаем результат
            return ex;
        }

        /// <summary>
        /// Получаем первый выделенный тег
        /// </summary>
        /// <returns>Первый выделенный тег</returns>
        public TagControl GetSelectedTagControl()
        {
            //Проходимся по тегам в панели
            foreach (TagControl tag in TagsPanel.Children)
                //Если тег выбран
                if (tag.IsSelected)
                    //Возвращаем его
                    return tag;
            //Возвращаем null, еслли ничего не нашли
            return null;
        }

        /// <summary>
        /// Получаем группу тегов
        /// </summary>
        /// <returns>Группа тегов с контролла</returns>
        public TagGroup GetGroup() =>
            //Формируем группу из списка тегов и заголовка контролла
            new TagGroup(GetTags(), (string)GroupExpander.Header);
    }
}
