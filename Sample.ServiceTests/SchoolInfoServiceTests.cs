using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Sample.Repository;

namespace Sample.ServiceTests
{
    [TestClass()]
    public class SchoolInfoServiceTests
    {
        private ISchoolInfoRepository SchoolInfoRepository { get; set; }

        [AssemblyInitialize]
        public void TestInit()
        {
            this.SchoolInfoRepository = Substitute.For<ISchoolInfoRepository>();
        }

        [TestMethod()]
        public void GetAllSchoolInfosTest()
        {
            Assert.Fail();
        }
    }
}