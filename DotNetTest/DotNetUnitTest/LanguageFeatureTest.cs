﻿using System;
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
            // predicate is a delegate with an in parameter
            Predicate<Pigeon> predicatePigeon = (pigeon) => pigeon.GetColor() == Pigeon.PigeonColor;
            Predicate<Bird> predicateBird = (bird) => bird.GetColor() == Bird.BirdColor;

            // in is contravariant (? super T in java) --> Contravariance enables you to use a less derived type than that specified by the generic parameter
            // https://msdn.microsoft.com/en-us/library/dd469484.aspx
            predicatePigeon = predicateBird;

            Assert.AreEqual(predicatePigeon, predicateBird);
            // in is not covariant --> this is a compilation error
            //predicateObject = predicateString;
        }

        [TestMethod]
        public void Out()
        {
            // predicate is a delegate with an in parameter
            Func<string, Pigeon> pigeonFactory = (name) => new Pigeon(name);
            Func<string, Bird> birdFactory = (name) => new Bird(name);

            // out is covariant (? extends T in java) --> Covariance enables you to use a more derived type than that specified by the generic parameter. 
            // https://msdn.microsoft.com/en-us/library/dd469487.aspx
            birdFactory = pigeonFactory;

            Assert.AreEqual(birdFactory, pigeonFactory);
            // in is not contravariant --> this is a compilation error
            //pigeonFactory = birdFactory;
        }

        [TestMethod]
        public void Operator()
        {
            Assert.AreEqual(new CustomNumber(2),  new CustomNumber(1) + new CustomNumber(1));
            Assert.IsTrue(new CustomNumber(2) ==  new CustomNumber(1) + new CustomNumber(1));
            Assert.IsFalse(new CustomNumber(2) !=  new CustomNumber(1) + new CustomNumber(1));
        }

        [TestMethod]
        public void Struct()
        {
            //A struct type is a value type that is typically used to encapsulate small groups of related variables, such as the coordinates of a rectangle or the characteristics of an item in an inventory.The following example shows a simple struct declaration
            //https://msdn.microsoft.com/nl-be/library/ah19swz4.aspx
            var testStruct = new TestStruct();
            testStruct.AProperty = "TestValue";

            Assert.AreEqual("TestValue", testStruct.AProperty);
        }

        [TestMethod]
        public void ObjectInitializer()
        {
            //Object initializers let you assign values to any accessible fields or properties of an object at creation time without having to invoke a constructor followed by lines of assignment statements. 
            //https://msdn.microsoft.com/nl-be/library/bb384062.aspx
            var testStruct = new TestStruct { AProperty = "TestValue" };

            Assert.AreEqual("TestValue", testStruct.AProperty);
        }

        [TestMethod]
        public void AnonymousObjectInitializer()
        {
            //https://msdn.microsoft.com/en-us/library/bb397696.aspx
            var pet = new { Age = 10, Name = "Fluffy" };
            // pet2 has same properties and same property order --> same type as pet
            var pet2 = new { Age = 20, Name = "Fluffiest" };
            // pet3 has same properties but different property order --> different type from pet
            var pet3 = new { Name = "Fluffiest", Age = 20 };

            Assert.AreEqual(10, pet.Age);
            Assert.AreEqual("Fluffy", pet.Name);
            Assert.AreNotEqual(pet3.GetType(), pet.GetType());
        }

        [TestMethod]
        public void CollectionInitializer()
        {
            var digits = new List<int> { 0 + 1, 12 % 3, (int) new Celsius(20).Degrees };

            CollectionAssert.AreEqual(new List<int> {1, 0, 20}, digits);
        }

        [TestMethod]
        public void At()
        {
            //use @ to be able to use reserved keywords as a variable name
            var @const = 1;

            Assert.AreEqual(1, @const);
        }
    }
}
