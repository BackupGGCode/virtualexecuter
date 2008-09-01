using System;
using System.Collections.Generic;
using System.Text;
using VXC.node;

namespace VxCompiler.Environment
{  
    public class TypeEnvironment
    {
        public static string GetTypename(PType type)
        {
            if (type.GetType() == typeof(ABooleanType))
            {
                return "bool";
            }
            if (type.GetType() == typeof(ASingleType))
            {
                return "single";
            }
            if (type.GetType() == typeof(ADoubleType))
            {
                return "double";
            }
            if (type.GetType() == typeof(AQuadType))
            {
                return "quad";
            }
            if (type.GetType() == typeof(AVoidType))
            {
                return "void";
            }            
            return "unknown";
        }
        public static int SizeOfType(PType type)
        {          
            if (type.GetType() == typeof(ASingleType))
            {
                return 1;
            }           
            if (type.GetType() == typeof(ADoubleType))
            {
                return 2;
            }
            if (type.GetType() == typeof(AQuadType))
            {
                return 4;
            }
            return 0;
        }
        public static bool AssignableTo(PType rhs, PType lhs)
        {
            return (TypeEnvironment.SizeOfType(rhs) <= TypeEnvironment.SizeOfType(lhs));
        }               
    }
}
