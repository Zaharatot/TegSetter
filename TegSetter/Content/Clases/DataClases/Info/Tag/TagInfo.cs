using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace TegSetter.Content.Clases.DataClases.Info.Tag
{
    /// <summary>
    /// Класс информации о теге
    /// </summary>
    public class TagInfo
    {
        /// <summary>
        /// Имя тега
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание тега
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Имя группы в которой состоит тег
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// Буква тега
        /// </summary>
        [XmlIgnore]
        public Key? Letter { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public TagInfo()
        {
            //Проставляем дефолтные значения
            Name = Description = Group = "";
            Letter = null;
        }


        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Имя тега</param>
        /// <param name="description">Описание тега</param>
        public TagInfo(string name, string description)
        {
            //Проставляем переданные значения
            Name = name;
            Description = description;
            Letter = null;
        }

        /// <summary>
        /// Проверяем совпадение имени тега с переданным
        /// </summary>
        /// <param name="name">Имя для сравнения</param>
        /// <returns>True - имена совпадают</returns>
        public bool IsEquals(string name) =>
            Name.ToLower().Equals(name.ToLower());
    }
}
