using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LambAndLentil.Domain.Entities; 

namespace LambAndLentil.Domain.Concrete
{
    public class RepositoryHelperMethods
    {
        public string GetPlainClassName<TVM>()
             where TVM : class, IEntity
        {
            char[] charsToTrim = { 'V', 'M' };
            string className = typeof(TVM).ToString().TrimEnd(charsToTrim);
            char[] splitterArray = { '.' };
            string[] classNameArray = className.Split(splitterArray);
            className = String.Concat("LambAndLentil.Domain.Entities.", classNameArray.Last());
            return className;
        }

        public string GetClassName<TVM>()
              where TVM : class, IEntity
        {
            string className = GetPlainClassName<TVM>();
            return (className == "ShoppingList") ? "Shopping List" : className;
        }

        //public string GetViewModelName<T>()
        //    where T: BaseEntity, IEntity
        //{
        //    string className= typeof(T).ToString();
        //    char[] splitterArray = { '.' };
        //    string[] classNameArray = className.Split(splitterArray);
        //    string vmName= String.Concat("LambAndLentil.UI.Models.", classNameArray.Last(),"VM");
        //    return vmName;
        //}
    }
}
