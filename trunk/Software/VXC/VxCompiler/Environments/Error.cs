using System;
using System.Collections.Generic;
using System.Text;
using VXC.node;
using VXC.lexer;
using VXC.parser;

namespace VxCompiler.Environment
{
    public enum ErrorType
    {
        InvalidInteger, InvalidAdress, InvalidInitializer, 
        VoidTypeNotReturnType
    }

    public static class Error
    {        
        public static void Fatal(ErrorType type, Token token)
        {
            string error = token.Line.ToString() + ", " + token.Pos.ToString() + " : " + type.ToString() + " : " + token.Text;
            throw new Exception(error);
        }

        public static void Fatal(ErrorType type)
        {            
            throw new Exception(type.ToString());
        }

        public static void Unsupported(string msg)
        {
            string error = msg + " is not supported yet!";
            throw new Exception(error);
        }
    }
}
