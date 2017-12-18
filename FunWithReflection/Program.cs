using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using LambAndLentil.Domain.Entities;
using System.Diagnostics;

namespace FunWithReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            Ingredient ingredient = new Ingredient();
            //PropertyInfo pinfo = typeof(Ingredient).GetProperty("CanHaveMenuChild");
            //ShoppingList sl = new ShoppingList();
            //PropertyInfo pinfo1 = typeof(ShoppingList).GetProperty("CanHaveMenuChild");
            //Type type = typeof(Ingredient);
            //Console.WriteLine("----------------------------------------------------");

            //Console.WriteLine(pinfo.GetValue(ingredient));
            //Console.WriteLine(pinfo1.GetValue(sl));
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 100000; i++)
            {
                CanAttachChild1<Ingredient>(ingredient);
            }
            stopwatch.Stop();
            Console.WriteLine("Ingredient Method One: " + stopwatch.Elapsed);
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            for (int i = 0; i < 100000; i++)
            {
                CanAttachChild<Ingredient,Ingredient>(ingredient);
            }
            stopwatch1.Stop();
            Console.WriteLine("Ingredient Method Two (Reflection): " + stopwatch1.Elapsed);

            Menu menu = new Menu();

            Stopwatch stopwatch3 = new Stopwatch();
            stopwatch3.Start();
            for (int i = 0; i < 100000; i++)
            {
                CanAttachChild1<Ingredient>(menu);
            }
            stopwatch3.Stop();
            Console.WriteLine("Menu Method One: " + stopwatch3.Elapsed);
            Stopwatch stopwatch4 = new Stopwatch();

            stopwatch4.Start();
            for (int i = 0; i < 100000; i++)
            {
                CanAttachChild<Menu, Ingredient>(menu);
            }
            stopwatch4.Stop();
            Console.WriteLine("Menu Method Two (Reflection): " + stopwatch4.Elapsed);
       

            Stopwatch stopwatch5 = new Stopwatch();
            stopwatch5.Start();
            for (int i = 0; i < 100000; i++)
            {
                ingredient.ParentCanHaveChild(ingredient);
            }
            stopwatch5.Stop();
            Console.WriteLine("Ingredient Method (New method): " + stopwatch5.Elapsed);
            Console.Read();
        }





        public static bool CanAttachChild1<TChild>(IEntity entity)
        {
            if (entity == null) { return false; }
            IPossibleChildren parent = (IPossibleChildren)entity;
            if (typeof(TChild) == typeof(Ingredient) && parent.CanHaveIngredientChild) { return true; }
            if (typeof(TChild) == typeof(Menu) && parent.CanHaveMenuChild) { return true; }
            if (typeof(TChild) == typeof(Plan) && parent.CanHavePlanChild) { return true; }
            if (typeof(TChild) == typeof(Person) && parent.CanHavePersonChild) { return true; }
            if (typeof(TChild) == typeof(Recipe) && parent.CanHaveRecipeChild) { return true; }
            if (typeof(TChild) == typeof(ShoppingList) && parent.CanHaveShoppingListChild) { return true; }
            return false;
        }

        public static bool CanAttachChild<TParent, TChild>(TParent parent)
        {
            if (parent == null) { return false; }

            string childName = typeof(TChild).ToString().Split('.').Last();
            string propertyName = string.Concat("CanHave", childName, "Child");
            //   string propertyName = "CanHaveIngredientChild";
            PropertyInfo pinfo;
            pinfo = typeof(TParent).BaseType.GetProperty(propertyName);
            if (pinfo == null)
            {
                pinfo = typeof(TParent).GetProperty(propertyName);
            }
            return (bool)pinfo.GetValue(parent);
        }

        public  bool CanHaveChild<TChild>(IPossibleChildren parent)
        {
            if (typeof(TChild) == typeof(Menu) && parent.CanHaveMenuChild) { return true; }
            return false;
        }

    }
}
