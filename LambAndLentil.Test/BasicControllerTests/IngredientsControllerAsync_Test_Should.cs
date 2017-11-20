using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LambAndLentil.UI.Controllers;

namespace LambAndLentil.Test.BasicControllerTests
{
    public class IngredientsControllerAsync_Test_Should:IngredientsController_Test_Should
    {
         protected IIngredientsControllerAsync  AsyncController = new IngredientsController(Repo); 
    }
}
