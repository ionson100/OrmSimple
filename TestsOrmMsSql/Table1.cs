using System;
using ORM_1_21_;
using ORM_1_21_.Attribute;

namespace TestsOrmMsSql
{
    [MapTableJoin("inner")]
    [MapTableName("[Table1]")]
    public class Table1 : Telephone
    {

        [MapPrimaryKey("[Id]", Generator.Native)]
        public Int32 IdT { get; set; }

        [MapForeignKey]
        [MapColumnName("[Id_Body]")]
        public decimal IdBodya { get; set; }

        [MapColumnName("[name_table]")]
        public string Namea { get; set; }
    }
}