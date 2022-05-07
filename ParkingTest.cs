using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTesting_Lab2_MoqFramework.Controllers;
using UnitTesting_Lab2_MoqFramework.Data;
using UnitTesting_Lab2_MoqFramework.Models;

namespace Lab2_ParkingTest
{
    [TestClass]
    public class ParkingTest
    {
        [TestInitialize]
        public void Initialize()
        {

        }

        [TestMethod]
        public void CreatePassTest()
        {
            //Mock passList
            var mockSet = new Mock<DbSet<Pass>>();

            //Mock context
            var mockContext = new Mock<ParkomaticContext>();
            mockContext.Setup(m => m.Passes).Returns(mockSet.Object);

            //Arrange and Act
            var service = new ParkingHelper(mockContext.Object);
            service.CreatePass("Angela", true, 5);

            //Assert
            mockSet.Verify(m => m.Add(It.IsAny<Pass>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void CreateParkingSpotTest()
        {
            //Mock parkingSpotList
            var parkingSpotList = new Mock<DbSet<ParkingSpot>>();

            //Mock context
            var mockContext = new Mock<ParkomaticContext>();
            mockContext.Setup(m => m.ParkingSpot).Returns(parkingSpotList.Object);

            //Arrange & Act
            var service = new ParkingHelper(mockContext.Object);
            service.CreateParkingSpot();

            //Assert
            //verify that a specific invocation(parkingSpotList.Add(parkingSpot)) was performed on the mock
            parkingSpotList.Verify(psList => psList.Add(It.IsAny<ParkingSpot>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}