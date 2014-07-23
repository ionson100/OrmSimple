using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ORM_1_21_;

namespace TestsOrm
{

    partial class NUnitTestFixture
    {
        [Test]
        public void TestImage()
        {
            var ses = Configure.GetSessionCore();
            var f = new TestImage { Image = Image.FromFile("E:/1.jpg") };
            ses.Save(f);
            var rr = ses.Get<TestImage>(f.Id).Image != null;
            Console.WriteLine(rr);
            var list = ses.Querion<TestImage>().ToList();
            foreach (var testImage in list)
            {
                ses.Delete(testImage);
            }
            var count = ses.Querion<TestImage>().Count();
            ses.Dispose();
            Assert.True(rr && count == 0);
        }

        [Test]
        public void TestSingleOrDefault()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestSingleOrDefault");
            Clear(ses);
            var body = new Body();
            ses.Save(body);
            var body2 = new Body { Description = "12" };
            ses.Save(body2);
            var body12 = new Body();
            ses.Save(body12);
            Body s = null;

            try
            {
                s = ses.Querion<Body>().Where(a => a.Description == null).SingleOrDefault();
            }
            catch
            {
            }
            var dd = ses.Querion<Body>().Where(a => a.Description == "12").SingleOrDefault();
            var dd1 = ses.Querion<Body>().SingleOrDefault(a => a.Description == "12");
            var dd3 = ses.Querion<Body>().SingleOrDefault(a => a.Description == "r12");


            Clear(ses);
            PrintSecondGround(ses, "TestSingleOrDefault");
            ses.Dispose();
            Assert.True(s == null && dd != null && dd1 != null && dd3 == null);
        }
        [Test]
        public void TestSingle()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestSingle");
            Clear(ses);
            var body = new Body();
            ses.Save(body);
            var body2 = new Body { Description = "12" };
            ses.Save(body2);
            var body12 = new Body();
            ses.Save(body12);
            Body s = null;
            try
            {
                s = ses.Querion<Body>().Where(a => a.Description == null).Single();

            }
            catch
            {


            }
            var dd = ses.Querion<Body>().Where(a => a.Description == "12").Single();
            var dd1 = ses.Querion<Body>().Single(a => a.Description == "12");
            Body dd3 = null;
            try
            {
                dd3 = ses.Querion<Body>().Single(a => a.Description == "r12");
            }
            catch (Exception)
            {


            }
            Clear(ses);
            PrintSecondGround(ses, "TestSingle");
            ses.Dispose();
            Assert.True(s == null && dd != null && dd1 != null && dd3 == null);
        }

        [Test]
        public void TestElementAt()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestElementAt");
            Clear(ses);
            var body = new Body();
            ses.Save(body);
            var body2 = new Body { Description = "12" };
            ses.Save(body2);
            var body12 = new Body();
            ses.Save(body12);
            Body dd2 = null;
            var dd1 = ses.Querion<Body>().Where(a => a.Description == null).ElementAt(1);
            try
            {
                dd2 = ses.Querion<Body>().Where(a => a.Description == null).ElementAt(3);
            }
            catch
            {


            }

            var dd3 = ses.Querion<Body>().Where(a => a.Description == "12").ElementAt(0);
            Clear(ses);
            PrintSecondGround(ses, "TestElementAt");
            ses.Dispose();
            Assert.True(dd1 != null && dd2 == null && dd3 != null);
        }

        [Test]
        public void TestElementAtOrDefault()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestElementAtOrDefault");
            Clear(ses);
            var body = new Body();
            ses.Save(body);
            var body2 = new Body { Description = "12" };
            ses.Save(body2);
            var body12 = new Body();
            ses.Save(body12);
            var dd1 = ses.Querion<Body>().Where(a => a.Description == null).ElementAtOrDefault(1);
            var dd2 = ses.Querion<Body>().Where(a => a.Description == null).ElementAtOrDefault(3);
            var dd3 = ses.Querion<Body>().Where(a => a.Description == "12").ElementAtOrDefault(0);
            Clear(ses);
            PrintSecondGround(ses, "TestElementAtOrDefault");
            ses.Dispose();
            Assert.True(dd1 != null && dd2 == null && dd3 != null);
        }



        [Test]
        public void TestSimpleSelect()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestSimpleSelect");
            Clear(ses);
            var body = new Body { Description = "12" };
            ses.Save(body);
            var body2 = new Body { Description = "12" };
            ses.Save(body2);
            var body12 = new Body { Description = "13" };
            ses.Save(body12);

            var body121 = new Body();
            ses.Save(body121);

            var dd1 = ses.Querion<Body>().Select(n => n.Description).ToList();
            var dd2 = ses.Querion<Body>().Select(n => new { n.Description }).ToList();


            Clear(ses);
            PrintSecondGround(ses, "TestSimpleSelect");
            ses.Dispose();
            Assert.True(dd1.Count == 4 && dd2.Count == 4);
        }

        [Test]
        public void TestGroupBy()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestGroupBy");
            Clear(ses);
            var body = new Body { Description = "12" };
            ses.Save(body);
            var body2 = new Body { Description = "12" };
            ses.Save(body2);
            var body12 = new Body { Description = "13" };
            ses.Save(body12);

            var body121 = new Body();
            ses.Save(body121);

            var dd33 = ses.Querion<Body>().Where(a => a.Description != null).Reverse().First();
            var dd333 = ses.Querion<Body>().GroupBy(a => a.Description).Count();

            var dd1 = ses.Querion<Body>().Where(a => a.Description == null).GroupBy(a => a.Description).ToList();
            var dd2 = ses.Querion<Body>().GroupBy(a => a.Description).ToList();
            // var dd3 = ses.Querion<Body>().GroupByCore(a=>a.Id).ToList();
            Clear(ses);
            PrintSecondGround(ses, "TestGroupBy");
            ses.Dispose();
            Assert.True(dd1.Count == 1 && dd2.Count == 3 && dd33.Description == "13");
        }

        [Test]
        public void TestGetSetProperties()
        {
            var body = new Body();
            var res1 = Utils.GetValue(body, "Description");
            Utils.SetValue(body, "Description", "123");
            Assert.True(res1 == null && body.Description == "123");
        }

        [Test]
        public void TestRevers()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestRevers");
            Clear(ses);
            var body = new Body { Description = "12" };
            ses.Save(body);
            var body2 = new Body { Description = "12" };
            ses.Save(body2);
            var body12 = new Body { Description = "13" };
            ses.Save(body12);


            var dd33 = ses.Querion<Body>().Where(a => a.Description != null).Reverse().First();

            var dd333 = ses.Querion<Body>().GroupBy(a => a.Description).Count();

            var dd1 = ses.Querion<Body>().Where(a => a.Description != null).Reverse().ToList();
            var dd2 = ses.Querion<Body>().GroupBy(a => a.Description).ToList();

            Clear(ses);
            PrintSecondGround(ses, "TestRevers");
            ses.Dispose();
            Assert.True(dd1.Any() && dd2.Count == 2 && dd1.First().Description == "13");
        }

        [Test]
        public void TestPosledAnonymus()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestPosledAnonymus");
            Clear(ses);
            var body = new Body { Description = "12" };
            ses.Save(body);
            var body2 = new Body { Description = "12" };
            ses.Save(body2);
            var body12 = new Body { Description = "13" };
            ses.Save(body12);
            var body121 = new Body();
            ses.Save(body121);

            object dd2 = null;
            var dd1 = ses.Querion<Body>().Where(a => a.Description != null).Select(a => new { a.Description }).First();
            try
            {
                dd2 = ses.Querion<Body>().Where(a => a.Description == "1").Select(a => new { a.Description }).First();
            }
            catch
            {
            }
            var dd3 = ses.Querion<Body>().Where(a => a.Description == null).Select(a => new { a.Description }).First();
            var dd4 = ses.Querion<Body>().Where(a => a.Description != null).Select(a => new { a.Description }).FirstOrDefault();
            var dd5 = ses.Querion<Body>().Where(a => a.Description == "1").Select(a => new { a.Description }).FirstOrDefault();
            var dd6 = ses.Querion<Body>().Where(a => a.Description == null).Select(a => new { a.Description }).FirstOrDefault();
            var dd7 = ses.Querion<Body>().Where(a => a.Description != null).Select(a => new { a.Description }).Last();
            ses.WriteLogFile("7");
            object dd8 = null;
            try
            {
                dd8 = ses.Querion<Body>().Where(a => a.Description == "1").Select(a => new { a.Description }).Last();
            }
            catch (Exception)
            {
            }
            var dd9 = ses.Querion<Body>().Where(a => a.Description == null).Select(a => new { a.Description }).Last();
            var dd10 = ses.Querion<Body>().Where(a => a.Description != null).Select(a => new { a.Description }).LastOrDefault();
            var dd11 = ses.Querion<Body>().Where(a => a.Description == "1").Select(a => new { a.Description }).LastOrDefault();
            var dd12 = ses.Querion<Body>().Where(a => a.Description == null).Select(a => new { a.Description }).LastOrDefault();
            ses.WriteLogFile("12");
            Clear(ses);
            PrintSecondGround(ses, "TestPosledAnonymus");
            ses.Dispose();
            Assert.True(dd1 != null && dd2 == null && dd3 != null && dd4 != null && dd5 == null && dd6 != null && dd7 != null && dd8 == null && dd9 != null && dd10 != null && dd11 == null && dd12 != null);
        }

        [Test]
        public void TestAll()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestAll");
            Clear(ses);
            var body = new Body { Description = "12" };
            ses.Save(body);
            var body2 = new Body { Description = "12" };
            ses.Save(body2);
            var body12 = new Body { Description = "13" };
            ses.Save(body12);

            var body121 = new Body();
            ses.Save(body121);

            var aa = ses.Querion<Body>().All(a => a.Description != null);
            var bb = ses.Querion<Body>().Where(c => c.Description == null).All(a => a.Description == "12");


            Clear(ses);
            PrintSecondGround(ses, "TestAll");
            ses.Dispose();
            Assert.True(aa && !bb);
        }

        [Test]
        public void TestLIKE()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestLIKE");
            Clear(ses);
            var body = new Body { Description = "12" };
            ses.Save(body);
            var body2 = new Body { Description = "12" };
            ses.Save(body2);
            var body12 = new Body { Description = "131" };
            ses.Save(body12);
            var body121 = new Body();
            ses.Save(body121);
            var aa = ses.Querion<Body>().Count(a => a.Description.StartsWith("1"));
            var aa1 = ses.Querion<Body>().Count(a => a.Description.EndsWith("2"));
            var aa2 = ses.Querion<Body>().Count(a => a.Description.Contains("3"));
            Clear(ses);
            PrintSecondGround(ses, "TestLIKE");
            ses.Dispose();
            Assert.True(aa == 3 && aa1 == 2 && aa2 == 1);
        }
        [Test]
        public void TestContains()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestContains");
            Clear(ses);
            var body = new Body { Description = "12" };
            ses.Save(body);
            var body2 = new Body { Description = "12" };
            ses.Save(body2);
            var body12 = new Body { Description = "131" };
            ses.Save(body12);
            var body121 = new Body();
            ses.Save(body121);
            var aa = ses.Querion<Body>().Contains(body);
            Clear(ses);
            PrintSecondGround(ses, "TestContains");
            ses.Dispose();
            Assert.True(aa);
        }


        [Test]
        public void TestFeeSql()//! не кушируются не в первом не во втором
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestFeeSql");
            Clear(ses);

            ses.Save(new Body { Description = "12" });

            ses.Save(new Body { Description = "12" });

            ses.Save(new Body { Description = "131" });

            ses.Save(new Body());

            var aa5 = ses.FreeSql<NewBody>("select id as Id,description as Description from body ");

            var aa = ses.FreeSql<Body>("select id as Id,description as Description from body ");

            var aa1 = ses.FreeSql<int>("select id  from body ");

            var aa2 = ses.FreeSql<object>("select id ,description from body ");

            var aa3 = ses.FreeSql<object>("select id ,description from body ");

            var aa4 = ses.FreeSqlParam<Body>("select id as Id,description as Description from body where description = @1", new Parameter("@1", 12));

            var aa6 = GetTestEnumerable(new {Id = 3, Description = ""}, ses);

            Clear(ses);
            PrintSecondGround(ses, "TestFeeSql");
            ses.Dispose();
            Assert.True(aa.Count() == 4 && aa1.Count() == 4 && aa2.Count() == 4 && aa3.Count() == 4 && aa4.Count() == 2 && aa5.Count() == 4&&aa6.Count()==4);
        }

        class NewBody
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }

        IEnumerable<T> GetTestEnumerable<T>(T t, ISession ses)
        {
            return ses.FreeSql<T>("select id as Id,description as Description from body");
        }






    }
}
