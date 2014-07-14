using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Transactions;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using ORM_1_21_;

namespace TestsOrm
{
    [TestFixture]
    public class NUnitTestFixture
    {
        Configure fg = new Configure("Server=localhost;Database=test;Uid=root;Pwd=;charset=utf8;Allow User Variables=True;", ProviderName.MySql, true, "E:/assa22.txt", true);
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
            foreach (var c in list)
            {
                ses.Delete(c);
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
            //может глючить из за разны
            var ses = Configure.GetSessionCore();
            Clear(ses);
            var body = new Body();
            ses.Save(body);
            body.Description = "sasda";
            ses.Save(body);
            ses.Delete(body);
            var count = ses.Querion<Body>().Count(s=>s.Id==body.Id);
            Clear(ses);
            ses.Dispose();
            Debug.WriteLine("body.IsDelete-" + body.IsDelete);
            Debug.WriteLine("body.IsInsert-" + body.IsInsert);
            Debug.WriteLine("body.IsDelete-" + body.IsDelete);
            Debug.WriteLine("count-"+count);

            Assert.True(body.IsDelete && body.IsInsert && body.IsDelete && count == 0);
        }

        [Test]
        public void TestSpeedOrm()
        {

            var ses = Configure.GetSessionCore();
            Clear(ses);
            for (var i = 0; i < 50; i++)
            {
                var b = new Body { Description = "dsdsdf" };
                ses.Save(b);
            }
            var d = ses.Querion<Body>().First(a => a.Description != null);//разогрев
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var list = ses.Querion<Body>().OverCache().Where(a => a.Description != null).ToArray();

            stopWatch.Stop();
            Debug.WriteLine("ORM: " + stopWatch.Elapsed);// 00:00:00.0120545


            Clear(ses);

            Assert.True(list.Count() == 50);
        }

        [Test]
        public void TestSpeedCore()
        {
            var ses = Configure.GetSessionCore();
            Clear(ses);
            for (var i = 0; i < 50; i++)
            {
                var b = new Body { Description = "dsdsdf" };
                ses.Save(b);
            }
            var stopWatch = new Stopwatch();
            var d = new MySqlCommand("select * from body where description <> null")
                    {
                        Connection =
                            new MySqlConnection(
                            "Server=localhost;Database=test;Uid=;Pwd=;charset=utf8;Allow User Variables=True;")
                    };

            stopWatch.Start();
            d.Connection.Open();
            var ee = d.ExecuteReader();
            foreach (var v in ee)
            {
            }
            d.Connection.Close();

            stopWatch.Stop();
            Debug.WriteLine("ADO.NetCore: " + stopWatch.Elapsed);//ORMCore: 00:00:00.0132468

            Clear(ses);
            Assert.True(true);
        }

        [Test]
        public void TestCacheFirstLevel()
        {
            var ses = Configure.GetSessionCore();

            Clear(ses);

            ses.Save(new Body());
            var c = ses.Querion<Body>().ToList().Count();

            var ses1 = Configure.GetSessionCore();
            ses1.Save(new Body());
            var c1 = ses1.Querion<Body>().ToList().Count();
            var c2 = ses.Querion<Body>().ToList().Count();

            Clear(ses);

            ses.Dispose();
            ses1.Dispose();
            Assert.True(c == 1 && c1 == 2 && c2 == 1);
        }

        [Test]
        public void TestCacheSecondLevel()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestCacheSecondLevel");
            Clear(ses);

            ses.Save(new Body());
            var c = ses.Querion<Body>().ToList();
            ses.WriteLogFile("Запрос  получен из кеша, смотри лог");

            var ses1 = Configure.GetSessionCore();
            Clear(ses);
            PrintSecondGround(ses, "TestCacheSecondLevel");
            ses.Dispose();

            Assert.True(true);
        }

        [Test]
        public void TestUpdate()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestUpdate");
            Clear(ses);
            ses.Save(new Body());
            ses.Querion<Body>().Where(s => s.Description == null).Update(a => new Dictionary<object, object> { { a.Description, "312312" } });
            var description = ses.Querion<Body>().First().Description;
            Clear(ses);
            PrintSecondGround(ses, "TestUpdate");
            ses.Dispose();
            Assert.True(description == "312312");
        }

        [Test]
        public void TestDelete()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestDelete");
            Clear(ses);
            var body = new Body();
            ses.Save(body);

            ses.Querion<Body>().Delete(a => a.Description == null);

            var count = ses.Querion<Body>().Count(a => a.Description == null);
            Clear(ses);
            PrintSecondGround(ses, "TestDelete");
            ses.Dispose();
            Assert.True(count == 0);
        }


        [Test]
        public void TestValidate()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestDelete");
            Clear(ses);
            var body = new Body { Description = "2222" };
            ses.Save(body);




            Clear(ses);
            PrintSecondGround(ses, "TestDelete");
            ses.Dispose();
            Assert.True(body.Description == "1111");
        }


        [Test]
        public void TestProcedure()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestProcedure");
            Clear(ses);


            var body = new Body { Description = "2222" };
            ses.Save(body);

            var ee = ses.ProcedureCall<Body>("Assa1;").Count();


            Clear(ses);
            PrintSecondGround(ses, "TestProcedure");
            ses.Dispose();
            Assert.True(ee == 1);
        }

        [Test]
        public void TestProcedureParam()
        {

            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestProcedureParam");
            Clear(ses);

            var body = new Body { Description = "2222" };
            ses.Save(body);
            var p1 = new ParameterStoredPr("p1", "qwqwqw", ParameterDirection.Input);
            var p2 = new ParameterStoredPr("p2", 2, ParameterDirection.Output);
            var res = ses.ProcedureCallParam<Body>("Assa2;", p1, p2).ToList();


            Clear(ses);
            PrintSecondGround(ses, "TestProcedureParam");
            ses.Dispose();
            Assert.True((int)p2.Value == 100);
        }


        [Test]
        public void TestUsageNativData()
        {
            var ses = Configure.GetSessionCore();
            Clear(ses);
            for (var i = 0; i < 10; i++)
            {
                var b = new Body { Description = "dsdsdf" };
                ses.Save(b);
            }
            var command = ses.GetCommand();
            command.CommandText = "select * from body ";
            command.Connection = ses.GetConnection();
            command.Connection.ConnectionString = ses.GetConnectionString();
            command.Connection.Open();
            var rider = command.ExecuteReader();
            int ie = 0;
            while (rider.Read())
            {
                ie++;
            }
            command.Connection.Close();
            rider.Dispose();
            command.Dispose();




            Clear(ses);
            Assert.True(ie == 10);
        }

        [Test]
        public void TestOverCache()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestOverCache");
            Clear(ses);

            var b = new Body { Description = "dsdsdf" };
            ses.Save(b);


            var r1 = ses.Querion<Body>().ToList().Count();//1

            var ses1 = Configure.GetSessionCore();
            ses1.Save(new Body { Description = "ass" });

            var r21 = ses1.Querion<Body>().ToList().Count();//2

            var r2 = ses.Querion<Body>().ToList().Count();//1

            var r3 = ses.Querion<Body>().OverCache().Where(s => s.Description != null).ToList().Count();//2

            Clear(ses);
            PrintSecondGround(ses, "TestOverCache");
            Assert.True(r1 == 1 && r2 == 1 && r3 == 2);
        }
        [Test]
        public void TestTrasactionScope()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestTrasactionScope");
            Clear(ses);
            using (var scope = new TransactionScope())
            {
                var b = new Body { Description = "dsdsdf" };
                ses.Save(b);
                var ses1 = Configure.GetSessionCore();
                ses1.Save(new Body());
            }
            var ee = ses.Querion<Body>().ToList().Count;
            Clear(ses);
            ses.Dispose();
            PrintSecondGround(ses, "TestTrasactionScope");
            Assert.True(ee == 0);

        }


        [Test]
        public void TestGetResemblance()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestGetResemblance");
            Clear(ses);

            var b = new Body { Description = "1233" };
            ses.Save(b);

            var ee = ses.GetList(b, "", true).First().Description;

            Clear(ses);
            ses.Dispose();
            PrintSecondGround(ses, "TestGetResemblance");
            Assert.True(ee == "1233");

        }

        [Test]
        public void TestLastAndLastOrDefault()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestLastAndLastOrDefault");
            Clear(ses);

            var b = new Body { Description = "1233" };
            ses.Save(b);
            var b1 = new Body { Description = "12332" };
            ses.Save(b1);

            var e1 = ses.Querion<Body>().LastOrDefault(a => a.Description == "1233");
            var e2 = ses.Querion<Body>().OverCache().Last(a => a.Description == "1233");
            var e3 = ses.Querion<Body>().LastOrDefault(a => a.Description == "123333333");

            Clear(ses);
            ses.Dispose();
            PrintSecondGround(ses, "TestLastAndLastOrDefault");
            Assert.True(e1.Description == e2.Description && e3 == null);

        }
         [Test]
        public void TestFirstAndFirstOrDefault()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestFirstAndFirstOrDefault");
            Clear(ses);
            var b1 = new Body { Description = "12332" };
            ses.Save(b1);
            var b = new Body { Description = "1233" };
            ses.Save(b);


            var e1 = ses.Querion<Body>().FirstOrDefault(a => a.Description == "1233");
            var e2 = ses.Querion<Body>().OverCache().First(a => a.Description == "1233");
            var e3 = ses.Querion<Body>().FirstOrDefault(a => a.Description == "123333333");

            Clear(ses);
            ses.Dispose();
            PrintSecondGround(ses, "TestFirstAndFirstOrDefault");
            Assert.True(e1.Description == e2.Description && e3 == null);

        }

         [Test]
         public void TestAny()
         {
             var ses = Configure.GetSessionCore();
             PrintFirstGround(ses, "TestAny");
             Clear(ses);
             var b1 = new Body { Description = "12332" };
             ses.Save(b1);
             var b = new Body { Description = "1233" };
             ses.Save(b);


             var e1 = ses.Querion<Body>().Any(a => a.Description == "1233");
             var e2 = ses.Querion<Body>().OverCache().Any(a => a.Description == "1233");
             var e3 = ses.Querion<Body>().Any(a => a.Description == "123333333");

             ses.WriteLogFile("cache");
             var e11 = ses.Querion<Body>().Where(a => a.Description == "1233").Select(d=>d.Description).AsEnumerable().Any();
             var e21 = ses.Querion<Body>().OverCache().Where(a => a.Description == "1233").Any();
             ses.WriteLogFile("cache");
             var e31 = ses.Querion<Body>().Where(a => a.Description == "123333333").Any();

             Clear(ses);
             ses.Dispose();
             PrintSecondGround(ses, "TestAny");
             Assert.True(e1&& e2 &&e3==false&&e11&&e21&&e31==false);

         }

         [Test]
         public void TestOrderByDescending()
         {
             var ses = Configure.GetSessionCore();
             PrintFirstGround(ses, "TestOrderByDescending");
             Clear(ses);
             var b1 = new Body { Description = "1" };
             ses.Save(b1);
             var b = new Body { Description = "2" };
             ses.Save(b);
            var list= ses.Querion<Body>().OrderByDescending(body => body.Id).ToList();
            bool e1 = list[0].Id > list[1].Id;
            var list1 = ses.Querion<Body>().OrderByDescending(body => body.Description).ToList();
            bool e2 = list1[0].Description=="2"&&list1[1].Description=="1";
           // var list2 = ses.Querion<Body>().OrderByDescending(a=>a.Id,new Comparers()) //не работает
             Clear(ses);
             ses.Dispose();
             PrintSecondGround(ses, "TestOrderByDescending");
             Assert.True(e1&&e2);

         }

         [Test]
         public void TestOrderBy()
         {
             var ses = Configure.GetSessionCore();
             
             PrintFirstGround(ses, "TestOrderBy");
             Clear(ses);
             var b1 = new Body { Description = "1" };
             ses.Save(b1);
             var b = new Body { Description = "2" };
             ses.Save(b);
             var list = ses.Querion<Body>().OrderByDescending(body => body.Id).ThenBy(c=>c.Description).ToList();
             bool e1 = list[0].Id > list[1].Id;
             var list1 = ses.Querion<Body>().OrderBy(body => body.Id).ToList();
             bool e2 = list1[0].Description == "1" && list1[1].Description == "2";
             // var list2 = ses.Querion<Body>().OrderByDescending(a=>a.Id,new Comparers()) //не работает
             Clear(ses);
             ses.Dispose();
             PrintSecondGround(ses, "TestOrderBy");
             Assert.True(e1 && e2);

         }
         [Test]
         public void TestClearCache()
         {
             //смотреть лог
             var ses = Configure.GetSessionCore();
            
             PrintFirstGround(ses, "TestClearCache");
             Clear(ses);
             var b1 = new Body { Description = "1" };
             ses.Save(b1);
             var b = new Body { Description = "2" };
             ses.Save(b);
             ses.WriteLogFile("base");
             var list = ses.Querion<Body>().OrderByDescending(body => body.Id).ThenBy(c => c.Description).ToList();
             ses.WriteLogFile("cache");
             var list1 = ses.Querion<Body>().OrderByDescending(body => body.Id).ThenBy(c => c.Description).ToList();

             var ses1 = Configure.GetSessionCore();
             ses.WriteLogFile("cache");
             var list2 = ses1.Querion<Body>().OrderByDescending(body => body.Id).ThenBy(c => c.Description).ToList();
             ses.ClearCache<Body>();
             ses.WriteLogFile("base");
             var ses2 = Configure.GetSessionCore();
             var list3 = ses2.Querion<Body>().OrderByDescending(body => body.Id).ThenBy(c => c.Description).ToList();
             
             Clear(ses);
             ses.Dispose();
             ses1.Dispose();
             ses2.Dispose();
             PrintSecondGround(ses, "TestClearCache");
             Assert.True(true);

         }
         [Test]
         public void TestSelectNew()
         {
             var ses = Configure.GetSessionCore();
             PrintFirstGround(ses, "TestSelectNew");
             Clear(ses);
             var b1 = new Body { Description = "1" };
             ses.Save(b1);
             var b = new Body { Description = "2" };
             ses.Save(b);
         
             var list =
                 ses.Querion<Body>()
                     .OrderByDescending(body => body.Id)
                     .ThenBy(c => c.Description)
                     .Select(d => d.Description)
                     .ToList();
       
             var list1 = ses.Querion<Body>().Where(f=>f.Description!=null).OrderByDescending(body => body.Id).ThenBy(c => c.Description).Select(g=>new{g.Id,g.Description}).ToList();

             Clear(ses);
             ses.Dispose();
         
             PrintSecondGround(ses, "TestSelectNew");
             Assert.True(list.Count==list1.Count);
         }

         [Test]
         public void TestCount()
         {
             var ses = Configure.GetSessionCore();
             PrintFirstGround(ses, "TestCount");
             Clear(ses);
             var b1 = new Body { Description = "1" };
             ses.Save(b1);
             var b = new Body { Description = "2" };
             ses.Save(b);
             var list = ses.Querion<Body>().Count();
             var list1 = ses.Querion<Body>().Where(f => f.Description != null).Count();
             var list2 = ses.Querion<Body>().Count(f => f.Description != null);
             var list3 = ses.Querion<Body>().Count(f => f.Description == null);
             Clear(ses);
             ses.Dispose();
             PrintSecondGround(ses, "TestCount");
             Assert.True(list ==2&& list1==2&&list2==2&&list3==0);
         }



        static void Clear(ISession ses)
        {
            var list1 = ses.Querion<Body>().ToList();
            foreach (var c in list1)
            {
                ses.Delete(c);
            }
        }

        static void PrintFirstGround(ISession ses, string testName)
        {
            ses.WriteLogFile(string.Format("{1}старт тест {0} _______________________________________________ {1}", testName, Environment.NewLine));
        }

        static void PrintSecondGround(ISession ses, string testName)
        {
            ses.WriteLogFile(string.Format("{0}финиш тест {1} _______________________________________________ {0}", Environment.NewLine, testName));
        }
    }

    class Comparers: IComparer<int>
    {

        public int Compare(int x, int y)
        {
            if (x > y) return 1;
            return -1;
        }
    }







}

