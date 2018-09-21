using System;
using PatientsPortal.Controllers;
using Xunit;

namespace PatientsPortal.Tests
{
    public class PatientsControllerTests
    {
        [Fact]
        public void NullContextTest()
        {
            var patientController = new PatientsController(null, null);
            Assert.Throws<NullReferenceException>(() =>
            {
                patientController.GetPatients();
            });
        }
    }
}
