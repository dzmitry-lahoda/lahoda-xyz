/*������� 1.1

������� ����������� ���������� �hello, world� � notepad.exe. 
 * �������������� � ������� ����������� ��������� ������ csc.exe

������� ������ �� ��������� ������ � ��������� ��������� ������. 
 * ��������� ��������� ��� ����������� ������, ��� � ������������.*/

using System;
namespace EPAM.Zadanie1_1
{
    static class Hello
    {
        public static void SayHello()
        {
            Console.WriteLine("Hello World!");
        }

        static void Main()
        {
            SayHello();
        }
    }
}
