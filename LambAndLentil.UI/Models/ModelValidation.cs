using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LambAndLentil.UI.Models
{
    public class ModelValidation
    {
        // TODO: implement
        // intended as a second line of defense. JSON errors should be caught in the Repo methods.  This class should catch logical errors in the model and mimic EF's model validation protocol(s).

        // Instead of thinking up ways a model may be invalid that may never happen, when one is found to be invalid as app progresses, this will be expanded to check the case. 

        public bool IsModelValid<T>(T item) where T : BaseVM
        {
            if (item.ID < 1 || item.ID>int.MaxValue)
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