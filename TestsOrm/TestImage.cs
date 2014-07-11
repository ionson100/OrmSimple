using System.Drawing;
using ORM_1_21_;
using ORM_1_21_.Attribute;

namespace TestsOrm
{
    [MapTableName("`testimage`")]
    class TestImage
    {
        [MapPrimaryKey("`id`", Generator.Native)]
        public int Id { get; set; }



        [MapColumnName("`image`")]
        public Image Image { get; set; }
    }
}