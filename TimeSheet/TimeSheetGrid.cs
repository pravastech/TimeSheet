using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

using TimeSheetBO;

namespace TimeSheet
{
    public class TimeSheetGrid
    {
         private List<TimeSheetBase> _gridRows = new List<TimeSheetBase>();
        public string objectName;
        public CurrentUser User { get; set; }
        public bool allowAdd { get; set; }
        public bool allowEdit { get; set; }
        public bool allowDelete { get; set; }
        public Func<TimeSheetBase, String> CustomButtons { get; set; }
        public TimeSheetTable gridTable { get; set; }

        public TimeSheetGrid(CurrentUser aUser, String aObjName)
        {
            this.objectName = aObjName;
            this.User = aUser;
            this.allowAdd = true;
            this.allowDelete = false;
            this.allowEdit = true;
        }

        public List<TimeSheetBase> Rows
        {
            get { return _gridRows;  }
            set { _gridRows = value; }
        }

        public string gridJS()
        {
            List<String> sbJS = new List<String>();
            int cnt = 0;
            StringBuilder rowItem = new StringBuilder();
            this.Rows.ForEach( row =>
            {
                rowItem.Append("{rowid:'" + cnt + "'");
                var fieldMappings = row.GetDataLink().FieldMappings;
                var objType = row.GetType();
                foreach (TimeSheetFieldMap mapping in fieldMappings.Values)
                {
                    if (!(mapping.FieldName.Equals("lastChange", StringComparison.InvariantCultureIgnoreCase) ||
                       mapping.FieldName.Equals("lastChangeUser", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        var field = objType.GetProperty(mapping.FieldName);
                        var fieldValue = field.GetValue(row, null);
                        if (field.PropertyType.Name == "Boolean")
                        {
                            String fldvalue = JSUtil.EnquoteJS(((fieldValue != null) ? fieldValue.ToString().ToLower() : "false"));
                            rowItem.Append("," + mapping.FieldName.ToLower() + ":" + fldvalue);
                        }
                        else
                        {
                            rowItem.Append("," + mapping.FieldName.ToLower() + ":'" +JSUtil.EnquoteJS((fieldValue != null) ? fieldValue.ToString() : "") + "'");
                        }
                    }
                }
                rowItem.Append("}");
                sbJS.Add(rowItem.ToString());
                rowItem.Clear();
                cnt++;
            });

            return "[" + string.Join(",",sbJS.ToArray()) + "]";
        }
    }
}