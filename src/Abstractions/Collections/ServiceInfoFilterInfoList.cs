using System.Collections;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.Collections;

[CollectionBuilder(typeof(ServiceInfoFilterInfoList), nameof(Create))]
/// <summary>
/// Represents an immutable list of <see cref="ServiceInfoFilterInfo"/> items.
/// </summary>
public sealed partial class ServiceInfoFilterInfoList(params IImmutableList<ServiceInfoFilterInfo> list)
    : IImmutableList<ServiceInfoFilterInfo>
{
    /// <summary>
    /// Gets an empty list.
    /// </summary>
    public static ServiceInfoFilterInfoList Empty => [];

    /// <summary>
    /// Gets the number of items in the list.
    /// </summary>
    public int Count => list.Count;

    /// <summary>
    /// Gets the item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index.</param>
    public ServiceInfoFilterInfo this[int index] => list[index];

    /// <summary>
    /// Returns a strongly-typed enumerator.
    /// </summary>
    public IEnumerator<ServiceInfoFilterInfo> GetEnumerator() => list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Adds an item to the list.
    /// </summary>
    /// <param name="value">The item to add.</param>
    /// <returns>The updated list.</returns>
    public IImmutableList<ServiceInfoFilterInfo> Add(ServiceInfoFilterInfo value) => list.Add(value);

    /// <summary>
    /// Adds a range of items to the list.
    /// </summary>
    /// <param name="items">The items to add.</param>
    /// <returns>The updated list.</returns>
    public IImmutableList<ServiceInfoFilterInfo> AddRange(IEnumerable<ServiceInfoFilterInfo> items) =>
        list.AddRange(items);

    /// <summary>
    /// Removes all items from the list.
    /// </summary>
    /// <returns>The cleared list.</returns>
    public IImmutableList<ServiceInfoFilterInfo> Clear() => list.Clear();

    /// <summary>
    /// Searches for an item within a range of the list.
    /// </summary>
    /// <param name="item">The item to locate.</param>
    /// <param name="index">The start index.</param>
    /// <param name="count">The number of items to search.</param>
    /// <param name="equalityComparer">The comparer to use.</param>
    /// <returns>The index of the item, or -1 if not found.</returns>
    public int IndexOf(
        ServiceInfoFilterInfo item, int index, int count, IEqualityComparer<ServiceInfoFilterInfo>? equalityComparer) =>
        list.IndexOf(item, index, count, equalityComparer);

    /// <summary>
    /// Inserts an item at the specified index.
    /// </summary>
    /// <param name="index">The index at which to insert.</param>
    /// <param name="element">The item to insert.</param>
    /// <returns>The updated list.</returns>
    public IImmutableList<ServiceInfoFilterInfo> Insert(int index, ServiceInfoFilterInfo element) =>
        list.Insert(index, element);

    /// <summary>
    /// Inserts a range of items at the specified index.
    /// </summary>
    /// <param name="index">The index at which to insert.</param>
    /// <param name="items">The items to insert.</param>
    /// <returns>The updated list.</returns>
    public IImmutableList<ServiceInfoFilterInfo> InsertRange(int index, IEnumerable<ServiceInfoFilterInfo> items) =>
        list.InsertRange(index, items);

    /// <summary>
    /// Searches for an item within a range of the list, starting from the end.
    /// </summary>
    /// <param name="item">The item to locate.</param>
    /// <param name="index">The start index.</param>
    /// <param name="count">The number of items to search.</param>
    /// <param name="equalityComparer">The comparer to use.</param>
    /// <returns>The last index of the item, or -1 if not found.</returns>
    public int LastIndexOf(
        ServiceInfoFilterInfo item, int index, int count, IEqualityComparer<ServiceInfoFilterInfo>? equalityComparer) =>
        list.LastIndexOf(item, index, count, equalityComparer);

    /// <summary>
    /// Removes the first occurrence of an item.
    /// </summary>
    /// <param name="value">The item to remove.</param>
    /// <param name="equalityComparer">The comparer to use.</param>
    /// <returns>The updated list.</returns>
    public IImmutableList<ServiceInfoFilterInfo> Remove(
        ServiceInfoFilterInfo value, IEqualityComparer<ServiceInfoFilterInfo>? equalityComparer) =>
        list.Remove(value, equalityComparer);

    /// <summary>
    /// Removes all items that match a predicate.
    /// </summary>
    /// <param name="match">The predicate to apply.</param>
    /// <returns>The updated list.</returns>
    public IImmutableList<ServiceInfoFilterInfo> RemoveAll(Predicate<ServiceInfoFilterInfo> match) =>
        list.RemoveAll(match);

    /// <summary>
    /// Removes the item at the specified index.
    /// </summary>
    /// <param name="index">The index of the item to remove.</param>
    /// <returns>The updated list.</returns>
    public IImmutableList<ServiceInfoFilterInfo> RemoveAt(int index) => list.RemoveAt(index);

    /// <summary>
    /// Removes a range of items.
    /// </summary>
    /// <param name="items">The items to remove.</param>
    /// <param name="equalityComparer">The comparer to use.</param>
    /// <returns>The updated list.</returns>
    public IImmutableList<ServiceInfoFilterInfo> RemoveRange(
        IEnumerable<ServiceInfoFilterInfo> items, IEqualityComparer<ServiceInfoFilterInfo>? equalityComparer) =>
        list.RemoveRange(items, equalityComparer);

    /// <summary>
    /// Removes a range of items by index.
    /// </summary>
    /// <param name="index">The start index.</param>
    /// <param name="count">The number of items to remove.</param>
    /// <returns>The updated list.</returns>
    public IImmutableList<ServiceInfoFilterInfo> RemoveRange(int index, int count) =>
        list.RemoveRange(index, count);

    /// <summary>
    /// Replaces an item with another item.
    /// </summary>
    /// <param name="oldValue">The item to replace.</param>
    /// <param name="newValue">The replacement item.</param>
    /// <param name="equalityComparer">The comparer to use.</param>
    /// <returns>The updated list.</returns>
    public IImmutableList<ServiceInfoFilterInfo> Replace(ServiceInfoFilterInfo oldValue, ServiceInfoFilterInfo newValue,
        IEqualityComparer<ServiceInfoFilterInfo>? equalityComparer) =>
        list.Replace(oldValue, newValue, equalityComparer);

    /// <summary>
    /// Replaces the item at the specified index.
    /// </summary>
    /// <param name="index">The index of the item to replace.</param>
    /// <param name="value">The replacement item.</param>
    /// <returns>The updated list.</returns>
    public IImmutableList<ServiceInfoFilterInfo> SetItem(int index, ServiceInfoFilterInfo value) =>
        list.SetItem(index, value);

    /// <summary>
    /// Filters the list using a predicate.
    /// </summary>
    /// <param name="predicate">The predicate to apply.</param>
    /// <returns>The filtered list.</returns>
    public ServiceInfoFilterInfoList Where(Predicate<ServiceInfoFilterInfo>? predicate) =>
        predicate is null ? this : list.Where(i => predicate(i)).ToImmutableList();

    /// <summary>
    /// Creates a list from a span of values.
    /// </summary>
    /// <param name="values">The values to include.</param>
    /// <returns>The created list.</returns>
    public static ServiceInfoFilterInfoList Create(ReadOnlySpan<ServiceInfoFilterInfo> values) => new([..values]);
}
