using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TegSetter.Content.Clases.DataClases.Info.Tag
{
    /// <summary>
    /// Класс группы тегов
    /// </summary>
    [Serializable]
    public class TagGroup
    {
        /// <summary>
        /// Список тегов
        /// </summary>
        public List<TagInfo> Tags { get; set; }
        /// <summary>
        /// Название группы
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Флаг безымянной группы тегов
        /// </summary>
        [XmlIgnore]
        public bool IsUnnamedGroup => string.IsNullOrEmpty(GroupName);

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public TagGroup()
        {
            //Проставляем дефолтные значения
            Tags = new List<TagInfo>();
            GroupName = "";
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="groupName">Имя добавляемой группы тегов</param>
        /// <param name="tags">Список тегов для добавления в группу</param>
        public TagGroup(List<TagInfo> tags, string groupName)
        {
            //Проставляем дефолтные значения
            Tags = tags;
            GroupName = groupName;
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="groupName">Имя добавляемой группы тегов</param>
        /// <param name="tag">Тег для добавления в группу</param>
        public TagGroup(TagInfo tag, string groupName)
        {
            //Проставляем дефолтные значения
            Tags = new List<TagInfo>() { tag };
            GroupName = groupName;
        }

        /// <summary>
        /// Возвращаем список имён тегов из данной группы
        /// </summary>
        /// <returns>Список имён тегов</returns>
        public List<string> GetTagNames() =>
            Tags.Select(tag => tag.Name).ToList();

        /// <summary>
        /// Получаем классы тегов по именам
        /// </summary>
        /// <param name="tagNames">Список имён тегов</param>
        /// <returns>Список классов тегов</returns>
        public List<TagInfo> GetTagsByNames(List<string> tagNames) =>
            //Возвращаем из списка только те теги, имена которых были переданы
            Tags.Where(tag => tagNames.Contains(tag.Name)).ToList();


        /// <summary>
        /// Метод получения заголовка для группы тегов
        /// </summary>
        /// <param name="group">Содержимое группы тегов</param>
        /// <returns>Заголовок группы</returns>
        public string GetGroupHeader() =>
            string.IsNullOrEmpty(GroupName) ? "Не распределённые теги" : GroupName;

        /// <summary>
        /// Проверяем наличие тега с таким именем в группе
        /// </summary>
        /// <param name="name">Имя тега для проверки</param>
        /// <returns>True - тег с таким именем есть</returns>
        public bool IsContainTag(string name) =>
            Tags.Any(tag => tag.IsEquals(name));

        /// <summary>
        /// Получаем список тегов отсортирвоанных по имени
        /// </summary>
        /// <returns>Список тегов</returns>
        public List<TagInfo> GetOrderedTags() =>
            Tags.OrderBy(tag => tag.Name).ToList();

        /// <summary>
        /// Сбрасываем буквы для тегов
        /// </summary>
        public void ClearLetters() =>
            //Проходимся по тегам группы и сбрасываем им буквы
            Tags.ForEach(tag => tag.Letter = null);
    }
}
