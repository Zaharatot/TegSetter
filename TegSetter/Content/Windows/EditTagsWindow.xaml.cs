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
            //Если нажат энтер
            if (e.Key == Key.Enter)
                //Успешно завершаем работу с окном
                this.DialogResult = true;
            else if (e.Key == Key.Escape)
                //Отменяем работу с окном
                this.DialogResult = false;
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
                //Добавляем тег на контролл, в виде чекбоксов
                TagsListBox.Items.Add(CreateTextBlock(enterTagTextWindow.TagText));
                //Указываем на изменение списка
                _isChanged = true;
            }
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку сохранения изменений
        /// </summary>
        private void SaveChangesButton_Click(object sender, RoutedEventArgs e) =>
            //Успешно завершаем работу с окном
            this.DialogResult = true;

        /// <summary>
        /// Обработчик события запроса на закрытие окна
        /// </summary>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //Если есть изменения
            if (_isChanged)
            {
                //Вызываем сообщение и смотрим на результат
                MessageBoxResult result = MessageBox.Show("Есть несохранённые изменения в списке тегов, закрытие окна " +
                    "их сбросит. Вы действительно хотите закрыть окно?", "Подтвержите закрытие", MessageBoxButton.YesNo);
                //Если нажата кнопка "Нет" - отменяем закрцытие
                e.Cancel = (result == MessageBoxResult.No);
            }
        }


        /// <summary>
        /// Формируем контролл текстового блока
        /// </summary>
        /// <param name="tag">Текст тега для блока</param>
        /// <returns>Созданный контролл текстового блока</returns>
        private TextBlock CreateTextBlock(string tag) => 
            new TextBlock() { 
                Margin = new Thickness(5),
                Text = tag
            };



        /// <summary>
        /// Выполняем получение списка тегов
        /// </summary>
        /// <returns>Список тегшов для получения</returns>
        public List<string> GetTags()
        {
            //Инициализируем выходной список
            List<string> ex = new List<string>();
            //Проходимся по текстовым блокам
            foreach (TextBlock textBlock in TagsListBox.Items)
                //Добавляем текст из них в список
                ex.Add(textBlock.Text);
            //Возвращаем результат
            return ex;
        }

        /// <summary>
        /// Выполняем загрузку списка тегов
        /// </summary>
        /// <param name="tags">Список тегов для загрузки</param>
        public void SetTags(List<string> tags)
        {
            //Удаляем все старые элементы из списка
            TagsListBox.Items.Clear();
            //Проходимся по тегам
            foreach (var tag in tags)
                //Добавляем теги на контролл, в виде чекбоксов
                TagsListBox.Items.Add(CreateTextBlock(tag));
            //Указываем, что изменений нет
            _isChanged = false;
        }

    }
}
