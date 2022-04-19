using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TegSetter.Content.Clases.DataClases.Info.Tag
{
    /// <summary>
    /// Класс списка тегов
    /// </summary>
    [Serializable]
    public class TagsCollection
    {
        /// <summary>
        /// Список групп тегов
        /// </summary>
        public List<TagGroup> Groups { get; set; }


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public TagsCollection()
        {
            //Проставляем дефолтные значения
            Groups = new List<TagGroup>();
        }


        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="groupName">Имя добавляемой группы тегов</param>
        /// <param name="tags">Список тегов для добавления в группу</param>
        public TagsCollection(List<TagInfo> tags, string groupName)
        {
            //Инициализируем группы тегов, добавив новую группу
            Groups = new List<TagGroup>() { 
                new TagGroup(tags, groupName)
            };
        }

        /// <summary>
        /// Получаем безымянную группу тегов
        /// </summary>
        /// <returns>Целевая группа или null</returns>
        private TagGroup GetUnNamedGroup() =>
            Groups.FirstOrDefault(group => group.IsUnnamedGroup);



        /// <summary>
        /// Возвращаем список имён тегов из данной группы
        /// </summary>
        /// <returns>Список имён тегов</returns>
        public List<string> GetTagNames() =>
            //Выполняем аггрегатную функцию, которая объединит в один список все дочерние
            Groups.Aggregate(new List<string>(), (x, y) => x.Concat(y.GetTagNames()).ToList());

        /// <summary>
        /// Получаем классы тегов по именам
        /// </summary>
        /// <param name="tagNames">Список имён тегов</param>
        /// <returns>Список классов тегов</returns>
        public List<TagInfo> GetTagsByNames(List<string> tagNames) =>
            //Выполняем аггрегатную функцию, которая объединит в один список все дочерние полученные списки
            Groups.Aggregate(new List<TagInfo>(), (x, y) => x.Concat(y.GetTagsByNames(tagNames)).ToList());

        /// <summary>
        /// Проверяем наличие тега с таким именем в коллекции
        /// </summary>
        /// <param name="name">Имя тега для проверки</param>
        /// <returns>True - тег с таким именем есть</returns>
        public bool IsContainTag(string name) =>
            Groups.Any(group => group.IsContainTag(name));

        /// <summary>
        /// Метод добавления тега в коллекцию
        /// </summary>
        /// <param name="tag">Тег для добавления</param>
        public void AddTag(TagInfo tag)
        {
            //Получаем безымянную группу
            TagGroup group = GetUnNamedGroup();
            //Если такой группы нет
            if(group == null)
            {
                //Инициализируем её
                group = new TagGroup();
                //И добавляем в список
                Groups.Add(group);
            }
            //Добавляем тег в группу
            group.Tags.Add(tag);
        }
    }
}
