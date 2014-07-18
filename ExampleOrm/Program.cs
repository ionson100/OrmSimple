using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using ORM_1_21_;
using ORM_1_21_.Attribute;

namespace ExampleOrm
{
    class Program
    {
        static void Main(string[] args)
        {
            new Configure(connectionString: @"Data Source=ION-PC\SQLEXPRESS;Initial Catalog=assa;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False",
                provider: ProviderName.MsSql,
                writeLog: true, 
                logFileName: "E:/assa22.txt",
                usageCache: true);
            var ses = Configure.GetSessionCore();
            var bodye = new Body { Description = "12" };
            ses.Save(bodye);
            var body2 = new Body { Description = "12" };
            ses.Save(body2);
            var body12 = new Body { Description = "133" };
            ses.Save(body12);
            var body122 = new Body();
            ses.Save(body122);
           // var aaghdgfh = ses.Querion<Body>().Contains(bodye);

            //var rtrttts = ses.Querion<Body>().Where(a => a.Description == null).Single();

            //var aafgdfg = ses.Querion<Body>().Count(a => a.Description.StartsWith("1"));
            //var aa1 = ses.Querion<Body>().Count(a => a.Description.EndsWith("2"));
            //var aa2 = ses.Querion<Body>().Count(a => a.Description.Contains("3"));

            //var e1fdfd = ses.Querion<Body>().Where(a=>a.Description!=null).Count();
            //var e2dfdf = ses.Querion<Body>().OverCache().Any(a => a.Description == "1233");
            //var e3sas = ses.Querion<Body>().Any(a => a.Description == "123333333");

            var eretzxzxeeerrrr =
                ses.Querion<Body>().Where(s => s.Description == "-23-23").Select(g =>new{ g.Description}).ToList();
            var ereetrrrerr = ses.Querion<Body>().FirstOrDefault(a => a.Description == "-4");

            var eretzxzxrrrrr = ses.Querion<Body>().FirstOrDefault(a => a.Description == "4");
            var ereetrrrererrr = ses.Querion<Body>().FirstOrDefault(a => a.Description == "-4");
            var erezxzxrrrrr = ses.Querion<Body>().LastOrDefault(a=>a.Description=="4");
            var ererrrrr = ses.Querion<Body>().LastOrDefault(a => a.Description == "-4");
            var r323we = ses.Querion<Body>().ToList().Count();
            var r323 = ses.Querion<Body>().ToList();
            var r3233 = ses.Querion<Body>().OverCache().ToList().Count();
            var eeex = ses.Querion<Body>().FirstOrDefault();
            var erererre = ses.Querion<Telephone>().Where(a => a.IdTel > 0).Select(a => new { sd = a.Description }).Limit(0, 20).ToList();
            var sdwswawwdas = ses.FreeSqlParam<object>("select description from body where id >@p1", new Parameter("p1", 0));
            var sdsqwwwawwdas = ses.FreeSqlParam<Body>("select * from body where id >@p1", new Parameter("p1", 0));
            var sdsawwwwdas = ses.FreeSqlParam<object>("select id,description from body where id >@p1", new Parameter("p1", 0));
            var sasdasd = ses.ProcedureCall<Body>("Assa1");

            var p1 = new ParameterStoredPr("p1", 0, ParameterDirection.Output);
            var p2 = new ParameterStoredPr("p2", 0, ParameterDirection.Output);
            var sasdwwasd = ses.ProcedureCallParam<object>( "Assa22",p1 ,p2);
                                                       
                                                         
            var sdswawwdas = ses.FreeSql<object>("select description from body");
            var sdsqwwawwdas = ses.FreeSql<Body>("select * from body");
            var sdsawwwdas = ses.FreeSql<object>("select id,description from body");
            var dfdsfsdd = ses.Querion<Body>()
                        .Join(ses.Querion<Body>(), body => body.Id, body => body.Id,
                              (body, body1) => new { body.Description, body1.Id });
            var asdasd =
                ses.Querion<Body>()
                   .Select(a => new { sd = a.Id, sddd = a.Description })
                   .Join(ses.Querion<Telephone>().Where(a => a.IdTel > 0), sedds => sedds.sd, sfs => sfs.IdBody,
                         (sdd, tt) => sdd.sd);
            var fdsfsfsd = ses.Querion<Table1>()
                              .Where(a => a.Description != null)
                              .Join(ses.Querion<Telephone>(), table1 => table1.Id, telephone => telephone.IdTel,
                                    (table1, telephone) => new { n = telephone.IdBody, table1.Description });
            ses.Querion<Table1>().Where(a => a.IdT > 0).Update(a => new Dictionary<object, object> { { a.Namea, "dasdasdasdasd" } });
            ses.Querion<Table1>().Update(a => new Dictionary<object, object> { { a.Namea, "dasdasdasdasd" } });

            ses.Querion<Body>().Where(a => a.Id > 0).Update(a => new Dictionary<object, object> { { a.Description, "dasdasdasdasd" } });
            ses.Querion<Body>().Update(a => new Dictionary<object, object> { { a.Description, "dasdasdasdasd" } });

            var t2ewe7 = ses.Querion<Telephone>().OverCache().OrderBy(w => w.Description).LastOrDefault(s => s.Description != null);
            var twewe28 = ses.Querion<Telephone>().OrderBy(a => a.Description).LastOrDefault(a => a.Description != null);
            var sssssww = ses.GetList<Table1>().FirstOrDefault();
         //  sssssww.Description = "dddddddd2222222222";
            //using (var d = new TransactionScope())
            //{
               // ses.Save(sssssww);
                //  d.Complete();
            //}


            var sgfsxlk =
                ses.Querion<Table1>()
                   .Where(a => a.Description != null && a.Id > 0).Any(s => s.Datet.Day < DateTime.Now.Day);
            var eeZxeeee = ses.Querion<Table1>().OverCache().Where(a => a.Id > 0).Select(a => Math.Round(a.IdBodya, 6)).Limit(0, 10).ToList();
            var trans = ses.BeginTransaction();
            var t1 = ses.GetList<Body>();
            var t1a2 = ses.GetList<Body>(" id < 4000000").FirstOrDefault();
            t1a2.Name = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            ses.Save(t1a2);
            var t2 = ses.GetList<Body>(" id < 4000000");
            var t3 = ses.GetList<Body>(" id < 4000000");
            var t4 = ses.GetList<Body>(" id < 4000000", true);
            var t5 = ses.GetListParam<Body>(" id >@p", 12);
            var T6 = ses.GetListParam<Body>(false, " id >@p", 12);
            var b = new Body();
            ses.Save(b);
            b.Description = "asdasdasd";
            ses.Save(b);


            var t12 = ses.GetList<Telephone>(" id < 4000000");
            var t13 = ses.GetList<Telephone>(" id < 4000000");
            var t14 = ses.GetList<Telephone>(" id < 4000000", true);
            var ewef = ses.Get<Telephone>((decimal)8);
            var tel = new Telephone() { Datet = DateTime.Now };

            ses.Save(tel);
            var t11 = ses.GetList<Telephone>();

            tel.Description = "asdasdasd";
            var trans1 = ses.BeginTransaction();
            ses.Save(tel);

            var t21 = ses.Querion<Body>().ToList();
            ses.Delete(t21[0]);

            var t22 = ses.Querion<Body>().OverCache().ToList();
            var t23 = ses.Querion<Body>().DistinctCore(a => a.Description).ToList();
            var t24 = ses.Querion<Body>().ToList();
            var tdd = new Telephone() { Datet = DateTime.Now.AddDays(-3) };
            ses.Save(tdd);

            var eex = ses.Querion<Telephone>().FirstOrDefault();
            eex.Description = "ion";
            var ssssss = new Table1() { Datet = DateTime.Now };
            ses.Save((Body)eex);

            var t25 =
                ses.Querion<Body>()
                   .Where(a => a.Description != null)
                   .Where(d => d.Id > 12)
                   .Select(s => new { d = s.Id, ff = s.Description })
                   .ToList();
            var t27 = ses.Querion<Telephone>().OrderBy(w => w.Description).LastOrDefault(s => s.Description != null);
            var t28 = ses.Querion<Telephone>().OrderBy(a => a.Description).LastOrDefault(a => a.Description != null);
            var t29 = ses.Querion<Telephone>().OrderBy(a => a.Description).FirstOrDefault(a => a.Description != null);
            var t30 = ses.Querion<Telephone>().OrderByDescending(a => a.Description).ToList();//.First(d => d.Description == null);
            var t31 = ses.Querion<Telephone>().LastOrDefault(d => d.Description == null);
            var t33 = ses.Querion<Telephone>().SingleOrDefault(a => a.Description == "sadsadsa");
        //    var t34 = ses.Querion<Telephone>().SingleOrDefault(a => a.Description == "sadsadsa");
            var t35 = ses.Querion<Telephone>().All(a => a.Description != null || a.Description == null);
            var t36 = ses.Querion<Telephone>().All(a => a.Description == "dsaas");
            var t37 = ses.Querion<Telephone>().All(a => a.Id < 60000000);
            var tt1 = ses.Querion<Body>().Any(a => a.Id < 22222222);
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
           // var t50 = ses.Querion<Telephone>().SingleOrDefault(a => a.Description == null);
          //  var t51 = ses.Querion<Telephone>().SingleOrDefault(a => a.Description.Length == 10000);
            var t52 = ses.Querion<Telephone>().SingleOrDefault(a => a.Description == null);
            var t53 = ses.Querion<Telephone>().SingleOrDefault(a => a.Description.Length == 10000);
            Func<Telephone, int> ddxx = telephone => (int)(telephone.IdTel * 100);
            var sd = ses.GetList<Table>().ToList();
            var dds = new Table { Id = new Random().Next(2000000000) };
            ses.Save(dds);
            var ssa = ses.Querion<Table>().FirstOrDefault();
            ssa.Name = "asdasd";
            ses.Querion<Table>().SaveOrUpdate(ssa);
            var s1 = ses.Querion<Table>().Select(a => new { ss = a.Id }).ToList();
            var st1 = ses.Querion<Table>().Select(a => new { ss = a.Id }).ToList();
            var s2e = ses.Querion<Table>().ElementAtOrDefault(1).Id;
            var s2 = ses.Querion<Table>().ElementAtOrDefault(0);
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

            //////////////////////////////var lim = ses.Querion<Telephone>().Limit(1, 3).ToList();

            var ion1 = ses.GetList(new Telephone(), "", true);
            var kk = ses.Querion<Telephone>().Where(a => a.Description.StartsWith("d")).ToList();
            var kk1 = ses.Querion<Telephone>().Where(a => a.Description == " asas").ToList();
            var kk2 = ses.Querion<Telephone>().Where(a => a.Description == " asas").ToList();

            for (int i = 0; i < 5; i++)
            {
                var t = new Telephone { Datet = DateTime.Now, Description = "" + i };
                ses.Querion<Telephone>().SaveOrUpdate(t);
            }
            var ee = ses.Querion<Telephone>().Delete(a => a.Description == "1");
            var u = ses.Querion<Telephone>().Where(a => a.Name == "asas").
                Update(a => new Dictionary<object, object> { { a.NameTelephone, "sdasd" } });
            var se32 = ses.Querion<Telephone>().Where(a => a.IdBody > 10).Select(bs => bs.Description.IndexOf('d')).ToList();
            var se2 = ses.Querion<Telephone>().Where(a => a.IdBody > 10).Select(bd => bd.Description.Substring(0, 1)).ToList();
            var se = ses.Querion<Telephone>().Where(a => a.IdBody > 10).Select(bf => bf.Description.IndexOf('s')).ToList();
            var se1 = ses.Querion<Telephone>().Where(a => a.IdBody > 10).Select(bg => bg.Description.Replace("a", "ss")).ToList();
            var se4 = ses.Querion<Telephone>().Where(a => a.IdBody > 10).Select(bh => bh.Description + "dsas").ToList();
            var se5 = ses.Querion<Telephone>().Where(a => a.IdBody > 10).Select(bn => bn.Description.ToLower()).ToList();
            var se6 = ses.Querion<Telephone>().Where(a => a.IdBody > 10).Select(bm => bm.Description.ToUpper()).ToList();
            var eee = ses.Querion<Telephone>().Select(a => a.Description.Trim());
            var ss = ses.Querion<Telephone>().Where(a => a.Datet == null).ToList();
            var aaxv = ses.Querion<Telephone>().Select(a => a.Datet.AddDays(3)).ToList();
            var aa1xcv = ses.Querion<Telephone>().Select(a => a.Datet.AddYears(3)).ToList();
            var aa2xcv = ses.Querion<Telephone>().Select(a => a.Datet.AddMonths(3)).ToList();
            var aa3 = ses.Querion<Telephone>().Select(a => a.Datet.AddHours(3)).ToList();
            var aa4 = ses.Querion<Telephone>().Select(a => a.Datet.AddMinutes(3)).ToList();
            var aa5 = ses.Querion<Telephone>().Select(a => a.Datet.AddSeconds(3)).ToList();
            var aa6 = ses.Querion<Telephone>().Select(a => string.Concat(a.Description, "sadas")).ToList();
            var aa7 = ses.Querion<Telephone>().Where(a => a.Description.Contains("as")).ToList();
            var aa8 = ses.Querion<Telephone>().Select(a => a.Description.Remove(2)).ToList();
            var aa9 = ses.Querion<Telephone>().Select(a => a.Description.TrimStart()).ToList();
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
            var ttt13 = ses.Querion<Telephone>().Select(s => Math.Atan2((double)s.IdBody, 34)).ToList();
            var ttt14 = ses.Querion<Telephone>().Select(s => Math.Cos((double)s.IdBody)).ToList();
            var ttt15 = ses.Querion<Telephone>().Select(s => Math.Exp(12) * (double)s.IdTel).ToList();
          //  var ttt9wt3 = ses.Querion<Telephone>().Select(s => Math.Log10((double)s.IdBody)).ToList();
            var ttt92 = ses.Querion<Telephone>().Select(s => Math.Sign(s.IdBody)).ToList();
            var ttt93 = ses.Querion<Telephone>().Select(s => Math.Tan((double)s.IdBody)).ToList();
            var ttt49 = ses.Querion<Telephone>().Select(s => Math.Sqrt((double)s.IdBody)).ToList();
            var twtst9t1 = ses.Querion<Telephone>().Select(s => Math.Sign(s.IdBody)).ToList();
            var twtst9t2 = ses.Querion<Telephone>().Select(s => Math.Sign(s.IdBody)).ToList();
            var twtst9t3 = ses.Querion<Telephone>().Select(s => Math.Floor(s.IdBody)).ToList();
            trans.Commit();
            ses.Dispose();
        }
    }

    [MapTableName("[Body]", "[body].[id] > 0 ")]
    public class Body
    {
        [MapBaseKeyAttribute]
        [MapPrimaryKeyAttribute("[id]", Generator.Native)]
        public virtual Decimal Id { get; set; }

        [MapColumnNameAttribute("[name_body]")]
        public virtual string Name { get; set; }

        [MapColumnNameAttribute("[description]")]
        public virtual string Description { get; set; }
    }
    [MapTableJoinAttribute("inner")]
    [MapTableName("[Telephone]")]
    public class Telephone : Body
    {

        [MapPrimaryKeyAttribute("[id_tel]", Generator.Native)]
        public virtual Decimal IdTel { get; set; }
        [MapColumnNameAttribute("[name_telephone]")]
        public virtual string NameTelephone { get; set; }
        [MapForeignKey]
        [MapColumnNameAttribute("[id_body]")]
        public virtual Decimal IdBody { get; set; }
        [MapColumnNameAttribute("[datet]")]
        public DateTime Datet { get; set; }
    }
    [MapTableName("[Table]")]
    public class Table : IActionDal<Table>
    {
        [MapPrimaryKeyAttribute("[Id]", Generator.Assigned)]
        public Int32 Id { get; set; }

        [MapColumnNameAttribute("[name_table]")]
        public string Name { get; set; }

        public void BeforeInsert(Table item)
        {

        }

        public void AfterInsert(Table item)
        {

        }

        public void BeforeUpdate(Table item)
        {

        }

        public void AfterUpdate(Table item)
        {

        }

        public void BeforeDelete(Table item)
        {

        }

        public void AfterDelete(Table item)
        {

        }
    }

    [MapTableJoinAttribute("inner")]
    [MapTableName("[Table1]")]
    public class Table1 : Telephone
    {

        [MapPrimaryKeyAttribute("[Id]", Generator.Native)]
        public Int32 IdT { get; set; }

        [MapForeignKey]
        [MapColumnNameAttribute("[Id_Body]")]
        public decimal IdBodya { get; set; }

        [MapColumnName("[name_table]")]
        public string Namea { get; set; }
    }
}
