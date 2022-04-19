using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TegSetter.Content.Clases.DataClases.Info;
using TegSetter.Content.Clases.DataClases.Info.Tag;

namespace TegSetter.Content.Clases.DataClases.Global
{
    /// <summary>
    /// КЛасс управления глобальными событиями
    /// </summary>
    internal class GlobalEvents
    {
        /// <summary>
        /// Делегат солбытия запроса на смену страницы
        /// </summary>
        public delegate void ChangePageRequestEventHandler();
        /// <summary>
        /// Делегат события запроса на добавление тега картинке
        /// </summary>
        /// <param name="tag">Тег для добавления</param>
        public delegate void AddTagRequestEventHandler(TagInfo tag);

        /// <summary>
        /// Событие запроса перехода на следующую страницу
        /// </summary>
        public static event ChangePageRequestEventHandler GoToNextPageRequest;
        /// <summary>
        /// Событие запроса перехода на предыдущую страницу
        /// </summary>
        public static event ChangePageRequestEventHandler GoToBackPageRequest;
        /// <summary>
        /// Событие запроса на добавление тега картинке
        /// </summary>
        public static event AddTagRequestEventHandler AddTagRequest;




        /// <summary>
        /// Метод вызова события запроса на переход к следующей странице
        /// </summary>
        public static void InvokeGoToNextPageRequest() =>
            GoToNextPageRequest?.Invoke();

        /// <summary>
        /// Метод вызова события запроса на переход к предыдущей странице
        /// </summary>
        public static void InvokeGoToBackPageRequest() =>
            GoToBackPageRequest?.Invoke();

        /// <summary>
        /// Метод вызова события запроса на добавление тега картинке
        /// </summary>
        /// <param name="tag">Тег для добавления</param>
        public static void InvokeAddTagRequest(TagInfo tag) =>
            AddTagRequest?.Invoke(tag);


    }
}
