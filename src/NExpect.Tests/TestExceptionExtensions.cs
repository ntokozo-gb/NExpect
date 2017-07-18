﻿using System;
using System.Net.NetworkInformation;
using NUnit.Framework;
using NExpect.Extensions;
using static NExpect.Implementations.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests
{
    [TestFixture]
    public class TestExceptionExtensions
    {
        [Test]
        public void Throw_WithNoGenericType_WhenSUTThrows_ShouldNotThrow()
        {
            // Arrange
            // Pre-Assert
            // Act
            Assert.DoesNotThrow(
                () => 
                {
                    Expect(() => 
                    {
                        throw new Exception(GetRandomString());
                    }).To.Throw();
                }
            );
            // Assert
        }

        [Test]
        public void Throw_WithNoGenericType_WhenSUTDoesNotThrow_ShouldThrow()
        {
            // Arrange
            // Pre-Assert
            // Act
            Assert.Throws<AssertionException>(() =>
            {
                Expect(() => { }).To.Throw();
            });
            // Assert
        }

        [Test]
        public void Throw_WithNoGenericType_WhenThrows_ShouldBeAbleToContinueWith_WithMessage_HappyPath()
        {
            // Arrange
            var expected = GetRandomString();
            // Pre-Assert
            // Act
            Assert.DoesNotThrow(() =>
            {
                Expect(() =>
                {
                    throw new Exception(expected);
                }).To.Throw()
                .With.Message.Containing(expected);
            });
            // Assert
        }

        [Test]
        public void Throw_WithNoGenericType_WhenThrows_ShouldBeAbleToContinueWith_WithMessage_SadPath()
        {
            // Arrange
            var expected = GetRandomString();
            var other = GetAnother(expected);
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(() =>
                {
                    throw new Exception(other);
                }).To.Throw()
                .With.Message.Containing(expected);
            }, Throws.Exception.InstanceOf<AssertionException>()
                    .With.Message.Contains($"to contain \"{expected}\""));
            // Assert
        }

        [Test]
        public void Throw_WithGenericType_WhenThrowsThatType_ShouldNotThrow()
        {
            // Arrange
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(() => throw new InvalidOperationException("moo"))
                    .To.Throw<InvalidOperationException>();
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void Throw_Negated_WithGenericType_WhenThrowsThatType_ShouldThrow()
        {
            // Arrange
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(() => throw new InvalidOperationException("moo"))
                    .Not.To.Throw<InvalidOperationException>();
            }, Throws.Exception.InstanceOf<AssertionException>()
                .With.Message.Matches(
                  $"Expected not to throw an exception of type (System.|){typeof(InvalidOperationException).Name}"
                )
            );
            // Assert
        }

        [Test]
        public void Throw_WithGenericType_WhenThrowsAnotherTypeType_ShouldThrow()
        {
            // Arrange
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(() => throw new InvalidOperationException("moo"))
                    .To.Throw<NotImplementedException>();
            }, Throws.Exception.InstanceOf<AssertionException>()
                .With.Message.Matches(
                    $"Expected to throw an exception of type (System.|){typeof(NotImplementedException).Name} but {typeof(InvalidOperationException).Name} was thrown instead"
                ));
            // Assert
        }

        [Test]
        public void Throw_WithGenericType_ShouldContinueOnToMessageTest_HappyPath()
        {
            // Arrange
            var expected = GetRandomString();

            // Pre-Assert

            // Act
            Assert.That(() =>
            {
                Expect(() => throw new AccessViolationException(expected))
                    .To.Throw<AccessViolationException>()
                    .With.Message.Containing(expected);
            }, Throws.Nothing);

            // Assert
        }

        [Test]
        public void Throw_WithGenericType_ShouldContinueOnToMessageTest_HappyPath_TestingEqualTo()
        {
            // Arrange
            var expected = GetRandomString();

            // Pre-Assert

            // Act
            Assert.That(() =>
            {
                Expect(() => throw new AccessViolationException(expected))
                    .To.Throw<AccessViolationException>()
                    .With.Message.Equal.To(expected);
            }, Throws.Nothing);

            // Assert
        }

        [Test]
        public void Throw_WithGenericType_ShouldContinueOnToMessageTest_SadPath_TestingEqualTo()
        {
            // Arrange
            var expected = GetRandomString();
            var unexpected = GetAnother(expected);
            // Pre-Assert

            // Act
            Assert.That(() =>
            {
                Expect(() => throw new AccessViolationException(unexpected))
                    .To.Throw<AccessViolationException>()
                    .With.Message.Equal.To(expected);
            }, Throws.Exception.InstanceOf<AssertionException>()
                .With.Message.Contains($"Expected \"{unexpected}\" to equal \"{expected}\""));

            // Assert
        }

    }
}