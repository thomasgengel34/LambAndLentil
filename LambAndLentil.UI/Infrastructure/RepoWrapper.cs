using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using AutoMapper;
using LambAndLentil.UI.Models;

namespace LambAndLentil.UI.Infrastructure
{
    public class RepoWrapper<TVM>
        where TVM:class, IBaseVM
    {
        public static MapperConfiguration AutoMapperConfig { get; set; } 
        static string Folder { get; set; }
        static Type T;
        static string className;
        static IRepository<TVM> BaseRepo;
        
        public RepoWrapper(IRepository<TVM> baseRepo )
        {
            // AutoMapper is initialized in Global.asax
            BaseRepo = baseRepo;
            char[] charsToTrim = { 'V', 'M' ,'>'};
            string x = BaseRepo.ToString() ;
            char[] c1 = { ']' };
            string y = String.Concat(x.TrimEnd(c1),">");
            string z = y.Replace("\'1[", "<");


             className = typeof(TVM).ToString().TrimEnd(charsToTrim);
            T = Type.GetType(className);
         
        }

        public void Add(TVM entity)
        {
            switch (className)
            {
                case "Ingredient": 
                    //
                    break;

                default:
                    break;
            }

        }

    }
}