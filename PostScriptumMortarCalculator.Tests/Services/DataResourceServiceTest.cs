using NUnit.Framework;
using PostScriptumMortarCalculator.Services;

namespace PostScriptumMortarCalculator.Tests.Services
{
    [TestFixture]
    public class DataResourceServiceTest
    {
        [Test]
        public void Maps_GetData_ReturnsNonEmptyCollection()
        {
            var service = new DataResourceService();
            var mapData = service.GetMapData();
            Assert.That(mapData, Is.Not.Null.Or.Empty);
        }
        
        [Test]
        public void Mortars_GetData_ReturnsNonEmptyCollection()
        {
            var service = new DataResourceService();
            var mortarData = service.GetMortarData();
            Assert.That(mortarData, Is.Not.Null.Or.Empty);
        }
        
        [Test]
        public void Credits_GetData_ReturnsNonEmptyCollections()
        {
            var service = new DataResourceService();
            var creditsData = service.GetCreditsData();
            Assert.That(creditsData.Contributors, Is.Not.Null.Or.Empty);
            Assert.That(creditsData.ExternalTools, Is.Not.Null.Or.Empty);
        }
        
        [Test]
        public void Help_GetData_ReturnsNonEmptyCollections()
        {
            var service = new DataResourceService();
            var helpData = service.GetHelpData();
            Assert.That(helpData.Hotkeys, Is.Not.Null.Or.Empty);
            Assert.That(helpData.Hints, Is.Not.Null.Or.Empty);
        }
    }
}