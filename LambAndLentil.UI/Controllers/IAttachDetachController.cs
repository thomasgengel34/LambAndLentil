using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.UI.Controllers
{
    public interface IAttachDetachController
    {
        ActionResult Attach<TChild>(int? iD, TChild child, int orderNumber = 0);
        ActionResult Detach<TChild>(int? iD, TChild child, int orderNumber = 0); 
        ActionResult DetachASetOf<TChild>(int ID, List<TChild> selected); 
    }
}