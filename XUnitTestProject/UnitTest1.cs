using System;
using Xunit;
using WebApi.Controllers;
using WebApi.Data;
using WebApi.Helper;
using WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Moq;

namespace XUnitTestProject
{

    public class UnitTest1
    {
        private AccountMeterDbContext _context;
        private EnsekAccountsController _controller;
        private ValidateHelper _helper = new ValidateHelper();

        [Fact]
        public void TestValidateFile1()
        {
            _helper = new ValidateHelper();

            bool expectedResult = true;
            string fileext = ".csv";
            // Act
            var result = _helper.ValidateFile(fileext);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void TestValidateFile2()
        {
            _helper = new ValidateHelper();

            bool expectedResult = false;
            string fileext = ".txt";
            // Act
            var result = _helper.ValidateFile(fileext);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void TestValidateReadingDate1()
        {
            _helper = new ValidateHelper();

            DateTime expectedResult = DateTime.MinValue;
            string readdate = "1239";
            // Act
            var result = _helper.ValidateReadingDate(readdate);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void TestValidateReadingDate2()
        {
            _helper = new ValidateHelper();

            DateTime expectedResult = new DateTime(2019, 05, 19, 9, 44, 00);
            string readdate = "19/05/2019 09:44";
            // Act
            var result = _helper.ValidateReadingDate(readdate);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void TestValidateReadingValue1()
        {
            _helper = new ValidateHelper();

            int expectedResult = 123;
            string readval = "123";
            // Act
            var result = _helper.ValidateReadingValue(readval);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void TestValidateReadingValue2()
        {
            _helper = new ValidateHelper();

            int expectedResult = -1;
            string readval = "VOID";
            // Act
            var result = _helper.ValidateReadingValue(readval);

            // Assert
            Assert.Equal(expectedResult, result);
        }


    }
}
