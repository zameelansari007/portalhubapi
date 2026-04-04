using System;

namespace PortalHub.Domain.Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class TableMapAttribute : Attribute
    {
        public string TableName { get; }
        public string Schema { get; }

        public TableMapAttribute(string tableName, string schema = "dbo")
        {
            TableName = tableName;
            Schema = schema;
        }
    }
}
