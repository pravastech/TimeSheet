using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TimeSheetBO;

namespace TimeSheet
{
    public class TimeSheetGridUtility
    {
        public static TimeSheetGrid TimeSheetTaskGrid(CurrentUser logUser)
        {
            TimeSheetGrid result = new TimeSheetGrid(logUser, "Users");
            TimeSheetTable TimeSheettaskTable = new TimeSheetTable();
            TimeSheettaskTable.tableId = "tblTimeSheetTask";
            TimeSheettaskTable.tableCSSClass = "dyntable";
            TimeSheettaskTable.Grid = result;
            TimeSheettaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "UserName", headerName = " User Name", isHeader = true });
            TimeSheettaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "ProjectName", headerName = "Project Name", isHeader = true });
            TimeSheettaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "TaskName", headerName = "Task Name", isHeader = true });
            TimeSheettaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "TaskDate", headerName = "Task Date", isHeader = true });
            TimeSheettaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "Percentage", headerName = "Percentage", isHeader = true });
            TimeSheettaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "Notes", headerName = "Notes", isHeader = true });
            TimeSheettaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "CodeSnippet", headerName = "CodeSnippet", isHeader = true });
            TimeSheettaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "Phone", headerName = "Phone", isHeader = true });
            TimeSheettaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "Role", headerName = "Role", isHeader = true });
            result.gridTable = TimeSheettaskTable;
            return result;

        }
		public static TimeSheetGrid taskGrid(CurrentUser logUser)
        {
            TimeSheetGrid result = new TimeSheetGrid(logUser, "Task");
            TimeSheetTable taskTable = new TimeSheetTable();
            taskTable.tableId = "tbltask";
            taskTable.tableCSSClass = "dyntable";
            taskTable.Grid = result;
            taskTable.Columns.Add(new TimeSheetColumn() { fieldName = "ProjectName", headerName = "Project Name", isHeader = true });
            taskTable.Columns.Add(new TimeSheetColumn() { fieldName = "TaskName", headerName = "Task Name", isHeader = true });
            taskTable.Columns.Add(new TimeSheetColumn() { fieldName = "BeginDate", headerName = "Begin Date", isHeader = true });
            taskTable.Columns.Add(new TimeSheetColumn() { fieldName = "EndDate", headerName = "End Date", isHeader = true });
            taskTable.Columns.Add(new TimeSheetColumn() { fieldName = "DevUserName", headerName = "Dev UserName", isHeader = true });
            taskTable.Columns.Add(new TimeSheetColumn() { fieldName = "QaUserName", headerName = "Qa UserName", isHeader = true });
            taskTable.Columns.Add(new TimeSheetColumn() { fieldName = "ApprovedUserName", headerName = "Approved UserName", isHeader = true });
            result.gridTable = taskTable;
            return result;
        }
        
   
        public static TimeSheetGrid UserFieldsGrid(CurrentUser logUser)
        {
            TimeSheetGrid result = new TimeSheetGrid(logUser, "Users");
            TimeSheetTable UserFieldsTable = new TimeSheetTable();
            UserFieldsTable.tableId = "tblUserFields";
            UserFieldsTable.tableCSSClass = "dyntable";
            UserFieldsTable.Grid = result;
            UserFieldsTable.Columns.Add(new TimeSheetColumn() { fieldName = "UserName", headerName = " User Name", isHeader = true });
            UserFieldsTable.Columns.Add(new TimeSheetColumn() { fieldName = "FullName", headerName = "Full Name", isHeader = true });
            UserFieldsTable.Columns.Add(new TimeSheetColumn() { fieldName = "Email Address", headerName = "Email Address", isHeader = true });
            UserFieldsTable.Columns.Add(new TimeSheetColumn() { fieldName = "Address1", headerName = "Address1", isHeader = true });
            UserFieldsTable.Columns.Add(new TimeSheetColumn() { fieldName = "Address2", headerName = "Address2", isHeader = true });
            UserFieldsTable.Columns.Add(new TimeSheetColumn() { fieldName = "City", headerName = "City", isHeader = true });
            UserFieldsTable.Columns.Add(new TimeSheetColumn() { fieldName = "State", headerName = "State",isHeader = true });
            UserFieldsTable.Columns.Add(new TimeSheetColumn() { fieldName = "Role", headerName = "Role", isHeader = true });
            result.gridTable = UserFieldsTable;
            return result;

        }

        //public static TimeSheetGrid TimeSheetTaskGrid(CurrentUser logUser)
        //{
        //    TimeSheetGrid result = new TimeSheetGrid(logUser, "Stores");
        //    TimeSheetTable storeTable = new TimeSheetTable();
        //    storeTable.tableId = "tblStores";
        //    storeTable.tableCSSClass = "dyntable";
        //    storeTable.Grid = result;
        //    storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "storeid", headerName = "Store ID", isHeader = true });
        //    storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "accountco", headerName = "Account Co", isHeader = true });
        //    storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "dba", headerName = "DBA", isHeader = true });
        //    storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "address1", headerName = "Address 1", isHeader = true });
        //    storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "address2", headerName = "Address 2", isHeader = true });
        //    storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "city", headerName = "DBA", isHeader = false });
        //    storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "state", headerName = "Address 1", isHeader = false });
        //    storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "zip", headerName = "Address 2", isHeader = false });
        //    storeTable.Columns.Add(new TimeSheetColumn()
        //    {
        //        fieldName = "citystatezip",
        //        headerName = "City State Zip",
        //        isHeader = true,
        //        isCalculated = true,
        //        formatFunc = new Func<TimeSheetBase, object>((obj) =>
        //        {
        //            TimeSheetBO.Stores store = (TimeSheetBO.Stores)obj;
        //            return (store.city ?? "") + " " + (store.state ?? "") + " " + (store.zip ?? "");
        //        })
        //    });
        //    storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "active", headerName = "Active ?", isHeader = true });
        //    result.gridTable = storeTable;

        //    return result;
        //}

    }
}