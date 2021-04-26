using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Test_Enumerable_Collection_List
{
    /////////////////////////////////////////////////////非泛型   (非泛型多用于支持遗留代码)

    public interface IEnumerator
    {
        /// <summary>
        /// 获取集合中位于枚举数当前位置的元素。
        /// 返回结果：
        ///     集合中位于枚举数当前位置的元素。
        /// </summary>
        object Current { get; }

        /// <summary>
        /// 将枚举数推进到集合的下一个元素。
        /// </summary>
        /// <returns>如果枚举数已成功地推进到下一个元素，则为 true；如果枚举数传递到集合的末尾，则为 false。</returns>
        bool MoveNext();

        /// <summary>
        /// 将枚举数设置为其初始位置，该位置位于集合中第一个元素之前。
        /// </summary>
        void Reset();
    }
    public interface IEnumerable
    {
        /// <summary>
        /// 返回循环访问集合的枚举数
        /// </summary>
        /// <returns>一个可用于循环访问集合的 System.Collections.IEnumerator 对象。</returns>
        IEnumerator GetEnumerator();
    }

    public interface ICollection : IEnumerable
    {
        /// <summary>
        /// 获取 System.Collections.ICollection 中包含的元素数。
        /// 返回结果:
        ///     System.Collections.ICollection 中包含的元素数。
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 获取可用于同步对 System.Collections.ICollection 的访问的对象。
        /// 返回结果:
        ///     可用于同步对 System.Collections.ICollection 的访问的对象。
        /// </summary>
        object SyncRoot { get; }

        /// <summary>
        /// 获取一个值，该值指示是否同步对 System.Collections.ICollection 的访问（线程安全）。
        /// 返回结果:
        ///     如果对 true 的访问是同步的（线程安全），则为 System.Collections.ICollection；否则为 false。
        /// </summary>
        bool IsSynchronized { get; }

        /// <summary>
        /// 从特定的 System.Collections.ICollection 索引处开始，将 System.Array 的元素复制到一个 System.Array中。
        /// </summary>
        /// <param name="array">一维 System.Array，它是从 System.Collections.ICollection 复制的元素的目标。 System.Array 必须具有从零开始的索引。</param>
        /// <param name="index">array 中从零开始的索引，从此处开始复制。</param>
        void CopyTo(Array array, int index);
    }

    public interface IList : ICollection, IEnumerable
    {
        /// <summary>
        /// 获取或设置指定索引处的元素。
        /// </summary>
        /// <param name="index">要获取或设置的元素的从零开始的索引。</param>
        /// <returns>指定索引处的元素。</returns>
        object this[int index] { get; set; }

        /// <summary>
        /// 获取一个值，该值指示 System.Collections.IList 是否为只读。
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// 获取一个值，该值指示 System.Collections.IList 是否具有固定大小。
        /// </summary>
        bool IsFixedSize { get; }

        /// <summary>
        /// 将某项添加到 System.Collections.IList 中。
        /// </summary>
        /// <param name="value">要添加到 System.Collections.IList 的对象。</param>
        /// <returns>插入了新元素的位置，-1 指示该项未插入到集合中。</returns>
        int Add(object value);

        /// <summary>
        /// 从 System.Collections.IList 中移除所有项。
        /// </summary>
        void Clear();

        /// <summary>
        /// 确定 System.Collections.IList 是否包含特定值。
        /// </summary>
        /// <param name="value">要在 System.Collections.IList 中定位的对象。</param>
        /// <returns>如果在 System.Collections.IList 中找到了 System.Object，则为 true；否则为 false。</returns>
        bool Contains(object value);
        /// <summary>
        /// 确定 System.Collections.IList 中特定项的索引。
        /// </summary>
        /// <param name="value">要在 System.Collections.IList 中定位的对象。</param>
        /// <returns>如果在列表中找到，则为 value 的索引；否则为 -1。</returns>
        int IndexOf(object value);

        /// <summary>
        /// 在 System.Collections.IList 中的指定索引处插入一个项。
        /// </summary>
        /// <param name="index">应插入 value 的从零开始的索引。</param>
        /// <param name="value">要插入到 System.Collections.IList 中的对象。</param>
        void Insert(int index, object value);

        /// <summary>
        /// 从 System.Collections.IList 中移除特定对象的第一个匹配项。
        /// </summary>
        /// <param name="value">要从 System.Collections.IList 中删除的对象。</param>
        void Remove(object value);

        /// <summary>
        /// 移除位于指定索引处的 System.Collections.IList 项。
        /// </summary>
        /// <param name="index">要移除的项的从零开始的索引。</param>
        void RemoveAt(int index);
    }

    /////////////////////////////////////////////////////泛型
    //泛型是C# 2.0才引入的，吸取了非泛型的经验和教训，从而设计了与之不同的但更优秀的接口，从而：
    //ICollection<T>没有派生自ICollection，IList<T>没有派生自IList，IDictionary<TKey,TValue>没有派生自IDictionary

    public interface IEnumerable<out T> : IEnumerable
    {
        new IEnumerator<T> GetEnumerator();
    }

    /// <summary>
    /// 相比ICollection，ICollection<T>取消了同步的功能，这是因为对于泛型集合，他们本身是线程安全的
    /// 假如要实现一个只读的ICollection<T>，那么在Add，Remove和Clear方法中抛出异常即可
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICollection<T> : IEnumerable<T>, IEnumerable
    {
        /// <summary>
        /// 获取 System.Collections.Generic.ICollection`1 中包含的元素数。
        /// 返回结果:
        ///     System.Collections.Generic.ICollection`1 中包含的元素数。
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 获取一个值，该值指示 System.Collections.Generic.ICollection`1 是否为只读。
        /// 返回结果:
        ///     如果 true 是只读的，则为 System.Collections.Generic.ICollection`1；否则为 false。
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// 将某项添加到 System.Collections.Generic.ICollection`1 中。
        /// </summary>
        /// <param name="item">要添加到 System.Collections.Generic.ICollection`1 的对象。</param>
        void Add(T item);

        /// <summary>
        /// 从 System.Collections.Generic.ICollection`1 中移除所有项。
        /// </summary>
        void Clear();

        /// <summary>
        /// 确定 System.Collections.Generic.ICollection`1 是否包含特定值。
        /// </summary>
        /// <param name="item">要在 System.Collections.Generic.ICollection`1 中定位的对象。</param>
        /// <returns>如果在 true 中找到 item，则为 System.Collections.Generic.ICollection`1；否则为 false。</returns>
        bool Contains(T item);

        /// <summary>
        /// 从特定的 System.Collections.Generic.ICollection`1 索引处开始，将 System.Array 的元素复制到一个 System.Array中。
        /// </summary>
        /// <param name="array">一维 System.Array，它是从 System.Collections.Generic.ICollection`1 复制的元素的目标。 System.Array必须具有从零开始的索引。</param>
        /// <param name="arrayIndex">array 中从零开始的索引，从此处开始复制。</param>
        void CopyTo(T[] array, int arrayIndex);

        /// <summary>
        /// 从 System.Collections.Generic.ICollection`1 中移除特定对象的第一个匹配项。
        /// </summary>
        /// <param name="item">要从 System.Collections.Generic.ICollection`1 中删除的对象。</param>
        /// <returns>如果从 true 中成功移除 item，则为 System.Collections.Generic.ICollection`1；否则为 false。 如果在原始 false 中没有找到 item，该方法也会返回 System.Collections.Generic.ICollection`1。</returns>
        bool Remove(T item);
    }

    /// <summary>
    /// 如果想通过为止获取集合元素，那么IList<T>就是此类集合的标准接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IList<T> : ICollection<T>, IEnumerable<T>, IEnumerable
    {
        /// <summary>
        /// 获取或设置指定索引处的元素。
        /// </summary>
        /// <param name="index">要获取或设置的元素的从零开始的索引。</param>
        /// <returns>指定索引处的元素。</returns>
        T this[int index] { get; set; }

        /// <summary>
        /// 确定 System.Collections.Generic.IList`1 中特定项的索引。
        /// </summary>
        /// <param name="item">要在 System.Collections.Generic.IList`1 中定位的对象。</param>
        /// <returns>如果在列表中找到，则为 item 的索引；否则为 -1。</returns>
        int IndexOf(T item);

        /// <summary>
        /// 在 System.Collections.Generic.IList`1 中的指定索引处插入一个项。
        /// </summary>
        /// <param name="index">应插入 item 的从零开始的索引。</param>
        /// <param name="item">要插入到 System.Collections.Generic.IList`1 中的对象。</param>
        void Insert(int index, T item);

        /// <summary>
        /// 移除位于指定索引处的 System.Collections.Generic.IList`1 项。
        /// </summary>
        /// <param name="index">要移除的项的从零开始的索引。</param>
        void RemoveAt(int index);
    }

    public interface IReadOnlyCollection<out T> : IEnumerable<T>, IEnumerable
    {
        /// <summary>
        /// 获取集合中的元素数。
        /// 
        /// 返回结果:
        ///     集合中的元素数。
        /// </summary>
        int Count { get; }
    }

    /// <summary>
    /// 为了与Windows运行时的制度集合互操作，Framework4.5引入了一个新的集合接口IReadOnlyList<T>。该接口自身就非常有用，也可以看作IList的缩减版，对外只公开用于只读的操作
    ///
    /// 因为类型参数仅仅用于输出位置，所以其标记为协变(covariant)。比如，一个cats列表，可以看作一个animals的只读列表。相反，在IList<T>中，T没有标记为协变，因为T应用于输入和输出位置。
    /// 
    /// 你可能认为IList<T>派生自IReadonlyList<T>，然后，微软并没有这么做，这是因为这么做就要求把IList<T>的成员移动到IReadonlyList<T>，这就给CLR4.5带来重的变化（程序员需要重新编辑程序以避免运行时错误）。实际上，微软在IList<T>的实现类中手动地添加了对IReadonlyList<T>接口的实现。
    /// 
    /// 在Windows运行时中IVectorView<T> 与.NET Framework的IReadonlyList<T> 相对应。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadOnlyList<out T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
    {
        /// <summary>
        /// 获取位于只读列表中指定索引处的元素。
        /// </summary>
        /// <param name="index">要获取的元素的索引(索引从零开始)。</param>
        /// <returns>在只读列表中指定索引处的元素。</returns>
        T this[int index] { get; }
    }

    public static class Test
    {
        public static void run()
        {
            List<int> a = new List<int>();
        }
    }
}
