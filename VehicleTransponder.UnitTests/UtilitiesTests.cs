using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTransponder.Utilities;

namespace VehicleTransponder.UnitTests
{
    [TestClass]
    public class UtilitiesTests
    {
        [TestMethod]
        public void ParseInt_Success()
        {
            int expected = 2010;
            int actual = ParsingUtility.GetIntFromString("2010");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ParseInt_Success_Negative()
        {
            // although negative numbers wouldn't be valid for a vehicle year, they should still be considered valid for 
            // a function whose job is just to parse strings into integers
            int expected = -2010;
            int actual = ParsingUtility.GetIntFromString("-2010");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ParseInt_Fail_NonNumeric()
        {
            string stringValue = "ID10T";
            string expectedExceptionMessage = $"{stringValue} is not a valid integer";
            try
            {
                ParsingUtility.GetIntFromString(stringValue);
                Assert.Fail("Exception should be thrown");
            }
            catch (Exception e)
            {
                Assert.AreEqual(expectedExceptionMessage, e.Message);
            }
        }

        [TestMethod]
        public void ParseInt_Fail_NonInteger()
        {
            string stringValue = "3.1415926";
            string expectedExceptionMessage = $"{stringValue} is not a valid integer";
            try
            {
                ParsingUtility.GetIntFromString(stringValue);
                Assert.Fail("Exception should be thrown");
            }
            catch (Exception e)
            {
                Assert.AreEqual(expectedExceptionMessage, e.Message);
            }
        }
    }
}
