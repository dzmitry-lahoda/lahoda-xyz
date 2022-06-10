


Line to insert during debugging:
```
            System.Diagnostics.Debug.WriteLine("=#-");
            System.Diagnostics.Debug.WriteLine(new System.Diagnostics.StackFrame().GetMethod().DeclaringType.Name + "." + new System.Diagnostics.StackFrame().GetMethod().Name + "." + new System.Diagnostics.StackFrame(true).GetFileLineNumber() + ";Thread: " + System.Threading.Thread.CurrentThread.ManagedThreadId + ";Ticks:" + System.DateTime.UtcNow.ToString("MM.ddTHH.mm.ss.fffffff", System.Globalization.CultureInfo.InvariantCulture));
            System.Diagnostics.Debug.WriteLine((null) + " " + (null) + " " + (null)); 
            System.Diagnostics.Debug.WriteLine("=#");
```



- lDumpDebug cannot be reused second time
- cannot put in the end + with null to replace
- better F# like piping feature
- Highly tuned for you Enterprise environment (no new namespaces you will hit limit, exact string is globalized to nothing)
- There not analyzes on length of string, but only for lines of code. 


```
  private static T _<T> (T target, Action<T> action) { action(target); return target; }
  
  
  var sw = new System.Diagnostics.Stopwatch();
  sw.Start();

  sw.Stop();
  System.Diagnostics.Debug.WriteLine("=#-"+new System.Diagnostics.StackFrame().GetMethod().DeclaringType.Name+"."+new System.Diagnostics.StackFrame().GetMethod().Name+"."+new System.Diagnostics.StackFrame(true).GetFileLineNumber()+";Thread: "+System.Threading.Thread.CurrentThread.ManagedThreadId+";Ticks:"+DateTime.UtcNow.Ticks + "  " + sw.);

  private static TResult _<T, TResult> (this T target, Func<T, TResult> func) => func(target);
    private static TResult _<T, TResult>(T target, System.Func<T, TResult> func, [System.Runtime.CompilerServices.CallerMemberName] string callerName = "")
    {
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        var r = func(target);
        sw.Stop();
        var elapsed = sw.Elapsed.TotalMilliseconds;
        var typeName = target.GetType().FullName;
        System.Diagnostics.Debug.WriteLine($"{typeName}.{callerName} = {elapsed}");
        return r;
    }

                var sw = new System.Diagnostics.Stopwatch(); sw.Start();
                sw.Stop(); System.Diagnostics.Debug.WriteLine("Hack Variables Rows slow " + sw.Elapsed.TotalMilliseconds);
```

Line to CPU free slow down processing by humand visible tiny delay:
```
var lmillisecondsDebug = 100; System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(lmillisecondsDebug));
```

Line for high CPU slow down processing by human visible tiny delay:
```
for (long verylonguseless = 0L; verylonguseless < 100_000l; verylonguseless++) { }
```