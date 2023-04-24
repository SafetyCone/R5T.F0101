using System;


namespace R5T.F0101
{
    public class ExpectionOperator : IExpectionOperator
    {
        #region Infrastructure

        public static IExpectionOperator Instance { get; } = new ExpectionOperator();


        private ExpectionOperator()
        {
        }

        #endregion
    }
}
