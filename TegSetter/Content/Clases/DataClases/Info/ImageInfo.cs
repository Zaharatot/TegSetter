using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TegSetter.Content.Clases.DataClases.Info
{
    /// <summary>
    /// Класс информации об изображении
    /// </summary>
    internal class ImageInfo
    {
        /// <summary>
        /// Имя файла изображения
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Полный путь к файлу изображения
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Список тегов изображения
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public ImageInfo()
        {
            //Проставляем дефолтные значения
            Name = Path = "";
            Tags = new List<string>();
        }

    }
}
