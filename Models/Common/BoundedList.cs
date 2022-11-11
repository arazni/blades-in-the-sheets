using System.Collections;

namespace Models.Common;

public class BoundedList<T> : IReadOnlyCollection<T>
{
	private readonly List<T> items;

	public BoundedList(int capacity)
	{
		Capacity = capacity;
		this.items = new(capacity);
	}

	public bool Add(T item)
	{
		if (Room == 0)
			return false;

		this.items.Add(item);
		return true;
	}

	/// <returns>The remaining enumerable that there was no room for.</returns>
	public IEnumerable<T> AddUntilBound(IEnumerable<T> items)
	{
		var addables = items.Take(Room);

		this.items.AddRange(addables);

		return items.Skip(Room);
	}

	public bool Remove(T item) =>
		this.items.Remove(item);

	public void Clear() =>
		this.items.Clear();

	public int Capacity { get; }

	public int Room =>
		Capacity - this.items.Count;

	public int Count =>
		this.items.Count;

	public bool IsFull =>
		Room == 0;

	public IEnumerator<T> GetEnumerator() =>
		this.items.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() =>
		this.items.GetEnumerator();
}
