using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMPReaderZ.Clases.DataClases
{
    /// <summary>
    /// Класс контейнера XMP
    /// </summary>
    [Serializable]
    public class XmpBag
    {
        /// <summary>
        /// Список тегов элемента
        /// </summary>
        [XmlElement(ElementName = "li", Namespace = "http://www.w3.org/1999/02/22-rdf-syntax-ns#")]
        public string[] Tags { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public XmpBag()
        {
            //Инициализируем дефолтные значения
            Tags = new string[0];
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="tags">Массив тегов</param>
        public XmpBag(string[] tags)
        {
            //Проставляем переданные значения
            Tags = tags;
        }



        /// <summary>
        /// Возвращаем массив тегов
        /// </summary>
        /// <returns>Массив тегов</returns>
        public string[] GetTags() =>
            //Возвращаем массив тегов или пустой массив,
            //если массив тегов не проинициализирован
            Tags ?? new string[0];
    }
}
