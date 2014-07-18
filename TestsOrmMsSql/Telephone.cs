using System;
using ORM_1_21_;
using ORM_1_21_.Attribute;

namespace TestsOrmMsSql
{
    [MapTableJoin("inner")]
    [MapTableName("[Telephone]")]
    public class Telephone : Body
    {

        [MapPrimaryKey("[id_tel]", Generator.Native)]
        public virtual Decimal IdTel { get; set; }
        [MapColumnName("[name_telephone]")]
        public virtual string NameTelephone { get; set; }
        [MapForeignKey]
        [MapColumnName("[id_body]")]
        public virtual Decimal IdBody { get; set; }
        [MapColumnName("[datet]")]
        public DateTime Datet { get; set; }
    }
}