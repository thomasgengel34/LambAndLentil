using LambAndLentil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LambAndLentil.UI.Models
{
    public class ModelValidation
    {
        // TODO: implement
        // intended as a second line of defense. JSON errors should be caught in the Repo methods.  This class should catch logical errors in the model and mimic EF's model validation protocol(s).

        // Instead of thinking up ways a model may be invalid that may never happen, when one is found to be invalid as app progresses, this will be expanded to check the case. 

        public bool IsModelValid<T>(T item) where T : BaseEntity,IEntity
        { 

            Type type = item.GetType();
            PropertyInfo prop = type.GetProperty("ID");
            int id = (int)prop.GetValue(item);


            if (id < 1 ||  id > int.MaxValue )
            {
                return false;
            } 
            else
            {
                return true;
            }
        }
    }
}