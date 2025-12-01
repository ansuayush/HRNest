using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mitr.Models;

namespace Mitr.ModelsMasterHelper
{
    interface IDLibraryHelper
    {
        List<DLibrary.TagList> GetTagLists(GetResponse modal);
        PostResponse SetTag(DLibrary.TagAdd tag);
        DLibrary.TagAdd GetTag(long id);
        DLibrary.ContentForm GetContentFormDetails(GetResponse modal);
        DLibrary.ProjectList GetProjectDetails(string Projectid);
        PostResponse AddContentForm(DLibrary.ContentForm tag);
        List<DropDownList> GetSubCategoryByCategory(string categoryid, string Doctype);
    }
}