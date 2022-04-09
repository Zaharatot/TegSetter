using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XMPReaderZ.Clases.DataClases
{
    /// <summary>
    /// Класс описывающий сам файл
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "xmpmeta", Namespace = "adobe:ns:meta/")]
    public class XmpDocument
    {
        /// <summary>
        /// Аттрибут данных документа
        /// </summary>
        [XmlAttribute(AttributeName = "xmptk", Namespace = "adobe:ns:meta/", Form = XmlSchemaForm.Qualified)]
        public string Data { get; set; }

        /// <summary>
        /// Контент документа
        /// </summary>
        [XmlElement(ElementName = "RDF", Namespace = "http://www.w3.org/1999/02/22-rdf-syntax-ns#")]
        public XmpContent Content { get; set; }


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public XmpDocument()
        {
            //Проставляем дефолтные значения
            Data = "XMP Core 4.4.0-Exiv2";
            Content = new XmpContent();
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="tags">Массив тегов</param>
        public XmpDocument(string[] tags)
        {
            //Проставляем дефолтные значения
            Data = "XMP Core 4.4.0-Exiv2";
            //Проставляем переданные значения
            Content = new XmpContent(tags);
        }



        /// <summary>
        /// Возвращаем массив тегов
        /// </summary>
        /// <returns>Массив тегов</returns>
        public string[] GetTags() =>
            //Возвращаем массив тегов или пустой массив,
            //если массив тегов не проинициализирован
            (Content == null) ? new string[0] : Content.GetTags();
    }
}
