using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.TestObjects
{
    public class TestPlan : Plan
    {
       
        public List<Plan> ListOfPlans { get; set; }


        public TestPlan() => ListOfPlans = new BaseTestObjects<Plan>().SetUpTList();

        internal Plan CreatePlan()
        {
           return new Plan() { ID = 1492 };
        }

       internal Plan AddIngredientChildrenToPlan(Plan plan)
        {
            //List<Plan> plans = new BaseTestObjects<Plan>().SetUpTList();
            //List<Ingredient> ingredientsAsIentity = new List<Ingredient>();
            //foreach (Plan item in plans)
            //{
            //    ingredientsAsIentity.Add(item);
            //}
            //plan.Ingredients = ingredientsAsIentity;
            return plan;
        }
    }
}