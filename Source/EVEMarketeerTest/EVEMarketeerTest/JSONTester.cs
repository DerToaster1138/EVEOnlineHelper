using EVEClassLibrary;
using System.Net;

namespace EVEMarketeerTest
{

    [TestClass]
    public class JSONTester 
    {
        /// <summary>
        /// Test for JSON deserializing
        /// </summary>
        [TestMethod]
        public void JSONFormatTest()
        {
            //Arrange
            EVEClassLibrary.JSONRipper ripper = new JSONRipper();
            WebClient client = new WebClient();
            string input = client.DownloadString("https://esi.evetech.net/latest/status/?datasource=tranquility");

            //Act
            string result = ripper.formatter(input);

            //Assert
            Assert.AreNotEqual(input, result);
        }
    }
}
