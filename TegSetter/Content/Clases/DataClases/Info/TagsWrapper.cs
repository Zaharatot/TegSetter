using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TegSetter.Content.Clases.DataClases.Info
{
    /// <summary>
    /// Класс-обёртка для сохранения/загрузки списка тегов
    /// </summary>
    [Serializable]
    public class TagsWrapper
    {
        /// <summary>
        /// Список тегов
        /// </summary>
        public List<string> Tags { get; set; }


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public TagsWrapper()
        {
            //Проставляем дефолтные значения
            Tags = new List<string>();
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="tags">Список тегов</param>
        public TagsWrapper(List<string> tags)
        {
            //Проставляем переданные значения
            Tags = tags;
        }
    }
}
