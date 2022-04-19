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
using TegSetter.Content.Clases.DataClases.Info;
using TegSetter.Content.Clases.DataClases.Info.Tag;

namespace TegSetter.Content.Controls.Tags
{
    /// <summary>
    /// Логика взаимодействия для TagControl.xaml
    /// </summary>
    public partial class TagControl : UserControl
    {
        /// <summary>
        /// Делегат события запроса на удаление тега
        /// </summary>
        /// <param name="tag">Контролл с тегом</param>
        public delegate void DeleteTagRequestEventHandler(TagControl tag);
        /// <summary>
        /// Событие запроса на удаление тега
        /// </summary>
        public event DeleteTagRequestEventHandler DeleteTagRequest;

        /// <summary>
        /// Делегат события выделения контролла
        /// </summary>
        /// <param name="tag">Выделенный контролл</param>
        public delegate void SelectControlEventHandler(TagControl tag);
        /// <summary>
        /// Cобытие выделения контролла
        /// </summary>
        public event SelectControlEventHandler SelectControl;


        /// <summary>
        /// Флаг отображения кнопки удаления тега
        /// </summary>
        public bool IsRemoveButtonVisible
        {
            get => RemoveTagButton.Visibility == Visibility.Visible;
            set => RemoveTagButton.Visibility =
                (value) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Флаг отображения буквы тега
        /// </summary>
        public bool IsTagLetterVisible
        {
            get => TagLetterTextBlock.Visibility == Visibility.Visible;
            set => TagLetterTextBlock.Visibility =
                (value) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Класс информации о теге
        /// </summary>
        public TagInfo TagValue
        {
            get => _tag;
            set => SetTag(value);
        }

        /// <summary>
        /// Флаг выбора элемента
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set => UpdateSelection(value);
        }


        /// <summary>
        /// Флаг, разрешающий выбор элементов
        /// </summary>
        public bool IsAllowSelected { get; set; }

       

        /// <summary>
        /// Класс информации о теге
        /// </summary>
        private TagInfo _tag;
        /// <summary>
        /// Флаг выбора элемента
        /// </summary>
        public bool _isSelected;


        /// <summary>
        /// Конструктор 
        /// </summary>
        public TagControl()
        {
            InitializeComponent();
            Init();
        }
        
        /// <summary>
        /// Инициализатор контролла
        /// </summary>
        private void Init()
        {
            //Проставляем дефолтные значения
            _tag = new TagInfo();
            _isSelected = false;
            IsAllowSelected = false;
        }

        /// <summary>
        /// Обработчик события клика по контроллу
        /// </summary>
        private void BackgroundBorder_MouseDown(object sender, MouseButtonEventArgs e) =>
            //Вызываем ивент выделения тега
            SelectControl?.Invoke(this);

        /// <summary>
        /// Обработчик события нажатия на кнопку запроса удаления тега
        /// </summary>
        private void RemoveTagButton_Click(object sender, RoutedEventArgs e) =>
            //Вызываем запрос удаления тега
            DeleteTagRequest?.Invoke(this);


        /// <summary>
        /// Проставляем в контролл информацию о теге
        /// </summary>
        /// <param name="tag">Информация о теге</param>
        private void SetTag(TagInfo tag)
        {
            //Проставляем переданные значения
            _tag = tag;
            //Проставляем имя тега и описание
            TagNameTextBlock.Text = tag.Name;
            ToolTipTextBlock.Text = tag.Description;
            TagLetterTextBlock.Text = (tag.Letter.HasValue) ? $"[ {tag.Letter.Value} ]" : "";
        }

        /// <summary>
        /// ВЫполняем обновление выделения
        /// </summary>
        /// <param name="isSelected">Флаг выделения</param>
        private void UpdateSelection(bool isSelected)
        {
            //Если выделение разрешено
            if (IsAllowSelected)
            {
                //Проставляем переданное значение
                _isSelected = isSelected;
                //Обновляем цвет фона контролла
                BackgroundBorder.Background = (isSelected)
                    ? Brushes.LightBlue : Brushes.White;
            }
        }

    }
}
