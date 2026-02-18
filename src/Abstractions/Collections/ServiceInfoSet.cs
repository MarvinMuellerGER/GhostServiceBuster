using System.Collections;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GhostServiceBuster.Detect;

namespace GhostServiceBuster.Collections;

[CollectionBuilder(typeof(ServiceInfoSet), nameof(Create))]
/// <summary>
/// Represents an immutable set of <see cref="ServiceInfo"/> items.
/// </summary>
public partial class ServiceInfoSet(IImmutableSet<ServiceInfo> set) : IImmutableSet<ServiceInfo>
{
    /// <summary>
    /// Initializes a new set from a first item and additional sets.
    /// </summary>
    /// <param name="first">The first service info.</param>
    /// <param name="list">Additional sets to include.</param>
    public ServiceInfoSet(ServiceInfo first, params IImmutableSet<ServiceInfo> list)
        : this(list.Prepend(first).ToImmutableHashSet())
    {
    }

    /// <summary>
    /// Initializes a new empty set.
    /// </summary>
    public ServiceInfoSet() : this([])
    {
    }

    /// <summary>
    /// Gets an empty set.
    /// </summary>
    public static ServiceInfoSet Empty => [];

    /// <summary>
    /// Gets the number of items in the set.
    /// </summary>
    public int Count => set.Count;

    /// <summary>
    /// Returns an enumerator that iterates through the set.
    /// </summary>
    public IEnumerator<ServiceInfo> GetEnumerator() => set.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Adds an item to the set.
    /// </summary>
    /// <param name="value">The item to add.</param>
    /// <returns>The updated set.</returns>
    public IImmutableSet<ServiceInfo> Add(ServiceInfo value) => set.Add(value);

    /// <summary>
    /// Removes all items from the set.
    /// </summary>
    /// <returns>The cleared set.</returns>
    public IImmutableSet<ServiceInfo> Clear() => set.Clear();

    /// <summary>
    /// Determines whether the set contains the specified item.
    /// </summary>
    /// <param name="value">The item to locate.</param>
    /// <returns><c>true</c> if the item is contained; otherwise, <c>false</c>.</returns>
    public bool Contains(ServiceInfo value) => set.Contains(value);

    IImmutableSet<ServiceInfo> IImmutableSet<ServiceInfo>.Except(IEnumerable<ServiceInfo> other) => set.Except(other);

    /// <summary>
    /// Produces the intersection of this set and another sequence.
    /// </summary>
    /// <param name="other">The other sequence.</param>
    /// <returns>The intersection set.</returns>
    public IImmutableSet<ServiceInfo> Intersect(IEnumerable<ServiceInfo> other) => set.Intersect(other);

    /// <summary>
    /// Determines whether this set is a proper subset of another sequence.
    /// </summary>
    /// <param name="other">The other sequence.</param>
    /// <returns><c>true</c> if this set is a proper subset; otherwise, <c>false</c>.</returns>
    public bool IsProperSubsetOf(IEnumerable<ServiceInfo> other) => set.IsProperSubsetOf(other);

    /// <summary>
    /// Determines whether this set is a proper superset of another sequence.
    /// </summary>
    /// <param name="other">The other sequence.</param>
    /// <returns><c>true</c> if this set is a proper superset; otherwise, <c>false</c>.</returns>
    public bool IsProperSupersetOf(IEnumerable<ServiceInfo> other) => set.IsProperSupersetOf(other);

    /// <summary>
    /// Determines whether this set is a subset of another sequence.
    /// </summary>
    /// <param name="other">The other sequence.</param>
    /// <returns><c>true</c> if this set is a subset; otherwise, <c>false</c>.</returns>
    public bool IsSubsetOf(IEnumerable<ServiceInfo> other) => set.IsSubsetOf(other);

    /// <summary>
    /// Determines whether this set is a superset of another sequence.
    /// </summary>
    /// <param name="other">The other sequence.</param>
    /// <returns><c>true</c> if this set is a superset; otherwise, <c>false</c>.</returns>
    public bool IsSupersetOf(IEnumerable<ServiceInfo> other) => set.IsSupersetOf(other);

    /// <summary>
    /// Determines whether this set overlaps another sequence.
    /// </summary>
    /// <param name="other">The other sequence.</param>
    /// <returns><c>true</c> if any items overlap; otherwise, <c>false</c>.</returns>
    public bool Overlaps(IEnumerable<ServiceInfo> other) => set.Overlaps(other);

    /// <summary>
    /// Removes the specified item from the set.
    /// </summary>
    /// <param name="value">The item to remove.</param>
    /// <returns>The updated set.</returns>
    public IImmutableSet<ServiceInfo> Remove(ServiceInfo value) => set.Remove(value);

    /// <summary>
    /// Determines whether this set contains the same items as another sequence.
    /// </summary>
    /// <param name="other">The other sequence.</param>
    /// <returns><c>true</c> if the sets are equal; otherwise, <c>false</c>.</returns>
    public bool SetEquals(IEnumerable<ServiceInfo> other) => set.SetEquals(other);

    /// <summary>
    /// Produces the symmetric difference of this set and another sequence.
    /// </summary>
    /// <param name="other">The other sequence.</param>
    /// <returns>The updated set.</returns>
    public IImmutableSet<ServiceInfo> SymmetricExcept(IEnumerable<ServiceInfo> other) =>
        set.SymmetricExcept(other);

    /// <summary>
    /// Attempts to find a value equal to the specified value.
    /// </summary>
    /// <param name="equalValue">The value to locate.</param>
    /// <param name="actualValue">The actual value found in the set.</param>
    /// <returns><c>true</c> if found; otherwise, <c>false</c>.</returns>
    public bool TryGetValue(ServiceInfo equalValue, out ServiceInfo actualValue) =>
        set.TryGetValue(equalValue, out actualValue);

    IImmutableSet<ServiceInfo> IImmutableSet<ServiceInfo>.Union(IEnumerable<ServiceInfo> other) => set.Union(other);

    /// <summary>
    /// Produces the union of this set and another sequence.
    /// </summary>
    /// <param name="other">The other sequence.</param>
    /// <returns>The union set.</returns>
    public ServiceInfoSet Union(IEnumerable<ServiceInfo> other) => new(((IImmutableSet<ServiceInfo>)this).Union(other));

    /// <summary>
    /// Produces the set difference of this set and another sequence.
    /// </summary>
    /// <param name="other">The other sequence.</param>
    /// <returns>The set difference.</returns>
    public ServiceInfoSet Except(IEnumerable<ServiceInfo> other) => new(set.Except(other));

    /// <summary>
    /// Concatenates this set with another sequence.
    /// </summary>
    /// <param name="other">The other sequence.</param>
    /// <returns>The concatenated set.</returns>
    public ServiceInfoSet Concat(IEnumerable<ServiceInfo> other) => set.Concat(other).ToImmutableHashSet();

    /// <summary>
    /// Filters the set using a predicate.
    /// </summary>
    /// <param name="predicate">The predicate to apply.</param>
    /// <returns>The filtered set.</returns>
    public ServiceInfoSet Where(Func<ServiceInfo, bool> predicate) =>
        new(((IEnumerable<ServiceInfo>)this).Where(predicate).ToImmutableHashSet());

    /// <summary>
    /// Creates a set from a span of values.
    /// </summary>
    /// <param name="values">The values to include.</param>
    /// <returns>The created set.</returns>
    public static ServiceInfoSet Create(ReadOnlySpan<ServiceInfo> values) => new([..values]);
}
