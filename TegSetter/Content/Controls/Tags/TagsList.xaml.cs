﻿using System;
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
    /// Логика взаимодействия для TagsList.xaml
    /// </summary>
    public partial class TagsList : UserControl
    {
        /// <summary>
        /// Делегат события запроса на обновление списка тегов
        /// </summary>
        public delegate void UpdateTagListRequestEventHandler();
        /// <summary>
        /// Событие обновления списка тегов
        /// </summary>
        public event UpdateTagListRequestEventHandler UpdateTagListRequest;


        /// <summary>
        /// Конструктор контролла
        /// </summary>
        public TagsList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик событяи нажатия на кнопку обновления списка тегов
        /// </summary>
        private void EditTagsListButton_Click(object sender, RoutedEventArgs e) =>
            //Запрашиваем обновление списка тегов
            UpdateTagListRequest?.Invoke();

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
                TagsListBox.Children.Remove(tag);
            }
        }

        /// <summary>
        /// Инициализируем контролл тега
        /// </summary>
        /// <param name="tag">Тег для добавления контролла</param>
        /// <param name="letter">Буква тега</param>
        /// <returns>Созданный контролл</returns>
        private TagControl CreateTagControl(string tag, Key letter)
        {
            //Инициализируем контролл тега
            TagControl elem = new TagControl() {
                TagText = tag,
                IsRemoveButtonVisible = false,
                TagLetter = letter
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
            foreach (TagControl elem in TagsListBox.Children)
                //Выполняем удаление ивентов контроллла тега
                RemoveTagEvents(elem);
            //Удаляем все контроллы с панели
            TagsListBox.Children.Clear();
        }




        /// <summary>
        /// Проставляем новый список тегов
        /// </summary>
        /// <param name="tags">Список тегов для добавления</param>
        public void SetTags(Dictionary<Key, string> tags)
        {
            // Выполняем удаление всех тегов с панели
            RemoveTags();
            //Проходимся по строкам тегов, и добавляем только уникальные
            foreach (KeyValuePair<Key, string> tag in tags)
                //Генерируем контроллы тегов и добавляем на панель
                TagsListBox.Children.Add(CreateTagControl(tag.Value, tag.Key));
        }

    }
}
