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
            var dd1 = ses.Querion<Body>().Where(a => a.Description == null).ElementAt(1);
            var dd2 = ses.Querion<Body>().Where(a => a.Description == null).ElementAt(3);
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
        public void TestGroupByCore()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestGroupByCore");
            Clear(ses);
            var body = new Body { Description = "12" };
            ses.Save(body);
            var body2 = new Body { Description = "12" };
            ses.Save(body2);
            var body12 = new Body { Description = "13" };
            ses.Save(body12);

            var body121 = new Body();
            ses.Save(body121);
            var dd1 = ses.Querion<Body>().Where(a => a.Description == null).GroupByCore(a=>a.Description).ToList();
            var dd2 = ses.Querion<Body>().GroupByCore(a=>a.Description).ToList();
           // var dd3 = ses.Querion<Body>().GroupByCore(a=>a.Id).ToList();
            Clear(ses);
            PrintSecondGround(ses, "TestGroupByCore");
            ses.Dispose();
            Assert.True(dd1.Count ==1 && dd2.Count == 3 );//&& dd3 .Count!= 3);
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


            var dd1 = ses.Querion<Body>().Where(a => a.Description == null).GroupBy(a => a.Description).ToList();
            var dd2 = ses.Querion<Body>().GroupBy(a => a.Description).ToList();
            // var dd3 = ses.Querion<Body>().GroupByCore(a=>a.Id).ToList();
            Clear(ses);
            PrintSecondGround(ses, "TestGroupBy");
            ses.Dispose();
            Assert.True(dd1.Count == 1 && dd2.Count == 3);//&& dd3 .Count!= 3);
        }



    }
}
