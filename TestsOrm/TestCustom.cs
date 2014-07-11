using ORM_1_21_;
using ORM_1_21_.Attribute;

namespace TestsOrm
{
    [MapTableName("`testcustom`")]
    class TestCustom
    {
        [MapPrimaryKey("`Id`", Generator.Native)]
        public int Id { get; set; }



        [MapColumnName("`body`")]
        public MyClass Class { get; set; }
    }
}