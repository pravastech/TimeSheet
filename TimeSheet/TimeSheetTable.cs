using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

using TimeSheetBO;

namespace TimeSheet
{
    public class TimeSheetTable
    {
        private List<TimeSheetColumn> _columns = new List<TimeSheetColumn>();
        private string _tableCSSClass = "dyntable";
        private string _tableId = "tbl";
        private TimeSheetGrid _grid;
        private bool _encodeCalcFieldHTML = true;

        public bool encodeCalcFieldHTML
        {
            get { return _encodeCalcFieldHTML; }
            set { _encodeCalcFieldHTML = value; }
        }

        public TimeSheetGrid Grid
        {
            get { return _grid; }
            set { _grid = value; }
        }

        public string tableId
        {
            get { return _tableId; }
            set { _tableId = value; }
        }

        public string tableCSSClass
        {
            get { return _tableCSSClass; }
            set { _tableCSSClass = value; }
        }

        public List<TimeSheetColumn> Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        public string columnJS()
        {
            List<String> colJs = new List<string>();
            if (!string.IsNullOrEmpty(_grid.objectName))
            {
                TimeSheetBase baseObj = _grid.User.protoTimeSheetBase(_grid.objectName);
                if (null != baseObj)
                {
                    this.Columns.ForEach(col =>
                    {
                        var field = baseObj.GetDataLink().TimeSheetField(baseObj, col.fieldName);
                        if (null != field)
                        {
                            colJs.Add("{name:'" + col.fieldName.ToLower() + "', type:'" + field.FieldInfo.FieldType.ToString().Replace("System.", "") + "',required:" + field.IsRequired.ToString().ToLower() + "}");
                        }
                        else
                        {
                            colJs.Add("{name:'" + col.fieldName + "', type:'Calculated'}");
                        }
                    });
                }
                else
                {
                    colJs.Add("{name:'baseObj null', type='baseObj null', required:'baseObj null'}");
                }
            }
            return string.Join(",", colJs.ToArray());
        }

        public string hiddenVars
        {
            get
            {
                return @"<input type=""hidden"" id=""submitKey"" name=""submitKey"" />
                    <input type=""hidden"" id=""guidfield"" name=""guidfield"" />
                    <input type=""hidden"" id=""deleteKey"" name=""deleteKey"" />";
            }
        }

        public string ToHTML()
        {
            StringBuilder sbTblHtml = new StringBuilder();
            if (_grid != null)
            {
                if (_grid.Rows != null)
                {
                    //1. Header
                    sbTblHtml.AppendLine(this.Header);
                    //2. Rows
                    sbTblHtml.AppendLine(this.RowsHTML());
                    //3.Footer
                    sbTblHtml.AppendLine(this.Footer);
                    //4. Table Footer
                }
                else
                {
                    sbTblHtml.AppendLine("Grid Rows is null.");
                }
            }
            else
            {
                sbTblHtml.AppendLine("Grid is null.");
            }

            return sbTblHtml.ToString();
        }

        private string Footer
        {
            get
            {
                return "<tfoot></tfoot></table>";
            }
        }

        private string RowsHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");

            int Cnt = 0;
            this.Grid.Rows.ForEach(row =>
            {
                sb.Append("<tr>");
                this.Columns.ForEach(col =>
                {
                    if (col.isHeader)
                    {
                        sb.Append("<td>");
                        if (!col.isCalculated)
                        {
                            //Table Data Field
                            object fieldValue = row.GetDataLink().fieldValue(row, col.fieldName);
                            sb.Append(HttpUtility.HtmlEncode((fieldValue != null) ? fieldValue.ToString() : ""));
                        }
                        else
                        {
                            // Calculated Fields
                            if (col.formatFunc != null)
                            {
                                if (encodeCalcFieldHTML)
                                {
                                    sb.Append(HttpUtility.HtmlEncode(col.formatFunc.Invoke(row).ToString()));
                                }
                                else
                                {
                                    sb.Append(col.formatFunc.Invoke(row).ToString());
                                }
                            }
                        }
                        sb.Append("</td>");
                    }
                });

                /* Action Button Edit */
                sb.Append("<td>");
                if (_grid.allowEdit)
                {
                    sb.Append(@"<a href=""javascript:void(0)"" onclick=""GridUtil.editRow('" + Cnt + @"');"">Editing</a>");
                }
                if (_grid.allowEdit && _grid.allowDelete)
                {
                    sb.Append(" | ");
                }
                if (_grid.allowDelete)
                {
                    sb.Append(@"<a href=""javascript:void(0)"" onclick=""GridUtil.deleteRow('" + Cnt + @"');"">Delete</a>");
                }
                if (_grid.CustomButtons != null)
                {
                    if (_grid.allowEdit || _grid.allowDelete)
                    {
                        sb.Append(" | ");
                    }
                    String btn = _grid.CustomButtons.Invoke(row);
                    sb.Append(btn);
                }
                sb.Append("</td>");
                sb.Append("</tr>");
                Cnt++;
            });
            sb.Append("</tbody>");

            return sb.ToString();
        }

        private string Header
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(@"<table cellpadding=""0"" cellspacing=""0"" border=""0"" class=""" + this.tableCSSClass + @""" role=""grid"" id=""" + this.tableId + @""">");
                sb.Append(@"<thead><tr>");
                int colCount = 0;
                this.Columns.ForEach(col =>
                {
                    if (col.isHeader)
                    {
                        sb.Append(@"<th class=""head" + (colCount % 2) + @""">" + HttpUtility.HtmlEncode(col.headerName) + "</th>");
                        colCount++;
                    }
                });
                sb.Append("<th class=\"head" + (colCount % 2) + "\">Action</th>");
                sb.AppendLine(@"</tr></thead>");
                return sb.ToString();
            }
        }

    }

    public class TimeSheetColumn
    {
        public string fieldName;
        public string headerName;
        public bool isHeader;
        public bool isCalculated;
        public bool isSubmit;

        public TimeSheetColumn()
        {
            isHeader = true;
            isCalculated = false;
            isSubmit = true;
        }

        public Func<TimeSheetBase, object> formatFunc;
    }
}