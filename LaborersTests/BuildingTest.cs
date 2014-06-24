using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Laborers;
using Moq;
using Moq.Protected;
using Laborers.Behaviors;

namespace LaborersTests
{
    [TestClass]
    public class BuildingTest
    {
        /// <summary>
        /// Tests that the building can be instantiated
        /// </summary>
        [TestMethod]
        public void Building_InstanceTest()
        {
            Building building = new Building();

            Assert.IsNotNull(building);
        }
        /// <summary>
        /// Tests that the building always has a default position
        /// </summary>
        [TestMethod]
        public void Building_PositionDefaultTest()
        {
            Building building = new Building();
            Position expectedPosition = new Position(0, 0, 0);
            Assert.IsNotNull(building);
            Assert.AreEqual(expectedPosition, building.Position);
        }
        /// <summary>
        /// Tests that the position can be get and set
        /// </summary>
        [TestMethod]
        public void Building_PositionGetSetTest()
        {
            Building building = new Building();
            Position expectedPosition = new Position(1, 2, 3);

            building.Position = expectedPosition;
            Assert.IsNotNull(building);
            Assert.AreEqual(expectedPosition, building.Position);
        }
        /// <summary>
        /// Tests the update method
        /// </summary>
        [TestMethod]
        public void Building_UpdateTest()
        {
            var buildingMock = new Mock<Building>();
            buildingMock.Setup(b => b.Update()).Verifiable();
            buildingMock.Object.Update();

            buildingMock.Verify();
        }
        /// <summary>
        /// Tests the default value for isbuilt is false
        /// </summary>
        [TestMethod]
        public void Building_IsBuiltDefaultIsFalseTest()
        {
            Building building = new Building();

            Assert.AreEqual(false, building.IsBuilt);
        }
        /// <summary>
        /// Tests that UpdateConstruction is called when IsBuilt is false
        /// </summary>
        [TestMethod]
        public void Building_Update_UpdateConstructionIfIsBuiltIsFalseTest()
        {
            ResourceList buildingRequirements = new ResourceList();
            var buildingMock = new Mock<Building>();
            buildingMock.CallBase = true;
            buildingMock.SetupGet(b => b.Requirements).Returns(buildingRequirements);
            buildingMock.SetupGet(b => b.IsBuilt).Returns(false);
            buildingMock.Setup(b => b.UpdateConstruction()).Verifiable();
            buildingMock.Object.Update();

            buildingMock.Verify();

        }
        /// <summary>
        /// Tests that the UpdateConstruction is never called when is Built is true
        /// </summary>
        [TestMethod]
        public void Building_Update_UpdateConstructionNeverCalledIfIsBuiltIsTrueTest()
        {
            ResourceList buildingRequirements = new ResourceList();
            var buildingMock = new Mock<Building>();
            buildingMock.CallBase = true;
            buildingMock.SetupGet(b => b.Requirements).Returns(buildingRequirements);
            buildingMock.SetupGet(b => b.IsBuilt).Returns(true);
            buildingMock.Object.Update();

            buildingMock.Verify();
            buildingMock.Verify(b => b.UpdateConstruction(), Times.Never());

        }
        /// <summary>
        /// Tests that the building executes the ConstructionComplete in UpdateConstruction
        /// </summary>
        [TestMethod]
        public void Building_UpdateConstruction_ConstructionCompleteTest()
        {
            
            ResourceList buildingRequirements = new ResourceList();

            var buildingMock = new Mock<Building>();
            buildingMock.CallBase = true;
            buildingMock.SetupGet(b => b.Requirements).Returns(buildingRequirements);
            buildingMock.Setup(b => b.ConstructionComplete());
            buildingMock.Object.UpdateConstruction();


            buildingMock.Verify();

        }
        /// <summary>
        /// Tests that the IsBuilt is set in the ConstructionComplete
        /// </summary>
        [TestMethod]
        public void Building_ConstructionComplete_SetIsBuiltTrueTest()
        {
            var buildingMock = new Mock<Building>();
            buildingMock.CallBase = true;
            buildingMock.Protected().SetupSet<bool>("IsBuilt",true).Verifiable();
            buildingMock.Object.ConstructionComplete();

            buildingMock.Verify();
        }
        /// <summary>
        /// Tests that the Requirements property is set when UpdateRequirements is called
        /// </summary>
        [TestMethod]
        public void Building_UpdateRequirementsTest()
        {
            var buildingRequirements = new ResourceList();
            var buildingMock = new Mock<Building>();
            buildingMock.CallBase = true;
            buildingMock.Protected().SetupSet<ResourceList>("Requirements", buildingRequirements).Verifiable();

            
            var recipeProvider = new Mock<IRecipeProvider>(MockBehavior.Strict);
            recipeProvider.Setup<ResourceList>(r => r.GetRequirementsForBuilding(It.Is<BuildingType>(b => b == buildingMock.Object.BuildingType))).Returns(buildingRequirements);

            buildingMock.Object.UpdateRequirements(recipeProvider.Object);


            recipeProvider.Verify();
            buildingMock.Verify();

        }
        /// <summary>
        /// Tests that the UpdateRequirements is called by the CheckRequirements when the Requirements property is null
        /// </summary>
        [TestMethod]
        public void Building_CheckRequirements_UpdateRequirementsIfRequirementsIsNullTest()
        {
            var buildingRequirements = new ResourceList();
            var buildingMock = new Mock<Building>();
            ResourceList currentRequirements = null;
            buildingMock.CallBase = true;
            buildingMock.SetupGet(b => b.Requirements).Returns(currentRequirements).Verifiable();
            buildingMock.Protected().SetupSet<ResourceList>("Requirements",buildingRequirements).Verifiable();

            var recipeProvider = new Mock<IRecipeProvider>(MockBehavior.Strict);
            recipeProvider.Setup<ResourceList>(r => r.GetRequirementsForBuilding(It.Is<BuildingType>(b => b == buildingMock.Object.BuildingType))).Returns(buildingRequirements);

            buildingMock.Object.CheckRequirements(recipeProvider.Object);

            buildingMock.Verify();
            recipeProvider.Verify();

        }
        /// <summary>
        /// Tests that the UpdateRequirements is never called by the CheckRequirements when the Requirements property is not null
        /// </summary>
        [TestMethod]
        public void Building_CheckRequirements_UpdateRequirementsNotCalledIfRequirementsIsNotNullTest()
        {
            var buildingMock = new Mock<Building>();
            ResourceList currentRequirements = new ResourceList();
            buildingMock.CallBase = true;
            buildingMock.SetupGet(b => b.Requirements).Returns(currentRequirements).Verifiable();

            var recipeProvider = new Mock<IRecipeProvider>(MockBehavior.Strict);
            buildingMock.Object.CheckRequirements(recipeProvider.Object);

            buildingMock.Verify();
            recipeProvider.Verify();

        }
    }
}
