using System;
using System.Collections.Generic;
using DotNetTest;
using DotNetTest.Birds;
using DotNetTest.Temperature;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetUnitTest
{
    [TestClass]
    public class LanguageFeatureTest
    {
        [TestMethod]
        public void Params()
        {
            List<int> integers = new LanguageFeature().ParamsToList(1, 2, 3);

            CollectionAssert.AreEqual(new List<int> {1, 2, 3}, integers);
        }

        [TestMethod]
        /* Use new in a subclass to mask a superclass method */
        public void NewAndBase()
        {
            Assert.AreEqual(Bird.Birdsound, new Bird().MakeSound());
            Assert.AreEqual(Pigeon.PigeonSound, new Pigeon().MakeSound());
            Assert.AreEqual(Bird.Birdsound /*wow, strange, I would expect pigeonsound here*/, ((Bird) new Pigeon()).MakeSound());
            Assert.AreEqual(Bird.Birdsound, new Pigeon().MakeBirdSound());
        }

        [TestMethod]
        /* Use virtual in a superclass to allow a method to be overridden in a subclass */
        public void VirtualAndOverride()
        {
            Assert.AreEqual(Bird.BirdColor, new Bird().GetColor());
            Assert.AreEqual(Pigeon.PigeonColor, new Pigeon().GetColor());
            Assert.AreEqual(Pigeon.PigeonColor, ((Bird) new Pigeon()).GetColor());
            Assert.AreEqual(Bird.BirdColor, new Pigeon().GetBirdColor());
        }

        [TestMethod]
        public void Unchecked()
        {
            int ten = 10;
            int integerOverflow = unchecked(2147483647 + ten);

            Assert.AreEqual(-2147483639, integerOverflow);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void Checked()
        {
            int ten = 10;
            var integer = checked(2147483647 + ten);
        }

        [TestMethod]
        public void Delegate()
        {
            // Func is a delegate type
            // implementation is: public delegate TResult Func<in T, out TResult>(T arg);
            Func<string, string> toUpperCaseLambda = (input) => input.ToUpper();
            Assert.AreEqual("TEST", toUpperCaseLambda.Invoke("test"));
            Func<string, string> toUpperCaseDelegate = delegate(string input) { return input.ToUpper(); };
            Assert.AreEqual("TEST", toUpperCaseDelegate.Invoke("test"));

            // Question: how to implement Function as an instance method reference?
            // in Java:
            // Function<String, String> toUpperCase = String::toUpperCase;
            // toUpperCase.apply("test");
        }

        [TestMethod]
        public void Event()
        {
            var eventEmitter = new EventEmitter();
            string result = null;
            eventEmitter.Changed += (sender, message) => result = message;
            eventEmitter.DoSomethingThatEmitsEvent();

            Assert.AreEqual(EventEmitter.SomethingInterestingHappened, result);
        }

        [TestMethod]
        public void Explicit()
        {
            // cast needed for conversion
            Celsius celsius = (Celsius)new Fahrenheit(100);
            Assert.AreEqual(38, Math.Round(celsius.Degrees));
        }

        [TestMethod]
        public void Implicit()
        {
            // no cast needed for conversion
            Fahrenheit fahrenheit = new Celsius(38);
            Assert.AreEqual(100, Math.Round(fahrenheit.Degrees));
        }

        [TestMethod]
        public void In()
        {
            // todo: with predicate as example?
        }

        [TestMethod]
        public void Operator()
        {
            Assert.AreEqual(new CustomNumber(2),  new CustomNumber(1) + new CustomNumber(1));
            Assert.IsTrue(new CustomNumber(2) ==  new CustomNumber(1) + new CustomNumber(1));
            Assert.IsFalse(new CustomNumber(2) !=  new CustomNumber(1) + new CustomNumber(1));
        }
    }
}
