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
        ActionResult Attach<TChild>(IRepository<T> Repo, int? parentID, TChild child )
            where TChild : BaseEntity, IEntity, IPossibleChildren,new();

        ActionResult Detach<TChild>(IRepository<T> Repo, int? parentID, TChild child)
             where TChild : BaseEntity, IEntity, IPossibleChildren, new();

        ActionResult DetachASetOf<TChild>(int? ID, List<TChild> selected)
              where TChild : BaseEntity, IEntity, IPossibleChildren,new();

        ActionResult DetachAll<TChild>(int? ID)
         where TChild : BaseEntity, IEntity, IPossibleChildren, new();
    }
}