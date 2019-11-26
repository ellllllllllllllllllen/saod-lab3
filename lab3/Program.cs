using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedList
{
    public class Item<T> // Класс, описывающий элемент связного списка
    {
        public T Data { get; set; } // Хранимые данные

        public Item<T> Next { get; set; } // Следующий элемент списка

        public Item(T data) // Создание нового экземпляра списка
        {
            // проверка входных эл-тов на null
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            Data = data;
        }

        public override string ToString() // Приведение объекта к строке.
        {
            return Data.ToString();
        }
    }

    public class LinkedList<T> : IEnumerable<T>
    {
        private Item<T> _head = null; // Первый (головной) элемент списка
        
        private Item<T> _tail = null; // Последний (хвостовой) элемент списка
        
        private int _count = 0; // Количество элементов списка
        public int Count
        {
            get => _count;
        }
        
        public void Add(T data)
        {
            // проверка входных эл-тов на null
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            
            var item = new Item<T>(data); //создаем новый эл-т

            // Если список пуст, то добавляем созданный элемент в начало
            
            if (_head == null)
            {
                _head = item;
            }
            // иначе добавляем этот элемент как следующий за крайним элементом
            else
            {
                _tail.Next = item;
            }

            // Устанавливаем этот элемент последним.
            _tail = item;

            // Увеличиваем счетчик количества элементов.
            _count++;
        }
        
        public void Delete(T data) // Удалить данные из связного списка.
        {
            // проверка входных эл-тов на null
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            // Текущий обозреваемый элемент списка.
            var current = _head;

            // Предыдущий элемент списка, перед обозреваемым.
            Item<T> previous = null;

            // Выполняем переход по всех элементам списка до его завершения,
            // или пока не будет найден элемент, который необходимо удалить.
            while (current != null)
            {
                // Если данные обозреваемого элемента совпадают с удаляемыми данными,
                // то выполняем удаление текущего элемента учитывая его положение в цепочке.
                if (current.Data.Equals(data))
                {
                    // Если элемент находится в середине или в конце списка,
                    // выкидываем текущий элемент из списка.
                    // Иначе это первый элемент списка,
                    // выкидываем первый элемент из списка.
                    if (previous != null)
                    {
                        // Устанавливаем у предыдущего элемента указатель на следующий элемент от текущего.
                        previous.Next = current.Next;

                        // Если это был последний элемент списка, 
                        // то изменяем указатель на крайний элемент списка.
                        if (current.Next == null)
                        {
                            _tail = previous;
                        }
                    }
                    else
                    {
                        // Устанавливаем головной элемент следующим.
                        _head = _head.Next;

                        // Если список оказался пустым,
                        // то обнуляем и крайний элемент.
                        if (_head == null)
                        {
                            _tail = null;
                        }
                    }

                    // Элемент был удален.
                    // Уменьшаем количество элементов и выходим из цикла.
                    // Для того, чтобы удалить все вхождения данных из списка
                    // необходимо не выходить из цикла, а продолжать до его завершения.
                    _count--;
                    break;
                }

                // Переходим к следующему элементу списка.
                previous = current;
                current = current.Next;
            }
        }
        
        public void Clear() // Очистить список.
        {
            _head = null;
            _tail = null;
            _count = 0;
        }
        
        // Вернуть перечислитель, выполняющий перебор всех элементов в связном списке.
        public IEnumerator<T> GetEnumerator()
        {
            // Перебираем все элементы связного списка, для представления в виде коллекции элементов.
            var current = _head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        
        // Вернуть перечислитель, который осуществляет итерационный переход по связному списку.
        IEnumerator IEnumerable.GetEnumerator()
        {
            // Просто возвращаем перечислитель, определенный выше.
            // Это необходимо для реализации интерфейса IEnumerable
            // чтобы была возможность перебирать элементы связного списка операцией foreach.
            return ((IEnumerable)this).GetEnumerator();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            /*unsafe
            {
                int* x1, x2, x3, x4, x5;
                int a1 = 10, a2 = 20, a3 = 30, a4 = 40, a5 = 50;
                x1 = &a1;
                x2 = &a2;
                x3 = &a3;
                x4 = &a4;
                x5 = &a5;


                var list1 = new LinkedList<int>();

                list1.Add(*x1);
                list1.Add(*x2);
                list1.Add(*x3);
                list1.Add(*x4);
                list1.Add(*x5);
            }*/
            // Создаем новый связный список.
            var list1 = new LinkedList<string>();
            var list2 = new LinkedList<string>();
            // Добавляем элементы.
            list1.Add("a");
            list1.Add("aa");
            list1.Add("aaa");
            list1.Add("aaaa");
            list1.Add("aaaaa");

            // Выводим все элементы на консоль.
            foreach (var item in list1)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            foreach (var item in list1)
            {
                if (item.Length == 5)
                {
                    list2.Add(item);
                }
            }
            foreach(var item in list1)
            {
                if (item.Length == 4)
                {
                    list2.Add(item);
                }
            }
            foreach (var item in list1)
            {
                if (item.Length == 3)
                {
                    list2.Add(item);
                }
            }
            foreach (var item in list1)
            {
                if (item.Length == 2)
                {
                    list2.Add(item);
                }
            }
            foreach (var item in list1)
            {
                if (item.Length == 1)
                {
                    list2.Add(item);
                }
            }
            foreach (var item in list2) //выводим отсортированный лист 2
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            //Удаляем элемент.
            //list1.Delete(17);

            
            Console.ReadLine();
        }
    }
}