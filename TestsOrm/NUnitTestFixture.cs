using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using ORM_1_21_;

namespace TestsOrm
{
    [TestFixture]
    public class NUnitTestFixture
    {
        Configure fg = new Configure("Server=localhost;Database=test;Uid=;Pwd=;charset=utf8;Allow User Variables=True;", ProviderName.MySql, true, "E:/assa22.txt", true);
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
        public void TestCustomAttibute()
        {

            var ses = Configure.GetSessionCore();
            var f1 = new TestCustom();
            ses.Save(f1);
            var r = ses.Get<TestCustom>(f1.Id).Class == null;
            var d = new MyClass { Name = "312312" };
            var f = new TestCustom { Class = d };
            ses.Save(f);
            var rr = ses.Get<TestCustom>(f.Id).Class.Name == "312312";
            var list = ses.Querion<TestCustom>().ToList();
            foreach (var testCustom in list)
            {
                ses.Delete(testCustom);
            }
            var count = ses.Querion<TestCustom>().Count();
            ses.Dispose();
            Assert.True(r && rr && count == 0);
        }

        [Test]
        public void TestTransaction()
        {
            var ses = Configure.GetSessionCore();
            var list = ses.Querion<TestCustom>().ToList();
            foreach (var testCustom in list)
            {
                ses.Delete(testCustom);
            }

            var ta = ses.BeginTransaction();
            try
            {
                var d = new MyClass { Name = "312312" };
                var f = new TestCustom { Class = d };
                ses.Save(f);
                throw new Exception();
            }
            catch (Exception)
            {

                ta.Rollback();
            }

            var count = ses.Querion<TestCustom>().Count();
            ses.Dispose();
            Assert.True(count == 0);
        }
        [Test]
        public void TestInterface()
        {
            var ses = Configure.GetSessionCore();
            var list = ses.Querion<Body>().ToList();
            foreach (var testCustom in list)
            {
                ses.Delete(testCustom);
            }
            var body = new Body();
            ses.Save(body);
            body.Description = "sasda";
            ses.Save(body);
            ses.Delete(body);
            var count = ses.Querion<Body>().Count();
            ses.Dispose();
            Assert.True(body.IsDelete && body.IsInsert && body.IsDelete && count == 0);
        }

        [Test]
        public void TestSpeedOrm()
        {

            var ses = Configure.GetSessionCore();
            for (var i = 0; i < 50; i++)
            {
                var b = new Body { Description = "dsdsdf" };
                ses.Save(b);
            }
            var d = ses.Querion<Body>().First(a => a.Description != null);
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var list = ses.Querion<Body>().OverCache().Where(a => a.Description != null).ToArray();

            stopWatch.Stop();
            Debug.WriteLine("ORM: " + stopWatch.Elapsed);// 00:00:00.0120545


            foreach (var testCustom in list)
            {
                ses.Delete(testCustom);
            }

            Assert.True(list.Count() == 50);
        }

        [Test]
        public void TestSpeedCore()
        {
            var ses = Configure.GetSessionCore();
            for (var i = 0; i < 50; i++)
            {
                var b = new Body { Description = "dsdsdf" };
                ses.Save(b);
            }
            var stopWatch = new Stopwatch();
            MySqlCommand d = new MySqlCommand("select * from body where description <> null");
            d.Connection = new MySqlConnection("Server=localhost;Database=test;Uid=;Pwd=;charset=utf8;Allow User Variables=True;");

            stopWatch.Start();
            d.Connection.Open();
            var ee = d.ExecuteReader();
            foreach (var v in ee)
            {
            }
            d.Connection.Close();

            stopWatch.Stop();
            Debug.WriteLine("ADO.NetCore: " + stopWatch.Elapsed);//ORMCore: 00:00:00.0132468

            var list = ses.Querion<Body>().Where(a => a.Description != null).ToArray();
            foreach (var testCustom in list)
            {
                ses.Delete(testCustom);
            }
            Assert.True(true);
        }

        [Test]
        public void TestCacheFirstLevel()
        {
            var ses = Configure.GetSessionCore();

            var list = ses.Querion<Body>().ToArray();
            foreach (var testCustom in list)
            {
                ses.Delete(testCustom);
            }

            ses.Save(new Body());
            var c = ses.Querion<Body>().ToList().Count();

            var ses1 = Configure.GetSessionCore();
            ses1.Save(new Body());
            var c1 = ses1.Querion<Body>().ToList().Count();
            var c2 = ses.Querion<Body>().ToList().Count();

            var list1 = ses.Querion<Body>().ToArray();
            foreach (var testCustom in list1)
            {
                ses.Delete(testCustom);
            }

            ses.Dispose();
            ses1.Dispose();
            Assert.True(c == 1 && c1 == 2 && c2 == 1);
        }

        [Test]
        public void TestCacheSecondLevel()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestCacheSecondLevel");
            var list = ses.Querion<Body>().ToArray();
            foreach (var testCustom in list)
            {
                ses.Delete(testCustom);
            }

            ses.Save(new Body());
            var c = ses.Querion<Body>().ToList();
            ses.WriteLogFile("Запрос  получен из кеша, смотри лог");

            var ses1 = Configure.GetSessionCore();
            var list1 = ses1.Querion<Body>().ToList();



            foreach (var testCustom in list1)
            {
                ses.Delete(testCustom);
            }
            PrintSecondGround(ses, "TestCacheSecondLevel");
            ses.Dispose();

            Assert.True(true);
        }

        [Test]
        public void TestUpdate()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestUpdate");
            var list = ses.Querion<Body>().ToArray();
            foreach (var testCustom in list)
            {
                ses.Delete(testCustom);
            }
            ses.Save(new Body());
            ses.Querion<Body>().Where(s => s.Description == null).Update(a => new Dictionary<object, object> { { a.Description, "312312" } });
            var description = ses.Querion<Body>().First().Description;
            var list1 = ses.Querion<Body>().ToList();
            foreach (var testCustom in list1)
            {
                ses.Delete(testCustom);
            }
            PrintSecondGround(ses, "TestUpdate");
            ses.Dispose();
            Assert.True(description == "312312");
        }

        [Test]
        public void TestDelete()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestDelete");
            var list = ses.Querion<Body>().ToArray();
            foreach (var testCustom in list)
            {
                ses.Delete(testCustom);
            }
            var body = new Body();
            ses.Save(body);

            ses.Querion<Body>().Delete(a => a.Description == null);

            var count = ses.Querion<Body>().Count(a => a.Description == null);
            var list1 = ses.Querion<Body>().ToList();
            foreach (var testCustom in list1)
            {
                ses.Delete(testCustom);
            }
            PrintSecondGround(ses, "TestDelete");
            ses.Dispose();
            Assert.True(count == 0);
        }

        void PrintFirstGround(ISession ses, string testName)
        {
            ses.WriteLogFile(string.Format("{1}старт тест {0} _______________________________________________ {1}", testName, Environment.NewLine));
        }

        void PrintSecondGround(ISession ses, string testName)
        {
            ses.WriteLogFile(string.Format("{0}финиш тест {1} _______________________________________________ {0}", Environment.NewLine, testName));
        }
    }









}

