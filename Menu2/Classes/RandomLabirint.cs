using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu2.Classes
{
    internal class RandomLabirint
    {
        // Создать массив или список комнат
        Room[] rooms = new Room[10];
        // Заполнить массив или список комнатами
        // ...

        // Создать генератор случайных чисел
        System.Random random = new System.Random();

        // Создать функцию, которая возвращает случайную комнату
        Room GetRandomRoom()
        {
            // Получить случайный индекс комнаты
            int index = random.Next(0, rooms.Length);
            // Вернуть комнату по индексу
            return rooms[index];
        }

        // Создать функцию, которая перемещает персонажа в новую комнату
        void MoveToNewRoom()
        {
            // Получить случайную комнату
            Room newRoom = GetRandomRoom();
            // Переместить персонажа в новую комнату
            // ...
        }

    }
}
