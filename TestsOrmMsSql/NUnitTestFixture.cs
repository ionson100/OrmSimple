using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using ORM_1_21_;

namespace TestsOrmMsSql
{
    [TestFixture]
    public class NUnitTestFixture
    {
        Configure f = new Configure(connectionString: @"Data Source=ION-PC\SQLEXPRESS;Initial Catalog=assa;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False",
                 provider: ProviderName.MsSql,
                 writeLog: true,
                 logFileName: "E:/assa22.txt",
                 usageCache: true);
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

            var d = ses.GetCommand();
            d.CommandText = "select * from body where description <> null";
            d.Connection = ses.GetConnection();
            d.Connection.ConnectionString = ses.GetConnectionString();
            stopWatch.Start();
            d.Connection.Open();
            var ee = d.ExecuteReader();
            while (ee.Read())
            {

            }
            d.Connection.Close();

            stopWatch.Stop();
            Debug.WriteLine("ADO.NetCore: " + stopWatch.Elapsed);//ORMCore: 00:00:00.0132468

            Clear(ses);
            Assert.True(true);
        }


        [Test]
        public void TestUpdateSimpe()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestUpdateSimpe");
            Clear(ses);
            ses.Save(new Body());
            ses.Querion<Body>().Where(s => s.Description == null).Update(a => new Dictionary<object, string> { { a.Description, "312312" } });
            var description = ses.Querion<Body>().First().Description;
            Clear(ses);
            PrintSecondGround(ses, "TestUpdateSimpe");
            ses.Dispose();
            Assert.True(description == "312312");
        }







        [Test]
        public void TestDelete()
        {
            var ses = Configure.GetSessionCore();
            Clear(ses);
            PrintFirstGround(ses, "TestDelete");
            Clear(ses);
            var body = new Body();
            ses.Save(body);

            ses.Querion<Body>().Delete(a => a.Description == "1");

            var count = ses.Querion<Body>().ToList().Count(a => a.Description == "1");
            Clear(ses);
            PrintSecondGround(ses, "TestDelete");
            ses.Dispose();
            Assert.True(count == 0);
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
            var scope = ses.BeginTransaction();

            try
            {
                var b = new Body { Description = "dsdsdf" };
                ses.Save(b);
                throw new Exception();
            }
            catch (Exception)
            {

                scope.Rollback();
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
            var e11 = ses.Querion<Body>().Where(a => a.Description == "1233").Select(d => d.Description).AsEnumerable().Any();
            var e21 = ses.Querion<Body>().OverCache().Where(a => a.Description == "1233").Any();
            ses.WriteLogFile("cache");
            var e31 = ses.Querion<Body>().Where(a => a.Description == "123333333").Any();

            Clear(ses);
            ses.Dispose();
            PrintSecondGround(ses, "TestAny");
            Assert.True(e1 && e2 && e3 == false && e11 && e21 && e31 == false);

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
            var list = ses.Querion<Body>().OrderByDescending(body => body.Id).ToList();
            bool e1 = list[0].Id > list[1].Id;
            var list1 = ses.Querion<Body>().OrderByDescending(body => body.Description).ToList();
            bool e2 = list1[0].Description == "2" && list1[1].Description == "1";
            // var list2 = ses.Querion<Body>().OrderByDescending(a=>a.Id,new Comparers()) //не работает
            Clear(ses);
            ses.Dispose();
            PrintSecondGround(ses, "TestOrderByDescending");
            Assert.True(e1 && e2);

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
            var list = ses.Querion<Body>().OrderByDescending(body => body.Id).ThenBy(c => c.Description).ToList();
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

            var list1 = ses.Querion<Body>().Where(f => f.Description != null).OrderByDescending(body => body.Id).ThenBy(c => c.Description).Select(g => new { g.Id, g.Description }).ToList();

            Clear(ses);
            ses.Dispose();

            PrintSecondGround(ses, "TestSelectNew");
            Assert.True(list.Count == list1.Count);
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
            Assert.True(list == 2 && list1 == 2 && list2 == 2 && list3 == 0);
        }

        [Test]
        public void TestSplitCore()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "SplitQueryable");
            Clear(ses);
            var b1 = new Body { Description = "1" };
            ses.Save(b1);
            var b = new Body { Description = "2" };
            ses.Save(b);
            var b2 = new Body { Description = "3" };
            ses.Save(b2);
            var list = ses.Querion<Body>().SplitQueryable(1).ToList();

            Clear(ses);
            ses.Dispose();
            PrintSecondGround(ses, "SplitQueryable");

            Assert.True(list.Count() == 3);
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

            ses.Save(new Body());

            ses.Save(new Body { Description = "12" });

            ses.Save(new Body());
            var er = ses.Querion<Body>().Where(a => a.Description == "12").ToList();
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
           
            ses.Save(new Body());
        
            ses.Save(new Body { Description = "12" });
         
            ses.Save(new Body());

            var dd13 = ses.Querion<Body>().ElementAtOrDefault(0);
            var dd12 = ses.Querion<Body>().ElementAtOrDefault(1);
            var dd122 = ses.Querion<Body>().ElementAtOrDefault(2);
            var dd1ty222 = ses.Querion<Body>().ElementAtOrDefault(3);
            var dd1222 = ses.Querion<Body>().ElementAtOrDefault(4);
            Body dd2 = null;
            var dd1 = ses.Querion<Body>().Where(a => a.Description == null).ElementAt(1);
            try
            {
                // dd2 = ses.Querion<Body>().Where(a => a.Description == null).ElementAt(3);
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
            
            ses.Save(new Body());
           
            ses.Save(new Body { Description = "12" });
         
            ses.Save(new Body());


            var dd1 = ses.Querion<Body>().Where(a => a.Description == null).ElementAtOrDefault(1);
            var dd2 = ses.Querion<Body>().Where(a => a.Description == null).ElementAtOrDefault(3);
            var dd3 = ses.Querion<Body>().Where(a => a.Description == "12").ElementAtOrDefault(0);
            Clear(ses);
            PrintSecondGround(ses, "TestElementAtOrDefault");
            ses.Dispose();
            Assert.True(dd1 != null && dd2 == null && dd3 != null);
        }

        //[Test]
        //public void TestGroupByCore()
        //{
        //    var ses = Configure.GetSessionCore();
        //    PrintFirstGround(ses, "TestGroupByCore");
        //    Clear(ses);
        //    var body = new Body { Description = "12" };
        //    ses.Save(body);
        //    var body2 = new Body { Description = "12" };
        //    ses.Save(body2);
        //    var body12 = new Body { Description = "13" };
        //    ses.Save(body12);

        //    var body121 = new Body();
        //    ses.Save(body121);
        //    var dd1 = ses.Querion<Body>().Where(a => a.Description == null).GroupByCore(a => a.Description).ToList();
        //    var dd2 = ses.Querion<Body>().GroupByCore(a => a.Description).ToList();
        //    // var dd3 = ses.Querion<Body>().GroupByCore(a=>a.Id).ToList();
        //    Clear(ses);
        //    PrintSecondGround(ses, "TestGroupByCore");
        //    ses.Dispose();
        //    Assert.True(dd1.Count == 1 && dd2.Count == 3);
        //}

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
            var dd333 = ses.Querion<Body>().GroupBy(a => a.Description).Count(); //не работает

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




            var dd1 = ses.Querion<Body>().Where(a => a.Description != null).Reverse().ToList();
            // var dd2 = ses.Querion<Body>().GroupBy(a => a.Description).ToList();

            Clear(ses);
            PrintSecondGround(ses, "TestRevers");
            ses.Dispose();
            Assert.True(dd1.Any());//&& dd2.Count == 2 && dd1.First().Description == "13");
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
        public void TestLimit()
        {
            var ses = Configure.GetSessionCore();
            PrintFirstGround(ses, "TestLimit");
            Clear(ses);

            ses.Save(new Body());

            ses.Save(new Body { Description = "12" });

            ses.Save(new Body());

            var dd13 = ses.Querion<Body>().Limit(0, 2).ToList();

         
            Clear(ses);
            PrintSecondGround(ses, "TestLimit");
            ses.Dispose();
            Assert.True(dd13.Count==2&&dd13[0].Description==null&&dd13[1].Description=="12");
        }
    }
}

