using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;

namespace LambAndLentil.UI.Controllers
{

    public class MenusController : MenusGController<Menu>
    {
        public MenusController(IRepository<Menu> repository) : base(repository) => Repo = repository;
    }

    public class MenusGController<Menu> : BaseAttachDetachController<Menu>
         where Menu : BaseEntity, IEntity, new()
    { 
        public MenusGController(IRepository<Menu> repository) : base(repository) => Repo = repository; 
    } 
} 