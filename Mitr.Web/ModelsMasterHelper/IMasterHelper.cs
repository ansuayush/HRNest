using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMasterHelper
{
    interface IMasterHelper
    {
        List<Department.List> GetDepartmentList(GetMasterResponse modal);
        Department.Add GetDepartment(GetMasterResponse modal);
        PostResponse fnSetDepartment(Department.Add model);
        List<MasterAll.List> GetMasterAllList(GetMasterResponse modal);
        MasterAll.Add GetMasterAll(GetMasterResponse modal);
        PostResponse fnSetMasterAll(MasterAll.Add model);
        List<Country> GetCountryList(long CountryID);
        List<City> GetCityList(long CityID);
        List<State> GetStateList(long StateID);
        List<Consultant> GetConsultantList(long ConsultantID);
        Consultant GetConsultant(long ConsultantID);
        List<ThematicArea> GetThematicAreaList(long ThematicAreaID);
        List<Subgrant> GetSubgrantList(long SubgrantID);
        List<SubgrantDetails> GetSubgrantDetailsList(long ID, string Doctype, long SubgrantID);
        SubgrantAdd GetSubgrantAdd(long SubgrantID);
        List<Announcement> GetAnnouncementList(long AnnouncementID);
        List<Holiday> GetHolidayList(long HolidayID);
        List<Question> GetQuestionList(long QuestionID);
        List<CompanyUpload> GetCompanyUpload(long id);
        List<MembershipPeriod> GetMembershipPeriodList(long MembershipPeriodID);
        List<FinYear> GetFinYearList(long FinYearID);
        List<LeaveMaster> GetLeaveMasterList(long leaveID);
        List<Location> GetLocationList(long LocationID);
        List<Designation> GetDesignationList(long DesignationID);
        List<Diem> GetDiemList(long DiemID);
    
        List<CostCenter> GetCostCenterList(long CostCenterID);
        List<SubLineItem> GetSubLineItemList(long SubLineItemID);
        List<Vendor> GetVendorList(long VendorID);
        List<DepreciationRate> GetDepreciationRateList(long DepID);
        List<ProcurementCommittee> GetProcurementCommitteeList(long ID);
        List<FreeMeal> GetFreeMealList(long FreeMealID);

        List<Job> GetJobList(long JobID);
        ViewJobDetails GetJobDetails(long JobID);
        List<JobRound> GetJobRound(long JobID, long JobDetailsID);
        List<Helpdesk.LocationGroup> GetLocationGroupList(long LocationGroupID);
        List<Donor.List> GetDonorList(GetResponse modal);
        Donor.Add GetDonor(GetResponse modal);
        Donor.View GetDonorView(GetResponse modal);
        PostResponse fnSetDonor(Donor.Add model);
        PostResponse fnSetDonorDetail(Donor.DonorDetails model);
        CompanyConfig fnGetCompnayConfiguration(long id);
        long fnUpdateCompanyConfiguration(long Id, string CompanyName, string CompanyAdress);

        PostResponse fnsetAddPerKm(PerKM model);
        List<PerKM> GetPerKmlist(long Id);

        PostResponse SetDeclarationMaster(DeclarationMaster model);
        DeclarationMaster GetDeclarationMaster();
        DeclarationMaster GetDeclarationMaster(int LoginID, int EMPID);
        DeclarationMaster GetRecordDeclarationMaster(long Id);
        List<MPRReports.SubSection> GetMPR_Reports_SubHeader(string MPRSID);
        PostResponse fnsetAddOfficeListing(OfficeListing model);
        List<OfficeListing> GetOfficeListing(long ID);
        List<Location> GetLocationOfficeList(long LocationID);
        List<Location> GetLocationOfficeListnew(long LocationID);

        List<BloodGroup> GetBloodGroupList(long id);

        PostResponse fnsetBloodGroupListing(BloodGroup model);

    }
}