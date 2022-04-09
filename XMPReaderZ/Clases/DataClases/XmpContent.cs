using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XMPReaderZ.Clases.DataClases
{

    /// <summary>
    /// Класс, описывающий контент XMP-документа
    /// </summary>
    [Serializable]
    public class XmpContent
    {
        /// <summary>
        /// Описание документа
        /// </summary>
        [XmlElement(ElementName = "Description", Namespace = "http://www.w3.org/1999/02/22-rdf-syntax-ns#")]
        public XmpDescription Description { get; set; }

        /// <summary>
        /// Отдельный неймспейс, который должен быть именно в текущем элементе
        /// </summary>
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces(new[] { 
            new XmlQualifiedName("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#")
        });


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public XmpContent()
        {
            //ПРоставляем дефолтные значения
            Description = new XmpDescription();
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="tags">Массив тегов</param>
        public XmpContent(string[] tags)
        {
            //Проставляем переданные значения
            Description = new XmpDescription(tags);
        }



        /// <summary>
        /// Возвращаем массив тегов
        /// </summary>
        /// <returns>Массив тегов</returns>
        public string[] GetTags() =>
            //Возвращаем массив тегов или пустой массив,
            //если массив тегов не проинициализирован
            (Description == null) ? new string[0] : Description.GetTags();
    }
}
