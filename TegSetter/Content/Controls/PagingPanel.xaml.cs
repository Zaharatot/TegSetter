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

namespace TegSetter.Content.Controls
{
    /// <summary>
    /// Логика взаимодействия для PagingPanel.xaml
    /// </summary>
    public partial class PagingPanel : UserControl
    {
        /// <summary>
        /// Делегат события обновления номера выбранной страницы
        /// </summary>
        /// <param name="currentId">Идентификатор новой выбранной страницы</param>
        public delegate void UpdatePageEventHandler(int currentId);
        /// <summary>
        /// Событие обновления номера выбранной страницы
        /// </summary>
        public event UpdatePageEventHandler UpdatePage;

        /// <summary>
        /// Идентификатор текущей выбранной страницы
        /// </summary>
        private int _currentPageId;
        /// <summary>
        /// Максимальное значение идентификатора страницы
        /// </summary>
        private int _maxId;

        /// <summary>
        /// Конструктор контролла
        /// </summary>
        public PagingPanel()
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
            _maxId = _currentPageId = 0;
        }


        /// <summary>
        /// Обработчик события клика по кнопке перехода к началу
        /// </summary>
        private void ToStartButton_Click(object sender, RoutedEventArgs e) => GoToStart();

        /// <summary>
        /// Обработчик события клика по кнопке перехода назад
        /// </summary>
        private void BackButton_Click(object sender, RoutedEventArgs e) => GoBack();

        /// <summary>
        /// Обработчик события клика по кнопке перехода вперёд
        /// </summary>
        private void NextButton_Click(object sender, RoutedEventArgs e) => GoNext();

        /// <summary>
        /// Обработчик события клика по кнопке перехода к концу
        /// </summary>
        private void ToEndButton_Click(object sender, RoutedEventArgs e) => GoToEnd();


        /// <summary>
        /// Обновляем отображение страниц
        /// </summary>
        private void UpdatePages()
        {
            //Если картинки вообще есть
            if (_maxId > 0)
            {
                //Проставляем значения в контроллы
                CurrentIdRun.Text = (_currentPageId + 1).ToString();
                MaxIdRun.Text = _maxId.ToString();
                //Вызываем ивент количества
                UpdatePage?.Invoke(_currentPageId);
            }
            //Если картинок нет
            else
                //Просто ставим прочерки
                CurrentIdRun.Text = MaxIdRun.Text = "-";
        }

        /// <summary>
        /// Получаем значение ограниченное с двух сторон
        /// </summary>
        /// <param name="val">Значение для обрезки</param>
        /// <param name="max">Максимум значения</param>
        /// <param name="min">Минимум значения</param>
        /// <returns></returns>
        private int Clamp(int val, int max, int min) =>
            Math.Min(Math.Max(val, min), max);

        /// <summary>
        /// Выполняем переход к следующей странице
        /// </summary>
        public void GoNext()
        {
            //Выполняем обновление значения страницы
            _currentPageId = Clamp(_currentPageId + 1, _maxId - 1, 0);
            //Обновляем отображение страниц
            UpdatePages();
        }

        /// <summary>
        /// Выполняем переход к предыдущей странице
        /// </summary>
        public void GoBack()
        {
            //Выполняем обновление значения страницы
            _currentPageId = Clamp(_currentPageId - 1, _maxId, 0);
            //Обновляем отображение страниц
            UpdatePages();
        }

        /// <summary>
        /// Выполняем переход к первой странице
        /// </summary>
        public void GoToStart()
        {
            //Сбрасываем выбранную страницу
            _currentPageId = 0;
            //Обновляем отображение страниц
            UpdatePages();
        }

        /// <summary>
        /// Выполняем переход к последней странице
        /// </summary>
        public void GoToEnd()
        {
            //Сбрасываем выбранную страницу
            _currentPageId = _maxId - 1;
            //Обновляем отображение страниц
            UpdatePages();
        }

        /// <summary>
        /// Метод получения идентификатора страницы
        /// </summary>
        /// <returns>Идентификатор страницы</returns>
        public int GetPageId() => 
            _currentPageId;

        /// <summary>
        /// Проставляем максимальное  количество страниц
        /// </summary>
        /// <param name="maxId">Новый максимум количества страниц</param>
        public void SetMaxId(int maxId)
        {
            //Проставляем переданное значение
            _maxId = maxId;
            //Сбрасываем выбранную страницу
            _currentPageId = 0;
            //Обновляем отображение страниц
            UpdatePages();
        }
    }
}
