using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mvc2UnitTestDemo1;

namespace BibliotekTester
{
    [TestClass]
    public class BookTests
    {
        private Book sut = new Book("123", "Nisse Nilsson", "Fina dikter");
       
        
        [TestMethod]
        public void When_buying_new_exemplars_count_should_be_set()
        {
            //
            sut.BuyNewEx(12);

            Assert.AreEqual(12,sut.Count);
        }


        [TestMethod]
        public void When_refilling_new_exemplars_new_ones_should_be_added_to_existing()
        {
            sut.BuyNewEx(12);
            sut.BuyNewEx(11);

            Assert.AreEqual(23, sut.Count);
        }


        //SES 10:25


        [TestMethod]
        public void Borrow_should_decrease_available()
        {
            sut.BuyNewEx(15);
            sut.Borrow("Stefan");
            Assert.AreEqual(14, sut.Available);
        }


    }
}


