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
using TegSetter.Content.Clases.DataClases.Global;
using TegSetter.Content.Clases.DataClases.Info;
using TegSetter.Content.Clases.WorkClases;
using TegSetter.Content.Clases.WorkClases.Keyboard;

namespace TegSetter.Content.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Основной рабочий класс
        /// </summary>
        private MainWork _mainWork;

        /// <summary>
        /// Конструктор окна
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Инициализатор окна
        /// </summary>
        private void Init()
        {
            InitVariables();
            InitEvents();
        }

        /// <summary>
        /// Инициализируем значения переменных
        /// </summary>
        private void InitVariables()
        {
            //Инициализируем испольуемые классы
            _mainWork = new MainWork();
        }
        
        /// <summary>
        /// Инициализатор обработчиков событий
        /// </summary>
        private void InitEvents()
        {
            //Добавляем обработчик события запроса на обновления списка тегов для работы
            TagsListControl.UpdateTagListRequest += TagsListControl_UpdateTagListRequest;
            //Добавляем обработчик события смены страницы
            PagingControl.UpdatePage += PagingControl_UpdatePage;
            //Добавляем обработчик глобального события нажатия кнопки клавиатуры, с фокусом на любом элементе окна
            this.AddHandler(UIElement.PreviewKeyDownEvent, new RoutedEventHandler(MainWindow_KeyDown));
            //Добавляем обработчик события запроса на добавление тега картинке
            GlobalEvents.AddTagRequest += GlobalEvents_AddTagRequest;
            //Добавляем обработчик события запроса на переход к предыдущей странице
            GlobalEvents.GoToBackPageRequest += GlobalEvents_GoToBackPageRequest;
            //Добавляем обработчик события запроса на переход к следующей странице
            GlobalEvents.GoToNextPageRequest += GlobalEvents_GoToNextPageRequest;
        }

        /// <summary>
        /// Обработчик события запроса на переход к следующей странице
        /// </summary>
        private void GlobalEvents_GoToNextPageRequest() =>
            //Выполняем переход вперёд
            PagingControl.GoNext();

        /// <summary>
        /// Обработчик события запроса на переход к предыдущей странице
        /// </summary>
        private void GlobalEvents_GoToBackPageRequest() =>
            //Выполняем переход назад
            PagingControl.GoBack();

        /// <summary>
        /// Обработчик события запроса на добавление тега картинке
        /// </summary>
        /// <param name="tag">Тег для добавления</param>
        private void GlobalEvents_AddTagRequest(string tag)
        {
            //Получаем идентификатор страницы
            int id = PagingControl.GetPageId();
            //Добавляем картинке тег
            _mainWork.AddImageTag(id, tag);
            //Добавляем тег в контролл тегов картинки
            ImageTagsControl.AddTag(tag);
        }

        /// <summary>
        /// Обработчик событяи нажатия на кнопку
        /// </summary>
        private void MainWindow_KeyDown(object sender, RoutedEventArgs e) =>
            //Вызываем обработку нажатия на клавишу
            _mainWork.ProcessKeyPress((System.Windows.Input.KeyEventArgs)e);


        /// <summary>
        /// Обработчик события смены страницы
        /// </summary>
        /// <param name="currentId">Идентификатор новой выбранной страницы</param>
        private void PagingControl_UpdatePage(int currentId)
        {
            //Загруждаем картинку по идентификатору
            ImageInfo image = _mainWork.LoadImage(currentId);
            //Проставляем теги картинки в контролл
            ImageTagsControl.SetTags(image.Tags);
            //ЗАгружаем саму карртинку
            ImageControl.LoadImage(image.Path);
        }

        /// <summary>
        /// Обработчик события запроса на обновления списка тегов для работы
        /// </summary>
        private void TagsListControl_UpdateTagListRequest()
        {
            //Инициализируем окно выбора тегов
            TagSelectorWindow tagSelectorWindow = new TagSelectorWindow();
            //Загружаем в окно полный список тегов
            tagSelectorWindow.SetTagsToList(_mainWork.GetTags());
            //Отображаем окно как диалоговое
            bool? result = tagSelectorWindow.ShowDialog();
            //Если окно закрылось с успехом
            if (result.GetValueOrDefault(false))
            {
                //Получаем выбранные теги
                List<string> tags = tagSelectorWindow.GetSelectedTags();
                //Пейрим теги к кнопкам
                Dictionary<Key, string> tagsDict = _mainWork.PairKeysToTags(tags);
                //ПРоставляем в панель словарь тегов
                TagsListControl.SetTags(tagsDict);
                //Проставляем словарь тегов для обработки нажатий
                _mainWork.SetNewTagsDict(tagsDict);
            }
        }

        /// <summary>
        /// Обработчик событяи клика по кнопке загрузки папки
        /// </summary>
        private void LoadFolderButton_Click(object sender, RoutedEventArgs e)
        {
            //Инициализируем окно выбора папки
            OpenFolderWinodw openFolderWinodw = new OpenFolderWinodw();
            //Отображаем окно как диалоговое и получаем результат
            bool? result = openFolderWinodw.ShowDialog();
            //Если выбор был успешен
            if (result.GetValueOrDefault(false))
            {
                //Выполняем загрузку изображений для указанной папки
                _mainWork.LoadImages(openFolderWinodw.SelectedPath);
                //Обновляем список тегов по загруженным картинкам
                UpdateTagsList();
                //Обновляем количество картинок
                PagingControl.SetMaxId(_mainWork.GetImagesCount());
            }
        }

        /// <summary>
        /// Обработчик событяи клика по кнопке сохранения изменений
        /// </summary>
        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            //Запрашиваем у пользователя подтверждение
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите сохранить изменения? Выбранные " +
                "теги перезапишут всё что было в изображениях до этого!", "Запрос сохранения", MessageBoxButton.YesNo);
            //Если юзер согласился
            if (result == MessageBoxResult.Yes)
            {
                //Выполняем сохранение тегов
                _mainWork.SaveImagesTags();
                //Выводим сообщение с инфой
                MessageBox.Show("Изменения были успешно сохранены! " +
                    "Чтобы обновить метаданные в 'F-Stop', нужно выделить изображения " +
                    "и выбрать пункт 'Revert metadata from disk'.");
            }
        }

        /// <summary>
        /// Обработчик событяи клика по кнопке окна тегов
        /// </summary>
        private void TagsWindowButton_Click(object sender, RoutedEventArgs e)
        {
            //Инициализируем окно редактирования списка тегов
            EditTagsWindow editTagsWindow = new EditTagsWindow();
            //Проставляем теги в окно
            editTagsWindow.SetTags(_mainWork.GetTags());
            //Отображаем окно как диалоговое
            bool? result = editTagsWindow.ShowDialog();
            //Если окно закрылось с успехом
            if (result.GetValueOrDefault(false))
                //Проставляем список тегов для сохранения
                _mainWork.SetTags(editTagsWindow.GetTags());
        }

        /// <summary>
        /// Обновляем список тегов по загруженным картинкам
        /// </summary>
        private void UpdateTagsList()
        {
            //Получаем список новых тегов из картинок
            List<string> newTags = _mainWork.GetNewTags();
            //Если были загружены новые теги
            if (newTags.Count > 0)
            {
                //Инициализируем окно выбора тегов
                TagSelectorWindow tagSelectorWindow = new TagSelectorWindow();
                //Загружаем в окно список тегов для добавления
                tagSelectorWindow.SetTagsToList(newTags);
                //Отображаем окно как диалоговое
                bool? result = tagSelectorWindow.ShowDialog();
                //Если окно закрылось с успехом
                if(result.GetValueOrDefault(false))
                    //Добавляем выбранные теги в список
                    _mainWork.AddTags(tagSelectorWindow.GetSelectedTags());
            }
        }
    }
}
