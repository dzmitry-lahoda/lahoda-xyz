using System.Collections.Generic;
using System.ComponentModel;
using System;


using NumberLike = System.UInt64;

void FibonacciAsLinkedList() {
var fib = new Lazy<LinkedList<NumberLike>>(fibStream(1,1));
    Console.WriteLine(index(fib, 0));
    Console.WriteLine(index(fib, 1));
    Console.WriteLine(index(fib, 5));
}

// mo = f: as: let res  = f as; in res // { override = nas: mo f (as // nas); }

record Overrideable<K, V, R>(Lazy<IReadOnlyDictionary<K, V>> set, Func<Lazy<IReadOnlyDictionary<K, V>>, R> f) {
    public Overrideable<K, V, R> apply(IReadOnlyDictionary<K, V> newSet) {
        var x = new Lazy(()= > set.value.Append(newSet));
        return new Overrideable<K, V, R>(x, f);
    }

    public R f() => this.f(set.value);
}


LinkedList<NumberLike> fibStream(NumberLike n, NumberLike m) =>     
    new LinkedList<NumberLike>(n, new Lazy<LinkedList<NumberLike>>(() => fibStream(m, n + m)));
T index<T>(Lazy<LinkedList<T>> s, NumberLike i) {
    if (i == 0) 
        return s.Value.head;
    else
        return index(s.Value.tail, i - 1);
}

record LinkedList<T>(T head, Lazy<LinkedList<T>> tail) {
    public static implicit operator Lazy<LinkedList<T>>(LinkedList<T> self) => new Lazy<LinkedList<T>>(self);
    public static implicit operator LazyContainer<LinkedList<T>>(LinkedList<T> self) => new Lazy<LinkedList<T>>(self);
}


interface IImutableContainer<T> {
    T Value {get;}
}

record LazyContainer<T>(Lazy<T> value) : IImutableContainer<T>
{
    public T Value => value.Value;
    public static implicit operator Lazy<T>(LazyContainer<T> self) => self.value;
    public static implicit operator LazyContainer<T>(Lazy<T> self) => new (self);
}