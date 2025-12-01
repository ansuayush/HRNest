using Mitr.Model;
using Mitr.Models;
using System.Collections.Generic;

namespace Mitr.DAL
{
    interface IGrievancesHelper
    {
        //List<Category> GetCategoryList(long CategoryID);
        List<SubCategory> GetSubCategoryList(long subCategoryID, int isDeleted);
        SubCategory GetSubCategoryDetail(SubCategory subCategory);
        List<ExternalMember> GetExternalMemberList(long subCategoryID);
        ExternalMember GetExternalMemberDetail(ExternalMember externalMember);
        PostResponseModel fnSetSubCategory(SubCategory model);
        PostResponseModel fnSetExternalMember(ExternalMember model);
        string GetExternalCode();
        PostResponseModel fnSetSubCategoryAssignee(SubCategoryAssigneeDetails modal);
        List<SubCategoryAssigneeDetails> GetSubCategoryAssigneeList(long id, long Categoryid, long subCategoryid);
        List<SubCategorySLAPolicy> GetSubCategorySLAPolicyList(long id, long subCategoryid);
        PostResponseModel fnSetSubCategorySLAPolicy(SubCategorySLAPolicy modal);
        PostResponseModel fnDeleteSubCategoryAssignee(SubCategoryAssigneeDetails model);
        List<UserGrievance> GetUserGrievanceList(UserGrievance modal);
        PostResponseModel fnSetUserGrievance(UserGrievance modal);
        UserGrievance GetUserGrievanceDetails(UserGrievance modal);
        List<UserGrievanceAccident> GetUserGrievanceAccidentList(long id);
        PostResponseModel fnSetUserGrievanceAccident(UserGrievanceAccident modal);
        List<SubcategoryGRAssigneeDetails> GetSubcategoryGRAssigneeDetails();


    }
}