using System;
using ORM_1_21_;
using ORM_1_21_.Attribute;

namespace TestsOrmMsSql
{
    [MapTableName("[Table]")]
    public class Table : IActionDal<Table>
    {
        [MapPrimaryKey("[Id]", Generator.Assigned)]
        public Int32 Id { get; set; }

        [MapColumnName("[name_table]")]
        public string Name { get; set; }

        public void BeforeInsert(Table item)
        {

        }

        public void AfterInsert(Table item)
        {

        }

        public void BeforeUpdate(Table item)
        {

        }

        public void AfterUpdate(Table item)
        {

        }

        public void BeforeDelete(Table item)
        {

        }

        public void AfterDelete(Table item)
        {

        }
    }
}