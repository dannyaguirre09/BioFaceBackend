using System;
using System.Reflection;

namespace ApiFace.Areas.HelpPage.ModelDescriptions
{
	public interface IModelDocumentationProvider
	{
		string GetDocumentation(MemberInfo member);

		string GetDocumentation(Type type);
	}
}