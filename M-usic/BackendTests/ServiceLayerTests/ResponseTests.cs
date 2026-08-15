using Backend.ServiceLayer;
using NUnit.Framework;
using System;

namespace BackendTests.ServiceLayerTests
{
    public class ResponseTest
    {
        private Response r1;
        private Response r2;

        [SetUp]
        public void Setup()
        {
            r1 = new Response("error message", "some value");
            r2 = new Response("only error");
        }

        [TearDown]
        public void TearDown()
        {
            r1 = null;
            r2 = null;
        }

        [TestCase("error", "value")]
        [TestCase("something went wrong", 42)]
        [TestCase("null value error", null)]
        public void Constructor_Response_WithErrorMsgAndRetVal_CreatesSuccessfully(string errorMsg, object retVal)
        {
            Response response = new Response(errorMsg, retVal);
            Assert.IsNotNull(response, "Response object should have been created successfully with error message and return value");
        }

        [TestCase("error occurred")]
        [TestCase("invalid input")]
        [TestCase("user not found")]
        public void Constructor_Response_WithErrorMsgOnly_CreatesSuccessfully(string errorMsg)
        {
            Response response = new Response(errorMsg);
            Assert.IsNotNull(response, "Response object should have been created successfully with error message only");
        }

        [TestCase("error occurred")]
        [TestCase("invalid input")]
        [TestCase("user not found")]
        public void Constructor_Response_WithErrorMsgOnly_RetValIsNull(string errorMsg)
        {
            Response response = new Response(errorMsg);
            Assert.IsNull(response.ReturnValue, "ReturnValue should be null when only error message is provided");
        }

        [TestCase("error", "value")]
        [TestCase("something went wrong", 42)]
        [TestCase("bad request", true)]
        public void GetErrorMsg_AfterConstructionWithBothArgs_ReturnsCorrectErrorMsg(string errorMsg, object retVal)
        {
            Response response = new Response(errorMsg, retVal);
            Assert.AreEqual(errorMsg, response.ErrorMessage, "ErrorMessage should match the value provided in the constructor");
        }

        [TestCase("error", "value")]
        [TestCase("something went wrong", 42)]
        [TestCase("bad request", true)]
        public void GetRetVal_AfterConstructionWithBothArgs_ReturnsCorrectRetVal(string errorMsg, object retVal)
        {
            Response response = new Response(errorMsg, retVal);
            Assert.AreEqual(retVal, response.ReturnValue, "ReturnValue should match the value provided in the constructor");
        }

        [TestCase(null, "value")]
        [TestCase(null, 42)]
        [TestCase(null, null)]
        public void Constructor_Response_WithNullErrorMsg_CreatesSuccessfully(string errorMsg, object retVal)
        {
            Response response = new Response(errorMsg, retVal);
            Assert.IsNotNull(response, "Response object should have been created successfully even with a null error message");
        }
    }
}