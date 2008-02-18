/* This file was generated by SableCC (http://www.sablecc.org/). */

using System;
using System.Collections;
using VXC.node;

namespace VXC.analysis {


public interface Analysis : Switch
{
    Object GetIn(Node node);
    void SetIn(Node node, Object inobj);
    Object GetOut(Node node);
    void SetOut(Node node, Object outobj);

    void CaseStart(Start node);
    void CaseASrcFile(ASrcFile node);
    void CaseAMethodDeclDecl(AMethodDeclDecl node);
    void CaseAGlobalDeclDecl(AGlobalDeclDecl node);
    void CaseAMethodDecl(AMethodDecl node);
    void CaseALocalDecl(ALocalDecl node);
    void CaseAGlobalDecl(AGlobalDecl node);
    void CaseAVoidType(AVoidType node);
    void CaseASsingleType(ASsingleType node);
    void CaseAUsingleType(AUsingleType node);
    void CaseASdoubleType(ASdoubleType node);
    void CaseAUdoubleType(AUdoubleType node);
    void CaseASquadType(ASquadType node);
    void CaseAUquadType(AUquadType node);
    void CaseAFloatType(AFloatType node);
    void CaseAStringType(AStringType node);

    void CaseTVoid(TVoid node);
    void CaseTSsingle(TSsingle node);
    void CaseTUsingle(TUsingle node);
    void CaseTSdouble(TSdouble node);
    void CaseTUdouble(TUdouble node);
    void CaseTSquad(TSquad node);
    void CaseTUquad(TUquad node);
    void CaseTFloat(TFloat node);
    void CaseTString(TString node);
    void CaseTIdentifier(TIdentifier node);
    void CaseTComment(TComment node);
    void CaseTWhitespace(TWhitespace node);
    void CaseTLPar(TLPar node);
    void CaseTRPar(TRPar node);
    void CaseTSemicolon(TSemicolon node);
    void CaseTComma(TComma node);
    void CaseEOF(EOF node);
}


public class AnalysisAdapter : Analysis
{
    private Hashtable inhash;
    private Hashtable outhash;

    public virtual Object GetIn(Node node)
    {
        if(inhash == null)
        {
            return null;
        }

        return inhash[node];
    }

    public virtual void SetIn(Node node, Object inobj)
    {
        if(this.inhash == null)
        {
            this.inhash = new Hashtable(1);
        }

        if(inobj != null)
        {
            this.inhash[node] = inobj;
        }
        else
        {
            this.inhash.Remove(node);
        }
    }
    public virtual Object GetOut(Node node)
    {
        if(outhash == null)
        {
            return null;
        }

        return outhash[node];
    }

    public virtual void SetOut(Node node, Object outobj)
    {
        if(this.outhash == null)
        {
            this.outhash = new Hashtable(1);
        }

        if(outobj != null)
        {
            this.outhash[node] = outobj;
        }
        else
        {
            this.outhash.Remove(node);
        }
    }
    public virtual void CaseStart(Start node)
    {
        DefaultCase(node);
    }

    public virtual void CaseASrcFile(ASrcFile node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAMethodDeclDecl(AMethodDeclDecl node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAGlobalDeclDecl(AGlobalDeclDecl node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAMethodDecl(AMethodDecl node)
    {
        DefaultCase(node);
    }
    public virtual void CaseALocalDecl(ALocalDecl node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAGlobalDecl(AGlobalDecl node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAVoidType(AVoidType node)
    {
        DefaultCase(node);
    }
    public virtual void CaseASsingleType(ASsingleType node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAUsingleType(AUsingleType node)
    {
        DefaultCase(node);
    }
    public virtual void CaseASdoubleType(ASdoubleType node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAUdoubleType(AUdoubleType node)
    {
        DefaultCase(node);
    }
    public virtual void CaseASquadType(ASquadType node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAUquadType(AUquadType node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAFloatType(AFloatType node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAStringType(AStringType node)
    {
        DefaultCase(node);
    }

    public virtual void CaseTVoid(TVoid node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTSsingle(TSsingle node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTUsingle(TUsingle node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTSdouble(TSdouble node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTUdouble(TUdouble node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTSquad(TSquad node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTUquad(TUquad node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTFloat(TFloat node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTString(TString node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTIdentifier(TIdentifier node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTComment(TComment node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTWhitespace(TWhitespace node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTLPar(TLPar node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTRPar(TRPar node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTSemicolon(TSemicolon node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTComma(TComma node)
    {
        DefaultCase(node);
    }

    public virtual void CaseEOF(EOF node)
    {
        DefaultCase(node);
    }

    public virtual void DefaultCase(Node node)
    {
    }
}


public class DepthFirstAdapter : AnalysisAdapter
{
    public virtual void InStart(Start node)
    {
        DefaultIn(node);
    }

    public virtual void OutStart(Start node)
    {
        DefaultOut(node);
    }

    public virtual void DefaultIn(Node node)
    {
    }

    public virtual void DefaultOut(Node node)
    {
    }

    public override void CaseStart(Start node)
    {
        InStart(node);
        node.GetPSrcFile().Apply(this);
        node.GetEOF().Apply(this);
        OutStart(node);
    }

    public virtual void InASrcFile(ASrcFile node)
    {
        DefaultIn(node);
    }

    public virtual void OutASrcFile(ASrcFile node)
    {
        DefaultOut(node);
    }

    public override void CaseASrcFile(ASrcFile node)
    {
        InASrcFile(node);
        {
            Object[] temp = new Object[node.GetDeclarations().Count];
            node.GetDeclarations().CopyTo(temp, 0);
            for(int i = 0; i < temp.Length; i++)
            {
                ((PDecl) temp[i]).Apply(this);
            }
        }
        OutASrcFile(node);
    }
    public virtual void InAMethodDeclDecl(AMethodDeclDecl node)
    {
        DefaultIn(node);
    }

    public virtual void OutAMethodDeclDecl(AMethodDeclDecl node)
    {
        DefaultOut(node);
    }

    public override void CaseAMethodDeclDecl(AMethodDeclDecl node)
    {
        InAMethodDeclDecl(node);
        if(node.GetDecl() != null)
        {
            node.GetDecl().Apply(this);
        }
        OutAMethodDeclDecl(node);
    }
    public virtual void InAGlobalDeclDecl(AGlobalDeclDecl node)
    {
        DefaultIn(node);
    }

    public virtual void OutAGlobalDeclDecl(AGlobalDeclDecl node)
    {
        DefaultOut(node);
    }

    public override void CaseAGlobalDeclDecl(AGlobalDeclDecl node)
    {
        InAGlobalDeclDecl(node);
        if(node.GetDecl() != null)
        {
            node.GetDecl().Apply(this);
        }
        OutAGlobalDeclDecl(node);
    }
    public virtual void InAMethodDecl(AMethodDecl node)
    {
        DefaultIn(node);
    }

    public virtual void OutAMethodDecl(AMethodDecl node)
    {
        DefaultOut(node);
    }

    public override void CaseAMethodDecl(AMethodDecl node)
    {
        InAMethodDecl(node);
        if(node.GetType() != null)
        {
            node.GetType().Apply(this);
        }
        if(node.GetName() != null)
        {
            node.GetName().Apply(this);
        }
        {
            Object[] temp = new Object[node.GetArgs().Count];
            node.GetArgs().CopyTo(temp, 0);
            for(int i = 0; i < temp.Length; i++)
            {
                ((PLocalDecl) temp[i]).Apply(this);
            }
        }
        OutAMethodDecl(node);
    }
    public virtual void InALocalDecl(ALocalDecl node)
    {
        DefaultIn(node);
    }

    public virtual void OutALocalDecl(ALocalDecl node)
    {
        DefaultOut(node);
    }

    public override void CaseALocalDecl(ALocalDecl node)
    {
        InALocalDecl(node);
        if(node.GetType() != null)
        {
            node.GetType().Apply(this);
        }
        if(node.GetName() != null)
        {
            node.GetName().Apply(this);
        }
        OutALocalDecl(node);
    }
    public virtual void InAGlobalDecl(AGlobalDecl node)
    {
        DefaultIn(node);
    }

    public virtual void OutAGlobalDecl(AGlobalDecl node)
    {
        DefaultOut(node);
    }

    public override void CaseAGlobalDecl(AGlobalDecl node)
    {
        InAGlobalDecl(node);
        if(node.GetType() != null)
        {
            node.GetType().Apply(this);
        }
        if(node.GetName() != null)
        {
            node.GetName().Apply(this);
        }
        OutAGlobalDecl(node);
    }
    public virtual void InAVoidType(AVoidType node)
    {
        DefaultIn(node);
    }

    public virtual void OutAVoidType(AVoidType node)
    {
        DefaultOut(node);
    }

    public override void CaseAVoidType(AVoidType node)
    {
        InAVoidType(node);
        OutAVoidType(node);
    }
    public virtual void InASsingleType(ASsingleType node)
    {
        DefaultIn(node);
    }

    public virtual void OutASsingleType(ASsingleType node)
    {
        DefaultOut(node);
    }

    public override void CaseASsingleType(ASsingleType node)
    {
        InASsingleType(node);
        OutASsingleType(node);
    }
    public virtual void InAUsingleType(AUsingleType node)
    {
        DefaultIn(node);
    }

    public virtual void OutAUsingleType(AUsingleType node)
    {
        DefaultOut(node);
    }

    public override void CaseAUsingleType(AUsingleType node)
    {
        InAUsingleType(node);
        OutAUsingleType(node);
    }
    public virtual void InASdoubleType(ASdoubleType node)
    {
        DefaultIn(node);
    }

    public virtual void OutASdoubleType(ASdoubleType node)
    {
        DefaultOut(node);
    }

    public override void CaseASdoubleType(ASdoubleType node)
    {
        InASdoubleType(node);
        OutASdoubleType(node);
    }
    public virtual void InAUdoubleType(AUdoubleType node)
    {
        DefaultIn(node);
    }

    public virtual void OutAUdoubleType(AUdoubleType node)
    {
        DefaultOut(node);
    }

    public override void CaseAUdoubleType(AUdoubleType node)
    {
        InAUdoubleType(node);
        OutAUdoubleType(node);
    }
    public virtual void InASquadType(ASquadType node)
    {
        DefaultIn(node);
    }

    public virtual void OutASquadType(ASquadType node)
    {
        DefaultOut(node);
    }

    public override void CaseASquadType(ASquadType node)
    {
        InASquadType(node);
        OutASquadType(node);
    }
    public virtual void InAUquadType(AUquadType node)
    {
        DefaultIn(node);
    }

    public virtual void OutAUquadType(AUquadType node)
    {
        DefaultOut(node);
    }

    public override void CaseAUquadType(AUquadType node)
    {
        InAUquadType(node);
        OutAUquadType(node);
    }
    public virtual void InAFloatType(AFloatType node)
    {
        DefaultIn(node);
    }

    public virtual void OutAFloatType(AFloatType node)
    {
        DefaultOut(node);
    }

    public override void CaseAFloatType(AFloatType node)
    {
        InAFloatType(node);
        OutAFloatType(node);
    }
    public virtual void InAStringType(AStringType node)
    {
        DefaultIn(node);
    }

    public virtual void OutAStringType(AStringType node)
    {
        DefaultOut(node);
    }

    public override void CaseAStringType(AStringType node)
    {
        InAStringType(node);
        OutAStringType(node);
    }
}


public class ReversedDepthFirstAdapter : AnalysisAdapter
{
    public virtual void InStart(Start node)
    {
        DefaultIn(node);
    }

    public virtual void OutStart(Start node)
    {
        DefaultOut(node);
    }

    public virtual void DefaultIn(Node node)
    {
    }

    public virtual void DefaultOut(Node node)
    {
    }

    public override void CaseStart(Start node)
    {
        InStart(node);
        node.GetEOF().Apply(this);
        node.GetPSrcFile().Apply(this);
        OutStart(node);
    }

    public virtual void InASrcFile(ASrcFile node)
    {
        DefaultIn(node);
    }

    public virtual void OutASrcFile(ASrcFile node)
    {
        DefaultOut(node);
    }

    public override void CaseASrcFile(ASrcFile node)
    {
        InASrcFile(node);
        {
            Object[] temp = new Object[node.GetDeclarations().Count];
            node.GetDeclarations().CopyTo(temp, 0);
            for(int i = temp.Length - 1; i >= 0; i--)
            {
                ((PDecl) temp[i]).Apply(this);
            }
        }
        OutASrcFile(node);
    }
    public virtual void InAMethodDeclDecl(AMethodDeclDecl node)
    {
        DefaultIn(node);
    }

    public virtual void OutAMethodDeclDecl(AMethodDeclDecl node)
    {
        DefaultOut(node);
    }

    public override void CaseAMethodDeclDecl(AMethodDeclDecl node)
    {
        InAMethodDeclDecl(node);
        if(node.GetDecl() != null)
        {
            node.GetDecl().Apply(this);
        }
        OutAMethodDeclDecl(node);
    }
    public virtual void InAGlobalDeclDecl(AGlobalDeclDecl node)
    {
        DefaultIn(node);
    }

    public virtual void OutAGlobalDeclDecl(AGlobalDeclDecl node)
    {
        DefaultOut(node);
    }

    public override void CaseAGlobalDeclDecl(AGlobalDeclDecl node)
    {
        InAGlobalDeclDecl(node);
        if(node.GetDecl() != null)
        {
            node.GetDecl().Apply(this);
        }
        OutAGlobalDeclDecl(node);
    }
    public virtual void InAMethodDecl(AMethodDecl node)
    {
        DefaultIn(node);
    }

    public virtual void OutAMethodDecl(AMethodDecl node)
    {
        DefaultOut(node);
    }

    public override void CaseAMethodDecl(AMethodDecl node)
    {
        InAMethodDecl(node);
        {
            Object[] temp = new Object[node.GetArgs().Count];
            node.GetArgs().CopyTo(temp, 0);
            for(int i = temp.Length - 1; i >= 0; i--)
            {
                ((PLocalDecl) temp[i]).Apply(this);
            }
        }
        if(node.GetName() != null)
        {
            node.GetName().Apply(this);
        }
        if(node.GetType() != null)
        {
            node.GetType().Apply(this);
        }
        OutAMethodDecl(node);
    }
    public virtual void InALocalDecl(ALocalDecl node)
    {
        DefaultIn(node);
    }

    public virtual void OutALocalDecl(ALocalDecl node)
    {
        DefaultOut(node);
    }

    public override void CaseALocalDecl(ALocalDecl node)
    {
        InALocalDecl(node);
        if(node.GetName() != null)
        {
            node.GetName().Apply(this);
        }
        if(node.GetType() != null)
        {
            node.GetType().Apply(this);
        }
        OutALocalDecl(node);
    }
    public virtual void InAGlobalDecl(AGlobalDecl node)
    {
        DefaultIn(node);
    }

    public virtual void OutAGlobalDecl(AGlobalDecl node)
    {
        DefaultOut(node);
    }

    public override void CaseAGlobalDecl(AGlobalDecl node)
    {
        InAGlobalDecl(node);
        if(node.GetName() != null)
        {
            node.GetName().Apply(this);
        }
        if(node.GetType() != null)
        {
            node.GetType().Apply(this);
        }
        OutAGlobalDecl(node);
    }
    public virtual void InAVoidType(AVoidType node)
    {
        DefaultIn(node);
    }

    public virtual void OutAVoidType(AVoidType node)
    {
        DefaultOut(node);
    }

    public override void CaseAVoidType(AVoidType node)
    {
        InAVoidType(node);
        OutAVoidType(node);
    }
    public virtual void InASsingleType(ASsingleType node)
    {
        DefaultIn(node);
    }

    public virtual void OutASsingleType(ASsingleType node)
    {
        DefaultOut(node);
    }

    public override void CaseASsingleType(ASsingleType node)
    {
        InASsingleType(node);
        OutASsingleType(node);
    }
    public virtual void InAUsingleType(AUsingleType node)
    {
        DefaultIn(node);
    }

    public virtual void OutAUsingleType(AUsingleType node)
    {
        DefaultOut(node);
    }

    public override void CaseAUsingleType(AUsingleType node)
    {
        InAUsingleType(node);
        OutAUsingleType(node);
    }
    public virtual void InASdoubleType(ASdoubleType node)
    {
        DefaultIn(node);
    }

    public virtual void OutASdoubleType(ASdoubleType node)
    {
        DefaultOut(node);
    }

    public override void CaseASdoubleType(ASdoubleType node)
    {
        InASdoubleType(node);
        OutASdoubleType(node);
    }
    public virtual void InAUdoubleType(AUdoubleType node)
    {
        DefaultIn(node);
    }

    public virtual void OutAUdoubleType(AUdoubleType node)
    {
        DefaultOut(node);
    }

    public override void CaseAUdoubleType(AUdoubleType node)
    {
        InAUdoubleType(node);
        OutAUdoubleType(node);
    }
    public virtual void InASquadType(ASquadType node)
    {
        DefaultIn(node);
    }

    public virtual void OutASquadType(ASquadType node)
    {
        DefaultOut(node);
    }

    public override void CaseASquadType(ASquadType node)
    {
        InASquadType(node);
        OutASquadType(node);
    }
    public virtual void InAUquadType(AUquadType node)
    {
        DefaultIn(node);
    }

    public virtual void OutAUquadType(AUquadType node)
    {
        DefaultOut(node);
    }

    public override void CaseAUquadType(AUquadType node)
    {
        InAUquadType(node);
        OutAUquadType(node);
    }
    public virtual void InAFloatType(AFloatType node)
    {
        DefaultIn(node);
    }

    public virtual void OutAFloatType(AFloatType node)
    {
        DefaultOut(node);
    }

    public override void CaseAFloatType(AFloatType node)
    {
        InAFloatType(node);
        OutAFloatType(node);
    }
    public virtual void InAStringType(AStringType node)
    {
        DefaultIn(node);
    }

    public virtual void OutAStringType(AStringType node)
    {
        DefaultOut(node);
    }

    public override void CaseAStringType(AStringType node)
    {
        InAStringType(node);
        OutAStringType(node);
    }
}
} // namespace VXC.analysis