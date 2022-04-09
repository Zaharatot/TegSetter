using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XMPReaderZ.Clases.DataClases
{
    /// <summary>
    /// Класс, описывающий описание XMP-документа
    /// </summary>
    [Serializable]
    public class XmpDescription
    {

        /// <summary>
        /// Аттрибут "favorite"
        /// </summary>
        [XmlAttribute(AttributeName = "favorite", Namespace = "http://www.fstopapp.com/xmp/", Form = XmlSchemaForm.Qualified)]
        public int Favorite { get; set; }

        /// <summary>
        /// Аттрибут "about"
        /// </summary>
        [XmlAttribute(AttributeName = "about", Namespace = "http://www.w3.org/1999/02/22-rdf-syntax-ns#", Form = XmlSchemaForm.Qualified)]
        public string About { get; set; }

            
        /// <summary>
        /// Отдельный неймспейс, который должен быть именно в текущем элементе
        /// </summary>
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces(new[] { 
                new XmlQualifiedName("dc", "http://purl.org/dc/elements/1.1/"),
                new XmlQualifiedName("fstop", "http://www.fstopapp.com/xmp/")
            });

        /// <summary>
        /// Знаечние дочерних элементов
        /// </summary>
        [XmlElement(ElementName = "subject", Namespace = "http://purl.org/dc/elements/1.1/")]
        public XmpSubject Subject { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public XmpDescription()
        {
            //Проставляем дефолтные значения
            Favorite = 0;
            About = "";
            Subject = new XmpSubject();
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="tags">Массив тегов</param>
        public XmpDescription(string[] tags)
        {
            //Проставляем дефолтные значения
            Favorite = 0;
            About = "";
            //Проставляем переданные значения
            Subject = new XmpSubject(tags);
        }



        /// <summary>
        /// Возвращаем массив тегов
        /// </summary>
        /// <returns>Массив тегов</returns>
        public string[] GetTags() =>
            //Возвращаем массив тегов или пустой массив,
            //если массив тегов не проинициализирован
            (Subject == null) ? new string[0] : Subject.GetTags();
    }
}
