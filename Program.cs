using System;
using System.Collections.Generic;
using System.Linq;

// Extension Methods
public static class Extensions
{
    // Reverse a string
    public static string ReverseString(this string str)
    {
        return new string(str.Reverse().ToArray());
    }

    // Count occurrences of a character in a string
    public static int CountOccurrences(this string str, char c)
    {
        return str.Count(ch => ch == c);
    }

    // Count occurrences of an element in an array
    public static int CountOccurrences<T>(this T[] array, T value) where T : IEquatable<T>
    {
        return array.Count(item => item.Equals(value));
    }

    // Create a unique array from the original array
    public static T[] GetUniqueElements<T>(this T[] array)
    {
        return array.Distinct().ToArray();
    }
}

// Generic ExtendedDictionary
public class ExtendedDictionaryElement<T, U, V>
{
    public T Key { get; set; }
    public U Value1 { get; set; }
    public V Value2 { get; set; }

    public ExtendedDictionaryElement(T key, U value1, V value2)
    {
        Key = key;
        Value1 = value1;
        Value2 = value2;
    }
}

public class ExtendedDictionary<T, U, V> : IEnumerable<ExtendedDictionaryElement<T, U, V>>
{
    private readonly List<ExtendedDictionaryElement<T, U, V>> _elements = new();

    public void Add(T key, U value1, V value2)
    {
        _elements.Add(new ExtendedDictionaryElement<T, U, V>(key, value1, value2));
    }

    public bool Remove(T key)
    {
        var element = _elements.FirstOrDefault(e => EqualityComparer<T>.Default.Equals(e.Key, key));
        if (element != null)
        {
            _elements.Remove(element);
            return true;
        }
        return false;
    }

    public bool ContainsKey(T key)
    {
        return _elements.Any(e => EqualityComparer<T>.Default.Equals(e.Key, key));
    }

    public bool ContainsValue(U value1, V value2)
    {
        return _elements.Any(e => EqualityComparer<U>.Default.Equals(e.Value1, value1) && EqualityComparer<V>.Default.Equals(e.Value2, value2));
    }

    public ExtendedDictionaryElement<T, U, V> this[T key]
    {
        get => _elements.First(e => EqualityComparer<T>.Default.Equals(e.Key, key));
    }

    public int Count => _elements.Count;

    public IEnumerator<ExtendedDictionaryElement<T, U, V>> GetEnumerator()
    {
        return _elements.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

// Demonstration
class Program
{
    static void Main()
    {
        // String extension methods
        string example = "hello world";
        Console.WriteLine(example.ReverseString());
        Console.WriteLine(example.CountOccurrences('o'));

        // Array extension methods
        int[] numbers = { 1, 2, 2, 3, 3, 3 };
        Console.WriteLine(numbers.CountOccurrences(3));
        Console.WriteLine(string.Join(", ", numbers.GetUniqueElements()));

        // Generic dictionary demonstration
        var dictionary = new ExtendedDictionary<int, string, string>();
        dictionary.Add(1, "Value1", "Extra1");
        dictionary.Add(2, "Value2", "Extra2");
        Console.WriteLine(dictionary.ContainsKey(1));
        Console.WriteLine(dictionary.ContainsValue("Value1", "Extra1"));

        foreach (var element in dictionary)
        {
            Console.WriteLine($"Key: {element.Key}, Value1: {element.Value1}, Value2: {element.Value2}");
        }
    }
}
