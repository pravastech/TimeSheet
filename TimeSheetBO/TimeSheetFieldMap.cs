using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace TimeSheetBO
{
    public class TimeSheetFieldMap
    {

        private static readonly ReflectionPermission _reflectionPermission = new ReflectionPermission(ReflectionPermissionFlag.NoFlags);

        private string _attributeName;
        private string _fieldName;
        private Type _referenceType;
        private Type _enumType;
        private bool _hasdefaultValue;
        private bool _required;

        private FieldInfo _fieldInfo;
        public string AttributeName
        {
            get { return _attributeName; }
        }

        public string FieldName
        {
            get { return _fieldName; }
        }

        public bool IsReference
        {
            get { return _referenceType != null; }
        }

        public bool IsEnum
        {
            get { return _enumType != null; }
        }

        public Type EnumType
        {
            get { return _enumType; }
        }

        public FieldInfo FieldInfo
        {
            get { return _fieldInfo; }
        }

        public bool hasDefaultValue
        {
            get { return _hasdefaultValue; }
        }

        public bool IsRequired
        {
            get { return _required;  }
        }

        public TimeSheetFieldMap(Type MQObjectType, string attributeName, string fieldName, bool hasDefault, bool isRequired)
        {

            _attributeName = attributeName;
            _fieldName = fieldName;
            _hasdefaultValue = hasDefault;
            _required = isRequired;

            _reflectionPermission.Demand();
            _reflectionPermission.Assert();
            try
            {
                _fieldInfo = MQObjectType.GetField(attributeName, BindingFlags.NonPublic | BindingFlags.Instance);
                if (null == _fieldInfo)
                {
                    throw new ApplicationException(string.Format("Couldn't access FieldInfo for {0} in {1}.", _attributeName, MQObjectType.Name));
                }
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }

            if (_fieldInfo.FieldType.IsSubclassOf(typeof(TimeSheetBase)))
            {
                _referenceType = _fieldInfo.FieldType;               
            }
            else if (_fieldInfo.FieldType.IsSubclassOf(typeof(System.Enum)))
            {
                _enumType = _fieldInfo.FieldType;
            }
        }
    }
}
