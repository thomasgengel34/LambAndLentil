using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;

namespace LambAndLentil.UI.Controllers
{ 
    public class MenusController :  BaseAttachDetachController<Menu> 
    {
        public MenusController(IRepository<Menu> repository) : base(repository) => Repo = repository;
    } 
} 