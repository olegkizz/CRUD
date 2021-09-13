using IdentityNLayer.DAL.Interfaces;
using Moq;
using NUnit.Framework;

namespace IdentityNLayer.Tests
{
    public class Tests
    {
        private Mock<IUnitOfWork> Db;

        [SetUp]
        public void Setup()
        {
            Db = new Mock<IUnitOfWork>();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }


}