using System.Collections;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.Collections;

[CollectionBuilder(typeof(ServiceInfoFilterInfoList), nameof(Create))]
public sealed partial class ServiceInfoFilterInfoList(IImmutableList<ServiceInfoFilterInfo> list)
    : IImmutableList<ServiceInfoFilterInfo>
{
    public ServiceInfoFilterInfoList(ServiceInfoFilterInfo first, params IImmutableList<ServiceInfoFilterInfo> list)
        : this(list.Prepend(first).ToImmutableList())
    {
    }

    public static ServiceInfoFilterInfoList Empty => new([]);

    public int Count => list.Count;

    public ServiceInfoFilterInfo this[int index] => list[index];

    public IEnumerator<ServiceInfoFilterInfo> GetEnumerator() => list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IImmutableList<ServiceInfoFilterInfo> Add(ServiceInfoFilterInfo value) => list.Add(value);

    public IImmutableList<ServiceInfoFilterInfo> AddRange(IEnumerable<ServiceInfoFilterInfo> items) =>
        list.AddRange(items);

    public IImmutableList<ServiceInfoFilterInfo> Clear() => list.Clear();

    public int IndexOf(
        ServiceInfoFilterInfo item, int index, int count, IEqualityComparer<ServiceInfoFilterInfo>? equalityComparer) =>
        list.IndexOf(item, index, count, equalityComparer);

    public IImmutableList<ServiceInfoFilterInfo> Insert(int index, ServiceInfoFilterInfo element) =>
        list.Insert(index, element);

    public IImmutableList<ServiceInfoFilterInfo> InsertRange(int index, IEnumerable<ServiceInfoFilterInfo> items) =>
        list.InsertRange(index, items);

    public int LastIndexOf(
        ServiceInfoFilterInfo item, int index, int count, IEqualityComparer<ServiceInfoFilterInfo>? equalityComparer) =>
        list.LastIndexOf(item, index, count, equalityComparer);

    public IImmutableList<ServiceInfoFilterInfo> Remove(
        ServiceInfoFilterInfo value, IEqualityComparer<ServiceInfoFilterInfo>? equalityComparer) =>
        list.Remove(value, equalityComparer);

    public IImmutableList<ServiceInfoFilterInfo> RemoveAll(Predicate<ServiceInfoFilterInfo> match) =>
        list.RemoveAll(match);

    public IImmutableList<ServiceInfoFilterInfo> RemoveAt(int index) => list.RemoveAt(index);

    public IImmutableList<ServiceInfoFilterInfo> RemoveRange(
        IEnumerable<ServiceInfoFilterInfo> items, IEqualityComparer<ServiceInfoFilterInfo>? equalityComparer) =>
        list.RemoveRange(items, equalityComparer);

    public IImmutableList<ServiceInfoFilterInfo> RemoveRange(int index, int count) =>
        list.RemoveRange(index, count);

    public IImmutableList<ServiceInfoFilterInfo> Replace(ServiceInfoFilterInfo oldValue, ServiceInfoFilterInfo newValue,
        IEqualityComparer<ServiceInfoFilterInfo>? equalityComparer) =>
        list.Replace(oldValue, newValue, equalityComparer);

    public IImmutableList<ServiceInfoFilterInfo> SetItem(int index, ServiceInfoFilterInfo value) =>
        list.SetItem(index, value);

    public static ServiceInfoFilterInfoList Create(ReadOnlySpan<ServiceInfoFilterInfo> values) => new([..values]);
}