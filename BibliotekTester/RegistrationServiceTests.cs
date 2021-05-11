using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mvc2UnitTestDemo1;

namespace BibliotekTester
{

    //class EMailFake : IEmailer
    //{
    //    public bool Called = false;
    //    public void Send(string to, string header, string body)
    //    {
    //        Called = true;
    //        return;
    //    }
    //}

    //class FakeUserRepository : IUserRepository
    //{
    // public bool Exists(string email)
    //    {
    //        if (email == "stefan@stefan.se") return true;
    //        return false;
    //    }

    //    public void Add(string email, string name)
    //    {

    //    }
    //}



    [TestClass]
    public class RegistrationServiceTests
    {
        private RegistrationService sut;
        //private EMailFake emailFake = new EMailFake();
        Mock<IEmailer> eMailFakeMock;
        Mock<IUserRepository> userRepositoryMock;

        public RegistrationServiceTests()
        {
            eMailFakeMock = new Mock<IEmailer>();
            userRepositoryMock = new Mock<IUserRepository>();
            sut = new RegistrationService(eMailFakeMock.Object, userRepositoryMock.Object);
        }

        [TestMethod]
        public void When_email_exists_should_return_false()
        {
            var model = new RegistrationViewModel { Email = "stefan@stefan.se", Name = "Stefan" };

            userRepositoryMock.Setup(r => r.Exists("stefan@stefan.se")).Returns(true);

            Assert.IsFalse(sut.Register(model));
        }

        [TestMethod]
        public void When_email_doesnt_exists_should_return_false()
        {
            var model = new RegistrationViewModel { Email = "1stefan@stefan.se", Name = "Stefan" };
            userRepositoryMock.Setup(r => r.Exists("1stefan@stefan.se")).Returns(false);
            Assert.IsTrue(sut.Register(model));
        }

        [TestMethod]
        public void When_email_exists_shouldnot_send_email()
        {
            var model = new RegistrationViewModel { Email = "stefan@stefan.se", Name = "Stefan" };
           sut.Register(model);
           eMailFakeMock.Verify(r => r.Send(It.IsAny<string>(),
               It.IsAny<string>(), It.IsAny<string>()), Times.Never);

        }

        [TestMethod]
        public void When_email_doesnt_exists_should_send_email()
        {
            var model = new RegistrationViewModel { Email = "1stefan@stefan.se", Name = "Stefan" };
            sut.Register(model);
            eMailFakeMock.Verify(r => r.Send("1stefan@stefan.se",
                It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }


    }
}