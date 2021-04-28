using System;
using MyLibrary;

namespace Test_MyLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite o seu nome :");
            
            string nome = Console.ReadLine();

            Console.WriteLine(Book1.Upper(nome));

        }
    }
}
