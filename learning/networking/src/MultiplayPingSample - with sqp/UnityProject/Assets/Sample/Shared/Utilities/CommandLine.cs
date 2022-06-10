using System;
using System.ComponentModel;
using UnityEngine;

public class CommandLine
{
    static Lazy<string[]> s_LazyArgs = new Lazy<string[]>(Environment.GetCommandLineArgs);

    // Try to get value from arg - will populate [value] with default() if not found
    public static bool TryGetCommandLineArgValue<T>(string argName, out T value)
    {
        var args = s_LazyArgs.Value;

        value = default;

        if (string.IsNullOrEmpty(argName))
            return false;

        try
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));

            if (!converter.CanConvertFrom(typeof(string)))
                return false;

            for (var i = 0; i < args.Length; i++)
            {
                if (string.Compare(args[i], argName, StringComparison.InvariantCultureIgnoreCase) != 0 ||
                    args.Length <= i + 1)
                    continue;

                value = (T)converter.ConvertFromString(args[i + 1]);
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    ///     Non-destructive version of TryGetCommandLineArgValue() - does not change value of [varToUpdate] if argument not
    ///     found
    /// </summary>
    /// <typeparam name="T">Type of variable to be updated</typeparam>
    /// <param name="varToUpdate">Variable to update</param>
    /// <param name="args">Arg (or args) to look for a value.</param>
    /// <returns>Returns the value of the first matching argument</returns>
    public static bool TryUpdateVariableWithArgValue<T>(ref T varToUpdate, params string[] args)
    {
        foreach (var arg in args)
            if (TryGetCommandLineArgValue(arg, out T argValue))
            {
                varToUpdate = argValue;
                return true;
            }

        return false;
    }

    // Returns true if command line flag found, false if not found
    public static bool HasArgument(string argName)
    {
        if (string.IsNullOrEmpty(argName))
            return false;

        var args = s_LazyArgs.Value;

        for (int i = 0, length = args.Length; i < length; i++)
            if (args[i] != null)
                if (string.Equals(argName, args[i], StringComparison.InvariantCultureIgnoreCase))
                    return true;

        return false;
    }

    public static bool HasArguments(params string[] arguments)
    {
        foreach (var arg in arguments)
            if (HasArgument(arg))
                return true;

        return false;
    }

    public static void PrintArgsToLog()
    {
        Debug.Log("Launch args: " + Environment.CommandLine);
    }
}
