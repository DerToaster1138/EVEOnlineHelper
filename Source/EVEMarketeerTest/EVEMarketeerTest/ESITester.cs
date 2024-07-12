using EVEClassLibrary;
using System.Net;

namespace EVEMarketeerTest
{
    [TestClass]
    public class ESITester
    {
        /// <summary>
        /// Check if Jita gets ID'd correctly
        /// </summary>
        [TestMethod]
        public void KeyToJitaTest()
        {
            ///Arrange
            EVEClassLibrary.ESIRunner Runner = new ESIRunner();
            int Jita = 0;

            ///Act
            Jita = Runner.KeyToTradehub(ConsoleKey.J);

            ///Assert
            Assert.AreEqual(10000002, Jita);
        }

        /// <summary>
        /// Check if Amarr gets ID'd correctly
        /// </summary>
        [TestMethod]
        public void KeyToAmarrTest() 
        {
            //Arrange
            EVEClassLibrary.ESIRunner Runner = new ESIRunner();
            int Amarr;

            //Act
            Amarr = Runner.KeyToTradehub(ConsoleKey.A);

            //Assert
            Assert.AreEqual(Amarr, 10000043);
        }

    }
}