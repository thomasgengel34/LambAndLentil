using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;

namespace LambAndLentil.UI.Controllers
{
    public interface IAttachDetachController<T>
        where T : class, IEntity
    {
        ActionResult Attach(IEntity parent, IEntity child );

        ActionResult Detach(IEntity parent, IEntity child);

        ActionResult DetachASetOf(IEntity parent, List<IEntity> selected);
   

        ActionResult DetachAll(IEntity parent, Type type);
    }
}