using System;
using ORM_1_21_.Attribute;

namespace TestsOrm
{
    [MapCustomType]
    [Serializable]
    class MyClass
    {
        public string Name { get; set; }
    }
}