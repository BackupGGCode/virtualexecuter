using System;
using System.Collections.Generic;
using System.Text;
using VXC;
using VXC.analysis;
using VXC.node;

namespace VxCompiler.Environment
{
    public class RuntimeEnvironment
    {        
        private Scope mGlobalScope;
        private Scope mCurrentScope;

        public RuntimeEnvironment()
        {
            mGlobalScope = new Scope();
            mCurrentScope = mGlobalScope;
        }

        public Scope GlobalScope
        {
            get { return mGlobalScope; }
        }

        public Scope CurrentScope
        {
            get { return mCurrentScope; }
            set { mCurrentScope = value; }
        }

        public static PType GetSmallestFit(long value)
        {
            // unsigned
            if (value >= 0 && value <= 255)
            {
                return new ASingleType();
            }
            else if (value >= 0 && value <= 65535)
            {
                return new ADoubleType();
            }
            else if (value >= 0 && value <= 4294967295 )
            {
                return new AQuadType();
            }
            else 
            {
                Error.Fatal(ErrorType.InvalidInteger);
                return null;
            }          
        }
        public static long ConvertToLong(TIntegerLiteral literal)
        {
            try
            {
                long result = Convert.ToInt64(literal.Text);
                return result;
            } 
            catch (Exception)
            {
                Error.Fatal(ErrorType.InvalidInteger, literal);
                return 0;
            }            
        }

        public bool GlobalSearch(PExp exp, out long value)
        {
            value = 0;
            Scope result = GlobalSearch(mGlobalScope, exp);
            if (result != null)
            {
                value = result.GetValueOf(exp);
                return true;
            }
            else
            {
                return false;
            }
        }

        private Scope GlobalSearch(Scope current, PExp exp)
        {
            if (current.Contains(exp))
            {
                return current;
            }
            foreach (Scope s in current.SubScopes)
            {
                Scope result = GlobalSearch(s, exp);
                if (result != null)
                    return result;
            }
            return null;
        }
    }
}
