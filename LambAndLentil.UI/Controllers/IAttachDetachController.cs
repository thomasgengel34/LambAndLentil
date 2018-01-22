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
        ActionResult Attach<TChild>(int? parentID, TChild child )
            where TChild : BaseEntity, IEntity, new();

        ActionResult Detach<TChild>(  int? parentID, TChild child)
             where TChild : BaseEntity, IEntity, new();

        ActionResult DetachASetOf<TChild>(int? ID, List<TChild> selected)
              where TChild : BaseEntity, IEntity, new();

        ActionResult DetachAll<TChild>(int? ID)
         where TChild : BaseEntity, IEntity, new();
    }
}