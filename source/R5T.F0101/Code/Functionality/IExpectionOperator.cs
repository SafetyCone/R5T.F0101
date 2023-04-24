using System;
using R5T.T0120;
using R5T.T0132;


namespace R5T.F0101
{
    [FunctionalityMarker]
    public partial interface IExpectionOperator : IFunctionalityMarker
    {
        public void Assert<TInput, TOutput>(
            IExpectation<TInput, TOutput> expectation,
            Func<TInput, TOutput> function)
        {
            Instances.Assertion.AreEqual(
                expectation,
                function);
        }
    }
}
