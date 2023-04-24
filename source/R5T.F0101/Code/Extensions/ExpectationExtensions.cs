using R5T.T0120;
using System;


namespace R5T.F0101.Extensions
{
    public static class ExpectationExtensions
    {
        public static void Assert<TInput, TOutput>(this IExpectation<TInput, TOutput> expectation,
            Func<TInput, TOutput> function)
        {
            Instances.ExpectionOperator.Assert(
                expectation,
                function);
        }
    }
}
