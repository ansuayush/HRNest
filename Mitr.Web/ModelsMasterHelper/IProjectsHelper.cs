using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMasterHelper
{
    interface IProjectsHelper
    {
        List<Project.List> GetProjectRegistrationList(GetResponse modal);
        Project.ProjectDetails GetProjectDetails(GetResponse modal);
        PostResponse SetProjectDetails(Project.ProjectDetails modal);
        Project.ClientsDetails GetProjectClientsDetails(GetResponse modal);

        PostResponse SetProjectClientsDetails(Project.ClientsDetails modal);
        PostResponse SetProject_ContactPerson(Project.ContactPerson modal);

        Project.BudgetDetails GetProjectBudgetDetails(GetResponse modal);
        PostResponse SetProjectBudgetDetails(Project.BudgetDetails modal);

        Project.SpecialConditions GetProjectSpecialConditions(GetResponse modal);
        Project.DonorsReport GetProjectDonorsReport(GetResponse modal);
        Project.Attachment GetProjectAttachment(GetResponse modal);
        PostResponse SetProject_SpecialConditions(Project.SpecialConditions.Condition.Add modal);
        PostResponse SetProject_DonorsReport(Project.DonorsReport.ReportList modal);
        PostResponse SetProject_Attachment(Project.Attachment.Document.Add modal);
        List<Project.DonorDetails> GetProject_PendingContactPerson(GetResponse modal);
        PostResponse SetProjectMakeItLive(GetResponse modal);
    }
}