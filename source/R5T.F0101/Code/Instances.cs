using System;


namespace R5T.F0101
{
    public static class Instances
    {
        public static IAssertion Assertion => F0101.Assertion.Instance;
        public static IExpectionOperator ExpectionOperator => F0101.ExpectionOperator.Instance;
    }
}