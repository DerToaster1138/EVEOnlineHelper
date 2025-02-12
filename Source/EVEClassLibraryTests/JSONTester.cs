using EVEClassLibrary;
using System.Net;

namespace EVEMarketeerTest
{

    [TestClass]
    public class JSONRipperTests
    {
        /// <summary>
        /// Test for JSON deserializing
        /// </summary>
        [TestMethod]
        public void formatter_working()
        {
            //Arrange
            var ripper = new JSONRipper();
            var client = new HttpClient();
            string input = client.GetStringAsync("https://esi.evetech.net/latest/status/?datasource=tranquility").Result;

            //Act
            string result = ripper.formatter(input);

            //Assert
            Assert.AreNotEqual(input, result);
        }
    }
}
