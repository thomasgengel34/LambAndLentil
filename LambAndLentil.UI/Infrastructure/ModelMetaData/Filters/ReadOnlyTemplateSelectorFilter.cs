using System;
using System.Collections.Generic;

namespace LambAndLentil.UI.Infrastructure.ModelMetaData.Filters
{
	public class ReadOnlyTemplateSelectorFilter : IModelMetadataFilter
	{
		public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata,
			IEnumerable<Attribute> attributes)
		{
			if (metadata.IsReadOnly &&
				string.IsNullOrEmpty(metadata.DataTypeName))
			{
				metadata.DataTypeName = "ReadOnly";
			}
		}
	}
}