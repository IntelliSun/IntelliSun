using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IntelliSun.Collections
{
    public class HistoryCollection<T> : IEnumerable<T>
    {
        private readonly Stack<T> downStack;
        private readonly Stack<T> upStack;

        private Item current;

        public HistoryCollection()
        {
            this.downStack = new Stack<T>();
            this.upStack = new Stack<T>();

            this.current = Item.Empty();
        }

        /// <summary>
        ///     Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.ICollection`1" /> is
        ///     read-only.
        /// </exception>
        public void Push(T item)
        {
            this.Push(item, false);
        }

        public T GoUp()
        {
            if (this.AtTop)
                throw new InvalidOperationException("${Resources.__}");

            var value = this.upStack.Pop();
            return this.Push(value, false);
        }

        public T GoDown()
        {
            if (this.AtButtom)
                throw new InvalidOperationException("${Resources.__}");

            var value = this.downStack.Pop();
            return this.Push(value, true);
        }

        public T PeekUp()
        {
            if (this.AtTop)
                throw new InvalidOperationException("${Resources.__}");

            return this.upStack.Peek();
        }

        public T PeekDown()
        {
            if (this.AtButtom)
                throw new InvalidOperationException("${Resources.__}");

            return this.downStack.Peek();
        }

        private T Push(T item, bool isDown)
        {
            var targetStack = isDown ? this.upStack : this.downStack;
            if (this.current)
                targetStack.Push(this.current);

            this.current = Item.Set(item);
            return item;
        }

        /// <summary>
        ///     Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.ICollection`1" /> is
        ///     read-only.
        /// </exception>
        public void Clear()
        {
            this.downStack.Clear();
            this.upStack.Clear();
        }

        /// <summary>
        ///     Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an
        ///     <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied
        ///     from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have
        ///     zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex" /> is less than 0.</exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The number of elements in the source
        ///     <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from
        ///     <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.
        /// </exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            var totalLength = this.Count;
            if (totalLength == 0)
                return;

            if (array.Length - arrayIndex < totalLength)
                throw new ArgumentException("${Resources.__}", "array");

            var indexA = arrayIndex;
            var indexB = indexA + this.downStack.Count;
            var indexC = indexB + 1;

            this.downStack.CopyTo(array, indexA);
            this.upStack.CopyTo(array, indexC);
            array[indexB] = this.current;
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.downStack
                       .Append(this.current.Value)
                       .Concat(this.upStack)
                       .GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public T Current
        {
            get { return this.current.Value; }
        }

        public bool AtTop
        {
            get { return this.upStack.Count == 0; }
        }

        public bool AtButtom
        {
            get { return this.downStack.Count == 0; }
        }

        /// <summary>
        ///     Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <returns>
        ///     The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        public int Count
        {
            get
            {
                return this.downStack.Count +
                       this.upStack.Count +
                       (int)this.current;
            }
        }

        private struct Item
        {
            private readonly T value;
            private readonly bool isEmpty;

            /// <summary>
            ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
            /// </summary>
            private Item(T value, bool isEmpty)
            {
                this.value = value;
                this.isEmpty = isEmpty;
            }

            public T Value
            {
                get { return this.value; }
            }

            private bool IsEmpty
            {
                get { return this.isEmpty; }
            }

            public static implicit operator T(Item item)
            {
                return item.Value;
            }

            public static implicit operator bool(Item item)
            {
                return !item.IsEmpty;
            }

            public static explicit operator int(Item item)
            {
                return item.isEmpty ? 0 : 1;
            }

            public static Item Empty()
            {
                return new Item(default(T), true);
            }

            public static Item Set(T value)
            {
                return new Item(value, false);
            }
        }
    }
}
