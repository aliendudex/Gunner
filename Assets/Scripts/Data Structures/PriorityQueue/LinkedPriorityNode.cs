namespace ED262C
{
    public class LinkedPriorityNode<T>
    {
        public LinkedPriorityNode<T> prev;
        public LinkedPriorityNode<T> next;
        public T value;
        public int priority;

        public LinkedPriorityNode(T value, int priority)
        {
            this.value = value;
            this.priority = priority;
            prev = null;
            next = null;
        }
    }
}