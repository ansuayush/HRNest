using Dapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static Mitr.Models.Employee;
namespace Mitr.ModelsMaster
{
    public class EmployeeMappingModal : IEmployeeMappingHelper
    {
        public List<Employee.List> GetEmployeeList(GetResponse modal)
        {
            List<Employee.List> result = new List<Employee.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Approve", dbType: DbType.Int32, value: modal.Approve, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmpList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeList. The query was executed :", ex.ToString(), "spu_GetEmp()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }

        public Employee.RegistrationDetails GetEmployeeRegistrationDetails(GetResponse modal)
        {
            Employee.RegistrationDetails result = new Employee.RegistrationDetails();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_Onboarding_GetEmployeeMapping", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.RegistrationDetails>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new Employee.RegistrationDetails();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DepartmentList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DesignationList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.JobList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.LocationList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.SkillList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.BankList = reader.Read<DropDownList>().ToList();
                        }

                        if (!reader.IsConsumed)
                        {
                            result.ThematicareaList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.SalaryAccount = reader.Read<Employee.EMP_Account>().FirstOrDefault();
                            if (result.SalaryAccount == null)
                            {
                                result.SalaryAccount = new Employee.EMP_Account();
                                result.SalaryAccount.Doctype = "Salary";
                                modal.Doctype = "Salary";
                                Employee.EMPAddress obj = GetEmployeeAddressCheckLock(modal);
                                result.SalaryAccount.LockStatus = obj.LockStatus;
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ReimbursementAccount = reader.Read<Employee.EMP_Account>().FirstOrDefault();
                            if (result.ReimbursementAccount == null)
                            {
                                result.ReimbursementAccount = new Employee.EMP_Account();
                                result.ReimbursementAccount.Doctype = "Reimbursement";
                                modal.Doctype = "Reimbursement";
                                Employee.EMPAddress obj = GetEmployeeAddressCheckLock(modal);
                                result.ReimbursementAccount.LockStatus = obj.LockStatus;
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.EMPList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.RoleList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.UserDetails = reader.Read<UserMan.Add>().FirstOrDefault();
                            if (result.UserDetails == null)
                            {
                                result.UserDetails = new UserMan.Add();
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstSalaryStucture = reader.Read<Employee.SalaryStructure>().FirstOrDefault();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeRegistrationDetails. The query was executed :", ex.ToString(), "spu_GetEmp()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }

        public Employee.PersonalDetails GetEmployeePersonalDetails(GetResponse modal)
        {
            Employee.PersonalDetails result = new Employee.PersonalDetails();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_Onboarding_GetEmployeeMapping", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.PersonalDetails>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Employee.PersonalDetails();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.CountryList = reader.Read<DropDownList>().ToList();
                            result.StateList = new List<DropDownList>();
                            result.CityList = new List<DropDownList>();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.QualificationList = reader.Read<Employee.Qualification>().ToList();
                            if (result.QualificationList.Count == 0)
                            {
                                Employee.Qualification obj = new Employee.Qualification();
                                result.QualificationList.Add(obj);
                            }

                        }
                        if (!reader.IsConsumed)
                        {
                            result.ReferencesList = reader.Read<Employee.References>().ToList();
                            if (result.ReferencesList.Count == 0)
                            {
                                Employee.References obj = new Employee.References();
                                result.ReferencesList.Add(obj);
                                result.ReferencesList.Add(obj);
                                result.ReferencesList.Add(obj);
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.LocalAddress = reader.Read<Employee.EMPAddress>().FirstOrDefault();
                            if (result.LocalAddress == null)
                            {
                                //result.LocalAddress = new Employee.EMPAddress();
                                modal.Doctype = "Local";
                                result.LocalAddress = GetEmployeeAddressCheckLock(modal);
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.PermanentAddress = reader.Read<Employee.EMPAddress>().FirstOrDefault();
                            if (result.PermanentAddress == null)
                            {
                                // result.PermanentAddress = new Employee.EMPAddress();
                                modal.Doctype = "Permanent";
                                result.PermanentAddress = GetEmployeeAddressCheckLock(modal);
                            }
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeePersonalDetails. The query was executed :", ex.ToString(), "spu_GetEmp()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public Employee.PersonalDetails GetMappingEmployeePersonalDetails(GetResponse modal)
        {
            Employee.PersonalDetails result = new Employee.PersonalDetails();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmployeeOnboardingMapped", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.PersonalDetails>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Employee.PersonalDetails();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.CountryList = reader.Read<DropDownList>().ToList();
                            result.StateList = new List<DropDownList>();
                            result.CityList = new List<DropDownList>();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.QualificationList = reader.Read<Employee.Qualification>().ToList();
                            if (result.QualificationList.Count == 0)
                            {
                                Employee.Qualification obj = new Employee.Qualification();
                                result.QualificationList.Add(obj);
                            }

                        }
                        if (!reader.IsConsumed)
                        {
                            result.ReferencesList = reader.Read<Employee.References>().ToList();
                            if (result.ReferencesList.Count == 0)
                            {
                                Employee.References obj = new Employee.References();
                                result.ReferencesList.Add(obj);
                                result.ReferencesList.Add(obj);
                                result.ReferencesList.Add(obj);
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.LocalAddress = reader.Read<Employee.EMPAddress>().FirstOrDefault();
                            if (result.LocalAddress == null)
                            {
                                //result.LocalAddress = new Employee.EMPAddress();
                                modal.Doctype = "Local";
                                result.LocalAddress = GetEmployeeAddressCheckLock(modal);
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.PermanentAddress = reader.Read<Employee.EMPAddress>().FirstOrDefault();
                            if (result.PermanentAddress == null)
                            {
                                // result.PermanentAddress = new Employee.EMPAddress();
                                modal.Doctype = "Permanent";
                                result.PermanentAddress = GetEmployeeAddressCheckLock(modal);
                            }
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMappingEmployeePersonalDetails. The query was executed :", ex.ToString(), "spu_GetEmp()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public Employee.GeneralInfo GetEmployeeGeneralInfo(GetResponse modal)
        {
            Employee.GeneralInfo result = new Employee.GeneralInfo();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_Onboarding_GetEmployeeMapping", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.GeneralInfo>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {
                            result.AirlinePreferencesList = reader.Read<Employee.AirlinePreferences>().ToList();
                            if (result.AirlinePreferencesList.Count == 0)
                            {
                                Employee.AirlinePreferences obj = new Employee.AirlinePreferences();
                                result.AirlinePreferencesList.Add(obj);
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.employmentDetails = reader.Read<Employee.EmploymentDetails>().FirstOrDefault();
                            if (result.employmentDetails == null)
                            {
                                result.employmentDetails = new Employee.EmploymentDetails();
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.SeatPreferencesList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.MealPreferencesList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DesignationList = reader.Read<DropDownList>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeGeneralInfo. The query was executed :", ex.ToString(), "spu_GetEmp()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public Employee.GeneralInfo GetMappingEmployeeGeneralInfo(GetResponse modal)
        {
            Employee.GeneralInfo result = new Employee.GeneralInfo();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmployeeOnboardingMapped", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.GeneralInfo>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {
                            result.AirlinePreferencesList = reader.Read<Employee.AirlinePreferences>().ToList();
                            if (result.AirlinePreferencesList.Count == 0)
                            {
                                Employee.AirlinePreferences obj = new Employee.AirlinePreferences();
                                result.AirlinePreferencesList.Add(obj);
                            }
                        }

                        if (!reader.IsConsumed)
                        {
                            result.employmentDetails = reader.Read<Employee.EmploymentDetails>().FirstOrDefault();
                            if (result.employmentDetails == null)
                            {
                                result.employmentDetails = new Employee.EmploymentDetails();
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.SeatPreferencesList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.MealPreferencesList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DesignationList = reader.Read<DropDownList>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeGeneralInfo. The query was executed :", ex.ToString(), "spu_GetEmp()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }

        public Employee.IDInfo GetEmployeeIDInfoList(GetResponse modal)
        {
            Employee.IDInfo result = new Employee.IDInfo();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_Onboarding_GetEmployeeMapping", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.IDInfo>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {
                            result.EMPAttachmentsList = reader.Read<Employee.EMPAttachments>().ToList();

                        }
                        if (!reader.IsConsumed)
                        {
                            result.Accident = reader.Read<Employee.EMPInsurance>().FirstOrDefault();
                            if (result.Accident == null)
                            {
                                result.Accident = new Employee.EMPInsurance();
                            }

                        }
                        if (!reader.IsConsumed)
                        {
                            result.Medical = reader.Read<Employee.EMPInsurance>().FirstOrDefault();
                            if (result.Medical == null)
                            {
                                result.Medical = new Employee.EMPInsurance();
                            }

                        }
                        List<ResidentStatus> list = new List<ResidentStatus>();
                        list.Add(new ResidentStatus { ID = 1, Name = "Resident" });
                        list.Add(new ResidentStatus { ID = 2, Name = "Non-Resident" });
                        result.ResidentStatus = list;
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeIDInfo. The query was executed :", ex.ToString(), "spu_GetEmp()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public Employee.IDInfo GetMappingEmployeeIDInfoList(GetResponse modal)
        {
            Employee.IDInfo result = new Employee.IDInfo();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmployeeOnboardingMapped", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.IDInfo>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {
                            result.EMPAttachmentsList = reader.Read<Employee.EMPAttachments>().ToList();

                        }
                        if (!reader.IsConsumed)
                        {
                            result.Accident = reader.Read<Employee.EMPInsurance>().FirstOrDefault();
                            if (result.Accident == null)
                            {
                                result.Accident = new Employee.EMPInsurance();
                            }

                        }
                        if (!reader.IsConsumed)
                        {
                            result.Medical = reader.Read<Employee.EMPInsurance>().FirstOrDefault();
                            if (result.Medical == null)
                            {
                                result.Medical = new Employee.EMPInsurance();
                            }

                        }

                        List<ResidentStatus> list = new List<ResidentStatus>();
                        list.Add(new ResidentStatus { ID = 1, Name = "Resident" });
                        list.Add(new ResidentStatus { ID = 2, Name = "Non-Resident" });
                        result.ResidentStatus = list;

                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMappingEmployeeIDInfoList. The query was executed :", ex.ToString(), "spu_GetEmp()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public Employee.Attachments GetEmployeeAttachments(GetResponse modal)
        {
            Employee.Attachments result = new Employee.Attachments();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_Onboarding_GetEmployeeMapping", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.Attachments>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {
                            result.EMPAttachmentsList = reader.Read<Employee.EMPAttachments>().ToList();

                        }
                        if (!reader.IsConsumed)
                        {
                            result.EMPOtherAttachmentsList = reader.Read<Employee.EMPAttachments>().ToList();
                            if (result.EMPOtherAttachmentsList.Count == 0)
                            {
                                result.EMPOtherAttachmentsList.Add(new Employee.EMPAttachments());
                            }

                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeAttachmentsList. The query was executed :", ex.ToString(), "spu_GetEmp()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public Employee.Attachments GetMappingEmployeeAttachments(GetResponse modal)
        {
            Employee.Attachments result = new Employee.Attachments();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmployeeOnboardingMapped", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.Attachments>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {
                            result.EMPAttachmentsList = reader.Read<Employee.EMPAttachments>().ToList();

                        }
                        if (!reader.IsConsumed)
                        {
                            result.EMPOtherAttachmentsList = reader.Read<Employee.EMPAttachments>().ToList();
                            if (result.EMPOtherAttachmentsList.Count == 0)
                            {
                                result.EMPOtherAttachmentsList.Add(new Employee.EMPAttachments());
                            }

                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMappingEmployeeAttachments. The query was executed :", ex.ToString(), "spu_GetEmp()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public Employee.Declaration GetEmployeeDeclaration(GetResponse modal)
        {
            Employee.Declaration result = new Employee.Declaration();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmp", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.Declaration>().FirstOrDefault();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeDeclarationList. The query was executed :", ex.ToString(), "spu_GetEmp()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }


        public PostResponse SetEMP_Account(Employee.EMP_Account modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMP_Account", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@AccountID", SqlDbType.Int).Value = modal.AccountID ?? 0;
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Doctype);
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = modal.EMPID;
                        command.Parameters.Add("@BankID", SqlDbType.Int).Value = modal.BankID ?? 0;
                        command.Parameters.Add("@AccountType", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.AccountType.ToString());
                        command.Parameters.Add("@AccountName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.AccountName);
                        command.Parameters.Add("@AccountNo", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.AccountNo);
                        command.Parameters.Add("@BranchName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.BranchName);
                        command.Parameters.Add("@BranchAddress", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.BranchAddress);
                        command.Parameters.Add("@IFSCCode", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.IFSCCode);
                        command.Parameters.Add("@SwiftCode", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.SwiftCode);
                        command.Parameters.Add("@OtherDetails", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.OtherDetails);
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }
        public PostResponse SetEMP_Onboarding_Account(Employee.EMP_Account modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_Onboarding_SetEMP_AccountMapping", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@AccountID", SqlDbType.Int).Value = modal.AccountID ?? 0;
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Doctype);
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = modal.EMPID;
                        command.Parameters.Add("@BankID", SqlDbType.Int).Value = modal.BankID ?? 0;
                        command.Parameters.Add("@AccountType", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.AccountType.ToString());
                        command.Parameters.Add("@AccountName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.AccountName);
                        command.Parameters.Add("@AccountNo", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.AccountNo);
                        command.Parameters.Add("@BranchName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.BranchName);
                        command.Parameters.Add("@BranchAddress", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.BranchAddress);
                        command.Parameters.Add("@IFSCCode", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.IFSCCode);
                        command.Parameters.Add("@SwiftCode", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.SwiftCode);
                        command.Parameters.Add("@OtherDetails", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.OtherDetails);
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public PostResponse SetEMP_RegistrationDetails(Employee.RegistrationDetails modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMP_RegistrationDetails", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = modal.ID;
                        command.Parameters.Add("@USER_ID", SqlDbType.Int).Value = modal.USER_ID;
                        command.Parameters.Add("@emp_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.emp_name);
                        command.Parameters.Add("@email", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.email);
                        command.Parameters.Add("@doj", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.DOJ);
                        command.Parameters.Add("@EmploymentTerm", SqlDbType.VarChar).Value = modal.EmploymentTerm;
                        command.Parameters.Add("@Contract_EndDate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Contract_EndDate);
                        command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = modal.DepartmentID;
                        command.Parameters.Add("@thematicarea_IDs", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.thematicarea_IDs);
                        command.Parameters.Add("@WorkLocationID", SqlDbType.Int).Value = modal.WorkLocationID;
                        command.Parameters.Add("@metro", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.metro.ToString());
                        command.Parameters.Add("@design_id", SqlDbType.Int).Value = modal.design_id;
                        command.Parameters.Add("@JobID", SqlDbType.Int).Value = modal.JobID;
                        command.Parameters.Add("@emp_status", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.emp_status.ToString());
                        command.Parameters.Add("@Probation_EndDate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Probation_EndDate);
                        command.Parameters.Add("@doc", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.doc);
                        command.Parameters.Add("@hod_name", SqlDbType.Int).Value = modal.hod_name;
                        command.Parameters.Add("@SecondaryHODID", SqlDbType.Int).Value = ClsCommon.EnsureNumber(modal.SecondaryHODID.ToString());
                        command.Parameters.Add("@ed_name", SqlDbType.Int).Value = ClsCommon.EnsureNumber(modal.ed_name.ToString());
                        command.Parameters.Add("@HRID", SqlDbType.Int).Value = ClsCommon.EnsureNumber(modal.HRID.ToString());
                        command.Parameters.Add("@AppraiserID", SqlDbType.Int).Value = modal.AppraiserID;
                        command.Parameters.Add("@co_ot", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.co_ot.ToString());
                        command.Parameters.Add("@ResidentialStatus", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ResidentialStatus.ToString());
                        command.Parameters.Add("@SkillsIDs", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.SkillsIDs);
                        command.Parameters.Add("@NoticePeriod", SqlDbType.Int).Value = modal.NoticePeriod;
                        command.Parameters.Add("@dor", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.DOR);
                        command.Parameters.Add("@lastworking_day", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.lastworking_day);
                        command.Parameters.Add("@PsychometricTest", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.PsychometricTest.ToString());
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.Parameters.Add("@Personalemail", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Personalemail);
                        command.Parameters.Add("@NoticePeriodPayable", SqlDbType.Int).Value = modal.NoticePeriodPayable;
                        command.Parameters.Add("@NoticePeriodWaived", SqlDbType.Int).Value = modal.NoticePeriodWaived;
                        command.Parameters.Add("@LDWLeaveStatus", SqlDbType.Int).Value = modal.LDWLeaveStatus;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }
        public PostResponse SetEMP_OnboardingRegistrationDetails(Employee.RegistrationDetails modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_Onboarding_SetEMP_RegistrationDetailsMapping", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = modal.ID;
                        command.Parameters.Add("@USER_ID", SqlDbType.Int).Value = modal.USER_ID;
                        command.Parameters.Add("@emp_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.emp_name);
                        command.Parameters.Add("@email", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.email);
                        command.Parameters.Add("@doj", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.DOJ);
                        command.Parameters.Add("@EmploymentTerm", SqlDbType.VarChar).Value = modal.EmploymentTerm;
                        command.Parameters.Add("@Contract_EndDate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Contract_EndDate);
                        command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = modal.DepartmentID;
                        command.Parameters.Add("@thematicarea_IDs", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.thematicarea_IDs);
                        command.Parameters.Add("@WorkLocationID", SqlDbType.Int).Value = modal.WorkLocationID;
                        command.Parameters.Add("@metro", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.metro.ToString());
                        command.Parameters.Add("@design_id", SqlDbType.Int).Value = modal.design_id;
                        command.Parameters.Add("@JobID", SqlDbType.Int).Value = modal.JobID;
                        command.Parameters.Add("@emp_status", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.emp_status.ToString());
                        command.Parameters.Add("@Probation_EndDate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Probation_EndDate);
                        command.Parameters.Add("@doc", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.doc);
                        command.Parameters.Add("@hod_name", SqlDbType.Int).Value = modal.hod_name;
                        command.Parameters.Add("@SecondaryHODID", SqlDbType.Int).Value = ClsCommon.EnsureNumber(modal.SecondaryHODID.ToString());
                        command.Parameters.Add("@ed_name", SqlDbType.Int).Value = ClsCommon.EnsureNumber(modal.ed_name.ToString());
                        command.Parameters.Add("@HRID", SqlDbType.Int).Value = ClsCommon.EnsureNumber(modal.HRID.ToString());
                        command.Parameters.Add("@AppraiserID", SqlDbType.Int).Value = modal.AppraiserID;
                        command.Parameters.Add("@co_ot", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.co_ot.ToString());
                        command.Parameters.Add("@ResidentialStatus", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ResidentialStatus.ToString());
                        command.Parameters.Add("@SkillsIDs", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.SkillsIDs);
                        command.Parameters.Add("@NoticePeriod", SqlDbType.Int).Value = modal.NoticePeriod;
                        command.Parameters.Add("@dor", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.DOR);
                        command.Parameters.Add("@lastworking_day", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.lastworking_day);
                        command.Parameters.Add("@PsychometricTest", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.PsychometricTest.ToString());
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.Parameters.Add("@Personalemail", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Personalemail);
                        command.Parameters.Add("@NoticePeriodPayable", SqlDbType.Int).Value = modal.NoticePeriodPayable;
                        command.Parameters.Add("@NoticePeriodWaived", SqlDbType.Int).Value = modal.NoticePeriodWaived;
                        command.Parameters.Add("@LDWLeaveStatus", SqlDbType.Int).Value = modal.LDWLeaveStatus;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }



        public PostResponse SetMaster_Emp_Qualification(Employee.Qualification modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_Onboarding_SetMaster_Emp_QualificationMapping", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@QID", SqlDbType.Int).Value = modal.QID ?? 0;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = modal.EMPID;
                        command.Parameters.Add("@Course", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Course);
                        command.Parameters.Add("@University", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.University);
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Location);
                        command.Parameters.Add("@Year", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Year);
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }


        public PostResponse SetMaster_Emp_References(Employee.References modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_Onboarding_SetMaster_Emp_ReferencesMapping", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@REFID", SqlDbType.Int).Value = modal.REFID ?? 0;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = modal.EMPID;
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Doctype);
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Name);
                        command.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.EmailID);
                        command.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Mobile);
                        command.Parameters.Add("@Relationship", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Relationship);
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }


        public PostResponse SetAddress(Address modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetAddress", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = modal.ID ?? 0;
                        command.Parameters.Add("@TableID", SqlDbType.Int).Value = modal.TableID;
                        command.Parameters.Add("@TableName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.TableName);
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Doctype);
                        command.Parameters.Add("@lane1", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.lane1);
                        command.Parameters.Add("@lane2", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.lane2);
                        command.Parameters.Add("@CountryID", SqlDbType.Int).Value = modal.CountryID ?? 0;
                        command.Parameters.Add("@StateID", SqlDbType.Int).Value = modal.StateID ?? 0;
                        command.Parameters.Add("@CityID", SqlDbType.Int).Value = modal.CityID ?? 0;
                        command.Parameters.Add("@phone_no", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.phone_no);
                        command.Parameters.Add("@Alt_No", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Alt_No);
                        command.Parameters.Add("@LandlineNo", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.LandlineNo);
                        command.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.EmailID);
                        command.Parameters.Add("@fax", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.fax);
                        command.Parameters.Add("@zip_code", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.zip_code);
                        command.Parameters.Add("@cell", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.cell);
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }


        public PostResponse SetEMP_PersonalDetails(Employee.PersonalDetails modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMP_PersonalDetails", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = modal.id;
                        command.Parameters.Add("@father_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.father_name);
                        command.Parameters.Add("@mother_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.mother_name);
                        command.Parameters.Add("@gender", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.gender.ToString());
                        command.Parameters.Add("@dob", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.dob);
                        command.Parameters.Add("@CountryID", SqlDbType.Int).Value = modal.CountryID ?? 0;
                        command.Parameters.Add("@marital_status", SqlDbType.VarChar).Value = modal.marital_status.ToString();
                        command.Parameters.Add("@SpouseName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.SpouseName);
                        command.Parameters.Add("@PartnerName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.PartnerName);
                        command.Parameters.Add("@NomineeName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.NomineeName);
                        command.Parameters.Add("@children", SqlDbType.Int).Value = modal.children;
                        command.Parameters.Add("@NomineeRelation", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.NomineeRelation);
                        command.Parameters.Add("@Nationality", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.nationality.ToString());
                        command.Parameters.Add("@VisaValidity_Date", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.VisaValidity_Date);
                        command.Parameters.Add("@VisaPermit_WorkDetail", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.VisaPermit_WorkDetail);
                        command.Parameters.Add("@Visa_OtherDetails", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Visa_OtherDetails);
                        command.Parameters.Add("@SSN_TIN", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.SSN_TIN);
                        command.Parameters.Add("@SpecialAbility", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.SpecialAbility.ToString());
                        command.Parameters.Add("@AnyMedicalCondition", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.AnyMedicalCondition);
                        command.Parameters.Add("@PhysicianName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.PhysicianName);
                        command.Parameters.Add("@PhysicianNumber", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.PhysicianNumber);
                        command.Parameters.Add("@PhysicianAlternate_No", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.PhysicianAlternate_No);
                        command.Parameters.Add("@BloodGroup", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.BloodGroup.ToString());
                        command.Parameters.Add("@emergContact_no", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.emergContact_no);
                        command.Parameters.Add("@emergContact_Name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.emergContact_Name);
                        command.Parameters.Add("@emergContact_Relation", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.emergContact_Relation);
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public PostResponse SetEMP_LastEmployment(Employee.EmploymentDetails model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_Onboarding_SetEMP_LastEmploymentMapping", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID ?? 0;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = model.EMPID;
                        command.Parameters.Add("@CompanyName", SqlDbType.VarChar, 500).Value = model.CompanyName ?? "";
                        command.Parameters.Add("@Designation", SqlDbType.VarChar, 500).Value = model.Designation ?? "";
                        command.Parameters.Add("@Location", SqlDbType.VarChar, 500).Value = model.Location ?? "";
                        command.Parameters.Add("@EmploymentTerm", SqlDbType.VarChar, 50).Value = model.EmploymentTerm ?? "";
                        command.Parameters.Add("@DOJ", SqlDbType.VarChar).Value = model.DOJ ?? "";
                        command.Parameters.Add("@DOR", SqlDbType.VarChar).Value = model.DOR ?? "";
                        command.Parameters.Add("@ShareSomething", SqlDbType.VarChar).Value = model.ShareSomething ?? "";
                        command.Parameters.Add("@AnnualCTC", SqlDbType.Decimal).Value = model.AnnualCTC ?? 0;
                        command.Parameters.Add("@TotalExperence", SqlDbType.Int).Value = model.TotalExperence ?? 0;
                        command.Parameters.Add("@IncomeAmount", SqlDbType.Decimal).Value = model.IncomeAmount ?? 0;
                        command.Parameters.Add("@TDSDeduction", SqlDbType.Decimal).Value = model.TDSDeduction ?? 0;
                        command.Parameters.Add("@IsConsiderIncome", SqlDbType.VarChar).Value = model.IsConsiderIncome ?? "No";
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }


        public PostResponse SetAirlinePreferences(Employee.AirlinePreferences model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetAirlinePreferencesMapping", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@AirID", SqlDbType.Int).Value = model.AirID ?? 0;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = model.EMPID ?? 0;
                        command.Parameters.Add("@AirlineName", SqlDbType.VarChar, 500).Value = model.AirlineName ?? "";
                        command.Parameters.Add("@FlyerNumber", SqlDbType.VarChar, 500).Value = model.FlyerNumber ?? "";
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = model.IsActive;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public PostResponse SetEMP_GeneralInfo(Employee.GeneralInfo model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_Onboarding_SetEMP_GeneralInfoMapping", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.id;
                        command.Parameters.Add("@MealPreferenceID", SqlDbType.Int).Value = model.MealPreferenceID ?? 0;
                        command.Parameters.Add("@SeatPreferencesID", SqlDbType.Int).Value = model.SeatPreferencesID ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }


        public PostResponse SetEMP_Attachments(Employee.EMPAttachments model, long AttachmentID = 0)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_Onboarding_SetEMP_AttachmentsMapping", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@EAttachID", SqlDbType.Int).Value = model.EAttachID ?? 0;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = model.EMPID;
                        command.Parameters.Add("@IsOpted", SqlDbType.Int).Value = model.IsOpted;
                        command.Parameters.Add("@No", SqlDbType.VarChar, 500).Value = model.No ?? "";
                        command.Parameters.Add("@Name", SqlDbType.VarChar, 500).Value = model.Name ?? "";
                        command.Parameters.Add("@OfficialName", SqlDbType.VarChar, 500).Value = model.OfficialName ?? "";
                        command.Parameters.Add("@IssueDate", SqlDbType.VarChar).Value = model.IssueDate ?? "";
                        command.Parameters.Add("@ExpiryDate", SqlDbType.VarChar).Value = model.ExpiryDate ?? "";
                        command.Parameters.Add("@PlaceOfIssue", SqlDbType.VarChar, 50).Value = model.PlaceOfIssue ?? "";
                        command.Parameters.Add("@AttachmentID", SqlDbType.Int).Value = AttachmentID;
                        command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = model.Remarks ?? "";
                        command.Parameters.Add("@UploadRemarks", SqlDbType.VarChar).Value = model.UploadRemarks ?? "";
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }


        public PostResponse SetEMP_Insurance(Employee.EMPInsurance model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_Onboarding_SetEMP_InsuranceMapping", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@InsuranceID", SqlDbType.Int).Value = model.InsuranceID ?? 0;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = model.EMPID ?? 0;
                        command.Parameters.Add("@InsuranceType", SqlDbType.VarChar, 50).Value = model.InsuranceType ?? "";
                        command.Parameters.Add("@Provider", SqlDbType.VarChar, 500).Value = model.Provider ?? "";
                        command.Parameters.Add("@PolicyNo", SqlDbType.VarChar, 50).Value = model.PolicyNo ?? "";
                        command.Parameters.Add("@TPA", SqlDbType.VarChar, 50).Value = model.TPA ?? "";
                        command.Parameters.Add("@TPAContactDetail", SqlDbType.VarChar, 500).Value = model.TPAContactDetail ?? "";
                        command.Parameters.Add("@CoverageAmt", SqlDbType.VarChar).Value = model.CoverageAmt ?? 0;
                        command.Parameters.Add("@StartDate", SqlDbType.VarChar).Value = model.StartDate ?? "";
                        command.Parameters.Add("@RenewalDate", SqlDbType.VarChar).Value = model.RenewalDate ?? "";
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }
        public Employee.LeaveBalance GetEmployeeLeaveBalance(GetResponse modal)
        {
            Employee.LeaveBalance result = new Employee.LeaveBalance();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Empid", dbType: DbType.Int32, value: modal.AdditionalID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMPDet", param: param, commandType: CommandType.StoredProcedure))
                    {
                        //result = reader.Read<Employee.LeaveBalance>().FirstOrDefault();
                        //if (result == null)
                        //{
                        //    result = new Employee.LeaveBalance();
                        //}
                        //if (result == null)
                        {
                            result.lstLeaveBalanceDet = reader.Read<Employee.LeaveBalanceList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstLeaveMaster = reader.Read<DropDownList>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeLevel. The query was executed :", ex.ToString(), "spu_GetEMPDet()", "EmployeeModal", "EmployeeModal", "");
            }
            return result;
        }
        public PostResponse SetEmployeeLeaveBalance(Employee.LeaveBalance modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEmpDet", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@emp_id", SqlDbType.Int).Value = modal.emp_id;
                        command.Parameters.Add("@srno", SqlDbType.Int).Value = modal.srno;
                        command.Parameters.Add("@leave_id", SqlDbType.Int).Value = modal.leave_id;
                        command.Parameters.Add("@type", SqlDbType.VarChar).Value = "";
                        command.Parameters.Add("@opening", SqlDbType.Decimal).Value = modal.opening;
                        command.Parameters.Add("@allotted", SqlDbType.Decimal).Value = modal.allotted;
                        command.Parameters.Add("@start_date", SqlDbType.VarChar).Value = modal.start_date;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }
        public List<Employee.List> GetEmployeeLeaveBalanceList(GetResponse modal)
        {
            List<Employee.List> result = new List<Employee.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Approve", dbType: DbType.Int32, value: modal.Approve, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmpLeaveBalanceList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeLeaveBalanceList. The query was executed :", ex.ToString(), "spu_GetEmp()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }


        public Dashboard.DashboardList GetDashboardEmpInfo()
        {
            Dashboard.DashboardList result = new Dashboard.DashboardList();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("LoginID"), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmpDashboardInfo", param: param, commandType: CommandType.StoredProcedure))
                    {
                        if (!reader.IsConsumed)
                        {
                            result.LeaveDashBoard = reader.Read<Dashboard.LeaveDashBoard>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.TravelDashBoard = reader.Read<Dashboard.TravelDashBoard>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.BirthdayDashBoard = reader.Read<Dashboard.BirthdayDashBoard>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.NewJoineesDashBoard = reader.Read<Dashboard.NewJoineesDashBoard>().ToList();

                        }
                        if (!reader.IsConsumed)
                        {
                            result.WorkanniversaryDashBoard = reader.Read<Dashboard.WorkanniversaryDashBoard>().ToList();
                        }

                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLead. The query was executed :", ex.ToString(), "spu_GetTrainingWorkshopSeminar()", "BudgetMasterModal", "BudgetMasterModal", "");

            }
            return result;
        }

        public List<Employee.Declaration> GetUserDeclartionPending(GetResponse modal)
        {
            List<Employee.Declaration> result = new List<Employee.Declaration>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@UserId", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("LoginID"), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDeclarationUserListPending", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.Declaration>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeDeclartionList. The query was executed :", ex.ToString(), "spu_GetDeclarationEmployeeListPending()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public List<Employee.Declaration> GetUserDeclartionList(GetResponse modal)
        {
            List<Employee.Declaration> result = new List<Employee.Declaration>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@UserId", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("LoginID"), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDeclarationUserListPending", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.Declaration>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeDeclartionList. The query was executed :", ex.ToString(), "spu_GetDeclarationEmployeeListPending()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public List<Employee.Declaration> GetOnboardingDeclartionPending(GetResponse modal)
        {
            List<Employee.Declaration> result = new List<Employee.Declaration>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@UserId", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_Onboarding_GetDeclarationMasterList", param: param, commandType: CommandType.StoredProcedure))
                    {//spu_GetDeclarationEmployeeListPending
                        result = reader.Read<Employee.Declaration>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetOnboardingEmployeeDeclartionList. The query was executed :", ex.ToString(), "spu_GetDeclarationEmployeeListPending()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public List<Employee.Declaration> GetOnboardingDeclartionApprove(GetResponse modal)
        {
            List<Employee.Declaration> result = new List<Employee.Declaration>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    //param.Add("@UserId", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_Onboarding_GetDeclarationUserListApprovedMapping", param: param, commandType: CommandType.StoredProcedure))
                    {//spu_GetDeclarationEmployeeListPending
                        result = reader.Read<Employee.Declaration>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetOnboardingEmployeeDeclartionList. The query was executed :", ex.ToString(), "spu_GetDeclarationEmployeeListPending()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public List<Employee.Declaration> GetEmployeeDeclartionList(GetResponse modal)
        {
            List<Employee.Declaration> result = new List<Employee.Declaration>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@UserId", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("LoginID"), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDeclarationEmployeeListPending", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.Declaration>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeDeclartionList. The query was executed :", ex.ToString(), "spu_GetDeclarationEmployeeListPending()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public List<Employee.Declaration> GetUserDeclartionApprovedList(GetResponse modal)
        {
            List<Employee.Declaration> result = new List<Employee.Declaration>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    //param.Add("@UserId", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("LoginID"), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDeclarationUserListApproved", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.Declaration>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeDeclartionList. The query was executed :", ex.ToString(), "spu_GetDeclarationEmployeeListPending()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public List<Employee.Declaration> GetEmployeeDeclartionApprovedList(GetResponse modal)
        {
            List<Employee.Declaration> result = new List<Employee.Declaration>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@UserId", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("LoginID"), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDeclarationEmployeeListApproved", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.Declaration>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeDeclartionList. The query was executed :", ex.ToString(), "spu_GetDeclarationEmployeeListPending()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public PostResponse SetDeclarationEmployee(Employee.Declaration model)
        {
            PostResponse result = new PostResponse();
            long LoginID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            string SQL = "";
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_Onboarding_SetDeclarationEmployeeAttachmentMapping", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@AttachmentId", SqlDbType.Int).Value = model.AttachmentId;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        if (LoginID > 0)
                        {
                            command.Parameters.Add("@createdby", SqlDbType.Int).Value = clsApplicationSetting.GetSessionValue("LoginID");
                            command.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = clsApplicationSetting.GetSessionValue("EMPID");
                        }
                        else
                        {
                            command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.UserId;
                            command.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = model.EmpId;
                        }

                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during spu_SetDeclarationEmployeeAttachment. The query was executed :", ex.ToString(), SQL, "EmployeeModal", "EmployeeModal", "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }
        public PostResponse SetHRDeclarationEmployee(Employee.Declaration model)
        {
            PostResponse result = new PostResponse();
            long LoginID = 0;
            long.TryParse(model.UserId.ToString(), out LoginID);
            string SQL = "";
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDeclarationEmployeeAttachment", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@AttachmentId", SqlDbType.Int).Value = model.AttachmentId;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        if (LoginID > 0)
                        {
                            command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.UserId.ToString();
                            command.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = model.EmpId.ToString();
                        }
                        else
                        {
                            command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.UserId;
                            command.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = model.EmpId;
                        }

                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during spu_SetDeclarationEmployeeAttachment. The query was executed :", ex.ToString(), SQL, "EmployeeModal", "EmployeeModal", "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public PostResponse UpdatebankdetailsEmployee(EmployeeMasterUpdate model)
        {
            PostResponse result = new PostResponse();
            string SQL = "";
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEmployeeMasterupdateAll", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@BankID", SqlDbType.Int).Value = model.BankID;
                        command.Parameters.Add("@AccountType", SqlDbType.VarChar).Value = model.AccountType;
                        command.Parameters.Add("@AccountName", SqlDbType.VarChar).Value = model.AccountName ?? "";
                        command.Parameters.Add("@AccountNo", SqlDbType.VarChar).Value = model.AccountNo ?? "";
                        command.Parameters.Add("@BranchAddress", SqlDbType.VarChar).Value = model.BranchAddress ?? "";
                        command.Parameters.Add("@IFSCCode", SqlDbType.VarChar).Value = model.IFSCCode ?? "";
                        command.Parameters.Add("@SwiftCode", SqlDbType.VarChar).Value = model.SwiftCode ?? "";
                        command.Parameters.Add("@OtherDetails", SqlDbType.VarChar).Value = model.OtherDetails ?? "";
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = model.Doctype;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ?? 0;
                        command.Parameters.Add("@BranchName", SqlDbType.VarChar).Value = model.BranchName ?? "";
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        if (model.Mobile == "Mobile")
                        {
                            command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                            command.Parameters.Add("@EMPID", SqlDbType.Int).Value = model.EMPID;
                        }
                        else
                        {
                            command.Parameters.Add("@createdby", SqlDbType.Int).Value = clsApplicationSetting.GetSessionValue("LoginID");
                            command.Parameters.Add("@EMPID", SqlDbType.Int).Value = clsApplicationSetting.GetSessionValue("EMPID");
                        }

                        command.Parameters.Add("@gender", SqlDbType.VarChar).Value = model.gender ?? "";
                        command.Parameters.Add("@marital_status", SqlDbType.VarChar).Value = model.marital_status ?? "";
                        command.Parameters.Add("@SpouseName", SqlDbType.VarChar).Value = model.SpouseName ?? "";
                        command.Parameters.Add("@PartnerName", SqlDbType.VarChar).Value = model.PartnerName ?? "";
                        command.Parameters.Add("@NomineeName", SqlDbType.VarChar).Value = model.NomineeName ?? "";
                        command.Parameters.Add("@NomineeRelation", SqlDbType.VarChar).Value = model.NomineeRelation ?? "";
                        command.Parameters.Add("@children", SqlDbType.Int).Value = model.children;
                        command.Parameters.Add("@lane1", SqlDbType.VarChar).Value = model.lane1 ?? "";
                        command.Parameters.Add("@zip_code", SqlDbType.VarChar).Value = model.zip_code ?? "";
                        command.Parameters.Add("@lane2", SqlDbType.VarChar).Value = model.lane2 ?? "";
                        command.Parameters.Add("@CountryID", SqlDbType.Int).Value = model.CountryID;
                        command.Parameters.Add("@StateId", SqlDbType.Int).Value = model.StateId;
                        command.Parameters.Add("@CityID", SqlDbType.Int).Value = model.CityID;
                        command.Parameters.Add("@AnyMedicalCondition", SqlDbType.VarChar).Value = model.AnyMedicalCondition ?? "";
                        command.Parameters.Add("@PhysicianName", SqlDbType.VarChar).Value = model.PhysicianName ?? "";
                        command.Parameters.Add("@PhysicianNumber", SqlDbType.VarChar).Value = model.PhysicianNumber ?? "";
                        command.Parameters.Add("@PhysicianAlternate_No", SqlDbType.VarChar).Value = model.PhysicianAlternate_No ?? "";
                        command.Parameters.Add("@emergContact_no", SqlDbType.VarChar).Value = model.emergContact_no ?? "";
                        command.Parameters.Add("@emergContact_Name", SqlDbType.VarChar).Value = model.emergContact_Name ?? "";
                        command.Parameters.Add("@emergContact_Relation", SqlDbType.VarChar).Value = model.emergContact_Relation ?? "";
                        command.Parameters.Add("@BloodGroup", SqlDbType.VarChar).Value = ClsCommon.EnsureString(model.BloodGroup.ToString());
                        command.Parameters.Add("@SpecialAbility", SqlDbType.VarChar).Value = ClsCommon.EnsureString(model.SpecialAbility.ToString());
                        command.Parameters.Add("@Course", SqlDbType.VarChar).Value = model.Course ?? "";
                        command.Parameters.Add("@University", SqlDbType.VarChar).Value = model.University ?? "";
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = model.Location ?? "";
                        command.Parameters.Add("@Year", SqlDbType.VarChar).Value = model.Year ?? "";
                        command.Parameters.Add("@AirlineName", SqlDbType.VarChar).Value = model.AirlineName ?? "";
                        command.Parameters.Add("@FlyerNumber", SqlDbType.VarChar).Value = model.FlyerNumber ?? "";
                        command.Parameters.Add("@PIO", SqlDbType.VarChar).Value = model.PIO ?? "";
                        command.Parameters.Add("@PIOName", SqlDbType.VarChar).Value = model.PIOName ?? "";
                        command.Parameters.Add("@OldPFNo", SqlDbType.VarChar).Value = model.OldPFNo ?? "";
                        command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = model.Remarks ?? "";
                        command.Parameters.Add("@Ammount", SqlDbType.VarChar).Value = model.Ammount;

                        command.Parameters.Add("@VoterId", SqlDbType.VarChar).Value = model.VoterId ?? "";
                        command.Parameters.Add("@PassportNo", SqlDbType.VarChar).Value = model.PassportNo ?? "";
                        command.Parameters.Add("@PassportName", SqlDbType.VarChar).Value = model.PassportName ?? "";
                        command.Parameters.Add("@DlRemarks", SqlDbType.VarChar).Value = model.DlRemarks ?? "";
                        command.Parameters.Add("@PassportPlaceOfissue", SqlDbType.VarChar).Value = model.PassportPlaceOfissue ?? "";
                        command.Parameters.Add("@PassportExpDate", SqlDbType.VarChar).Value = model.PassportExpDate ?? "";
                        command.Parameters.Add("@DlPlaceOfissue", SqlDbType.VarChar).Value = model.DlPlaceOfissue ?? "";
                        command.Parameters.Add("@DlExpDate", SqlDbType.VarChar).Value = model.DlExpDate ?? "";
                        command.Parameters.Add("@DinNo", SqlDbType.VarChar).Value = model.DinNo ?? "";
                        command.Parameters.Add("@DinName", SqlDbType.VarChar).Value = model.DinName ?? "";
                        command.Parameters.Add("@DlNo", SqlDbType.VarChar).Value = model.DlNo ?? "";
                        command.Parameters.Add("@DlName", SqlDbType.VarChar).Value = model.DlName ?? "";
                        command.Parameters.Add("@IssueDate", SqlDbType.VarChar).Value = model.IssueDate ?? "";
                        command.Parameters.Add("@AttachmentID", SqlDbType.Int).Value = model.AttachmentID;
                        command.Parameters.Add("@MealPreferenceID", SqlDbType.Int).Value = model.MealPreferenceID;
                        command.Parameters.Add("@SeatPreferencesID", SqlDbType.Int).Value = model.SeatPreferencesID;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during spu_SetEmployeeMasterupdateAll. The query was executed :", ex.ToString(), SQL, "EmployeeModal", "EmployeeModal", "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }
        public List<Employee.List> GetEmployeePendingApprovedList(GetResponse modal)
        {
            List<Employee.List> result = new List<Employee.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Doctype", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetHRVerifyPendingEmplist", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeePendingApprovedList. The query was executed :", ex.ToString(), "spu_GetHRVerifyPendingEmplist()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public Employee.EMP_Account GetEmployeeAccountdetailsUpdateDetails(GetResponse modal)
        {
            Employee.EMP_Account result = new Employee.EMP_Account();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Status", dbType: DbType.Int32, value: modal.Approve, direction: ParameterDirection.Input);
                    param.Add("@Id", dbType: DbType.Int32, value: modal.AdditionalID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMP_AccountDetailsNew", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.EMP_Account>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Employee.EMP_Account();
                        }


                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeAccountdetailsUpdateDetails. The query was executed :", ex.ToString(), "spu_GetEMP_AccountDetailsNew()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public Employee.EMPAddress GetEmployeeAddressCheckLock(GetResponse modal)
        {
            Employee.EMPAddress result = new Employee.EMPAddress();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMP_AccountCheckLock", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.EMPAddress>().FirstOrDefault();
                        //if (result == null)
                        //{
                        //    result = new Employee.EMPAddress();
                        //}


                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeAddressCheckLock. The query was executed :", ex.ToString(), "spu_GetEMP_AccountCheckLock()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }


        public Employee.PersonalDetailsnew GetEmployeePersonalDetailsNew(GetResponse modal)
        {
            Employee.PersonalDetailsnew result = new Employee.PersonalDetailsnew();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Status", dbType: DbType.Int32, value: modal.Approve, direction: ParameterDirection.Input);
                    param.Add("@Id", dbType: DbType.Int32, value: modal.AdditionalID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMP_PersonalDetailsNew", param: param, commandType: CommandType.StoredProcedure))
                    {
                        if (!reader.IsConsumed)
                        {
                            result.Personal = reader.Read<Employee.Personal>().FirstOrDefault();
                            if (result.Personal == null)
                            {
                                result.Personal = new Employee.Personal();
                            }


                            if (!reader.IsConsumed)
                            {
                                result.LocalAddress = reader.Read<Employee.EMPAddress>().FirstOrDefault();
                                if (result.LocalAddress == null)
                                {

                                    result.LocalAddress = new Employee.EMPAddress();

                                }
                            }
                            if (!reader.IsConsumed)
                            {
                                result.PermanentAddress = reader.Read<Employee.EMPAddress>().FirstOrDefault();
                                if (result.PermanentAddress == null)
                                {
                                    result.PermanentAddress = new Employee.EMPAddress();
                                }
                            }
                            if (!reader.IsConsumed)
                            {
                                result.Medical = reader.Read<Employee.Medical>().FirstOrDefault();
                                if (result.Medical == null)
                                {
                                    result.Medical = new Employee.Medical();
                                }
                            }

                            if (!reader.IsConsumed)
                            {
                                result.QualificationList = reader.Read<Employee.Qualification>().ToList();
                                if (result.QualificationList.Count == 0)
                                {
                                    Employee.Qualification obj = new Employee.Qualification();
                                    result.QualificationList.Add(obj);
                                }

                            }

                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeePersonalDetailsNew. The query was executed :", ex.ToString(), "spu_GetEMP_PersonalDetailsNew()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public Employee.GeneralInfo GetEmployeeGeneralInfoFlyer(GetResponse modal)
        {
            Employee.GeneralInfo result = new Employee.GeneralInfo();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Status", dbType: DbType.Int32, value: modal.Approve, direction: ParameterDirection.Input);
                    param.Add("@Id", dbType: DbType.Int32, value: modal.AdditionalID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMP_FlyerDetailsNew", param: param, commandType: CommandType.StoredProcedure))
                    {
                        if (!reader.IsConsumed)
                        {
                            result.AirlinePreferencesList = reader.Read<Employee.AirlinePreferences>().ToList();
                            if (result.AirlinePreferencesList.Count == 0)
                            {
                                Employee.AirlinePreferences obj = new Employee.AirlinePreferences();
                                result.AirlinePreferencesList.Add(obj);
                            }
                        }

                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeGeneralInfoFlyer. The query was executed :", ex.ToString(), "spu_GetEMP_FlyerDetailsNew()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }

        public Employee.GeneralInfo GetEmployeeGeneralInfoMealandSeat(GetResponse modal)
        {
            Employee.GeneralInfo result = new Employee.GeneralInfo();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Status", dbType: DbType.Int32, value: modal.Approve, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMP_SeatandMealList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        if (!reader.IsConsumed)
                        {
                            result = reader.Read<Employee.GeneralInfo>().FirstOrDefault();
                        }

                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeGeneralInfoMealandSeat. The query was executed :", ex.ToString(), "spu_GetEMP_SeatandMealList()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }

        public Employee.GeneralInfo GetEmployeeGeneralInfoMasterUpdateMealandSeat(GetResponse modal)
        {
            Employee.GeneralInfo result = new Employee.GeneralInfo();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Status", dbType: DbType.Int32, value: modal.Approve, direction: ParameterDirection.Input);
                    param.Add("@Id", dbType: DbType.Int32, value: modal.AdditionalID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMasterUpdateEmp_SeatandMealList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        if (!reader.IsConsumed)
                        {
                            result = reader.Read<Employee.GeneralInfo>().FirstOrDefault();
                        }

                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeGeneralInfoMasterUpdateMealandSeat. The query was executed :", ex.ToString(), "spu_GetMasterUpdateEmp_SeatandMealList()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public Employee.IDInfoNew GetEmployeeIDInfoNew(GetResponse modal)
        {
            Employee.IDInfoNew result = new Employee.IDInfoNew();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Status", dbType: DbType.Int32, value: modal.Approve, direction: ParameterDirection.Input);
                    param.Add("@Id", dbType: DbType.Int32, value: modal.AdditionalID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMP_IDDetailsNew", param: param, commandType: CommandType.StoredProcedure))
                    {
                        if (!reader.IsConsumed)
                        {
                            result = reader.Read<Employee.IDInfoNew>().FirstOrDefault();

                        }

                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeIDInfoNew. The query was executed :", ex.ToString(), "spu_GetEMP_IDDetailsNew()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public Employee.AttachmentUrl GetEmployeeAttachment(GetResponse modal)
        {
            Employee.AttachmentUrl result = new Employee.AttachmentUrl();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Id", dbType: DbType.Int32, value: modal.AdditionalID, direction: ParameterDirection.Input);
                    param.Add("@Status", dbType: DbType.Int32, value: modal.Approve, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMP_AttachementUpdateDetails", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.AttachmentUrl>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Employee.AttachmentUrl();
                        }


                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeAttachment. The query was executed :", ex.ToString(), "spu_GetEMP_AttachementUpdateDetails()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public List<Employee.List> GetEmployeeListFinalReport(GetResponse modal)
        {
            List<Employee.List> result = new List<Employee.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Approve", dbType: DbType.Int32, value: modal.Approve, direction: ParameterDirection.Input);
                    param.Add("@FYId ", dbType: DbType.Int32, value: modal.AdditionalID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmpListFullFinalReport", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeListFinalReport. The query was executed :", ex.ToString(), "spu_GetEmpListFullFinalReport()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }


        public List<Employee.Declaration> GetUserDeclartionList(int EmpID, int LoginID)
        {
            List<Employee.Declaration> result = new List<Employee.Declaration>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: EmpID, direction: ParameterDirection.Input);
                    param.Add("@UserId", dbType: DbType.Int32, value: LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDeclarationEmployeeListPending", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.Declaration>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeDeclartionList. The query was executed :", ex.ToString(), "spu_GetDeclarationEmployeeListPending()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }

        public List<Employee.Declaration> GetUserDeclartionApprovedList(int EmpID, int LoginID)
        {
            List<Employee.Declaration> result = new List<Employee.Declaration>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: EmpID, direction: ParameterDirection.Input);
                    param.Add("@UserId", dbType: DbType.Int32, value: LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDeclarationEmployeeListApproved", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.Declaration>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeDeclartionList. The query was executed :", ex.ToString(), "spu_GetDeclarationEmployeeListPending()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
                                         
        public Employee.RegistrationDetails GetMappingEmployeeRegistrationDetails(GetResponse modal)
        {
            Employee.RegistrationDetails result = new Employee.RegistrationDetails();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmployeeOnboardingMapped", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.RegistrationDetails>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new Employee.RegistrationDetails();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DepartmentList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DesignationList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.JobList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.LocationList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.SkillList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.BankList = reader.Read<DropDownList>().ToList();
                        }

                        if (!reader.IsConsumed)
                        {
                            result.ThematicareaList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.SalaryAccount = reader.Read<Employee.EMP_Account>().FirstOrDefault();
                            if (result.SalaryAccount == null)
                            {
                                result.SalaryAccount = new Employee.EMP_Account();
                                result.SalaryAccount.Doctype = "Salary";
                                modal.Doctype = "Salary";
                                Employee.EMPAddress obj = GetEmployeeAddressCheckLock(modal);
                                result.SalaryAccount.LockStatus = obj.LockStatus;
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ReimbursementAccount = reader.Read<Employee.EMP_Account>().FirstOrDefault();
                            if (result.ReimbursementAccount == null)
                            {
                                result.ReimbursementAccount = new Employee.EMP_Account();
                                result.ReimbursementAccount.Doctype = "Reimbursement";
                                modal.Doctype = "Reimbursement";
                                Employee.EMPAddress obj = GetEmployeeAddressCheckLock(modal);
                                result.ReimbursementAccount.LockStatus = obj.LockStatus;
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.EMPList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.RoleList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.UserDetails = reader.Read<UserMan.Add>().FirstOrDefault();
                            if (result.UserDetails == null)
                            {
                                result.UserDetails = new UserMan.Add();
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstSalaryStucture = reader.Read<Employee.SalaryStructure>().FirstOrDefault();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMappingEmployeeRegistrationDetails. The query was executed :", ex.ToString(), "spu_GetEmp()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }

        public List<Employee.OfficePolicy> GetOfficePoliciesListList(GetResponse modal)
        {
            List<Employee.OfficePolicy> result = new List<Employee.OfficePolicy>();
            try
            {
                using (IDbConnection dbContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    dbContext.Open();
                    var parameters = new DynamicParameters();
                    using (var reader = dbContext.QueryMultiple("spu_Get_OfficePoliciesList", param: parameters, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Employee.OfficePolicy>().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetOfficePoliciesList. The query was executed :", ex.ToString(), "spu_Get_OfficePoliciesList()", "OfficePolicyModel", "OfficePolicyModel", "");
            }

            return result;
        }
    }
}