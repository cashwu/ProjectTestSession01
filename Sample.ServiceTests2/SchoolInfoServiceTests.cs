using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Excel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Sample.Repository;
using Sample.Repository.Models;
using Sample.Service;
using System.Collections.Generic;
using System.Linq;

namespace Sample.ServiceTests2
{
    [TestClass]
    [DeploymentItem(@"TestData\SchoolInfoModels.xlsx")]
    public class SchoolInfoServiceTests
    {
        private ISchoolInfoRepository SchoolInfoRepository { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            this.SchoolInfoRepository = Substitute.For<ISchoolInfoRepository>();
        }

        [TestMethod()]
        public void GetAllSchoolInfos_取得全部學校資料()
        {
            // arrange
            var models = this.GetSourceDataFromExcel();
            this.SchoolInfoRepository.GetAllSchoolInfos().Returns(models);

            var sut = new SchoolInfoService(this.SchoolInfoRepository);

            // act
            var actual = sut.GetAllSchoolInfos();

            // assert
            actual.Should().NotBeNull();
            actual.Any().Should().BeTrue();
            actual.Count.Should().Be(4035);
        }

        [TestMethod]
        public void GetSchoolInfosByCity_cityid輸入0000_應取得台北市所有學校資料()
        {
            // arrange
            string cityId = "0000";

            var models = this.GetSourceDataFromExcel();
            var taipeiSchools = models.Where(x => x.CityId == "0000").ToList();

            this.SchoolInfoRepository.GetByCity(Arg.Any<string>())
                .Returns(taipeiSchools);

            var sut = new SchoolInfoService(this.SchoolInfoRepository);

            // act
            var actual = sut.GetSchoolInfosByCity(cityId);

            // assert
            actual.Should().NotBeNull();
            actual.Any().Should().BeTrue();
            actual.All(x => x.CityId == "0000").Should().BeTrue();
        }

        private List<SchoolInfoModel> GetSourceDataFromExcel()
        {
            var models = new List<SchoolInfoModel>();

            using (var workBook = new XLWorkbook("SchoolInfoModels.xlsx"))
            {
                var workSheet = workBook.Worksheets.First(x => x.Name == "Worksheet");

                using (var reader = new CsvReader(new ExcelParser(workSheet)))
                {
                    var records = reader.GetRecords<SchoolInfoModel>();
                    models.AddRange(records);
                }
            }

            return models;
        }
    }
}