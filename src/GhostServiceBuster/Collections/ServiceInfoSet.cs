using System.Collections;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GhostServiceBuster.Detect;

namespace GhostServiceBuster.Collections;

[CollectionBuilder(typeof(ServiceInfoSet), nameof(Create))]
public partial class ServiceInfoSet(IImmutableSet<ServiceInfo> set) : IImmutableSet<ServiceInfo>
{
    public ServiceInfoSet(ServiceInfo first, params IImmutableSet<ServiceInfo> list)
        : this(list.Prepend(first).ToImmutableHashSet())
    {
    }

    public ServiceInfoSet() : this([])
    {
    }

    public static ServiceInfoSet Empty => [];

    public int Count => set.Count;

    public IEnumerator<ServiceInfo> GetEnumerator() => set.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IImmutableSet<ServiceInfo> Add(ServiceInfo value) => set.Add(value);

    public IImmutableSet<ServiceInfo> Clear() => set.Clear();

    public bool Contains(ServiceInfo value) => set.Contains(value);

    IImmutableSet<ServiceInfo> IImmutableSet<ServiceInfo>.Except(IEnumerable<ServiceInfo> other) => set.Except(other);

    public IImmutableSet<ServiceInfo> Intersect(IEnumerable<ServiceInfo> other) => set.Intersect(other);

    public bool IsProperSubsetOf(IEnumerable<ServiceInfo> other) => set.IsProperSubsetOf(other);

    public bool IsProperSupersetOf(IEnumerable<ServiceInfo> other) => set.IsProperSupersetOf(other);

    public bool IsSubsetOf(IEnumerable<ServiceInfo> other) => set.IsSubsetOf(other);

    public bool IsSupersetOf(IEnumerable<ServiceInfo> other) => set.IsSupersetOf(other);

    public bool Overlaps(IEnumerable<ServiceInfo> other) => set.Overlaps(other);

    public IImmutableSet<ServiceInfo> Remove(ServiceInfo value) => set.Remove(value);

    public bool SetEquals(IEnumerable<ServiceInfo> other) => set.SetEquals(other);

    public IImmutableSet<ServiceInfo> SymmetricExcept(IEnumerable<ServiceInfo> other) =>
        set.SymmetricExcept(other);

    public bool TryGetValue(ServiceInfo equalValue, out ServiceInfo actualValue) =>
        set.TryGetValue(equalValue, out actualValue);

    IImmutableSet<ServiceInfo> IImmutableSet<ServiceInfo>.Union(IEnumerable<ServiceInfo> other) => set.Union(other);

    public ServiceInfoSet Union(IEnumerable<ServiceInfo> other) => new(((IImmutableSet<ServiceInfo>)this).Union(other));

    public ServiceInfoSet Except(IEnumerable<ServiceInfo> other) => new(set.Except(other));

    public ServiceInfoSet Concat(IEnumerable<ServiceInfo> other) => set.Concat(other).ToImmutableHashSet();

    public ServiceInfoSet Where(Func<ServiceInfo, bool> predicate) =>
        new(((IEnumerable<ServiceInfo>)this).Where(predicate).ToImmutableHashSet());

    public static ServiceInfoSet Create(ReadOnlySpan<ServiceInfo> values) => new([..values]);
}