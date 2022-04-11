using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TegSetter.Content.Clases.DataClases.Global;
using TegSetter.Content.Clases.DataClases.Info;

namespace TegSetter.Content.Clases.WorkClases.Keyboard
{
    /// <summary>
    /// Класс обработки нажатий на кнопки
    /// </summary>
    internal class KeyActionProcessor
    {
        /// <summary>
        /// Класс проверки на то, что данное нажатие нельзя расценивать как хоткей
        /// </summary>
        private HotKeyCheck _notActionKeyCheck;
        /// <summary>
        /// Словарь тегов связанный с клавишами
        /// </summary>
        private Dictionary<Key, TagInfo> _tags;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public KeyActionProcessor()
        {
            Init();
        }

        /// <summary>
        /// Инициализатор класса
        /// </summary>
        private void Init()
        {
            //Инициализируем дефолтные значения
            _tags = new Dictionary<Key, TagInfo>();
            //Инициализируем используемые классы
            _notActionKeyCheck = new HotKeyCheck();
        }

        /// <summary>
        /// Проверка нажатия кнопки "Ctrl" на клавиатуре
        /// </summary>
        /// <param name="e">Информация о нажатой кнопке</param>
        /// <returns>TRue - кнопка "Ctrl" была нажата</returns>
        private bool IsControlPressed(KeyEventArgs e) =>
            (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0;

        /// <summary>
        /// Обрабатываем сочетания с клавишей Ctrl
        /// </summary>
        /// <param name="key">Нажатая кнопка</param>
        /// <returns>True - нажатие было обработано</returns>
        private bool ProcessControlKeys(Key key) => false;

        /// <summary>
        /// Обрабатываем обычные нажатия клавишь
        /// </summary>
        /// <param name="key">Нажатая кнопка</param>
        /// <returns>True - нажатие было обработано</returns>
        private bool ProcessKeys(Key key)
        {
            //Обрабатываем кнопку для основного окна
            bool ex = true;
            //Если нажата кнопка в лево
            if (key == Key.Left)
                //Вызываем ивент перехода назад
                GlobalEvents.InvokeGoToBackPageRequest();
            //Если нажата кнопка в право
            else if (key == Key.Right)
                //Вызываем ивент перехода вперёд
                GlobalEvents.InvokeGoToNextPageRequest();
            //Если кнопка связана с тегом
            else if (_tags.ContainsKey(key))
                //Вызываем ивент добавления тега
                GlobalEvents.InvokeAddTagRequest(_tags[key]);
            //Во всех остальных случаях
            else
                //Нажатие не было обработано
                ex = false;
            //Возвращаемс результат
            return ex;
        }


        /// <summary>
        /// Обрабатываем нажатие на кнопку клавиатуры
        /// </summary>
        /// <param name="e">Информация о нажатой кнопке</param>
        public void ProcessKeyPress(KeyEventArgs e)
        {
            //Если данное нажатие можно обрабатывать как хоткей
            if (!_notActionKeyCheck.IsNotHotkey(e))
            {
                //Если была нажата кнопка "Ctrl"
                bool isKeyProcessed = IsControlPressed(e)
                    //Обрабатываем сочетания с клавишей Ctrl
                    ? ProcessControlKeys(e.Key)
                    //В противном случае обрабатываем обычные нажатия клавишь
                    : ProcessKeys(e.Key);
                //Если нажатие было обработано
                if (isKeyProcessed)
                    //Отменяем дальнейжую обработку нажатий
                    e.Handled = true;
            }
        }

        /// <summary>
        /// Проставляем новый словарь тегов
        /// </summary>
        /// <param name="tags">Новый словарь тегов</param>
        public void SetNewTagsDict(Dictionary<Key, TagInfo> tags) =>
            _tags = tags;
    }
}
