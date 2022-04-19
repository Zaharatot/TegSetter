using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TegSetter.Content.Clases.DataClases.Info;
using TegSetter.Content.Clases.DataClases.Info.Tag;
using TegSetter.Content.Clases.DataClases.Keyboard;

namespace TegSetter.Content.Clases.WorkClases.Keyboard
{
    /// <summary>
    /// Класс маппинга клавишь
    /// </summary>
    internal class KeyMapper
    {
        /// <summary>
        /// Список допустимых клавишь
        /// </summary>
        private List<Key> _validKeys;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public KeyMapper()
        {
            Init();
        }

        /// <summary>
        /// Инициализатор класса
        /// </summary>
        private void Init()
        {
            //Формируем список допустимых клавишь
            _validKeys = CreateValidKeysList();
        }


        /// <summary>
        /// Инициализируем список допустимых клавишь
        /// </summary>
        /// <returns>Список допустимых клавишь</returns>
        private List<Key> CreateValidKeysList()
        {
            List<Key> ex = new List<Key>();
            //Получаем список доступных диапазонов клавишь
            List<KeyRange> ranges = CreateRanges();
            //Добавляем все клавиши в общий список
            ranges.ForEach(range => ex.AddRange(range.Keys));
            //Возвращаем результат
            return ex;
        }

        /// <summary>
        /// Метод формирования списка диапазонов доступных клавишь
        /// </summary>
        /// <returns>Список диапазонов клавишь</returns>
        private List<KeyRange> CreateRanges() =>
            //Список кодов сделан отдельно и расписан в комменте под классом
            new List<KeyRange>() { 
                new KeyRange(Key.NumPad0, Key.Subtract),
                new KeyRange(Key.Divide, Key.F9),
                new KeyRange(Key.F11, Key.F12),
                new KeyRange(Key.Left, Key.Down),
                new KeyRange(Key.D0, Key.Z),
            };


        /// <summary>
        /// Формируем ключ-пары из тегов и клавишь
        /// </summary>
        /// <param name="tags">Список тегов</param>
        public void PairKeysToTags(ref List<TagInfo> tags)
        {
            //Лимитируем максимум для цикла
            int max = Math.Min(_validKeys.Count, tags.Count);
            //Сортируем теги по имени
            tags = tags.OrderBy(tag => tag.Name).ToList();
            //Проходимся в цикле по всем элементам
            for (int i = 0; i < max; i++)
                //Присваивает тегу клавишу
                tags[i].Letter = _validKeys[i];
        }
    }
}

/* Список клавишь, которые можно использовать

[Left = 23]
[Up = 24]
[Right = 25]
[Down = 26]


[D0 = 34]
[D1 = 35]
[D2 = 36]
[D3 = 37]
[D4 = 38]
[D5 = 39]
[D6 = 40]
[D7 = 41]
[D8 = 42]
[D9 = 43]
[A = 44]
[B = 45]
[C = 46]
[D = 47]
[E = 48]
[F = 49]
[G = 50]
[H = 51]
[I = 52]
[J = 53]
[K = 54]
[L = 55]
[M = 56]
[N = 57]
[O = 58]
[P = 59]
[Q = 60]
[R = 61]
[S = 62]
[T = 63]
[U = 64]
[V = 65]
[W = 66]
[X = 67]
[Y = 68]
[Z = 69]

[NumPad0 = 74]
[NumPad1 = 75]
[NumPad2 = 76]
[NumPad3 = 77]
[NumPad4 = 78]
[NumPad5 = 79]
[NumPad6 = 80]
[NumPad7 = 81]
[NumPad8 = 82]
[NumPad9 = 83]
[Multiply = 84]
[Add = 85]
[Separator = 86]
[Subtract = 87]

[Divide = 89]
[F1 = 90]
[F2 = 91]
[F3 = 92]
[F4 = 93]
[F5 = 94]
[F6 = 95]
[F7 = 96]
[F8 = 97]
[F9 = 98]

[F10 = 99] - Вроде бы, у меня в сплиттере с этой клавишей проблемы были, так что исключил её

[F11 = 100]
[F12 = 101]


 */