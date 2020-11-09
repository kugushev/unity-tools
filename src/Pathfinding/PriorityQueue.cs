using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding
{
    public class PriorityQueue<T>
    {
        private readonly Dictionary<int, Queue<T>> _items = new Dictionary<int, Queue<T>>();
        private int _lowestPriority = Int32.MaxValue;

        public int Count { get; private set; }

        public void Enqueue(T item, int priority)
        {
            if (!_items.TryGetValue(priority, out var bucket))
                _items[priority] = bucket = new Queue<T>();

            bucket.Enqueue(item);
            Count++;

            if (priority < _lowestPriority)
                _lowestPriority = priority;
        }

        public T Dequeue()
        {
            if (_items.Count == 0 || Count == 0)
                throw new ApplicationException("Unable to dequeue empty collection");

            if (_items.TryGetValue(_lowestPriority, out var bucket) && bucket.Count > 0)
            {
                var result = bucket.Dequeue();

                if (bucket.Count == 0)
                {
                    _items.Remove(_lowestPriority);

                    _lowestPriority = _items.Keys.Count > 0 
                        ? _items.Keys.OrderBy(k => k).First() 
                        : int.MaxValue;
                }

                Count--;
                return result;
            }

            throw new ApplicationException(
                $"Priority queue issue: {string.Join(";", _items.Select(p => $"{p.Key}:{p.Value.Count}"))}");
        }
    }
}