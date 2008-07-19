using System;
using System.Collections.Generic;
using System.Text;
using VxCompiler.Environment;

namespace VxCompiler.CodeEmission
{
    public class DataSegmentElement
    {
        private string mIdentifier;
        private VxcType mType;

        public VxcType Type
        {
            get { return mType; }
            set { mType = value; }
        }
	
        public string Identifier
        {
            get { return mIdentifier; }
            set { mIdentifier = value; }
        }


        public DataSegmentElement(string identifier, VxcType type)
        {
            mIdentifier = identifier;
            mType = type;
        }

        public static string GetDataSegmentKeyword()
        {
            return ".data";
        }

        public string Generate()
        {
            return mIdentifier + ": " + TypeEnvironment.GetSizeOf(mType) + "\n";
        }
    }
}
