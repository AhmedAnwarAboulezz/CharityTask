

using System.Collections.Generic;

namespace Common.StandardInfrastructure
{
    public static class Constants
    {
        //Controller
        public const string EmployeesController = "Employees/";

        //actions
        public const string GetPredicateWithOrder = "GetPredicateWithOrder";

        public const string ExcelExtensionAcceptOldVersion = ".xls";
        public const string ExcelExtensionAcceptNewVersion = ".xlsx";
        public const string LogFileFolderName = "LogFile";
        public const string EncryptDecryptKey = "dNNPiSXzbLgFFmqLJ9GBnyK5fgBjfFNF";
        public static List<string> PropertiesList = new List<string>() { "OrganizationId", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
        //NotificationsUrl -- MyRequest
        public const string RequesterMyPermissionUrl = "/main/my-request/permission-tabs/permission-tabs/permission-request-details/";
        public const string RequesterMyLeaveUrl = "/main/my-request/leave-tabs/leave-tabs/leave-request-details/";
        public const string RequesterMyLeaveReturnUrl = "/main/my-request/return-leave-tabs/return-leave-tabs/return-leave-details/";
        public const string RequesterMyOverTimeUrl = "/main/my-request/overtime-tabs/overtime-tabs/overtime-request-details/";
        public const string RequesterMyFullDayPermissionUrl = "/main/my-request/full-day-permission-tabs/full-day-permission-tabs/full-day-request-details/";

        //NotificationsUrl -- Request 
        public const string ApproverPermissionUrl = "/main/request/permission-tabs/permission-tabs/approve-permission-request/";
        public const string ApproverLeaveUrl = "/main/request/leave-tabs/leave-tabs/approve-leave-request/";
        public const string ApproverLeaveReturnUrl = "/main/request/return-leave-tabs/return-leave-tabs/approve-return-leave-request/";
        public const string ApproverOverTimeUrl = "/main/request/overtime-tabs/overtime-tabs/approve-overtime-request/";
        public const string ApproverFullDayPermissionUrl = "/main/request/full-day-permission-tabs/full-day-permission-tabs/approve-full-day-permission-request/";

    }
}
