using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace ConsoleApplication1
{
    [TestFixture]
    public class UnitTests1
    {
        [Test]
        public void Assa()
        {
            string d1 = "1";
            string d3 = null;
            //d3=""; - если убрать комментарий не ругается, пустое значение не изменяется
            string d2 = "2";
            int result = Convert.ToInt32(d2) + 1;
            string d2n = Convert.ToString(result);
            result = Convert.ToInt32(d2) - 1;
            string d2n1 = Convert.ToString(result);
            var bytes = new List<byte> { 1, 2, 3, 4, 5, 6, 7, 8,8,10,11,12 };
            foreach (var b in bytes)
            {
                switch (b)
                {
                    case 1:
                        d1 = "'" + d2 + "0101' and " + "'" + d2 + "0201'";
                        d3 = "'" + d2n1 + "1001' and " + "'" + d2 + "0101'";
                        break;
                    case 2:
                        d1 = "'" + d2 + "0201' and " + "'" + d2 + "0301'";
                        d3 = "'" + d2 + "0101' and " + "'" + d2 + "0201'";
                        break;
                    case 3:
                        d1 = "'" + d2 + "0301' and " + "'" + d2 + "0401'";
                        d3 = "'" + d2 + "0201' and " + "'" + d2 + "0301'";
                        break;
                    case 4:
                        d1 = "'" + d2 + "0401' and " + "'" + d2 + "0501'";
                        d3 = "'" + d2 + "0301' and " + "'" + d2 + "0401'";
                        break;
                    case 5:
                        d1 = "'" + d2 + "0501' and " + "'" + d2 + "0601'";
                        d3 = "'" + d2 + "0401' and " + "'" + d2 + "0501'";
                        break;
                    case 6:
                        d1 = "'" + d2 + "0601' and " + "'" + d2 + "0701'";
                        d3 = "'" + d2 + "0501' and " + "'" + d2 + "0601'";
                        break;
                    case 7:
                        d1 = "'" + d2 + "0701' and " + "'" + d2 + "0801'";
                        d3 = "'" + d2 + "0601' and " + "'" + d2 + "0701'";
                        break;
                    case 8:
                        d1 = "'" + d2 + "0801' and " + "'" + d2 + "0901'";
                        d3 = "'" + d2 + "0701' and " + "'" + d2 + "0801'";
                        break;
                    case 9:
                        d1 = "'" + d2 + "0901' and " + "'" + d2 + "1001'";
                        d3 = "'" + d2 + "0801' and " + "'" + d2 + "0901'";
                        break;
                    case 10:
                        d1 = "'" + d2 + "1001' and " + "'" + d2 + "1101'";
                        d3 = "'" + d2 + "0901' and " + "'" + d2 + "1001'";
                        break;
                    case 11:
                        d1 = "'" + d2 + "1101' and " + "'" + d2 + "1201'";
                        d3 = "'" + d2 + "1001' and " + "'" + d2 + "1101'";
                        break;
                    case 12:
                        d1 = "'" + d2 + "1201 and " + d2n + "0101";
                        d3 = "'" + d2 + "1101' and " + "'" + d2 + "1201'";
                        break;
                }
                Debug.WriteLine("____________________________________");
                Debug.WriteLine("byte =" + b);
                Debug.WriteLine("d2=" + d2);
                Debug.WriteLine(d1);
                Debug.WriteLine(d3);
                Debug.WriteLine("____________________________________");
            }
           

            Assert.True(true);
        }
       
    }
}