using System;
using System.Collections.Generic;
using System.Text;
using VxCompiler.Environment;

namespace VxCompiler.CodeEmission
{
   
    public enum Command 
    {
        call, load, pop, @out, nop, exit, ret
    }   

    public class Commands
    {
        public static string GetCommand(Command cmd)
        {
            return cmd.ToString();
        }

        public static string GetCommand(Command cmd, string name)
        {
            return cmd.ToString() + " " + name;
        }

        public static string GetCommand(Command cmd, VxcType type, params string[] arg)
        {
            string postFix = "";
            if (type == VxcType.ssingle || type == VxcType.usingle)
                postFix = "s";
            if (type == VxcType.squad || type == VxcType.uquad)
                postFix = "q";
            if (type == VxcType.sdouble || type == VxcType.udouble)
                postFix = "d";

            string argumentList = " ";
            switch (cmd)
            {
                case Command.load:
                case Command.pop:
                    if (arg.Length == 1)
                        argumentList += arg[0];
                    else
                        // error?
                        argumentList += "0";
                    break;
            }
            return cmd.ToString() + postFix + argumentList;
        }
    }
}
