using System;

namespace PortalHub.Domain.Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class KeyColumnAttribute : Attribute
    {
        public string ColumnName { get; }

        public KeyColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}
