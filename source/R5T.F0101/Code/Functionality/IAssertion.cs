using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using R5T.T0120;
using R5T.T0132;


namespace R5T.F0101
{
    [FunctionalityMarker]
    public partial interface IAssertion : IFunctionalityMarker
    {
        public void AreEqual<T>(
            T expected,
            T actual)
        {
            Assert.AreEqual(expected, actual);
        }

        public void AreEqual_ForArray<T>(
            T[] expected,
            T[] actual)
        {
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Allows explicitly stating that an action should not thrown an exception.
        /// With MSTest, if code throws an exception the test will fail. However, relying on this behavior means that test code will lack an assertion, which makes these test look different from all other tests.
        /// This method allows a conforming assertion to be made in the case of testing for no exceptions.
        /// </summary>
        /// <remarks>
        /// From: https://stackoverflow.com/a/9417242
        /// </remarks>
        public async Task DoesNotThrowExceptionAsync(
            Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception exception)
            {
                Assert.Fail($"Expected no exception, found:\n{exception.Message}");
            }
        }

        public void IsNull(
            object value)
        {
            Assert.IsNull(value);
        }

        public void ThrowsException<TException>(
            Action action)
            where TException : Exception
        {
            Assert.ThrowsException<TException>(action);
        }

        public void ThrowsException<TException>(
            Action action,
            string expectedMessageOfException)
            where TException : Exception
        {
            var exceptionWasThrown = false;
            var exceptionMessageWasAsExpected = false;

            try
            {
                action();
            }
            catch (TException ex)
            {
                exceptionWasThrown = true;

                exceptionMessageWasAsExpected = ex.Message == expectedMessageOfException;
            }

            if (!exceptionWasThrown)
            {
                Assert.Fail($"Expected an exception, but no exception occurred.");
            }

            if (!exceptionMessageWasAsExpected)
            {
                Assert.Fail($"Exception message was not as expected.\nExpected:{expectedMessageOfException}");
            }
        }

        public async Task ThrowsExceptionAsync(
            Func<Task> action)
        {
            var exceptionWasThrown = false;
            try
            {
                await action();
            }
            catch
            {
                exceptionWasThrown = true;
            }

            if (!exceptionWasThrown)
            {
                Assert.Fail($"Expected an exception, but no exception occurred.");
            }
        }

        public async Task ThrowsExceptionAsync<TException>(
            Func<Task> action)
            where TException : Exception
        {
            await Assert.ThrowsExceptionAsync<TException>(action);
        }

        public void AreEqual<TInput, TOutput>(
            InputOutputPair<TInput, TOutput> expectedValue,
            TOutput encounteredValue)
        {
            Assert.AreEqual(expectedValue.Output, encounteredValue);
        }

        public void AreEqual<TInput, TOutput>(
            InputOutputPair<TInput, TOutput> expectedValue,
            TOutput encounteredValue,
            IEqualityComparer<TOutput> equalityComparer)
        {
            Assert.AreEqual(expectedValue.Output, encounteredValue, equalityComparer);
        }

        public void AreEqual<TInput, TOutput>(
            IEnumerable<InputOutputPair<TInput, TOutput>> expectedValues,
            IEnumerable<TOutput> encounteredValues)
        {
            var pairs = expectedValues.ZipWithEqualLengthRequirement(encounteredValues);

            foreach (var pair in pairs)
            {
                this.AreEqual(pair.Item1, pair.Item2);
            }
        }

        public void AreEqual<TInput, TOutput>(
            Expectation<TInput, TOutput> expectation,
            TOutput encounteredValue)
        {
            var areEqual = expectation.Verify(encounteredValue);

            Assert.IsTrue(areEqual);
        }

        public void AreEqual<TInput, TOutput>(
            IEnumerable<Expectation<TInput, TOutput>> expectations,
            IEnumerable<TOutput> encounteredValues)
        {
            var pairs = expectations.ZipWithEqualLengthRequirement(encounteredValues);

            foreach (var pair in pairs)
            {
                this.AreEqual(pair.Item1, pair.Item2);
            }
        }
    }
}
