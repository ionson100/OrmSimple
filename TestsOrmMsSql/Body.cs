using System;
using ORM_1_21_;
using ORM_1_21_.Attribute;

namespace TestsOrmMsSql
{
    [MapTableName("[Body]", "[body].[id] > 0 ")]
    public class Body
    {
        [MapBaseKey]
        [MapPrimaryKey("[id]", Generator.Native)]
        public virtual Decimal Id { get; set; }

        [MapColumnName("[name_body]")]
        public virtual string Name { get; set; }

        [MapColumnName("[description]")]
        public virtual string Description { get; set; }
    }
}