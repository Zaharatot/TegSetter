using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMPReaderZ.Clases.DataClases
{
    /// <summary>
    /// Класс Xmp-темы
    /// </summary>
    [Serializable]
    public class XmpSubject
    {
        /// <summary>
        /// Элемент контейнеря
        /// </summary>
        [XmlElement(ElementName = "Bag", Namespace = "http://www.w3.org/1999/02/22-rdf-syntax-ns#")]
        public XmpBag Bag { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public XmpSubject()
        {
            //Инициализируем дефолтные значения
            Bag = new XmpBag();
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="tags">Массив тегов</param>
        public XmpSubject(string[] tags)
        {
            //Проставляем переданные значения
            Bag = new XmpBag(tags);
        }



        /// <summary>
        /// Возвращаем массив тегов
        /// </summary>
        /// <returns>Массив тегов</returns>
        public string[] GetTags() =>
            //Возвращаем массив тегов или пустой массив,
            //если массив тегов не проинициализирован
            (Bag == null) ? new string[0] : Bag.GetTags();
    }
}
