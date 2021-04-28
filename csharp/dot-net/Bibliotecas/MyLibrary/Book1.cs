using System;

namespace MyLibrary
{
    public static class Book1
    {
        public static string Upper(this string Conteudo)
        {
            if (!String.IsNullOrWhiteSpace(Conteudo))
            {
                return Conteudo.ToUpper();
            }
            else
            {
                return Conteudo;
            }
        }
    }
}
