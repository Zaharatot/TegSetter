using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TegSetter.Content.Clases.DataClases.Info
{
    /// <summary>
    /// Класс информации о загружаемых картинках
    /// </summary>
    internal class LoadInfo
    {
        /// <summary>
        /// Путь к корневой папке для загрузки
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Флаг загрузки только картинок без тегов
        /// </summary>
        public bool IsOnlyWithoutTags { get; set; }
        /// <summary>
        /// Флаг загрузки по всем дочерним папкам
        /// </summary>
        public bool IsRecursive { get; set; }


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public LoadInfo()
        {
            //Проставляем дефолтные значения
            Path = null;
            IsOnlyWithoutTags = IsRecursive = false;
        }
    }
}
