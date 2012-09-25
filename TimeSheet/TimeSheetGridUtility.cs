using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TimeSheetBO;

namespace TimeSheet
{
    public class TimeSheetGridUtility
    {
        public static TimeSheetGrid UserRolesGrid(CurrentUser logUser)
        {
            TimeSheetGrid result = new TimeSheetGrid(logUser, "UserRoles");
            TimeSheetTable UserRoleTable = new TimeSheetTable();
            UserRoleTable.tableId = "tblUserRoles";
            UserRoleTable.tableCSSClass = "dyntable";
            UserRoleTable.Grid = result;
            UserRoleTable.Columns.Add(new TimeSheetColumn() { fieldName = "rolename", headerName = " User Name", isHeader = true });
            UserRoleTable.Columns.Add(new TimeSheetColumn() { fieldName = "roledescription", headerName = "Role Description", isHeader = true });
            UserRoleTable.Columns.Add(new TimeSheetColumn() { fieldName = "allowview", headerName = "Allow View", isHeader = true });
            UserRoleTable.Columns.Add(new TimeSheetColumn() { fieldName = "allowadd", headerName = "Allow Add", isHeader = true });
            UserRoleTable.Columns.Add(new TimeSheetColumn() { fieldName = "allowedit", headerName = "Allow Edit", isHeader = true });
            UserRoleTable.Columns.Add(new TimeSheetColumn() { fieldName = "allowdelete", headerName = "Allow Delete", isHeader = true });
            result.gridTable = UserRoleTable;
            return result;

        }

        public static TimeSheetGrid storesGrid(CurrentUser logUser)
        {
            TimeSheetGrid result = new TimeSheetGrid(logUser, "Stores");
            TimeSheetTable storeTable = new TimeSheetTable();
            storeTable.tableId = "tblStores";
            storeTable.tableCSSClass = "dyntable";
            storeTable.Grid = result;
            storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "storeid", headerName = "Store ID", isHeader = true });
            storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "accountco", headerName = "Account Co", isHeader = true });
            storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "dba", headerName = "DBA", isHeader = true });
            storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "address1", headerName = "Address 1", isHeader = true });
            storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "address2", headerName = "Address 2", isHeader = true });
            storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "city", headerName = "DBA", isHeader = false });
            storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "state", headerName = "Address 1", isHeader = false });
            storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "zip", headerName = "Address 2", isHeader = false });
            storeTable.Columns.Add(new TimeSheetColumn()
            {
                fieldName = "citystatezip",
                headerName = "City State Zip",
                isHeader = true,
                isCalculated = true,
                formatFunc = new Func<TimeSheetBase, object>((obj) =>
                {
                    TimeSheetBO.Stores store = (TimeSheetBO.Stores)obj;
                    return (store.city ?? "") + " " + (store.state ?? "") + " " + (store.zip ?? "");
                })
            });
            storeTable.Columns.Add(new TimeSheetColumn() { fieldName = "active", headerName = "Active ?", isHeader = true });
            result.gridTable = storeTable;

            return result;
        }

    }
}