using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Reflection;
using System.Diagnostics;

namespace TestDispatchUtility
{
	class Program
	{
		static void Main(string[] args)
		{
			// http://msdn.microsoft.com/en-us/library/z9ty6h50(VS.85).aspx
			// Note: The WriteBlankLines method is actually on TextStream not FileSystemObject.
			TestProgIdInstance("Scripting.FileSystemObject", "DeleteFile", "WriteBlankLines", "Drives", "GetParentFolderName", "FlyingMonkey");

			// ADO returns 1000+ for each new member we ask for.  IDispatchEx at work?
			object conn = TestProgIdInstance("ADODB.Connection", "Open", "ConnectionString", "PingTheInterwebs", "BatmanVsSuperman");

			// Invoke by name.
			string provider = (string)DispatchUtility.Invoke(conn, "Provider", null);
			Console.Write("Provider by name: ");
			Console.WriteLine(provider);

			// Invoke by DISPID.
			int providerDispId;
			if (DispatchUtility.TryGetDispId(conn, "Provider", out providerDispId))
			{
				provider = (string)DispatchUtility.Invoke(conn, providerDispId, null);
				Console.Write("Provider by DISPID: ");
				Console.WriteLine(provider);
			}

			if (Debugger.IsAttached)
			{
				Console.Write("Press any key to continue. . . ");
				Console.ReadKey(true);
			}
		}

		private static object TestProgIdInstance(string progId, params string[] memberNames)
		{
			Console.WriteLine("------ ProgId: {0} ------", progId);

			Type standardType = Type.GetTypeFromProgID(progId);
			WriteAllMembers(standardType);

			object obj = Activator.CreateInstance(standardType);
			WriteAllMembers(obj.GetType());

			// Make sure it implements IDispatch.
			if (!DispatchUtility.ImplementsIDispatch(obj))
			{
				throw new ArgumentException("The object created for " + progId + " doesn't implement IDispatch.");
			}

			// See if we can get Type info and then do some reflection.
			Type dispatchType = DispatchUtility.GetType(obj, false);
			if (dispatchType != null)
			{
				WriteAllMembers(dispatchType);
			}

			// Look up the requested member names.
			Console.WriteLine("Requested Member: DISPID");
			foreach (string memberName in memberNames)
			{
				LookupMemberDispId(obj, memberName);
			}

			return obj;
		}

		private static void WriteAllMembers(Type type)
		{
			Console.WriteLine("All Members of {0}:", type.FullName);
			MemberInfo[] members = type.GetMembers();
			foreach (MemberInfo member in members)
			{
				Console.WriteLine("\t{0} -- {1}", member.MemberType, member);
			}
		}

		private static void LookupMemberDispId(object obj, string name)
		{
			Console.Write("\t" + name + ": ");
			int dispId;
			if (DispatchUtility.TryGetDispId(obj, name, out dispId))
			{
				Console.WriteLine(dispId);
			}
			else
			{
				Console.WriteLine("<not found>");
			}
		}
	}
}
