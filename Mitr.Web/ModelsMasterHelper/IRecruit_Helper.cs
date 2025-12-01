using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMasterHelper
{
    interface IRecruit_Helper
    {
        Recruit.Initiate GetREC_Initiate(long ProjectDetailID);
        Recruit.Request.Fill GetREC_Request(long REC_ReqID);
        List<Recruit.InternalStaff> GetIAvailStaff(long REC_ReqID, long PillarID, long LocationID, long JobID);
        Recruit.Request.View GetREC_Request_View(long REC_ReqID);
        List<Recruit.Recom_Preference> GetREC_Recom_Preference(long REC_ReqID);
        Recruit.Final_Preference GetREC_Final_Preference(long REC_ReqID);
        EXRecruit.View GetEREC_Request_View(long REC_ReqID);
        EXRecruit.Vacancy.HR GetVacancy_HR(long REC_ReqID);
        EXRecruit.Vacancy.Comm GetVacancy_Comm(long REC_ReqID);
        EXRecruit.Vacancy.Final GetVacancy_Final(long REC_ReqID);
        EXRecruit.Final_ConfirmedCV GetFinishConfirmedCV(long REC_ReqID);
        List<Interview.Round> GetInterviewRound(long REC_ReqID, long REC_InterviewSetID);
        List<InterviewSelection.Round> GetInterviewSelectionRound(long REC_ReqID);
        InterviewSelection.MoveToRound GetMoveToRound(long REC_AppID);
        //List<InterviewSelection.MoveToRound> GetMoveToRound(long REC_AppID);
        InterviewSelection.Approve GetInterviewApprove(long REC_AppID);
        List<InterviewSelection.ddList> GetInterviewRound(long RecId);
        List<InterviewSelection.Attachments> GetInterviewRound_Attachment(long REC_AppID);
        List<InterviewSelection.Attachments> GetInterviewRound_AttachementList(long REC_InterviewSetID);
    }
}