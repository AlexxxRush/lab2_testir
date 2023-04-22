using Lab2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lab2_Test
{
    [TestClass]
    public class Lab2Tests
    {   //1
        [TestMethod]
        public void arifm_10_20_30_exp_900()
        {
            //arrange
            int x = 10;
            int y = 20;
            int z = 30;
            int expected = 900;
            //act
            Class1 c = new Class1();
            int actual = c.arifm(x, y, z);
            //assert
            Assert.AreEqual(expected, actual);   
        }
        //2
        [TestMethod]
        public void arifm_1_2_3_exp_9()
        {
            //arrange
            int x = 1;
            int y = 2;
            int z = 3;
            int expected = 9;
            //act
            Class1 c = new Class1();
            int actual = c.arifm(x, y, z);
            //assert
            Assert.AreEqual(expected, actual);
        }
        //3
        [TestMethod]
        public void arifm_10_otr20_30_exp_otr300()
        {
            //arrange
            int x = 10;
            int y = -20;
            int z = 30;
            int expected = -300;
            //act
            Class1 c = new Class1();
            int actual = c.arifm(x, y, z);
            //assert
            Assert.AreEqual(expected, actual);
        }
        //4
        [TestMethod]
        public void arifm_5_5_0_exp_0()
        {
            //arrange
            int x = 5;
            int y = 5;
            int z = 0;
            int expected = 0;
            //act
            Class1 c = new Class1();
            int actual = c.arifm(x, y, z);
            //assert
            Assert.AreEqual(expected, actual);
        }
        //5
        [TestMethod]
        public void arifm_500_2000_8_exp_20000()
        {
            //arrange
            int x = 500;
            int y = 2000;
            int z = 8;
            int expected = 20000;
            //act
            Class1 c = new Class1();
            int actual = c.arifm(x, y, z);
            //assert
            Assert.AreEqual(expected, actual);
        }
        //6
        [TestMethod]
        public void arifm_otr10_otr20_otr30_exp_900()
        {
            //arrange
            int x = -10;
            int y = -20;
            int z = -30;
            int expected = 900;
            //act
            Class1 c = new Class1();
            int actual = c.arifm(x, y, z);
            //assert
            Assert.AreEqual(expected, actual);
        }
        //7
        [TestMethod]
        public void arifm_0_0_0_exp_0()
        {
            //arrange
            int x = 0;
            int y = 0;
            int z = 0;
            int expected = 0;
            //act
            Class1 c = new Class1();
            int actual = c.arifm(x, y, z);
            //assert
            Assert.AreEqual(expected, actual);
        }
        //8
        [TestMethod]
        public void arifm_10ADD_20_30_exp_1050()
        {
            //arrange
            int x = 10;
            int y = 20;
            int z = 30;
            int expected = 1050;
            //act
            Class1 c = new Class1();
            int actual = c.arifm(x + 5, y, z);
            //assert
            Assert.AreEqual(expected, actual);
        }
        //9
        [TestMethod]
        public void arifm_2_2_5_exp_20()
        {
            //arrange
            int x = 2;
            int y = 2;
            int z = 5;
            int expected = 20;
            //act
            Class1 c = new Class1();
            int actual = c.arifm(x, y, z);
            //assert
            Assert.AreEqual(expected, actual);
        }
        //10
        [TestMethod]
        public void arifm_11_11_31_exp_682()
        {
            //arrange
            int x = 11;
            int y = 11;
            int z = 31;
            int expected = 682;
            //act
            Class1 c = new Class1();
            int actual = c.arifm(x, y, z);
            //assert
            Assert.AreEqual(expected, actual);
        }


    }
}
