using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMasterHelper
{
   interface IRecruitmentHelper
    {
        List<InternalStaff> GetInternalStaffList(long ProjectDetailID, long LocationID);
        InitiateRecruitment GetInitiateRecruitment(long ProjectDetailID);
        List<InternalStaff> GetSelectedRecruitmentCandidate_InternalList(long InternalCandidateID, long RecruitmentRequestID);
        List<RecruitmentRequest> GetRecruitmentRequestInternal(long RecruitmentRequestID,string For);
        RecruitmentRequest GetRecruitmentPreferenceRaw(long RecruitmentRequestID);
        List<RecruitmentRequestExternal> GetRecruitmentRequestExternal(long RecruitmentRequestID, string For);
        List<JobPost> GetJobPost(long JobPostID, string For);


    }
}