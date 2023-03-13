using Newtonsoft.Json;
using System.Collections;

namespace Models.Common;

public class BoundedCollection<T> : ICollection<T>, IReadOnlyCollection<T>
{
	[JsonProperty]
	private List<T> Items { get; set; }

	public BoundedCollection(int capacity)
	{
		Capacity = capacity;
		Items = new(capacity);
	}

	public bool Add(T item)
	{
		if (Room == 0)
			return false;

		Items.Add(item);
		return true;
	}

	/// <returns>The remaining enumerable that there was no room for.</returns>
	public IEnumerable<T> AddUntilBound(IEnumerable<T> items)
	{
		var addables = items.Take(Room);

		Items.AddRange(addables);

		return items.Skip(Room);
	}

	public bool Remove(T item) =>
		Items.Remove(item);

	public void Clear() =>
		Items.Clear();

	public int Capacity { get; private set; }

	public int Room =>
		Capacity - Items.Count;

	public int Count =>
		Items.Count;

	public bool IsFull =>
		Room == 0;

	public bool IsReadOnly => false;

	public IEnumerator<T> GetEnumerator() =>
		Items.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() =>
		Items.GetEnumerator();

	void ICollection<T>.Add(T item)
	{
		Add(item);
	}

	public bool Contains(T item) => Items.Contains(item);

	public void CopyTo(T[] array, int arrayIndex) => Items.CopyTo(array, arrayIndex);
}
