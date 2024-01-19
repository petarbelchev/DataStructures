namespace SequenceNToM
{
    using System.Collections.Generic;

    public class SequenceNToM
    {
        private class Item
        {
            public Item(int value)
            {
                this.Value = value;
            }

            public Item(int value, Item previous)
                : this(value)
            {
                this.Previous = previous;
            }

            public int Value { get; set; }

            public Item Previous { get; set; }
        }

        public string FindSequence(int n, int m)
        {
            if (n > m)
                return "(no solution)";

            var queue = new Queue<Item>();
            queue.Enqueue(new Item(n));

            while (queue.Count > 0)
            {
                var currItem = queue.Dequeue();

                if (currItem.Value < m)
                {
                    var item = new Item(currItem.Value + 1, currItem);
                    if (item.Value == m)
                        return PrintResult(item);
                    queue.Enqueue(item);

                    item = new Item(currItem.Value + 2, currItem);
                    if (item.Value == m)
                        return PrintResult(item);
                    queue.Enqueue(item);

                    item = new Item(currItem.Value * 2, currItem);
                    if (item.Value == m)
                        return PrintResult(item);
                    queue.Enqueue(item);
                }
            }
            return "(no solution)";
        }

        private string PrintResult(Item item)
        {
            var values = new Stack<int>();

            while (item != null)
            {
                values.Push(item.Value);
                item = item.Previous;
            }

            return string.Join(" -> ", values);
        }
    }
}
