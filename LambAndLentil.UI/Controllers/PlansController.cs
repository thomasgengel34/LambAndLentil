﻿using System;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.UI.Controllers
{
    public class PlansController : PlansGenericController<Plan>
    {
        public PlansController(IRepository<Plan> repository) : base(repository) => Repo = repository;


    } 
}
