using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMasterHelper
{
   interface IExitHelper
    {
        List<Exit.EMPList> GetEMPList_ByLocation(int LocationID);
        List<Exit.Request.List> GetExitRequestList(int Approved);
        Exit.Request.Add GetExitRequest(long Exit_ID);
        List<Exit.Req_Received.List> GetExit_ReqRecList(int Approved);
        Exit.Request_View GetExit_Req_View(long Exit_ID);
        Exit.Req_Process.Forward GetHR_ExitForward(long Exit_ID);
        Exit.Req_Process.Approve GetHR_ExitApproveAndProcess(long Exit_ID);
        List<Exit.Req_Approved.List> GetExit_Req_ApprovedList(int Approved);
        PostResponse SetExit_Handover_Persons(Exit.Req_Process.Approve.Department modal);
        PostResponse SetExit_Tasks(Exit.Exit_Task modal);
        PostResponse SetExit_Approved(long Exit_ID, string Approved_Remarks, int DeactiveSurvey, DateTime RelievingDate);
        List<Exit.LevelApprovals.List> GetExit_ApproversList(int Approved);
        Exit.LevelApprovals.Add GetExit_ApproversAdd(long Exit_APP_ID);
        PostResponse SetExit_Approvers_Action(Exit.LevelApprovals.Add modal);
        PostResponse SetExit_Handover_Persons_Default(long Exit_ID);
        Exit.StartExitProcess GetExit_StarExitProcess(long Exit_ID);
        PostResponse SetExit_StartProcess(Exit.StartExitProcess modal);
        List<Exit.NOC_Request.List> GetExit_NOC_List(long Approved);
        List<Exit.NOC_Dashboard.List> GetExit_NOC_Dashboard_List(long Approved);
        List<Exit.Confirmation.List> GetExit_Confirmation_List(long Approved);
        List<Exit.LevelApprovers_Details> GetExit_ApproversSuggationsList(long Exit_ID);
    }
}