using System;
using ORM_1_21_;
using ORM_1_21_.Attribute;

namespace TestsOrm
{
    [MapTableName("body")]
    public class Body : IErrorDal<Body>, IValidateDal<Body>, IActionDal<Body>
    {
        private bool _validate, _beforeInsert, _afterInsert;
        private bool _beforeUpdate, _afterUpdate;
        private bool _beforeDelete, _afterDelete;
        public bool IsInsert
        {
            get
            {
                return _validate && _beforeInsert && _afterInsert;
            }
        }
        public bool IsUpdate
        {
            get
            {
                return _beforeUpdate && _afterUpdate;
            }
        }
        public bool IsDelete
        {
            get
            {
                return _beforeDelete && _afterDelete;
            }
        }

        [MapPrimaryKey("id", Generator.Native)]
        public Int32 Id { get; set; }

        [MapColumnName("description")]
        public string Description { get; set; }

        public void ErrorDal(Body currentObject, string messageError)
        {
            throw new NotImplementedException();
        }

        public void Validate(Body type)
        {
            _validate = true;
        }

        public void BeforeInsert(Body item)
        {
            _beforeInsert = true;
        }

        public void AfterInsert(Body item)
        {
            _afterInsert = true;
        }

        public void BeforeUpdate(Body item)
        {
            _beforeUpdate = true;
        }

        public void AfterUpdate(Body item)
        {
            _afterUpdate = true;
        }

        public void BeforeDelete(Body item)
        {
            _beforeDelete = true;
        }

        public void AfterDelete(Body item)
        {
            _afterDelete = true;
        }
    }
}