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
        /// Флаг отображения кнопки удаления тега
        /// </summary>
        public bool IsRemoveButtonVisible
        {
            get => (RemoveButtonColumn.Width.Value == 40);
            set => RemoveButtonColumn.Width = (value) ? new GridLength(40) : new GridLength(0);
        }

        /// <summary>
        /// Текст тега
        /// </summary>
        public string TagText
        {
            get => TagNameRun.Text;
            set => TagNameRun.Text = value;
        }

        /// <summary>
        /// Буква тега
        /// </summary>
        public Key? TagLetter
        {
            set => TagLetterRun.Text = (value.HasValue) ? $"[ {value.Value.ToString()} ]" : "";
        }

        /// <summary>
        /// Конструктор 
        /// </summary>
        public TagControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку запроса удаления тега
        /// </summary>
        private void RemoveTagButton_Click(object sender, RoutedEventArgs e) =>
            //Вызываем запрос удаления тега
            DeleteTagRequest?.Invoke(this);

    }
}
