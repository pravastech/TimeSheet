using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace TimeSheetObjects
{
    public partial class _Default : System.Web.UI.Page
    {
        private string ConString
        {
            get
            {
                return ConfigurationManager.AppSettings["ConnectionString"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string selTable = cmbTable.Text; 
            string tsql = "Select name from sysobjects where xtype in ('V','U') order by name";
            SqlConnection dbCon = new SqlConnection(this.ConString);
            dbCon.Open();
            SqlCommand dbCmd = new SqlCommand(tsql, dbCon);
            SqlDataReader dbRead = dbCmd.ExecuteReader();
            cmbTable.DataSource = dbRead;
            cmbTable.DataTextField = "name";
            cmbTable.DataValueField = "name";
            cmbTable.DataBind();
            if (!string.IsNullOrEmpty(selTable))
            {
                cmbTable.Text = selTable;
            }
        }

        protected void cmdMakeObject_Click(object sender, EventArgs e)
        {
            string tsql = @"SELECT COLUMN_NAME,DATA_TYPE,isnull(CHARACTER_MAXIMUM_LENGTH,0) AS 'LENGTH',
isnull(Prec,0) as precision,isnull(Scale,0) as scale,case when ISNULLABLE = 0 then cast(0 as bit) else cast(1 as bit) end as IsNullable, 
ISNULL((SELECT max('Y') FROM SYSFOREIGNKEYS WHERE FKEYID = syscolumns.ID AND FKEY=syscolumns.COLID),'N') as 'IsForeignKey',isnull(syscomments.text,'') as defaulttext,
Ordinal_Position, cast(isnull(isRowGuidCol,0) as bit) as IsRowGuidCol
FROM SYSCOLUMNS   LEFT  JOIN syscomments ON syscomments.id = syscolumns.cdefault inner join sysobjects on sysobjects.id = syscolumns.id and sysobjects.type in ('V','U')
inner join
(SELECT Table_Name,COLUMN_NAME, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, Ordinal_position, 0 AS IsIdentity,0 as isrowguidcol
FROM INFORMATION_SCHEMA.COLUMNS) AS A  
on  a.column_name = syscolumns.name and a.Table_Name = sysobjects.name
where sysobjects.name = @tablename
Order By a.Ordinal_Position;
"; 
            SqlConnection dbCon = new SqlConnection(this.ConString);
            dbCon.Open();
            SqlCommand dbCmd = new SqlCommand(tsql, dbCon);
            dbCmd.Parameters.Add(new SqlParameter("@tablename", cmbTable.Text));
            SqlDataReader dbReader = dbCmd.ExecuteReader();
            
            dbfields.DataSource = dbReader;
            dbfields.DataBind();

            dbReader.Close();
            dbReader = dbCmd.ExecuteReader();
            var privateFields = new StringBuilder();
            var publicFields = new StringBuilder();
            var addMapFields = new StringBuilder();

            string colname;
            string datatype;
            string defaulttext;
            int ordinal;
            bool isNullable;

            while (dbReader.Read())
            {
                colname = (string)dbReader.GetValue(0);
                datatype = (string)dbReader.GetValue(1);
                isNullable = (! (bool)dbReader.GetValue(5));
                defaulttext = (string)dbReader.GetValue(7);
                ordinal = (int)dbReader.GetValue(8);
                string csharp_datatype = dbFieldMap.FieldType(datatype);
                privateFields.AppendLine("private " + csharp_datatype + " _" + colname + ";");
 
                string publicField = (@"        public [paramFieldType] [paramFieldName]
        {
            get { return _[paramFieldName]; }
            set { [paramSetFieldName](ref _[paramFieldName], value); }
        }");
                publicField = publicField.Replace("[paramFieldType]", csharp_datatype);
                publicField = publicField.Replace("[paramFieldName]", colname);
                publicField = publicField.Replace("[paramSetFieldName]", "Set"+csharp_datatype);
                
                publicFields.AppendLine(publicField);
                addMapFields.AppendLine("AddFieldMapping(\"_" + colname + "\",\"" + colname + "\",false," + isNullable.ToString().ToLower() + ");"); 
            }

            string objtemplate = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;

using TimeSheetUtil;

namespace TimeSheetBO
{
    public class [paramObjName] : TimeSheetBase
    {
        private static Hashtable _cache = null;
        private CurrentUser User;
        [paramPrivateFields]

        public [paramObjName]() : base()
        {

        }

        public [paramObjName](CurrentUser User): base()
        {
            this.User = User;
        }

        public static [paramObjName]DataLink [paramObjName]DataLink
        {
            get
            {
                return [paramObjName]DataLink.Instance;
            }
        }

        public override TimeSheetDataLink GetDataLink()
        {
            return [paramObjName]DataLink;
        }

        public static Hashtable GetCache1()
        {
            return _cache;
        }

        [paramPublicFields]
}
    public sealed class [paramObjName]DataLink : TimeSheetDataLink
    {
        private static volatile [paramObjName]DataLink _instance = new [paramObjName]DataLink();

        private [paramObjName]DataLink() : base(typeof([paramObjName]), ""[paramObjName]"")
        {
            [paramAddMappingFields]
        }

        public static [paramObjName]DataLink Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof([paramObjName]DataLink))
                    {
                        if (_instance == null)
                        {
                            _instance = new [paramObjName]DataLink();
                        }
                    }
                }

                return _instance;
            }
        }

        public override TimeSheetBase Init()
        {
            return new [paramObjName]();
        }
    }

}";

            objtemplate = objtemplate.Replace("[paramObjName]", cmbTable.Text);
            objtemplate = objtemplate.Replace("[paramPrivateFields]", privateFields.ToString());
            objtemplate = objtemplate.Replace("[paramPublicFields]", publicFields.ToString());
            objtemplate = objtemplate.Replace("[paramAddMappingFields]", addMapFields.ToString());

            txtResults.Text = objtemplate;
        }


    }

    public class dbFieldMap
    {
        public static string FieldType(string typename)
        {
            string value = "";
            switch (typename.ToLower())
            {
                case ("nchar"):
                case ("char"):
                case ("nvarchar"):
                case ("varchar"):
                case ("text"):
                case ("ntext"):
                    value = "String";
                    break;
                case "tinyint":
                    value = "Byte";
                    break;
                case "smallint":
                    value = "Int16";
                    break;
                case "int":
                    value = "Int32";
                    break;
                case "bit":
                    value = "Boolean";
                    break;
                case "smalldatetime":
                case "datetime":
                    value = "DateTime";
                    break;
                case "money":
                case "decimal":
                    value = "Decimal";
                    break;
                case "bigint":
                    value = "Int64";
                    break;
                case "uniqueidentifier":
                    value = "Guid";
                    break;
                default:
                    value = "";
                    break;
            }
            return value;
        }

    }
}
