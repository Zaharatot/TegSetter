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
using TegSetter.Content.Clases.DataClases.Info.Tag;

namespace TegSetter.Content.Windows
{
    /// <summary>
    /// Логика взаимодействия для SetTagsWindow.xaml
    /// </summary>
    public partial class SetTagsWindow : Window
    {
        /// <summary>
        /// Текущая выбранная коллекция тегов
        /// </summary>
        private TagsCollection _tags;
        /// <summary>
        /// Выбранные теги по группам
        /// </summary>
        private List<TagInfo>[] _selectedTags;
        /// <summary>
        /// Идентификатор текущей выбранной страницы
        /// </summary>
        private int _currentSelectId;

        /// <summary>
        /// Конструктор окна
        /// </summary>
        public SetTagsWindow()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Инициализатор окна
        /// </summary>
        private void Init()
        {
            //Инициализируем дефолтные значения
            _tags = new TagsCollection();
            _selectedTags = new List<TagInfo>[0];
            _currentSelectId = 0;
            //Проставляем параметры блока группы
            GroupBlock.IsExpanded = true;
            GroupBlock.IsAlwaysExpanded = true;
            //Добавляем обработчик события смены страницы
            GroupsPaging.UpdatePage += GroupsPaging_UpdatePage;
        }

        /// <summary>
        /// Обработчик события смены страницы
        /// </summary>
        /// <param name="currentId">Идентификатор новой выбранной страницы</param>
        private void GroupsPaging_UpdatePage(int currentId)
        {
            //Если идентификатор корректен
            if ((currentId >= 0) && (currentId < _tags.Groups.Count))
            {
                //Если выбрана не текущая страница
                if(_currentSelectId != currentId)
                    //Получаем теги с текущей страницы
                    SetTagsToSelectedList(_currentSelectId);
                //Проставляем в контролл выбранную группу
                GroupBlock.SetGroup(_tags.Groups[currentId]);
                //Проставляем текущие выбранные для группы теги
                GroupBlock.SetSelectedTags(GetSelectedTagNames(currentId));
                //Обновляем идентификатор страницы
                _currentSelectId = currentId;
            }
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
        /// Обработчик события клика по кнопке простановки тегов
        /// </summary>
        private void SetTagsButton_Click(object sender, RoutedEventArgs e) =>
            //Успешно завершаем работу с окном
            this.DialogResult = true;

        /// <summary>
        /// Обработчик события клика по кнопке отмены простановки
        /// </summary>
        private void CancelTagsButton_Click(object sender, RoutedEventArgs e) =>
            //Отменяем работу с окном
            this.DialogResult = false;

        /// <summary>
        /// Создаём массив для выбранных тегов
        /// </summary>
        /// <param name="count">Количество групп</param>
        /// <returns>Массив списков выбранных тегов по группам</returns>
        private List<TagInfo>[] CreateSeletedTags(int count)
        {
            //Инициализируем массив списков
            List<TagInfo>[] ex = new List<TagInfo>[count];
            //Инициализируем каэдый элемент массива
            for (int i = 0; i < count; i++)
                ex[i] = new List<TagInfo>();
            //Возвращаем результат
            return ex;
        }

        /// <summary>
        /// Возвращаем список имён выбранных тегов
        /// </summary>
        /// <param name="id">Идентификатор группы выбранного тега</param>
        /// <returns>Список имён выбранных тегов</returns>
        private List<string> GetSelectedTagNames(int id) =>
             _selectedTags[id].Select(tag => tag.Name).ToList();

        /// <summary>
        /// Проставляем теги в массив
        /// </summary>
        /// <param name="id">Идентификатор группы выбранного тега</param>
        private void SetTagsToSelectedList(int id) =>
            //Обновляем выбранные для группы теги
            _selectedTags[id] = GroupBlock.GetSelectedTags();

        /// <summary>
        /// Проставляем выбранные теги
        /// </summary>
        /// <param name="tagNames">Имена выбранных тегов</param>
        private void SetSelectedTags(List<string> tagNames)
        {
            //Проходимся по группам
            for (int i = 0; i < _tags.Groups.Count; i++)
                //Проходимся по тегам группы
                foreach (TagInfo tag in _tags.Groups[i].Tags)
                    //Если тег имеет имя из списка
                    if (tagNames.Contains(tag.Name))
                        //Добавляем его в выделенные
                        _selectedTags[i].Add(tag);
        }



        /// <summary>
        /// Метод получения всех выбранных на всех страницах тегов
        /// </summary>
        /// <returns>Список выбранных тегов</returns>
        public List<TagInfo> GetSelectedTags()
        {
            //Получаем теги с текущей страницы
            SetTagsToSelectedList(_currentSelectId);
            //Выполняем аггрегатную функцию, которая объединит в один список все дочерние
            return _selectedTags.Aggregate(new List<TagInfo>(), (x, y) => x.Concat(y).ToList());
        }

        /// <summary>
        /// Проставляем коллекцию тегов
        /// </summary>
        /// <param name="tags">Коллекция тегов для работы</param>
        /// <param name="tagNames">Список выбранныхз тегов</param>
        public void SetCollection(TagsCollection tags, List<string> tagNames)
        {
            //Запоминаем переданную коллекцию тегов
            _tags = tags;
            //Инициализируем массив выбранных тегов по группам
            _selectedTags = CreateSeletedTags(_tags.Groups.Count);
            //Проставляем выбранные теги
            SetSelectedTags(tagNames);
            //Сбрасываем идентификатор текущей выбранной страницы
            _currentSelectId = 0;
            //Обновляем количество картинок
            GroupsPaging.SetMaxId(_tags.Groups.Count);
        }
    }
}
