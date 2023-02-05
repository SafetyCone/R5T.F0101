using System;


namespace R5T.F0101
{
    public class Assertion : IAssertion
    {
        #region Infrastructure

        public static IAssertion Instance { get; } = new Assertion();


        private Assertion()
        {
        }

        #endregion
    }
}
