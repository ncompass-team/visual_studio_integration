using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework

{
    
    public class DataClass
    {
        [Test, TestCaseSource("DataReg")]
        public static object[] DataReg()
        {
            object[][] MyData = new object[2][];    //Row=No. of times test to be executed

            MyData[0] = new object[2];
            MyData[0][0] = "mumair@broadpeakit.com";
            MyData[0][1] = "admin1953";


            MyData[1] = new object[2];
            MyData[1][0] = "ricky@gmail.com";
            MyData[1][1] = "admin1952";
            return MyData;
        }
    }
}