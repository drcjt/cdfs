using NameNode.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NameNodeTests
{
    [TestFixture]
    class ErrorViewModelTests
    {
        [Test]
        public void ShowRequestId_WithEmptyRequestId_ReturnsTrue()
        {
            // Arrange
            var errorViewModel = new ErrorViewModel();
            errorViewModel.RequestId = "";

            // Act
            var showRequestId = errorViewModel.ShowRequestId;

            // Assert
            Assert.IsFalse(showRequestId);
        }
    }
}
