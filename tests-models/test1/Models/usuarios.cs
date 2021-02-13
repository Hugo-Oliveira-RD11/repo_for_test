using System;
using System.ComponentModel.DataAnnotations;

namespace test1.Models
{
    public class usuarios
    {
        public int id{get;set;}
        public string nome{get;set;}
        public string email{get;set;}
        public string senha{get;set;}
        public enum nivel{estagiario=0,junior=1,senior=2,pleno=3}
    }
}