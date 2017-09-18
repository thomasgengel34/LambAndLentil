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
    public class RepoWrapper<T>
        where T : BaseEntity, IEntity
    {
        public static MapperConfiguration AutoMapperConfig { get; set; } 
        static string Folder { get; set; } 
        static string className;
        static IRepository<T> BaseRepo;
        
        public RepoWrapper(IRepository<T> baseRepo )
        {
            // AutoMapper is initialized in Global.asax
            BaseRepo = baseRepo;
            //char[] charsToTrim = { 'V', 'M' ,'>'};    // not needed but save, in case I go back to view models
            //string x = BaseRepo.ToString() ;
            //char[] c1 = { ']' };
            //string y = String.Concat(x.TrimEnd(c1),">");
            //string z = y.Replace("\'1[", "<");


            // className = typeof(T).ToString().TrimEnd(charsToTrim);
            //T = Type.GetType(className);
         
        }

        public void Add(T  entity)
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