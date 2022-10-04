// See https://aka.ms/new-console-template for more information



Console.WriteLine("42");


interface IFunctor<T0> {
	IFunctor<T1> Map<T1>(Func<T0,T1> mapping);
}

record Functor<T0>(T0 value) : IFunctor<T0> {
	public IFunctor<T1> Map<T1>(Func<T0, T1> mapping) => 
      new Functor<T1>(mapping(value));
	
}