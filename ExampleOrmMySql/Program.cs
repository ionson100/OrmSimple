using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using ORM_1_21_;
using ORM_1_21_.Attribute;

namespace ExampleOrmMySql
{
    class Test1
    {
        private readonly long _id;
        private readonly string _sd;

        public Test1(Int64 id, string sd)
        {
            _id = id;
            _sd = sd;
        }
    }
    class Comparers : IComparer<int>
    {

        public int Compare(int x, int y)
        {
            if (x < y) return 1;
            return -1;
        }
    }
    class Program
    {
        static void Main()
        {
            //ORM_1_21_.Utils.
            new Configure("Server=localhost;Database=test;Uid=root;Pwd=;charset=utf8;Allow User Variables=True;", ProviderName.MySql, true, "E:/assa22.txt", true);




            var ses = Configure.GetSessionCore();
            foreach (var VARIABLE in ses.Querion<Body>())
            {
                ses.Delete(VARIABLE);
            }

            var bodye = new Body { Description = "12" };
            ses.Save(bodye);
            var body2 = new Body { Description = "12" };
            ses.Save(body2);
            var body12 = new Body { Description = "13" };
            ses.Save(body12);
            var body122 = new Body();
            ses.Save(body122);


           
            var dd133 = ses.Querion<Body>().Where(a => a.Description != null).Select(a => new { a.Description }).First();

            var ddfd3 = ses.Querion<Body>().Where(a => a.Description == null).Select(a => new { a.Description }).ElementAtOrDefault(0);
           
            var dd3 = ses.Querion<Body>().Where(a => a.Description == null).Select(a => new { a.Description }).First();
            var dd4 = ses.Querion<Body>().Where(a => a.Description != null).Select(a => new { a.Description }).FirstOrDefault();
          //  var dd5 = ses.Querion<Body>().Where(a => a.Description == "1").Select(a => new { a.Description }).FirstOrDefault();
            var dd6 = ses.Querion<Body>().Where(a => a.Description == null).Select(a => new { a.Description }).FirstOrDefault();
            var dd7 = ses.Querion<Body>().Where(a => a.Description != null).Select(a => new { a.Description }).Last();
            
           
            var dd9 = ses.Querion<Body>().Where(a => a.Description == null).Select(a => new { a.Description }).Last();
            var dd10 = ses.Querion<Body>().Where(a => a.Description != null).Select(a => new { a.Description }).LastOrDefault();
           // var dd11 = ses.Querion<Body>().Where(a => a.Description == "1").Select(a => new { a.Description }).LastOrDefault();
            var dd12 = ses.Querion<Body>().Where(a => a.Description == null).Select(a => new { a.Description }).LastOrDefault();



            var dd1gj = ses.Querion<Body>().Where(a => a.Description != null).GroupByCore(a => a.Description).ToList();

            var dd1sdewm = ses.Querion<Body>().Where(a => a.Description != null).GroupBy(a => a.Description).ToList();
            var dd1ewm =
                ses.Querion<Body>().Where(a => a.Description != null).Select(d => new {d.Description, d.Id}).ToList();

            var dd1rwter = ses.Querion<Body>().Where(a => a.Description == null).ElementAtOrDefault(1);
            var dd2trthtrh = ses.Querion<Body>().Where(a => a.Description == null).ElementAtOrDefault(3);
            var dd3erhhth = ses.Querion<Body>().Where(a => a.Description == "12").ElementAtOrDefault(0);








            /// var rtpos = ses.Querion<Body>().Where(a => a.Description == null).SingleOrDefault();


            //  var dddffg1 = ses.Querion<Body>().Where(a => a.Description == "12").SingleOrDefault().Description;
            //  var dddffg = ses.Querion<Body>().Where(a => a.Description == "12").SingleOrDefault().Description;

            ses.Save(new Body());
            ses.Querion<Body>().Where(s => s.Description == null).Update(a => new Dictionary<object, object> { { a.Description, "312312" } });

            ses.Save(new Body());

            var rttt = ses.Querion<Body>().Select(a => a.Description).ToList();
            var list2 = ses.Querion<Body>().OrderByDescending(a => a.Id, new Comparers()).ToList();
            var e11 = ses.Querion<Body>().Where(a => a.Description == "1233").Select(d => d.Description).AsEnumerable().Any();
            var e1fdfd = ses.Querion<Body>().Any(a => a.Description == "1233");
            var e2dfdf = ses.Querion<Body>().OverCache().Any(a => a.Description == "1233");
            var e3sas = ses.Querion<Body>().Any(a => a.Description == "123333333");
            var adffff = ses.Querion<Body>().Last().Description;
            var ererrrrr = ses.Querion<Body>().Last().Description;

            var p1 = new ParameterStoredPr("p1", "qwqwqw", ParameterDirection.Input);
            var p2 = new ParameterStoredPr("p2", 2, ParameterDirection.Output);
            var res = ses.ProcedureCallParam<Body>("Assa2;", p1, p2).ToList();



            var ss1 = ses.Querion<Telephone>().Select(a => new { a.Name }).DistinctCore(d => d.Name).ToList();


            //var lasrtion = ses.Querion<Telephone>().Select(a => new { a.Description, a.Id }).
            //    Join(ses.Querion<Table1>(), d => d.Id, f => f.Id, (g, h) => new { g.Description, h.Id });


            var sdwswawwdas = ses.FreeSqlParam<string>("select description from body where id >?p1", new Parameter("p1", 0)).Distinct();
            var sddwswwwawwdas = ses.FreeSqlParam<string>("select description from body where id >?p1", new Parameter("p1", 0)).Distinct();
            var sdsqwwwawwdas = ses.FreeSqlParam<Body>("select id,description from body where id >?p1", new Parameter("p1", 0));
            var sdsawwwwdas = ses.FreeSqlParam<dynamic>("select id,description from body where id >?p1", new Parameter("p1", 0));
            var sdswadas = ses.FreeSqlParam<Test1>("select id,description from body where id >?p1", new Parameter("p1", 0));
            var sasdasd = ses.ProcedureCall<Body>("Assa1;");
            //Dictionary<string, object> ddss;
            //var sasdwwasd = ses.ProcedureCallParam<object>(out ddss, "ion100",
            //                                             new ParameterStoredPr("p1", 0, ParameterDirection.Input, null),
            //                                             new ParameterStoredPr("p2", 3, ParameterDirection.InputOutput, "sdasd"));
            var sdswawwdas = ses.FreeSql<object>("select description from body");
            var sdsqwwawwdas = ses.FreeSql<Body>("select id,description from body");
            var sdsawwwdas = ses.FreeSql<object>("select id,description from body");
            var sdsadas = ses.FreeSql<Test1>("select id,description from body");




            var l = ses.Querion<Telephone>().OverCache().Select(a => new { dd = a.Description }).ToList();


            var rerer = ses.Querion<Table1>().Select(a => new { aa = a.Id, ass = a.Description })
                           .Join(ses.Querion<Body>().Select(a => new { ss = a.Description, ssss = a.Id }), arg => arg.aa,
                                 arg => arg.ssss, (ssd, df) => ssd.aa);

            var ewerwer = ses.Querion<Table1>().Where(a => a.Description != null).Join(ses.Querion<Telephone>().Select(a => new { assa = a.Description + "DAD", sddd = a.Id }),
                telephone => telephone.Id, body => body.sddd, (table1, telephone) =>
                    new { ss = telephone.assa, ddsd = table1.Description, sdsd = telephone.sddd });

            var assa = from c in ses.Querion<Body>().OrderBy(a => a.Description)
                       where c.Description != null
                       join p in ses.Querion<Table1>() on c.Id equals p.Id
                       select new { aaa = c.Description };











            var dsjsdh = ses.Querion<Body>().Where(a => a.Description != null).Where(a => a.Id > 0)
                            .Join(ses.Querion<Table1>().Select(a => new { sd = a.Description, id = a.Id }), body => body.Id, body => body.id,
                                      (body, body1) => new { dd = body.Description }).ToList();

            var sassasas = ses.Querion<Table1>().Join(ses.Querion<Body>().Where(a => a.Description != null), table1 => table1.Id, body =>
                                                                                                    body.Id,
                                                          (table1, body) => new { table1.Description, body.Id }).ToList();






            for (int i = 0; i < 20; i++)
            {
                ses.Save(new Telephone() { Datet = DateTime.Now });
            }

            //.ForEach(a=>ses.Querion<Telephone>().Delete(a));

            var eesweq =
               ses.Querion<Telephone>().Where(a => a.Description != null).OrderBy(q => q.Description).GroupByCore(a => a.Name).ToList();

            var eewewe =
                ses.Querion<Telephone>().Where(a => a.Description != null).OrderBy(q => q.Description).GroupByCore(a => a.Name != null).ToList();
            var eewwqwewe =
               ses.Querion<Telephone>().Where(a => a.Description != null).GroupByCore(a => a.Name).Select(a => new { a.Name }).ToList();
            var eewwqwaSewe =
               ses.Querion<Telephone>().OverCache().Where(a => a.Description != null).GroupByCore(a => a.Name, a => a.Description).ToList();
            var sdwqwfdfwwsd = ses.Querion<Telephone>().Where(a => a.Description != null).GroupByCore(a => a.Name).ToList();
            var sdsd = ses.Querion<Telephone>().FirstOrDefault(a => a.Id < 0);
            var sdwsd = ses.Querion<Telephone>().FirstOrDefault(a => a.Id < 0);
            var sqwfdsd = ses.Querion<Telephone>().LastOrDefault(a => a.Id < 0);
            var sdfwqwwsd = ses.Querion<Telephone>().LastOrDefault(a => a.Id < 0);
            //var sqwffdsd = ses.Querion<Telephone>().SingleOrDefault(a => a.Id < 0);
           // var sdwqfdfwwsd = ses.Querion<Telephone>().Single(a => a.Id < 0);

            var e1 = ses.Querion<Telephone>().OverCache().FirstOrDefault();
            var tr = ses.BeginTransaction();

            try
            {

                ses.Save(e1);
                throw new Exception("fdsfsdf");
                tr.Commit();
            }
            catch (Exception)
            {

                tr.Rollback();
            }
            using (var d = new TransactionScope())
            {
                var e13w3 = ses.Querion<Telephone>().OverCache().First();
                e13w3.Description = "sssswwwwwwwwwwwwwwwwwwwwwwwwwwwwwwsss";
                ses.Save(e13w3);
                d.Complete();
            }



            var eeewewe = ses.Querion<Telephone>().Where(a => a.Id < 409999999999999999).Update(a => new Dictionary<object, object> { { a.Name, DateTime.Now } });
            var e1we = ses.Querion<Body>().OverCache().ToList();

            var tSS11 = ses.GetList<Telephone>();

            var tdds = new Telephone { Datet = DateTime.Now };
            var t1 = ses.GetList<Body>();
            var t2 = ses.GetList<Body>(" id < 4000000");
            var t3 = ses.GetList<Body>(" id < 4000000");
            var t4 = ses.GetList<Body>(" id < 4000000", true);
            var t5 = ses.GetListParam<Body>(" id >?p", 12);
            var T6 = ses.GetListParam<Body>(false, " id >?p", 12);

            var b = new Body();
            ses.Save(b);
            b.Description = "asdasdasd";
            ses.Save(b);
            var dsdd = ses.GetList<Telephone>().FirstOrDefault();
            dsdd.Description = "ion100";
            dsdd.Datet = DateTime.Now;
            dsdd.Name = "ion100";
            ses.Save(dsdd);

            ses.Save(new Telephone() { Datet = DateTime.Now });

            var t11 = ses.GetList<Telephone>();
            var t12 = ses.GetList<Telephone>(" id < 4000000");
            var t13 = ses.GetList<Telephone>(" id < 4000000");
            var t14 = ses.GetList<Telephone>(" id < 4000000", true);
            var tel = new Telephone { Datet = DateTime.Now };
            ses.Save(tel);
            tel.Description = "asdasdasd";
            ses.Save(tel);
            var t21 = ses.Querion<Body>().ToList();
            var t22 = ses.Querion<Body>().ToList();
            var t23 = ses.Querion<Body>().DistinctCore(a => a.Description).ToList();
            var t24 = ses.Querion<Body>().ToList();
            var tdd = new Telephone { Datet = DateTime.Now };
            ses.Save(tdd);
            var eex = ses.Querion<Telephone>().First();
            eex.Description = "ion";
            ses.Save(eex);
            var t25 =
                ses.Querion<Body>()
                   .Where(a => a.Description != null)
                   .Where(d => d.Id > 12)
                   .Select(s => new { d = s.Id, ff = s.Description })
                   .ToList();
            var t27 = ses.Querion<Telephone>().OrderBy(w => w.Description).ThenBy(s => s.IdTelephone).LastOrDefault(s => s.Description != null);
            var t28 = ses.Querion<Telephone>().OrderBy(a => a.Description == null && a.Description == "asad").LastOrDefault(a => a.Description != null);
            var t29 = ses.Querion<Telephone>().OrderBy(a => a.Description == null && a.Description == "asad").FirstOrDefault(a => a.Description != null);
            var t30 = ses.Querion<Telephone>().OrderByDescending(a => a.Description).ToList().FirstOrDefault(d => d.Description == null);
            var t31 = ses.Querion<Telephone>().Last(d => d.Description == null);
            var t32 = ses.Querion<Telephone>().OrderByDescending(a => a.Description).ToList().LastOrDefault(d => d.Description == null);
            //var t33 = ses.Querion<Telephone>().Single(a => a.Description == "sadsadsa");
            //var t34 = ses.Querion<Telephone>().SingleOrDefault(a => a.Description == "sadsadsa");
            var t35 = ses.Querion<Telephone>().All(a => a.Description != null || a.Description == null);
            var t36 = ses.Querion<Telephone>().All(a => a.Description == "dsaas");
            var t37 = ses.Querion<Telephone>().All(a => a.Id < 60000000);

            var t38 = ses.Querion<Telephone>().Any(a => a.Description != null || a.Description == null);
            var t39 = ses.Querion<Telephone>().Any(a => a.Description == "dsaas");
            var t40 = ses.Querion<Telephone>().Any(a => a.Id < 60000000);
            var t41 = ses.Querion<Telephone>().Where(s => s.Id > 0).Count(d => d.Description == null);
            var t42 = ses.Querion<Telephone>().Where(s => s.Description != null).Select(a => a.Description.Length).ToList();
            var t43 = ses.Querion<Telephone>().Select(a => a.Description.Length + a.Id).ToList();
            var t44 = ses.Querion<Telephone>().Select(a => a.Description.Length).ToList();
            var t45 = ses.Querion<Telephone>().Where(s => s.Description != null).Select(a => new { dd = a.Description.Length + a.Id, f = a.IdBody }).ToList();
            var t456 =
                ses.Querion<Telephone>()
                   .Where(s => s.Description == "ttttttttt")
                   .Select(a => new { dd = a.Description.Length + a.Id, f = a.IdBody }).ToList();

            var t46 = ses.Querion<Telephone>().Where(a => a.Description != null).Sum(a => a.Id);
            var t47 = ses.Querion<Telephone>().Where(a => a.Description != null).Max(a => a.Id);
            var t418 = ses.Querion<Telephone>().Where(a => a.Description != null).Min(a => a.Id);

            var t48 = ses.Querion<Telephone>().Where(a => a.Description != null).Min(a => a.Id);
            var t49 = ses.Querion<Telephone>().Where(s => s.Description == "ttttttttt").DistinctCore(a => a.Description);
            //var t50 = ses.Querion<Telephone>().SingleOrDefault(a => a.Description == null);
           // var t51 = ses.Querion<Telephone>().SingleOrDefault(a => a.Description.Length == 10000);
           // var t52 = ses.Querion<Telephone>().Single(a => a.Description == null);
           // var t53 = ses.Querion<Telephone>().Single(a => a.Description.Length == 10000);
            var t54 = ses.Querion<Telephone>().Where(a => a.Description != null).GroupByCore(a => a.Description).ToList();
            var t55 = ses.FreeSql<Body>("select * from body");
            var t56 = ses.FreeSql<object>("select * from telephones");


            var sd = ses.GetList<Table1>();
            var dds = new Table1 { Id = new Random().Next(30000000) };
            ses.Save(dds);
            var ss = ses.Querion<Table1>().First();
            ss.Description = "asdasd";
            ses.Querion<Table1>().SaveOrUpdate(ss);
            var s1 = ses.Querion<Table1>().Select(a => new { ss = a.Id }).ToList();
            var st1 = ses.Querion<Table1>().Select(a => new {  a.Id,a.Description }).ToList();
            var s2 = ses.Querion<Table1>().ElementAtOrDefault(4).Id;
            var dd = ses.Querion<Telephone>().Where(a => a.Description == "sdasdasd").SplitQueryable(3).ToList();
            var dd1 = ses.Querion<Telephone>().Where(a => a.Description != null).Split(3).ToList();
            var dd2 = ses.Querion<Telephone>().Where(a => a.Description != null).ToList().Split(2);
            var rev = ses.Querion<Telephone>().Where(a => a.Description == null).OrderBy(s => s.IdBody).Reverse().ToList();
            var t345 = ses.Querion<Telephone>().Select(a => new { d = a.Description });
            var order =
                ses.Querion<Telephone>()
                   .Where(a => a.Description != null).OrderBy(a => a.Description).ThenByDescending(a => a.IdBody)
                   .LastOrDefault(a => a.Id < 2);
            var order1 =
                ses.Querion<Telephone>()
                   .Where(a => a.Description != null).OrderBy(a => a.Description).ThenByDescending(a => a.IdBody)
                   .LastOrDefault(a => a.Id < 2);
            var order3 =
               ses.Querion<Telephone>()
                  .Where(a => a.Description != null).OrderBy(a => a.Description).ThenByDescending(a => a.IdBody)
                  .FirstOrDefault(a => a.Id < 2);
            var order4 =
                ses.Querion<Telephone>()
                   .Where(a => a.Description != null).OrderBy(a => a.Description).ThenByDescending(a => a.IdBody)
                   .FirstOrDefault(a => a.Id < 2);

            var lim = ses.Querion<Telephone>().Limit(1, 3).ToList();
            var kk = ses.Querion<Telephone>().WhereFindLikeStartsWith(a => a.Description == "asas").ToList();
            var kk1 = ses.Querion<Telephone>().WhereFindLikeEndsWith(a => a.Description == " asas").ToList();
            var kk2 = ses.Querion<Telephone>().WhereFindLikeContains(a => a.Description == " asas").ToList();

            for (var i = 0; i < 5; i++)
            {
                var t = new Telephone { Datet = DateTime.Now, Description = "" + i };
                ses.Querion<Telephone>().SaveOrUpdate(t);
            }
            var ee = ses.Querion<Telephone>().Delete(a => a.Description == "1");
            var u = ses.Querion<Telephone>().Where(a => a.Description == "asas").
              Update(a => new Dictionary<object, object> { { a.Description, "sdasd" }, { a.Name, "sdasd" } });
            var se = ses.Querion<Telephone>().Where(a => a.IdBody < 0).ToList();
            var se32 = ses.Querion<Telephone>().Where(a => a.IdBody > 10).Select(bs => bs.Description.IndexOf('d')).ToList();
            var se2 = ses.Querion<Telephone>().Where(a => a.IdBody > 10).Select(bd => bd.Description.Substring(0, 1)).ToList();
            var swee = ses.Querion<Telephone>().Where(a => a.IdBody > 10).Select(bf => bf.Description.IndexOf('s')).ToList();
            var se1 = ses.Querion<Telephone>().Where(a => a.IdBody > 10).Select(bg => bg.Description.Replace("a", "ss")).ToList();
            var se4 = ses.Querion<Telephone>().Where(a => a.IdBody > 10).Select(bh => bh.Description + "dsas").ToList();
            var sew4 = ses.Querion<Telephone>().Where(a => a.IdBody > 10).Select(bh => string.Concat(bh.Description, "dsas")).ToList();
            var swew4 = ses.Querion<Telephone>().Where(a => a.IdBody > 10).Select(bh => bh.Description.Contains("dsas")).ToList();
            var se5 = ses.Querion<Telephone>().Where(a => a.IdBody > 10).Select(bn => bn.Description.ToLower()).ToList();
            var se6 = ses.Querion<Telephone>().Where(a => a.IdBody > 10).Select(bm => bm.Description.ToUpper()).ToList();
            var eeeqw = ses.Querion<Telephone>().Select(a => a.Description.StartsWith("asdasd")).ToList();
            var eeeqw1 = ses.Querion<Telephone>().Select(a => a.Description.EndsWith("asdasd")).ToList();
            var eee = ses.Querion<Telephone>().Select(a => a.Description.Trim("asd".ToArray())).ToList();
            var eee_1 = ses.Querion<Telephone>().Select(a => a.Description.Trim('d')).ToList();

            var eee1 = ses.Querion<Telephone>().Select(a => a.Description.TrimEnd()).ToList();
            var eee2 = ses.Querion<Telephone>().Select(a => a.Description.TrimStart()).ToList();
            var ssww = ses.Querion<Telephone>().Where(a => a.Datet == null).ToList();
            var aa = ses.Querion<Telephone>().Select(a => a.Datet.AddDays(3)).ToList();
            var aa1 = ses.Querion<Telephone>().Select(a => a.Datet.AddYears(3)).ToList();
            var aa2 = ses.Querion<Telephone>().Select(a => a.Datet.AddMonths(3)).ToList();
            var aa3 = ses.Querion<Telephone>().Select(a => a.Datet.AddHours(3)).ToList();
            var aa4 = ses.Querion<Telephone>().Select(a => a.Datet.AddMinutes(3)).ToList();
            var aa5 = ses.Querion<Telephone>().Select(a => a.Datet.AddSeconds(3)).ToList();
            var aa6 = ses.Querion<Telephone>().Select(a => string.Concat(a.Description, "sadas")).ToList();
            var aa7 = ses.Querion<Telephone>().Select(a => a.Description.Contains("as")).ToList();
            var aa8 = ses.Querion<Telephone>().Select(a => a.Description.Remove(2)).ToList();
            var aa9 = ses.Querion<Telephone>().Select(a => a.Description.Trim()).ToList();
            var ttt = ses.Querion<Telephone>().Select(s => decimal.Add(s.IdBody, 23)).ToList();
            var ttt1 = ses.Querion<Telephone>().Select(s => decimal.Subtract(s.IdBody, 23)).ToList();
            var ttt2 = ses.Querion<Telephone>().Select(s => decimal.Multiply(s.IdBody, 23)).ToList();
            var ttt3 = ses.Querion<Telephone>().Select(s => decimal.Divide(s.IdBody, 23)).ToList();
            var ttt4 = ses.Querion<Telephone>().Select(s => decimal.Remainder(s.IdBody, 23)).ToList();

            var ttt5 = ses.Querion<Telephone>().Select(s => decimal.Negate(s.IdBody)).ToList();
            var ttt6 = ses.Querion<Telephone>().Select(s => decimal.Round(s.IdBody, 23)).ToList();
            var ttt7 = ses.Querion<Telephone>().Select(s => decimal.Ceiling(s.IdBody)).ToList();
            var ttt8 = ses.Querion<Telephone>().Select(s => decimal.Floor(s.IdBody)).ToList();
            var ttt9 = ses.Querion<Telephone>().Select(s => Math.Abs(s.IdBody)).ToList();

            var ttt10 = ses.Querion<Telephone>().Select(s => Math.Abs(s.IdBody)).ToList();
            var ttt911 = ses.Querion<Telephone>().Select(s => Math.Acos(0.12) * (double)s.IdBody).ToList();
            var ttt12 = ses.Querion<Telephone>().Select(s => Math.Atan(23)).ToList();
            var ttt13 = ses.Querion<Telephone>().Select(s => Math.Atan2(s.IdBody, 34)).ToList();
            var ttt14 = ses.Querion<Telephone>().Select(s => Math.Cos(s.IdBody)).ToList();
            var ttt15 = ses.Querion<Telephone>().Select(s => Math.Exp(12) * (double)s.IdTelephone).ToList();
            var ttt9Wt3 = ses.Querion<Telephone>().Select(s => Math.Log10(s.IdBody)).ToList();
            var ttt92 = ses.Querion<Telephone>().Select(s => Math.Sign(s.IdBody)).ToList();
            var ttt93 = ses.Querion<Telephone>().Select(s => Math.Tan(s.IdBody)).ToList();
            var ttt49 = ses.Querion<Telephone>().Select(s => Math.Sqrt(s.IdBody)).ToList();
            var twtst9T1 = ses.Querion<Telephone>().Select(s => Math.Sign(s.IdBody)).ToList();
            var twtst9T2 = ses.Querion<Telephone>().Select(s => Math.Sign(s.IdBody)).ToList();
            var twtst9T3 = ses.Querion<Telephone>().Select(s => Math.Floor((decimal)s.IdBody)).ToList();
            var easse = ses.GetList<Telephone>().ToList();
            var ease = ses.GetList<Telephone>(" `telephones`.`name`='sadasdasd' LIMIT 0,1").ToList();
            var ease1 = ses.GetList<Body>(" `body`.`id`= 233 LIMIT 0,1").ToList();
            var oner = ses.Get<Telephone>((Int64)23);
            var ease12 = ses.GetListParam<Body>(" `body`.`id`= ?p1 LIMIT 0,1", 2333).ToList();
            var eas3e = ses.GetListParam<Telephone>(" `telephones`.`id_telephones` > ?p1 LIMIT 0,1", 0).ToList();
            var edfgase = ses.GetList<Telephone>().ToList();

            var ser = ses.GetList(new Telephone(), "", false);
            var sdfer = ses.GetList(new Telephone(), "", true);
            var kkaa = ses.Querion<Telephone>().WhereFindLikeStartsWith(a => a.Description == "asas").ToList();
            var kk1a = ses.Querion<Telephone>().WhereFindLikeEndsWith(a => a.Description == " asas").ToList();
            var kk2a = ses.Querion<Telephone>().ToList();

            ses.Dispose();
        }
    }
    [MapTableName("body", "body.id > 0")]
    public class Body
    {
        [MapBaseKey]
        [MapPrimaryKey("id", Generator.Native)]
        public Int32 Id { get; set; }

        [MapColumnNameAttribute("description")]
        public string Description { get; set; }
    }

    [MapTableJoin("Cross")]
    [MapTableName("telephones", "telephones.id_body > 0")]
    public class Telephone : Body, IErrorDal<Telephone>, IValidateDal<Body>, ORM_1_21_.IActionDal<Body>
    {
        [MapPrimaryKey("id_telephones", Generator.Native)]
        public Int32 IdTelephone { get; set; }
        [MapForeignKey]
        [MapColumnName("id_body")]
        public Int32 IdBody { get; set; }

        [MapColumnNameAttribute("`name`")]
        public string Name { get; set; }


        [MapColumnNameAttribute("`datet`")]
        public DateTime Datet { get; set; }
        public void ErrorDal(Telephone currentObject, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Validate(Body type)
        {

        }

        public void BeforeInsert(Body item)
        {

        }

        public void AfterInsert(Body item)
        {

        }

        public void BeforeUpdate(Body item)
        {

        }

        public void AfterUpdate(Body item)
        {

        }

        public void BeforeDelete(Body item)
        {

        }

        public void AfterDelete(Body item)
        {

        }
    }

    [MapTableName("`table_1`")]
    public class Table1
    {
        [MapPrimaryKeyAttribute("`id_table_1`", Generator.Assigned)]
        public Int32 Id { get; set; }

        [MapColumnNameAttribute("`name`")]
        public string Description { get; set; }
    }
}
