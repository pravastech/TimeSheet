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
            TimeSheetTable TimeSheetTaskTable = new TimeSheetTable();
            TimeSheetTaskTable.tableId = "tblUserFields";
            TimeSheetTaskTable.tableCSSClass = "dyntable";
            TimeSheetTaskTable.Grid = result;
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "UserName", headerName = " User Name", isHeader = true });
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "ProjectName", headerName = "Project Name", isHeader = true });
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "TaskName", headerName = "Task Name", isHeader = true });
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "TaskDate", headerName = "Task Date", isHeader = true });
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "Percentage", headerName = "Percentage", isHeader = true });
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "Notes", headerName = "Notes", isHeader = true });
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "CodeSnippet", headerName = "CodeSnippet", isHeader = true });
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "Phone", headerName = "Phone", isHeader = true });
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "Role", headerName = "Role", isHeader = true });
            result.gridTable = TimeSheetTaskTable;
            return result;

        }
        
   
        public static TimeSheetGrid UserFieldsGrid(CurrentUser logUser)
        {
            TimeSheetGrid result = new TimeSheetGrid(logUser, "Users");
            TimeSheetTable TimeSheetTaskTable = new TimeSheetTable();
            TimeSheetTaskTable.tableId = "tblUserFields";
            TimeSheetTaskTable.tableCSSClass = "dyntable";
            TimeSheetTaskTable.Grid = result;
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "UserName", headerName = " User Name", isHeader = true });
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "ProjectName", headerName = "Project Name", isHeader = true });
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "TaskName", headerName = "Task Name", isHeader = true });
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "TaskDate", headerName = "Task Date", isHeader = true });
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "Percentage", headerName = "Percentage", isHeader = true });
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "Notes", headerName = "Notes", isHeader = true });
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "CodeSnippet", headerName = "CodeSnippet", isHeader = true });
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "Phone", headerName = "Phone", isHeader = true });
            TimeSheetTaskTable.Columns.Add(new TimeSheetColumn() { fieldName = "Role", headerName = "Role", isHeader = true });
            result.gridTable = TimeSheetTaskTable;
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