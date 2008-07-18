using System;
using System.Collections.Generic;
using System.Text;
using VXC.node;

namespace VxCompiler.Environment
{
    public enum VxcType
    {
        ssingle, usingle, sdouble, udouble, uquad, squad
    }

    public class TypeEnvironment
    {
        public static VxcType GetVxcTypeOf(Type type)
        {
            if (type == typeof(ASsingleTypeSpecifier))
            {
                return VxcType.ssingle;
            }
            else if (type == typeof(ASsingleTypeSpecifier))
            {
                return VxcType.usingle;
            }
            else if (type == typeof(ASquadTypeSpecifier))
            {
                return VxcType.squad;
            }
            else if (type == typeof(AUquadTypeSpecifier))
            {
                return VxcType.uquad;
            }
            else if (type == typeof(ASdoubleTypeSpecifier))
            {
                return VxcType.sdouble;
            }
            else if (type == typeof(AUdoubleTypeSpecifier))
            {
                return VxcType.udouble;
            }
            return 0;
        }

        public static int GetSizeOf(VxcType type)
        {
            switch (type)
            {
                case VxcType.ssingle:
                case VxcType.usingle:
                    return 1;
                case VxcType.sdouble:
                case VxcType.udouble:
                    return 2;
                case VxcType.squad:
                case VxcType.uquad:
                    return 4;              
                default:
                    return 0;
            }
        }

        public static long GetValueOf(PExpression exp)
        {
            if (exp.GetType() == typeof(AIntegerConstantExpression))
            {
                long temp = 0;
                AIntegerConstantExpression expCast = (AIntegerConstantExpression)exp;
                if (long.TryParse(expCast.GetIntegerLiteral().Text, out temp))
                {
                    if (expCast.GetSign() != null)
                        temp = -temp;
                    return temp;
                }
                else
                    // error?
                    return 0;
            }
            else
                //error or composite?
                return 0;
        }
    }
}
