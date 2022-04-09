using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TegSetter.Content.Clases.DataClases.Keyboard
{
    /// <summary>
    /// Класс диапазона клавишь
    /// </summary>
    internal class KeyRange
    {
        /// <summary>
        /// Список клавишь диапазона
        /// </summary>
        public List<Key> Keys { get; set; }

        /// <summary>
        /// Конструктор клавишь
        /// </summary>
        /// <param name="startKey">Клавиша начала диапазона (включительно)</param>
        /// <param name="endKey">Клавиша конца диапазона (включительно)</param>
        public KeyRange(Key startKey, Key endKey)
        {
            //ИНициализируем список клавишь диапазона
            Keys = InitKeysList(startKey, endKey);
        }


        /// <summary>
        /// Создаём список клавишь диапазона
        /// </summary>
        /// <param name="startKey">Клавиша начала диапазона (включительно)</param>
        /// <param name="endKey">Клавиша конца диапазона (включительно)</param>
        /// <returns>Список клавишь из диапазона</returns>
        private List<Key> InitKeysList(Key startKey, Key endKey)
        {
            //Инициализируем выходной массиув
            List<Key> ex = new List<Key>();
            //Проставляем коды клавишь начала и конца диапазона
            int minCode = (int)startKey;
            int maxCode = (int)endKey;
            //Проходимся по диапазону кодов клавишь
            for (int i = minCode; i <= maxCode; i++)
                //Добавляем клавиши в список
                ex.Add((Key)i);
            //Возвращаем результат
            return ex;
        }
    }
}
