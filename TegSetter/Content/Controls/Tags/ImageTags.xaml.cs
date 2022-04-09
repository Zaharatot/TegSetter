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

namespace TegSetter.Content.Controls.Tags
{
    /// <summary>
    /// Логика взаимодействия для ImageTags.xaml
    /// </summary>
    public partial class ImageTags : UserControl
    {

        /// <summary>
        /// Конструктор контролла
        /// </summary>
        public ImageTags()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события запроса на удаление тега
        /// </summary>
        /// <param name="tag">Контролл тега для удаления</param>
        private void Elem_DeleteTagRequest(TagControl tag)
        {
            //Вызываем месседжбокс с запросом удаления
            MessageBoxResult result = MessageBox.Show($"Вы действительно хотите удалить тег '{tag.TagText}'?", 
                "Запрос удаления", MessageBoxButton.YesNo);
            //Удаляем только при подтверждении
            if (result == MessageBoxResult.Yes)
            {
                //Выполняем удаление ивентов контроллла тега
                RemoveTagEvents(tag);
                //Удаляем тег с панели
                TagsPanel.Children.Remove(tag);
            }
        }

        /// <summary>
        /// Инициализируем контролл тега
        /// </summary>
        /// <param name="tag">Тег для добавления контролла</param>
        /// <returns>Созданный контролл</returns>
        private TagControl CreateTagControl(string tag)
        {
            //Инициализируем контролл тега
            TagControl elem = new TagControl() { 
                TagText = tag,
                IsRemoveButtonVisible = true,
                TagLetter = null
            };
            //Добавляем обработчик события запроса на удаление тега
            elem.DeleteTagRequest += Elem_DeleteTagRequest;
            //Возвращаем результат
            return elem;
        }

        /// <summary>
        /// Выполняем удаление ивентов контроллла тега
        /// </summary>
        /// <param name="elem">Контролл тега для удаления ивентов</param>
        private void RemoveTagEvents(TagControl elem)
        {
            //Удалем обработчик события запроса на удаление тега
            elem.DeleteTagRequest += Elem_DeleteTagRequest;
        }

        /// <summary>
        /// Выполняем удаление всех тегов с панели
        /// </summary>
        private void RemoveTags()
        {
            //Проходимся по тегам панели
            foreach(TagControl elem in TagsPanel.Children)
                //Выполняем удаление ивентов контроллла тега
                RemoveTagEvents(elem);
            //Удаляем все контроллы с панели
            TagsPanel.Children.Clear();
        }


        /// <summary>
        /// Проставляем теги в контролл
        /// </summary>
        /// <param name="tags">Список тегов изображения</param>
        public void SetTags(List<string> tags)
        {
            // Выполняем удаление всех тегов с панели
            RemoveTags();
            //Проходимся по строкам тегов, и добавляем только уникальные
            foreach (string tag in tags.Distinct())
                //Генерируем контроллы тегов и добавляем на панель
                TagsPanel.Children.Add(CreateTagControl(tag));
        }

        /// <summary>
        /// Добавляем тег контроллу
        /// </summary>
        /// <param name="tag">Тег для добавления</param>
        public void AddTag(string tag)
        {
            //Если данного тега нет в списке текущих
            if(!GetTags().Contains(tag))
                //Генерируем контроллы тегов и добавляем на панель
                TagsPanel.Children.Add(CreateTagControl(tag));
        }

        /// <summary>
        /// Получаем все теги из контролла
        /// </summary>
        /// <returns>Список текущих добавленных тегов</returns>
        public List<string> GetTags()
        {
            //Инициализируем список тегов
            List<string> ex = new List<string>();
            //Проходимся по тегам панели
            foreach (TagControl elem in TagsPanel.Children)
                //Добавляем имя тега в список
                ex.Add(elem.TagText);
            //Возвращаем результат, при этом - добавляя только уникальные теги
            return ex.Distinct().ToList();
        }
    }
}
