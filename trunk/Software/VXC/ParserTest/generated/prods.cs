/* This file was generated by SableCC (http://www.sablecc.org/). */

using System;
using System.Collections;
using System.Text;

using  VXC.analysis;

namespace VXC.node {

public abstract class PSourceFile : Node
{
}

public abstract class PDeclaration : Node
{
}

public abstract class PFunctionDefinition : Node
{
}

public abstract class PFormalParameter : Node
{
}

public abstract class PVariableDefinition : Node
{
}

public abstract class PPortDefinition : Node
{
}

public abstract class PTypeQualifier : Node
{
}

public abstract class PTypeSpecifier : Node
{
}

public abstract class PStatement : Node
{
}

public abstract class PReturnStatement : Node
{
}

public abstract class PFormalsAndBody : Node
{
}

public abstract class PExpression : Node
{
}


public sealed class ASourceFile : PSourceFile
{
    private TypedList _declaration_;

    public ASourceFile ()
    {
        this._declaration_ = new TypedList(new Declaration_Cast(this));
    }

    public ASourceFile (
            IList _declaration_
    )
    {
        this._declaration_ = new TypedList(new Declaration_Cast(this));
        this._declaration_.Clear();
        this._declaration_.AddAll(_declaration_);
    }

    public override Object Clone()
    {
        return new ASourceFile (
            CloneList (_declaration_)
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseASourceFile(this);
    }

    public IList GetDeclaration ()
    {
        return _declaration_;
    }

    public void setDeclaration (IList list)
    {
        _declaration_.Clear();
        _declaration_.AddAll(list);
    }

    public override string ToString()
    {
        return ""
            + ToString (_declaration_)
        ;
    }

    internal override void RemoveChild(Node child)
    {
        if ( _declaration_.Contains(child) )
        {
            _declaration_.Remove(child);
            return;
        }
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
        for ( int i = 0; i < _declaration_.Count; i++ )
        {
            Node n = (Node)_declaration_[i];
            if(n == oldChild)
            {
                if(newChild != null)
                {
                    _declaration_[i] = newChild;
                    oldChild.Parent(null);
                    return;
                }

                _declaration_.RemoveAt(i);
                oldChild.Parent(null);
                return;
            }
        }
    }

    private class Declaration_Cast : Cast
    {
        ASourceFile obj;

        internal Declaration_Cast (ASourceFile obj)
        {
          this.obj = obj;
        }

        public Object Cast(Object o)
        {
            PDeclaration node = (PDeclaration) o;

            if((node.Parent() != null) &&
                (node.Parent() != obj))
            {
                node.Parent().RemoveChild(node);
            }

            if((node.Parent() == null) ||
                (node.Parent() != obj))
            {
                node.Parent(obj);
            }

            return node;
        }

        public Object UnCast(Object o)
        {
            PDeclaration node = (PDeclaration) o;
            node.Parent(null);
            return node;
        }
    }
}
public sealed class AVariableDeclaration : PDeclaration
{
    private PVariableDefinition _declaration_;

    public AVariableDeclaration ()
    {
    }

    public AVariableDeclaration (
            PVariableDefinition _declaration_
    )
    {
        SetDeclaration (_declaration_);
    }

    public override Object Clone()
    {
        return new AVariableDeclaration (
            (PVariableDefinition)CloneNode (_declaration_)
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAVariableDeclaration(this);
    }

    public PVariableDefinition GetDeclaration ()
    {
        return _declaration_;
    }

    public void SetDeclaration (PVariableDefinition node)
    {
        if(_declaration_ != null)
        {
            _declaration_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _declaration_ = node;
    }

    public override string ToString()
    {
        return ""
            + ToString (_declaration_)
        ;
    }

    internal override void RemoveChild(Node child)
    {
        if ( _declaration_ == child )
        {
            _declaration_ = null;
            return;
        }
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
        if ( _declaration_ == oldChild )
        {
            SetDeclaration ((PVariableDefinition) newChild);
            return;
        }
    }

}
public sealed class AFunctionDeclaration : PDeclaration
{
    private PFunctionDefinition _declaration_;

    public AFunctionDeclaration ()
    {
    }

    public AFunctionDeclaration (
            PFunctionDefinition _declaration_
    )
    {
        SetDeclaration (_declaration_);
    }

    public override Object Clone()
    {
        return new AFunctionDeclaration (
            (PFunctionDefinition)CloneNode (_declaration_)
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAFunctionDeclaration(this);
    }

    public PFunctionDefinition GetDeclaration ()
    {
        return _declaration_;
    }

    public void SetDeclaration (PFunctionDefinition node)
    {
        if(_declaration_ != null)
        {
            _declaration_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _declaration_ = node;
    }

    public override string ToString()
    {
        return ""
            + ToString (_declaration_)
        ;
    }

    internal override void RemoveChild(Node child)
    {
        if ( _declaration_ == child )
        {
            _declaration_ = null;
            return;
        }
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
        if ( _declaration_ == oldChild )
        {
            SetDeclaration ((PFunctionDefinition) newChild);
            return;
        }
    }

}
public sealed class APortDeclaration : PDeclaration
{
    private PPortDefinition _declaration_;

    public APortDeclaration ()
    {
    }

    public APortDeclaration (
            PPortDefinition _declaration_
    )
    {
        SetDeclaration (_declaration_);
    }

    public override Object Clone()
    {
        return new APortDeclaration (
            (PPortDefinition)CloneNode (_declaration_)
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAPortDeclaration(this);
    }

    public PPortDefinition GetDeclaration ()
    {
        return _declaration_;
    }

    public void SetDeclaration (PPortDefinition node)
    {
        if(_declaration_ != null)
        {
            _declaration_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _declaration_ = node;
    }

    public override string ToString()
    {
        return ""
            + ToString (_declaration_)
        ;
    }

    internal override void RemoveChild(Node child)
    {
        if ( _declaration_ == child )
        {
            _declaration_ = null;
            return;
        }
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
        if ( _declaration_ == oldChild )
        {
            SetDeclaration ((PPortDefinition) newChild);
            return;
        }
    }

}
public sealed class AFunctionDefinition : PFunctionDefinition
{
    private PTypeSpecifier _type_specifier_;
    private TIdentifier _name_;
    private PFormalsAndBody _formals_and_body_;

    public AFunctionDefinition ()
    {
    }

    public AFunctionDefinition (
            PTypeSpecifier _type_specifier_,
            TIdentifier _name_,
            PFormalsAndBody _formals_and_body_
    )
    {
        SetTypeSpecifier (_type_specifier_);
        SetName (_name_);
        SetFormalsAndBody (_formals_and_body_);
    }

    public override Object Clone()
    {
        return new AFunctionDefinition (
            (PTypeSpecifier)CloneNode (_type_specifier_),
            (TIdentifier)CloneNode (_name_),
            (PFormalsAndBody)CloneNode (_formals_and_body_)
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAFunctionDefinition(this);
    }

    public PTypeSpecifier GetTypeSpecifier ()
    {
        return _type_specifier_;
    }

    public void SetTypeSpecifier (PTypeSpecifier node)
    {
        if(_type_specifier_ != null)
        {
            _type_specifier_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _type_specifier_ = node;
    }
    public TIdentifier GetName ()
    {
        return _name_;
    }

    public void SetName (TIdentifier node)
    {
        if(_name_ != null)
        {
            _name_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _name_ = node;
    }
    public PFormalsAndBody GetFormalsAndBody ()
    {
        return _formals_and_body_;
    }

    public void SetFormalsAndBody (PFormalsAndBody node)
    {
        if(_formals_and_body_ != null)
        {
            _formals_and_body_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _formals_and_body_ = node;
    }

    public override string ToString()
    {
        return ""
            + ToString (_type_specifier_)
            + ToString (_name_)
            + ToString (_formals_and_body_)
        ;
    }

    internal override void RemoveChild(Node child)
    {
        if ( _type_specifier_ == child )
        {
            _type_specifier_ = null;
            return;
        }
        if ( _name_ == child )
        {
            _name_ = null;
            return;
        }
        if ( _formals_and_body_ == child )
        {
            _formals_and_body_ = null;
            return;
        }
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
        if ( _type_specifier_ == oldChild )
        {
            SetTypeSpecifier ((PTypeSpecifier) newChild);
            return;
        }
        if ( _name_ == oldChild )
        {
            SetName ((TIdentifier) newChild);
            return;
        }
        if ( _formals_and_body_ == oldChild )
        {
            SetFormalsAndBody ((PFormalsAndBody) newChild);
            return;
        }
    }

}
public sealed class AFormalParameter : PFormalParameter
{
    private PTypeSpecifier _type_;
    private TIdentifier _name_;

    public AFormalParameter ()
    {
    }

    public AFormalParameter (
            PTypeSpecifier _type_,
            TIdentifier _name_
    )
    {
        SetType (_type_);
        SetName (_name_);
    }

    public override Object Clone()
    {
        return new AFormalParameter (
            (PTypeSpecifier)CloneNode (_type_),
            (TIdentifier)CloneNode (_name_)
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAFormalParameter(this);
    }

    public PTypeSpecifier GetType ()
    {
        return _type_;
    }

    public void SetType (PTypeSpecifier node)
    {
        if(_type_ != null)
        {
            _type_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _type_ = node;
    }
    public TIdentifier GetName ()
    {
        return _name_;
    }

    public void SetName (TIdentifier node)
    {
        if(_name_ != null)
        {
            _name_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _name_ = node;
    }

    public override string ToString()
    {
        return ""
            + ToString (_type_)
            + ToString (_name_)
        ;
    }

    internal override void RemoveChild(Node child)
    {
        if ( _type_ == child )
        {
            _type_ = null;
            return;
        }
        if ( _name_ == child )
        {
            _name_ = null;
            return;
        }
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
        if ( _type_ == oldChild )
        {
            SetType ((PTypeSpecifier) newChild);
            return;
        }
        if ( _name_ == oldChild )
        {
            SetName ((TIdentifier) newChild);
            return;
        }
    }

}
public sealed class AVariableDefinition : PVariableDefinition
{
    private PTypeSpecifier _type_specifier_;
    private PTypeQualifier _type_qualifier_;
    private PExpression _init_;
    private TIdentifier _name_;

    public AVariableDefinition ()
    {
    }

    public AVariableDefinition (
            PTypeSpecifier _type_specifier_,
            PTypeQualifier _type_qualifier_,
            PExpression _init_,
            TIdentifier _name_
    )
    {
        SetTypeSpecifier (_type_specifier_);
        SetTypeQualifier (_type_qualifier_);
        SetInit (_init_);
        SetName (_name_);
    }

    public override Object Clone()
    {
        return new AVariableDefinition (
            (PTypeSpecifier)CloneNode (_type_specifier_),
            (PTypeQualifier)CloneNode (_type_qualifier_),
            (PExpression)CloneNode (_init_),
            (TIdentifier)CloneNode (_name_)
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAVariableDefinition(this);
    }

    public PTypeSpecifier GetTypeSpecifier ()
    {
        return _type_specifier_;
    }

    public void SetTypeSpecifier (PTypeSpecifier node)
    {
        if(_type_specifier_ != null)
        {
            _type_specifier_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _type_specifier_ = node;
    }
    public PTypeQualifier GetTypeQualifier ()
    {
        return _type_qualifier_;
    }

    public void SetTypeQualifier (PTypeQualifier node)
    {
        if(_type_qualifier_ != null)
        {
            _type_qualifier_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _type_qualifier_ = node;
    }
    public PExpression GetInit ()
    {
        return _init_;
    }

    public void SetInit (PExpression node)
    {
        if(_init_ != null)
        {
            _init_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _init_ = node;
    }
    public TIdentifier GetName ()
    {
        return _name_;
    }

    public void SetName (TIdentifier node)
    {
        if(_name_ != null)
        {
            _name_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _name_ = node;
    }

    public override string ToString()
    {
        return ""
            + ToString (_type_specifier_)
            + ToString (_type_qualifier_)
            + ToString (_init_)
            + ToString (_name_)
        ;
    }

    internal override void RemoveChild(Node child)
    {
        if ( _type_specifier_ == child )
        {
            _type_specifier_ = null;
            return;
        }
        if ( _type_qualifier_ == child )
        {
            _type_qualifier_ = null;
            return;
        }
        if ( _init_ == child )
        {
            _init_ = null;
            return;
        }
        if ( _name_ == child )
        {
            _name_ = null;
            return;
        }
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
        if ( _type_specifier_ == oldChild )
        {
            SetTypeSpecifier ((PTypeSpecifier) newChild);
            return;
        }
        if ( _type_qualifier_ == oldChild )
        {
            SetTypeQualifier ((PTypeQualifier) newChild);
            return;
        }
        if ( _init_ == oldChild )
        {
            SetInit ((PExpression) newChild);
            return;
        }
        if ( _name_ == oldChild )
        {
            SetName ((TIdentifier) newChild);
            return;
        }
    }

}
public sealed class APortDefinition : PPortDefinition
{
    private TIdentifier _name_;

    public APortDefinition ()
    {
    }

    public APortDefinition (
            TIdentifier _name_
    )
    {
        SetName (_name_);
    }

    public override Object Clone()
    {
        return new APortDefinition (
            (TIdentifier)CloneNode (_name_)
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAPortDefinition(this);
    }

    public TIdentifier GetName ()
    {
        return _name_;
    }

    public void SetName (TIdentifier node)
    {
        if(_name_ != null)
        {
            _name_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _name_ = node;
    }

    public override string ToString()
    {
        return ""
            + ToString (_name_)
        ;
    }

    internal override void RemoveChild(Node child)
    {
        if ( _name_ == child )
        {
            _name_ = null;
            return;
        }
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
        if ( _name_ == oldChild )
        {
            SetName ((TIdentifier) newChild);
            return;
        }
    }

}
public sealed class AConstTypeQualifier : PTypeQualifier
{


    public AConstTypeQualifier (
    )
    {
    }

    public override Object Clone()
    {
        return new AConstTypeQualifier (
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAConstTypeQualifier(this);
    }


    public override string ToString()
    {
        return ""
        ;
    }

    internal override void RemoveChild(Node child)
    {
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
    }

}
public sealed class AVolatileTypeQualifier : PTypeQualifier
{


    public AVolatileTypeQualifier (
    )
    {
    }

    public override Object Clone()
    {
        return new AVolatileTypeQualifier (
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAVolatileTypeQualifier(this);
    }


    public override string ToString()
    {
        return ""
        ;
    }

    internal override void RemoveChild(Node child)
    {
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
    }

}
public sealed class AVoidTypeSpecifier : PTypeSpecifier
{


    public AVoidTypeSpecifier (
    )
    {
    }

    public override Object Clone()
    {
        return new AVoidTypeSpecifier (
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAVoidTypeSpecifier(this);
    }


    public override string ToString()
    {
        return ""
        ;
    }

    internal override void RemoveChild(Node child)
    {
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
    }

}
public sealed class ASsingleTypeSpecifier : PTypeSpecifier
{


    public ASsingleTypeSpecifier (
    )
    {
    }

    public override Object Clone()
    {
        return new ASsingleTypeSpecifier (
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseASsingleTypeSpecifier(this);
    }


    public override string ToString()
    {
        return ""
        ;
    }

    internal override void RemoveChild(Node child)
    {
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
    }

}
public sealed class AUsingleTypeSpecifier : PTypeSpecifier
{


    public AUsingleTypeSpecifier (
    )
    {
    }

    public override Object Clone()
    {
        return new AUsingleTypeSpecifier (
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAUsingleTypeSpecifier(this);
    }


    public override string ToString()
    {
        return ""
        ;
    }

    internal override void RemoveChild(Node child)
    {
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
    }

}
public sealed class ASdoubleTypeSpecifier : PTypeSpecifier
{


    public ASdoubleTypeSpecifier (
    )
    {
    }

    public override Object Clone()
    {
        return new ASdoubleTypeSpecifier (
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseASdoubleTypeSpecifier(this);
    }


    public override string ToString()
    {
        return ""
        ;
    }

    internal override void RemoveChild(Node child)
    {
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
    }

}
public sealed class AUdoubleTypeSpecifier : PTypeSpecifier
{


    public AUdoubleTypeSpecifier (
    )
    {
    }

    public override Object Clone()
    {
        return new AUdoubleTypeSpecifier (
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAUdoubleTypeSpecifier(this);
    }


    public override string ToString()
    {
        return ""
        ;
    }

    internal override void RemoveChild(Node child)
    {
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
    }

}
public sealed class ASquadTypeSpecifier : PTypeSpecifier
{


    public ASquadTypeSpecifier (
    )
    {
    }

    public override Object Clone()
    {
        return new ASquadTypeSpecifier (
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseASquadTypeSpecifier(this);
    }


    public override string ToString()
    {
        return ""
        ;
    }

    internal override void RemoveChild(Node child)
    {
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
    }

}
public sealed class AUquadTypeSpecifier : PTypeSpecifier
{


    public AUquadTypeSpecifier (
    )
    {
    }

    public override Object Clone()
    {
        return new AUquadTypeSpecifier (
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAUquadTypeSpecifier(this);
    }


    public override string ToString()
    {
        return ""
        ;
    }

    internal override void RemoveChild(Node child)
    {
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
    }

}
public sealed class AFloatTypeSpecifier : PTypeSpecifier
{


    public AFloatTypeSpecifier (
    )
    {
    }

    public override Object Clone()
    {
        return new AFloatTypeSpecifier (
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAFloatTypeSpecifier(this);
    }


    public override string ToString()
    {
        return ""
        ;
    }

    internal override void RemoveChild(Node child)
    {
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
    }

}
public sealed class AStringTypeSpecifier : PTypeSpecifier
{


    public AStringTypeSpecifier (
    )
    {
    }

    public override Object Clone()
    {
        return new AStringTypeSpecifier (
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAStringTypeSpecifier(this);
    }


    public override string ToString()
    {
        return ""
        ;
    }

    internal override void RemoveChild(Node child)
    {
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
    }

}
public sealed class AEmptyStatement : PStatement
{


    public AEmptyStatement (
    )
    {
    }

    public override Object Clone()
    {
        return new AEmptyStatement (
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAEmptyStatement(this);
    }


    public override string ToString()
    {
        return ""
        ;
    }

    internal override void RemoveChild(Node child)
    {
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
    }

}
public sealed class AVoidReturnStatement : PReturnStatement
{
    private TReturn _token_;

    public AVoidReturnStatement ()
    {
    }

    public AVoidReturnStatement (
            TReturn _token_
    )
    {
        SetToken (_token_);
    }

    public override Object Clone()
    {
        return new AVoidReturnStatement (
            (TReturn)CloneNode (_token_)
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAVoidReturnStatement(this);
    }

    public TReturn GetToken ()
    {
        return _token_;
    }

    public void SetToken (TReturn node)
    {
        if(_token_ != null)
        {
            _token_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _token_ = node;
    }

    public override string ToString()
    {
        return ""
            + ToString (_token_)
        ;
    }

    internal override void RemoveChild(Node child)
    {
        if ( _token_ == child )
        {
            _token_ = null;
            return;
        }
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
        if ( _token_ == oldChild )
        {
            SetToken ((TReturn) newChild);
            return;
        }
    }

}
public sealed class AFormalsAndBody : PFormalsAndBody
{
    private TypedList _formals_;
    private TypedList _locals_;
    private TypedList _statements_;
    private PReturnStatement _return_;

    public AFormalsAndBody ()
    {
        this._formals_ = new TypedList(new Formals_Cast(this));
        this._locals_ = new TypedList(new Locals_Cast(this));
        this._statements_ = new TypedList(new Statements_Cast(this));
    }

    public AFormalsAndBody (
            IList _formals_,
            IList _locals_,
            IList _statements_,
            PReturnStatement _return_
    )
    {
        this._formals_ = new TypedList(new Formals_Cast(this));
        this._formals_.Clear();
        this._formals_.AddAll(_formals_);
        this._locals_ = new TypedList(new Locals_Cast(this));
        this._locals_.Clear();
        this._locals_.AddAll(_locals_);
        this._statements_ = new TypedList(new Statements_Cast(this));
        this._statements_.Clear();
        this._statements_.AddAll(_statements_);
        SetReturn (_return_);
    }

    public override Object Clone()
    {
        return new AFormalsAndBody (
            CloneList (_formals_),
            CloneList (_locals_),
            CloneList (_statements_),
            (PReturnStatement)CloneNode (_return_)
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAFormalsAndBody(this);
    }

    public IList GetFormals ()
    {
        return _formals_;
    }

    public void setFormals (IList list)
    {
        _formals_.Clear();
        _formals_.AddAll(list);
    }
    public IList GetLocals ()
    {
        return _locals_;
    }

    public void setLocals (IList list)
    {
        _locals_.Clear();
        _locals_.AddAll(list);
    }
    public IList GetStatements ()
    {
        return _statements_;
    }

    public void setStatements (IList list)
    {
        _statements_.Clear();
        _statements_.AddAll(list);
    }
    public PReturnStatement GetReturn ()
    {
        return _return_;
    }

    public void SetReturn (PReturnStatement node)
    {
        if(_return_ != null)
        {
            _return_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _return_ = node;
    }

    public override string ToString()
    {
        return ""
            + ToString (_formals_)
            + ToString (_locals_)
            + ToString (_statements_)
            + ToString (_return_)
        ;
    }

    internal override void RemoveChild(Node child)
    {
        if ( _formals_.Contains(child) )
        {
            _formals_.Remove(child);
            return;
        }
        if ( _locals_.Contains(child) )
        {
            _locals_.Remove(child);
            return;
        }
        if ( _statements_.Contains(child) )
        {
            _statements_.Remove(child);
            return;
        }
        if ( _return_ == child )
        {
            _return_ = null;
            return;
        }
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
        for ( int i = 0; i < _formals_.Count; i++ )
        {
            Node n = (Node)_formals_[i];
            if(n == oldChild)
            {
                if(newChild != null)
                {
                    _formals_[i] = newChild;
                    oldChild.Parent(null);
                    return;
                }

                _formals_.RemoveAt(i);
                oldChild.Parent(null);
                return;
            }
        }
        for ( int i = 0; i < _locals_.Count; i++ )
        {
            Node n = (Node)_locals_[i];
            if(n == oldChild)
            {
                if(newChild != null)
                {
                    _locals_[i] = newChild;
                    oldChild.Parent(null);
                    return;
                }

                _locals_.RemoveAt(i);
                oldChild.Parent(null);
                return;
            }
        }
        for ( int i = 0; i < _statements_.Count; i++ )
        {
            Node n = (Node)_statements_[i];
            if(n == oldChild)
            {
                if(newChild != null)
                {
                    _statements_[i] = newChild;
                    oldChild.Parent(null);
                    return;
                }

                _statements_.RemoveAt(i);
                oldChild.Parent(null);
                return;
            }
        }
        if ( _return_ == oldChild )
        {
            SetReturn ((PReturnStatement) newChild);
            return;
        }
    }

    private class Formals_Cast : Cast
    {
        AFormalsAndBody obj;

        internal Formals_Cast (AFormalsAndBody obj)
        {
          this.obj = obj;
        }

        public Object Cast(Object o)
        {
            PFormalParameter node = (PFormalParameter) o;

            if((node.Parent() != null) &&
                (node.Parent() != obj))
            {
                node.Parent().RemoveChild(node);
            }

            if((node.Parent() == null) ||
                (node.Parent() != obj))
            {
                node.Parent(obj);
            }

            return node;
        }

        public Object UnCast(Object o)
        {
            PFormalParameter node = (PFormalParameter) o;
            node.Parent(null);
            return node;
        }
    }
    private class Locals_Cast : Cast
    {
        AFormalsAndBody obj;

        internal Locals_Cast (AFormalsAndBody obj)
        {
          this.obj = obj;
        }

        public Object Cast(Object o)
        {
            PVariableDefinition node = (PVariableDefinition) o;

            if((node.Parent() != null) &&
                (node.Parent() != obj))
            {
                node.Parent().RemoveChild(node);
            }

            if((node.Parent() == null) ||
                (node.Parent() != obj))
            {
                node.Parent(obj);
            }

            return node;
        }

        public Object UnCast(Object o)
        {
            PVariableDefinition node = (PVariableDefinition) o;
            node.Parent(null);
            return node;
        }
    }
    private class Statements_Cast : Cast
    {
        AFormalsAndBody obj;

        internal Statements_Cast (AFormalsAndBody obj)
        {
          this.obj = obj;
        }

        public Object Cast(Object o)
        {
            PStatement node = (PStatement) o;

            if((node.Parent() != null) &&
                (node.Parent() != obj))
            {
                node.Parent().RemoveChild(node);
            }

            if((node.Parent() == null) ||
                (node.Parent() != obj))
            {
                node.Parent(obj);
            }

            return node;
        }

        public Object UnCast(Object o)
        {
            PStatement node = (PStatement) o;
            node.Parent(null);
            return node;
        }
    }
}
public sealed class AIntegerConstantExpression : PExpression
{
    private TMinus _sign_;
    private TIntegerLiteral _integer_literal_;

    public AIntegerConstantExpression ()
    {
    }

    public AIntegerConstantExpression (
            TMinus _sign_,
            TIntegerLiteral _integer_literal_
    )
    {
        SetSign (_sign_);
        SetIntegerLiteral (_integer_literal_);
    }

    public override Object Clone()
    {
        return new AIntegerConstantExpression (
            (TMinus)CloneNode (_sign_),
            (TIntegerLiteral)CloneNode (_integer_literal_)
        );
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseAIntegerConstantExpression(this);
    }

    public TMinus GetSign ()
    {
        return _sign_;
    }

    public void SetSign (TMinus node)
    {
        if(_sign_ != null)
        {
            _sign_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _sign_ = node;
    }
    public TIntegerLiteral GetIntegerLiteral ()
    {
        return _integer_literal_;
    }

    public void SetIntegerLiteral (TIntegerLiteral node)
    {
        if(_integer_literal_ != null)
        {
            _integer_literal_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _integer_literal_ = node;
    }

    public override string ToString()
    {
        return ""
            + ToString (_sign_)
            + ToString (_integer_literal_)
        ;
    }

    internal override void RemoveChild(Node child)
    {
        if ( _sign_ == child )
        {
            _sign_ = null;
            return;
        }
        if ( _integer_literal_ == child )
        {
            _integer_literal_ = null;
            return;
        }
    }

    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
        if ( _sign_ == oldChild )
        {
            SetSign ((TMinus) newChild);
            return;
        }
        if ( _integer_literal_ == oldChild )
        {
            SetIntegerLiteral ((TIntegerLiteral) newChild);
            return;
        }
    }

}

public sealed class Start : Node
{
    private PSourceFile _base_;
    private EOF _eof_;

    public Start()
    {
    }

    public Start(
        PSourceFile _base_,
        EOF _eof_)
    {
        SetPSourceFile(_base_);
        SetEOF(_eof_);
    }

    public override Object Clone()
    {
        return new Start(
            (PSourceFile) CloneNode(_base_),
            (EOF) CloneNode(_eof_));
    }

    public override void Apply(Switch sw)
    {
        ((Analysis) sw).CaseStart(this);
    }

    public PSourceFile GetPSourceFile()
    {
        return _base_;
    }
    public void SetPSourceFile(PSourceFile node)
    {
        if(_base_ != null)
        {
            _base_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _base_ = node;
    }

    public EOF GetEOF()
    {
        return _eof_;
    }
    public void SetEOF(EOF node)
    {
        if(_eof_ != null)
        {
            _eof_.Parent(null);
        }

        if(node != null)
        {
            if(node.Parent() != null)
            {
                node.Parent().RemoveChild(node);
            }

            node.Parent(this);
        }

        _eof_ = node;
    }

    internal override void RemoveChild(Node child)
    {
        if(_base_ == child)
        {
            _base_ = null;
            return;
        }

        if(_eof_ == child)
        {
            _eof_ = null;
            return;
        }
    }
    internal override void ReplaceChild(Node oldChild, Node newChild)
    {
        if(_base_ == oldChild)
        {
            SetPSourceFile((PSourceFile) newChild);
            return;
        }

        if(_eof_ == oldChild)
        {
            SetEOF((EOF) newChild);
            return;
        }
    }

    public override string ToString()
    {
        return "" +
            ToString(_base_) +
            ToString(_eof_);
    }
}
}
