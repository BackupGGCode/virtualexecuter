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
    void CaseASourceFile(ASourceFile node);
    void CaseAVariableDeclaration(AVariableDeclaration node);
    void CaseAFunctionDeclaration(AFunctionDeclaration node);
    void CaseAPortDeclaration(APortDeclaration node);
    void CaseAFunctionDefinition(AFunctionDefinition node);
    void CaseAFormalParameter(AFormalParameter node);
    void CaseAVariableDefinition(AVariableDefinition node);
    void CaseAPortDefinition(APortDefinition node);
    void CaseAConstTypeQualifier(AConstTypeQualifier node);
    void CaseAVolatileTypeQualifier(AVolatileTypeQualifier node);
    void CaseAVoidTypeSpecifier(AVoidTypeSpecifier node);
    void CaseASsingleTypeSpecifier(ASsingleTypeSpecifier node);
    void CaseAUsingleTypeSpecifier(AUsingleTypeSpecifier node);
    void CaseASdoubleTypeSpecifier(ASdoubleTypeSpecifier node);
    void CaseAUdoubleTypeSpecifier(AUdoubleTypeSpecifier node);
    void CaseASquadTypeSpecifier(ASquadTypeSpecifier node);
    void CaseAUquadTypeSpecifier(AUquadTypeSpecifier node);
    void CaseAFloatTypeSpecifier(AFloatTypeSpecifier node);
    void CaseAStringTypeSpecifier(AStringTypeSpecifier node);
    void CaseAEmptyStatement(AEmptyStatement node);
    void CaseAVoidReturnStatement(AVoidReturnStatement node);
    void CaseAFormalsAndBody(AFormalsAndBody node);
    void CaseAIntegerConstantExpression(AIntegerConstantExpression node);

    void CaseTVoid(TVoid node);
    void CaseTSsingle(TSsingle node);
    void CaseTUsingle(TUsingle node);
    void CaseTSdouble(TSdouble node);
    void CaseTUdouble(TUdouble node);
    void CaseTSquad(TSquad node);
    void CaseTUquad(TUquad node);
    void CaseTFloat(TFloat node);
    void CaseTString(TString node);
    void CaseTConst(TConst node);
    void CaseTVolatile(TVolatile node);
    void CaseTReturn(TReturn node);
    void CaseTPort(TPort node);
    void CaseTComment(TComment node);
    void CaseTWhitespace(TWhitespace node);
    void CaseTLPar(TLPar node);
    void CaseTRPar(TRPar node);
    void CaseTLCurly(TLCurly node);
    void CaseTRCurly(TRCurly node);
    void CaseTSemicolon(TSemicolon node);
    void CaseTComma(TComma node);
    void CaseTMinus(TMinus node);
    void CaseTAt(TAt node);
    void CaseTIntegerLiteral(TIntegerLiteral node);
    void CaseTIdentifier(TIdentifier node);
    void CaseTAssign(TAssign node);
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

    public virtual void CaseASourceFile(ASourceFile node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAVariableDeclaration(AVariableDeclaration node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAFunctionDeclaration(AFunctionDeclaration node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAPortDeclaration(APortDeclaration node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAFunctionDefinition(AFunctionDefinition node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAFormalParameter(AFormalParameter node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAVariableDefinition(AVariableDefinition node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAPortDefinition(APortDefinition node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAConstTypeQualifier(AConstTypeQualifier node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAVolatileTypeQualifier(AVolatileTypeQualifier node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAVoidTypeSpecifier(AVoidTypeSpecifier node)
    {
        DefaultCase(node);
    }
    public virtual void CaseASsingleTypeSpecifier(ASsingleTypeSpecifier node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAUsingleTypeSpecifier(AUsingleTypeSpecifier node)
    {
        DefaultCase(node);
    }
    public virtual void CaseASdoubleTypeSpecifier(ASdoubleTypeSpecifier node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAUdoubleTypeSpecifier(AUdoubleTypeSpecifier node)
    {
        DefaultCase(node);
    }
    public virtual void CaseASquadTypeSpecifier(ASquadTypeSpecifier node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAUquadTypeSpecifier(AUquadTypeSpecifier node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAFloatTypeSpecifier(AFloatTypeSpecifier node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAStringTypeSpecifier(AStringTypeSpecifier node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAEmptyStatement(AEmptyStatement node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAVoidReturnStatement(AVoidReturnStatement node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAFormalsAndBody(AFormalsAndBody node)
    {
        DefaultCase(node);
    }
    public virtual void CaseAIntegerConstantExpression(AIntegerConstantExpression node)
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
    public virtual void CaseTConst(TConst node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTVolatile(TVolatile node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTReturn(TReturn node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTPort(TPort node)
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
    public virtual void CaseTLCurly(TLCurly node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTRCurly(TRCurly node)
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
    public virtual void CaseTMinus(TMinus node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTAt(TAt node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTIntegerLiteral(TIntegerLiteral node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTIdentifier(TIdentifier node)
    {
        DefaultCase(node);
    }
    public virtual void CaseTAssign(TAssign node)
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
        node.GetPSourceFile().Apply(this);
        node.GetEOF().Apply(this);
        OutStart(node);
    }

    public virtual void InASourceFile(ASourceFile node)
    {
        DefaultIn(node);
    }

    public virtual void OutASourceFile(ASourceFile node)
    {
        DefaultOut(node);
    }

    public override void CaseASourceFile(ASourceFile node)
    {
        InASourceFile(node);
        {
            Object[] temp = new Object[node.GetDeclaration().Count];
            node.GetDeclaration().CopyTo(temp, 0);
            for(int i = 0; i < temp.Length; i++)
            {
                ((PDeclaration) temp[i]).Apply(this);
            }
        }
        OutASourceFile(node);
    }
    public virtual void InAVariableDeclaration(AVariableDeclaration node)
    {
        DefaultIn(node);
    }

    public virtual void OutAVariableDeclaration(AVariableDeclaration node)
    {
        DefaultOut(node);
    }

    public override void CaseAVariableDeclaration(AVariableDeclaration node)
    {
        InAVariableDeclaration(node);
        if(node.GetDeclaration() != null)
        {
            node.GetDeclaration().Apply(this);
        }
        OutAVariableDeclaration(node);
    }
    public virtual void InAFunctionDeclaration(AFunctionDeclaration node)
    {
        DefaultIn(node);
    }

    public virtual void OutAFunctionDeclaration(AFunctionDeclaration node)
    {
        DefaultOut(node);
    }

    public override void CaseAFunctionDeclaration(AFunctionDeclaration node)
    {
        InAFunctionDeclaration(node);
        if(node.GetDeclaration() != null)
        {
            node.GetDeclaration().Apply(this);
        }
        OutAFunctionDeclaration(node);
    }
    public virtual void InAPortDeclaration(APortDeclaration node)
    {
        DefaultIn(node);
    }

    public virtual void OutAPortDeclaration(APortDeclaration node)
    {
        DefaultOut(node);
    }

    public override void CaseAPortDeclaration(APortDeclaration node)
    {
        InAPortDeclaration(node);
        if(node.GetDeclaration() != null)
        {
            node.GetDeclaration().Apply(this);
        }
        OutAPortDeclaration(node);
    }
    public virtual void InAFunctionDefinition(AFunctionDefinition node)
    {
        DefaultIn(node);
    }

    public virtual void OutAFunctionDefinition(AFunctionDefinition node)
    {
        DefaultOut(node);
    }

    public override void CaseAFunctionDefinition(AFunctionDefinition node)
    {
        InAFunctionDefinition(node);
        if(node.GetTypeSpecifier() != null)
        {
            node.GetTypeSpecifier().Apply(this);
        }
        if(node.GetName() != null)
        {
            node.GetName().Apply(this);
        }
        if(node.GetFormalsAndBody() != null)
        {
            node.GetFormalsAndBody().Apply(this);
        }
        OutAFunctionDefinition(node);
    }
    public virtual void InAFormalParameter(AFormalParameter node)
    {
        DefaultIn(node);
    }

    public virtual void OutAFormalParameter(AFormalParameter node)
    {
        DefaultOut(node);
    }

    public override void CaseAFormalParameter(AFormalParameter node)
    {
        InAFormalParameter(node);
        if(node.GetType() != null)
        {
            node.GetType().Apply(this);
        }
        if(node.GetName() != null)
        {
            node.GetName().Apply(this);
        }
        OutAFormalParameter(node);
    }
    public virtual void InAVariableDefinition(AVariableDefinition node)
    {
        DefaultIn(node);
    }

    public virtual void OutAVariableDefinition(AVariableDefinition node)
    {
        DefaultOut(node);
    }

    public override void CaseAVariableDefinition(AVariableDefinition node)
    {
        InAVariableDefinition(node);
        if(node.GetTypeSpecifier() != null)
        {
            node.GetTypeSpecifier().Apply(this);
        }
        if(node.GetTypeQualifier() != null)
        {
            node.GetTypeQualifier().Apply(this);
        }
        if(node.GetInit() != null)
        {
            node.GetInit().Apply(this);
        }
        if(node.GetName() != null)
        {
            node.GetName().Apply(this);
        }
        OutAVariableDefinition(node);
    }
    public virtual void InAPortDefinition(APortDefinition node)
    {
        DefaultIn(node);
    }

    public virtual void OutAPortDefinition(APortDefinition node)
    {
        DefaultOut(node);
    }

    public override void CaseAPortDefinition(APortDefinition node)
    {
        InAPortDefinition(node);
        if(node.GetName() != null)
        {
            node.GetName().Apply(this);
        }
        OutAPortDefinition(node);
    }
    public virtual void InAConstTypeQualifier(AConstTypeQualifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutAConstTypeQualifier(AConstTypeQualifier node)
    {
        DefaultOut(node);
    }

    public override void CaseAConstTypeQualifier(AConstTypeQualifier node)
    {
        InAConstTypeQualifier(node);
        OutAConstTypeQualifier(node);
    }
    public virtual void InAVolatileTypeQualifier(AVolatileTypeQualifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutAVolatileTypeQualifier(AVolatileTypeQualifier node)
    {
        DefaultOut(node);
    }

    public override void CaseAVolatileTypeQualifier(AVolatileTypeQualifier node)
    {
        InAVolatileTypeQualifier(node);
        OutAVolatileTypeQualifier(node);
    }
    public virtual void InAVoidTypeSpecifier(AVoidTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutAVoidTypeSpecifier(AVoidTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseAVoidTypeSpecifier(AVoidTypeSpecifier node)
    {
        InAVoidTypeSpecifier(node);
        OutAVoidTypeSpecifier(node);
    }
    public virtual void InASsingleTypeSpecifier(ASsingleTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutASsingleTypeSpecifier(ASsingleTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseASsingleTypeSpecifier(ASsingleTypeSpecifier node)
    {
        InASsingleTypeSpecifier(node);
        OutASsingleTypeSpecifier(node);
    }
    public virtual void InAUsingleTypeSpecifier(AUsingleTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutAUsingleTypeSpecifier(AUsingleTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseAUsingleTypeSpecifier(AUsingleTypeSpecifier node)
    {
        InAUsingleTypeSpecifier(node);
        OutAUsingleTypeSpecifier(node);
    }
    public virtual void InASdoubleTypeSpecifier(ASdoubleTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutASdoubleTypeSpecifier(ASdoubleTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseASdoubleTypeSpecifier(ASdoubleTypeSpecifier node)
    {
        InASdoubleTypeSpecifier(node);
        OutASdoubleTypeSpecifier(node);
    }
    public virtual void InAUdoubleTypeSpecifier(AUdoubleTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutAUdoubleTypeSpecifier(AUdoubleTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseAUdoubleTypeSpecifier(AUdoubleTypeSpecifier node)
    {
        InAUdoubleTypeSpecifier(node);
        OutAUdoubleTypeSpecifier(node);
    }
    public virtual void InASquadTypeSpecifier(ASquadTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutASquadTypeSpecifier(ASquadTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseASquadTypeSpecifier(ASquadTypeSpecifier node)
    {
        InASquadTypeSpecifier(node);
        OutASquadTypeSpecifier(node);
    }
    public virtual void InAUquadTypeSpecifier(AUquadTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutAUquadTypeSpecifier(AUquadTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseAUquadTypeSpecifier(AUquadTypeSpecifier node)
    {
        InAUquadTypeSpecifier(node);
        OutAUquadTypeSpecifier(node);
    }
    public virtual void InAFloatTypeSpecifier(AFloatTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutAFloatTypeSpecifier(AFloatTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseAFloatTypeSpecifier(AFloatTypeSpecifier node)
    {
        InAFloatTypeSpecifier(node);
        OutAFloatTypeSpecifier(node);
    }
    public virtual void InAStringTypeSpecifier(AStringTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutAStringTypeSpecifier(AStringTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseAStringTypeSpecifier(AStringTypeSpecifier node)
    {
        InAStringTypeSpecifier(node);
        OutAStringTypeSpecifier(node);
    }
    public virtual void InAEmptyStatement(AEmptyStatement node)
    {
        DefaultIn(node);
    }

    public virtual void OutAEmptyStatement(AEmptyStatement node)
    {
        DefaultOut(node);
    }

    public override void CaseAEmptyStatement(AEmptyStatement node)
    {
        InAEmptyStatement(node);
        OutAEmptyStatement(node);
    }
    public virtual void InAVoidReturnStatement(AVoidReturnStatement node)
    {
        DefaultIn(node);
    }

    public virtual void OutAVoidReturnStatement(AVoidReturnStatement node)
    {
        DefaultOut(node);
    }

    public override void CaseAVoidReturnStatement(AVoidReturnStatement node)
    {
        InAVoidReturnStatement(node);
        if(node.GetToken() != null)
        {
            node.GetToken().Apply(this);
        }
        OutAVoidReturnStatement(node);
    }
    public virtual void InAFormalsAndBody(AFormalsAndBody node)
    {
        DefaultIn(node);
    }

    public virtual void OutAFormalsAndBody(AFormalsAndBody node)
    {
        DefaultOut(node);
    }

    public override void CaseAFormalsAndBody(AFormalsAndBody node)
    {
        InAFormalsAndBody(node);
        {
            Object[] temp = new Object[node.GetFormals().Count];
            node.GetFormals().CopyTo(temp, 0);
            for(int i = 0; i < temp.Length; i++)
            {
                ((PFormalParameter) temp[i]).Apply(this);
            }
        }
        {
            Object[] temp = new Object[node.GetLocals().Count];
            node.GetLocals().CopyTo(temp, 0);
            for(int i = 0; i < temp.Length; i++)
            {
                ((PVariableDefinition) temp[i]).Apply(this);
            }
        }
        {
            Object[] temp = new Object[node.GetStatements().Count];
            node.GetStatements().CopyTo(temp, 0);
            for(int i = 0; i < temp.Length; i++)
            {
                ((PStatement) temp[i]).Apply(this);
            }
        }
        if(node.GetReturn() != null)
        {
            node.GetReturn().Apply(this);
        }
        OutAFormalsAndBody(node);
    }
    public virtual void InAIntegerConstantExpression(AIntegerConstantExpression node)
    {
        DefaultIn(node);
    }

    public virtual void OutAIntegerConstantExpression(AIntegerConstantExpression node)
    {
        DefaultOut(node);
    }

    public override void CaseAIntegerConstantExpression(AIntegerConstantExpression node)
    {
        InAIntegerConstantExpression(node);
        if(node.GetSign() != null)
        {
            node.GetSign().Apply(this);
        }
        if(node.GetIntegerLiteral() != null)
        {
            node.GetIntegerLiteral().Apply(this);
        }
        OutAIntegerConstantExpression(node);
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
        node.GetPSourceFile().Apply(this);
        OutStart(node);
    }

    public virtual void InASourceFile(ASourceFile node)
    {
        DefaultIn(node);
    }

    public virtual void OutASourceFile(ASourceFile node)
    {
        DefaultOut(node);
    }

    public override void CaseASourceFile(ASourceFile node)
    {
        InASourceFile(node);
        {
            Object[] temp = new Object[node.GetDeclaration().Count];
            node.GetDeclaration().CopyTo(temp, 0);
            for(int i = temp.Length - 1; i >= 0; i--)
            {
                ((PDeclaration) temp[i]).Apply(this);
            }
        }
        OutASourceFile(node);
    }
    public virtual void InAVariableDeclaration(AVariableDeclaration node)
    {
        DefaultIn(node);
    }

    public virtual void OutAVariableDeclaration(AVariableDeclaration node)
    {
        DefaultOut(node);
    }

    public override void CaseAVariableDeclaration(AVariableDeclaration node)
    {
        InAVariableDeclaration(node);
        if(node.GetDeclaration() != null)
        {
            node.GetDeclaration().Apply(this);
        }
        OutAVariableDeclaration(node);
    }
    public virtual void InAFunctionDeclaration(AFunctionDeclaration node)
    {
        DefaultIn(node);
    }

    public virtual void OutAFunctionDeclaration(AFunctionDeclaration node)
    {
        DefaultOut(node);
    }

    public override void CaseAFunctionDeclaration(AFunctionDeclaration node)
    {
        InAFunctionDeclaration(node);
        if(node.GetDeclaration() != null)
        {
            node.GetDeclaration().Apply(this);
        }
        OutAFunctionDeclaration(node);
    }
    public virtual void InAPortDeclaration(APortDeclaration node)
    {
        DefaultIn(node);
    }

    public virtual void OutAPortDeclaration(APortDeclaration node)
    {
        DefaultOut(node);
    }

    public override void CaseAPortDeclaration(APortDeclaration node)
    {
        InAPortDeclaration(node);
        if(node.GetDeclaration() != null)
        {
            node.GetDeclaration().Apply(this);
        }
        OutAPortDeclaration(node);
    }
    public virtual void InAFunctionDefinition(AFunctionDefinition node)
    {
        DefaultIn(node);
    }

    public virtual void OutAFunctionDefinition(AFunctionDefinition node)
    {
        DefaultOut(node);
    }

    public override void CaseAFunctionDefinition(AFunctionDefinition node)
    {
        InAFunctionDefinition(node);
        if(node.GetFormalsAndBody() != null)
        {
            node.GetFormalsAndBody().Apply(this);
        }
        if(node.GetName() != null)
        {
            node.GetName().Apply(this);
        }
        if(node.GetTypeSpecifier() != null)
        {
            node.GetTypeSpecifier().Apply(this);
        }
        OutAFunctionDefinition(node);
    }
    public virtual void InAFormalParameter(AFormalParameter node)
    {
        DefaultIn(node);
    }

    public virtual void OutAFormalParameter(AFormalParameter node)
    {
        DefaultOut(node);
    }

    public override void CaseAFormalParameter(AFormalParameter node)
    {
        InAFormalParameter(node);
        if(node.GetName() != null)
        {
            node.GetName().Apply(this);
        }
        if(node.GetType() != null)
        {
            node.GetType().Apply(this);
        }
        OutAFormalParameter(node);
    }
    public virtual void InAVariableDefinition(AVariableDefinition node)
    {
        DefaultIn(node);
    }

    public virtual void OutAVariableDefinition(AVariableDefinition node)
    {
        DefaultOut(node);
    }

    public override void CaseAVariableDefinition(AVariableDefinition node)
    {
        InAVariableDefinition(node);
        if(node.GetName() != null)
        {
            node.GetName().Apply(this);
        }
        if(node.GetInit() != null)
        {
            node.GetInit().Apply(this);
        }
        if(node.GetTypeQualifier() != null)
        {
            node.GetTypeQualifier().Apply(this);
        }
        if(node.GetTypeSpecifier() != null)
        {
            node.GetTypeSpecifier().Apply(this);
        }
        OutAVariableDefinition(node);
    }
    public virtual void InAPortDefinition(APortDefinition node)
    {
        DefaultIn(node);
    }

    public virtual void OutAPortDefinition(APortDefinition node)
    {
        DefaultOut(node);
    }

    public override void CaseAPortDefinition(APortDefinition node)
    {
        InAPortDefinition(node);
        if(node.GetName() != null)
        {
            node.GetName().Apply(this);
        }
        OutAPortDefinition(node);
    }
    public virtual void InAConstTypeQualifier(AConstTypeQualifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutAConstTypeQualifier(AConstTypeQualifier node)
    {
        DefaultOut(node);
    }

    public override void CaseAConstTypeQualifier(AConstTypeQualifier node)
    {
        InAConstTypeQualifier(node);
        OutAConstTypeQualifier(node);
    }
    public virtual void InAVolatileTypeQualifier(AVolatileTypeQualifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutAVolatileTypeQualifier(AVolatileTypeQualifier node)
    {
        DefaultOut(node);
    }

    public override void CaseAVolatileTypeQualifier(AVolatileTypeQualifier node)
    {
        InAVolatileTypeQualifier(node);
        OutAVolatileTypeQualifier(node);
    }
    public virtual void InAVoidTypeSpecifier(AVoidTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutAVoidTypeSpecifier(AVoidTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseAVoidTypeSpecifier(AVoidTypeSpecifier node)
    {
        InAVoidTypeSpecifier(node);
        OutAVoidTypeSpecifier(node);
    }
    public virtual void InASsingleTypeSpecifier(ASsingleTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutASsingleTypeSpecifier(ASsingleTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseASsingleTypeSpecifier(ASsingleTypeSpecifier node)
    {
        InASsingleTypeSpecifier(node);
        OutASsingleTypeSpecifier(node);
    }
    public virtual void InAUsingleTypeSpecifier(AUsingleTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutAUsingleTypeSpecifier(AUsingleTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseAUsingleTypeSpecifier(AUsingleTypeSpecifier node)
    {
        InAUsingleTypeSpecifier(node);
        OutAUsingleTypeSpecifier(node);
    }
    public virtual void InASdoubleTypeSpecifier(ASdoubleTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutASdoubleTypeSpecifier(ASdoubleTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseASdoubleTypeSpecifier(ASdoubleTypeSpecifier node)
    {
        InASdoubleTypeSpecifier(node);
        OutASdoubleTypeSpecifier(node);
    }
    public virtual void InAUdoubleTypeSpecifier(AUdoubleTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutAUdoubleTypeSpecifier(AUdoubleTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseAUdoubleTypeSpecifier(AUdoubleTypeSpecifier node)
    {
        InAUdoubleTypeSpecifier(node);
        OutAUdoubleTypeSpecifier(node);
    }
    public virtual void InASquadTypeSpecifier(ASquadTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutASquadTypeSpecifier(ASquadTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseASquadTypeSpecifier(ASquadTypeSpecifier node)
    {
        InASquadTypeSpecifier(node);
        OutASquadTypeSpecifier(node);
    }
    public virtual void InAUquadTypeSpecifier(AUquadTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutAUquadTypeSpecifier(AUquadTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseAUquadTypeSpecifier(AUquadTypeSpecifier node)
    {
        InAUquadTypeSpecifier(node);
        OutAUquadTypeSpecifier(node);
    }
    public virtual void InAFloatTypeSpecifier(AFloatTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutAFloatTypeSpecifier(AFloatTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseAFloatTypeSpecifier(AFloatTypeSpecifier node)
    {
        InAFloatTypeSpecifier(node);
        OutAFloatTypeSpecifier(node);
    }
    public virtual void InAStringTypeSpecifier(AStringTypeSpecifier node)
    {
        DefaultIn(node);
    }

    public virtual void OutAStringTypeSpecifier(AStringTypeSpecifier node)
    {
        DefaultOut(node);
    }

    public override void CaseAStringTypeSpecifier(AStringTypeSpecifier node)
    {
        InAStringTypeSpecifier(node);
        OutAStringTypeSpecifier(node);
    }
    public virtual void InAEmptyStatement(AEmptyStatement node)
    {
        DefaultIn(node);
    }

    public virtual void OutAEmptyStatement(AEmptyStatement node)
    {
        DefaultOut(node);
    }

    public override void CaseAEmptyStatement(AEmptyStatement node)
    {
        InAEmptyStatement(node);
        OutAEmptyStatement(node);
    }
    public virtual void InAVoidReturnStatement(AVoidReturnStatement node)
    {
        DefaultIn(node);
    }

    public virtual void OutAVoidReturnStatement(AVoidReturnStatement node)
    {
        DefaultOut(node);
    }

    public override void CaseAVoidReturnStatement(AVoidReturnStatement node)
    {
        InAVoidReturnStatement(node);
        if(node.GetToken() != null)
        {
            node.GetToken().Apply(this);
        }
        OutAVoidReturnStatement(node);
    }
    public virtual void InAFormalsAndBody(AFormalsAndBody node)
    {
        DefaultIn(node);
    }

    public virtual void OutAFormalsAndBody(AFormalsAndBody node)
    {
        DefaultOut(node);
    }

    public override void CaseAFormalsAndBody(AFormalsAndBody node)
    {
        InAFormalsAndBody(node);
        if(node.GetReturn() != null)
        {
            node.GetReturn().Apply(this);
        }
        {
            Object[] temp = new Object[node.GetStatements().Count];
            node.GetStatements().CopyTo(temp, 0);
            for(int i = temp.Length - 1; i >= 0; i--)
            {
                ((PStatement) temp[i]).Apply(this);
            }
        }
        {
            Object[] temp = new Object[node.GetLocals().Count];
            node.GetLocals().CopyTo(temp, 0);
            for(int i = temp.Length - 1; i >= 0; i--)
            {
                ((PVariableDefinition) temp[i]).Apply(this);
            }
        }
        {
            Object[] temp = new Object[node.GetFormals().Count];
            node.GetFormals().CopyTo(temp, 0);
            for(int i = temp.Length - 1; i >= 0; i--)
            {
                ((PFormalParameter) temp[i]).Apply(this);
            }
        }
        OutAFormalsAndBody(node);
    }
    public virtual void InAIntegerConstantExpression(AIntegerConstantExpression node)
    {
        DefaultIn(node);
    }

    public virtual void OutAIntegerConstantExpression(AIntegerConstantExpression node)
    {
        DefaultOut(node);
    }

    public override void CaseAIntegerConstantExpression(AIntegerConstantExpression node)
    {
        InAIntegerConstantExpression(node);
        if(node.GetIntegerLiteral() != null)
        {
            node.GetIntegerLiteral().Apply(this);
        }
        if(node.GetSign() != null)
        {
            node.GetSign().Apply(this);
        }
        OutAIntegerConstantExpression(node);
    }
}
} // namespace VXC.analysis
