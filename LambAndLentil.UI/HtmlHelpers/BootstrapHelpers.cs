﻿using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LambAndLentil.UI.Helpers
{
	public static class BootstrapHelpers
	{
		public static IHtmlString BootstrapLabelFor<TModel, TProp>(
				this HtmlHelper<TModel> helper,
				Expression<Func<TModel, TProp>> property)
		{
			return helper.LabelFor(property, new
			{
				@class = "col-md-2 control-label"
			});
		} 
	}
}