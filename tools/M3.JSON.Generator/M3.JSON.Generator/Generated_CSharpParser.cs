

// ############################################################################
// #                                                                          #
// #        ---==>  T H I S  F I L E  I S   G E N E R A T E D  <==---         #
// #                                                                          #
// # This means that any edits to the .cs file will be lost when its          #
// # regenerated. Changes should instead be applied to the corresponding      #
// # template file (.tt)                                                      #
// ############################################################################





#pragma warning disable 0162
// ReSharper disable CheckNamespace
// ReSharper disable CSharpWarnings::CS0162
// ReSharper disable HeuristicUnreachableCode
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart


namespace M3.JSON.Generator.Parser
{
    using System.Collections.Generic;
    using M3.JSON.Generator.Source.Common;


    enum ParserState
    {
        Error,
        InvalidValueError,
        ParseValue,
        ObjectValue,
        MemberName,
        MemberSeparatorExpected,
        MemberValueExpected,
        ArrayValue,
        ArrayValueExpected,
        ArraySeparatorExpected,
        ParseString,
        StringValue,
        NumberValue,
        TrueValue,
        FalseValue,
        NullValue,
    }

    enum ParserResult
    {
        Error   ,
        Continue,
        Done    ,
    }


    sealed partial class Scanner
    {
        partial void BeginAcceptValue ();
        partial void EndAcceptValue ();


        partial void Partial_StateTransition__From_Error ();

        partial void Partial_StateTransition__To_Error ();

        partial void Partial_StateTransition__From_Error__To_Error ();
        partial void Partial_StateTransition__From_InvalidValueError ();

        partial void Partial_StateTransition__To_InvalidValueError ();

        partial void Partial_StateTransition__From_InvalidValueError__To_Error ();
        partial void Partial_StateTransition__From_ParseValue ();

        partial void Partial_StateTransition__To_ParseValue ();

        partial void Partial_StateTransition__From_ParseValue__To_ParseValue ();
        partial void Partial_StateTransition__From_ParseValue__To_StringValue ();
        partial void Partial_StateTransition__From_ParseValue__To_NumberValue ();
        partial void Partial_StateTransition__From_ParseValue__To_ObjectValue ();
        partial void Partial_StateTransition__From_ParseValue__To_ArrayValue ();
        partial void Partial_StateTransition__From_ParseValue__To_TrueValue ();
        partial void Partial_StateTransition__From_ParseValue__To_FalseValue ();
        partial void Partial_StateTransition__From_ParseValue__To_NullValue ();
        partial void Partial_StateTransition__From_ParseValue__To_InvalidValueError ();
        partial void Partial_StateTransition__From_ObjectValue ();

        partial void Partial_StateTransition__To_ObjectValue ();

        partial void Partial_StateTransition__From_ObjectValue__To_ObjectValue ();
        partial void Partial_StateTransition__From_ObjectValue__To_MemberName ();
        partial void Partial_StateTransition__From_ObjectValue__To_<NoName> ();
        partial void Partial_StateTransition__From_ObjectValue__To_InvalidValueError ();
        partial void Partial_StateTransition__From_MemberName ();

        partial void Partial_StateTransition__To_MemberName ();

        partial void Partial_StateTransition__From_MemberName__To_ParseString ();
        partial void Partial_StateTransition__From_MemberSeparatorExpected ();

        partial void Partial_StateTransition__To_MemberSeparatorExpected ();

        partial void Partial_StateTransition__From_MemberSeparatorExpected__To_MemberSeparatorExpected ();
        partial void Partial_StateTransition__From_MemberSeparatorExpected__To_MemberValueExpected ();
        partial void Partial_StateTransition__From_MemberSeparatorExpected__To_<NoName> ();
        partial void Partial_StateTransition__From_MemberSeparatorExpected__To_InvalidValueError ();
        partial void Partial_StateTransition__From_MemberValueExpected ();

        partial void Partial_StateTransition__To_MemberValueExpected ();

        partial void Partial_StateTransition__From_MemberValueExpected__To_ParseValue ();
        partial void Partial_StateTransition__From_ArrayValue ();

        partial void Partial_StateTransition__To_ArrayValue ();

        partial void Partial_StateTransition__From_ArrayValue__To_ArrayValue ();
        partial void Partial_StateTransition__From_ArrayValue__To_<NoName> ();
        partial void Partial_StateTransition__From_ArrayValue__To_ArrayValueExpected ();
        partial void Partial_StateTransition__From_ArrayValueExpected ();

        partial void Partial_StateTransition__To_ArrayValueExpected ();

        partial void Partial_StateTransition__From_ArrayValueExpected__To_ParseValue ();
        partial void Partial_StateTransition__From_ArraySeparatorExpected ();

        partial void Partial_StateTransition__To_ArraySeparatorExpected ();

        partial void Partial_StateTransition__From_ArraySeparatorExpected__To_ArraySeparatorExpected ();
        partial void Partial_StateTransition__From_ArraySeparatorExpected__To_ArrayValueExpected ();
        partial void Partial_StateTransition__From_ArraySeparatorExpected__To_<NoName> ();
        partial void Partial_StateTransition__From_ArraySeparatorExpected__To_InvalidValueError ();
        partial void Partial_StateTransition__From_ParseString ();

        partial void Partial_StateTransition__To_ParseString ();

        partial void Partial_StateTransition__From_ParseString__To_<NoName> ();
        partial void Partial_StateTransition__From_StringValue ();

        partial void Partial_StateTransition__To_StringValue ();

        partial void Partial_StateTransition__From_StringValue__To_ParseString ();
        partial void Partial_StateTransition__From_NumberValue ();

        partial void Partial_StateTransition__To_NumberValue ();

        partial void Partial_StateTransition__From_NumberValue__To_<NoName> ();
        partial void Partial_StateTransition__From_TrueValue ();

        partial void Partial_StateTransition__To_TrueValue ();

        partial void Partial_StateTransition__From_TrueValue__To_<NoName> ();
        partial void Partial_StateTransition__From_FalseValue ();

        partial void Partial_StateTransition__To_FalseValue ();

        partial void Partial_StateTransition__From_FalseValue__To_<NoName> ();
        partial void Partial_StateTransition__From_NullValue ();

        partial void Partial_StateTransition__To_NullValue ();

        partial void Partial_StateTransition__From_NullValue__To_<NoName> ();

        Stack<ParserState> ResumeWith   = new Stack<ParserState> (64);
        ParserState State               = default (ParserState);

        ParserResult Result             ;
        SubString    CurrentLine        ;
        int          CurrentPos         ;
        char         CurrentCharacter   ;


        public bool AcceptValue (SubString ss)
        {
            Result      = ParserResult.Continue;
            CurrentLine = ss;

            BeginAcceptValue ();            

            var bs      = ss.BaseString;
            var begin   = ss.Begin;
            var end     = ss.End;

            for (CurrentPos = begin; CurrentPos < end; ++CurrentPos)
            {
                CurrentCharacter = bs[CurrentPos];
apply:
                if (Result != ParserResult.Continue)
                {
                    break;
                }

                switch (State)
                {
                case ParserState.Error:
                    switch (CurrentCharacter)
                    {
                    default:
                            Partial_StateTransition__From_Error ();
                            Partial_StateTransition__From_Error__To_Error ();
                            Partial_StateTransition__To_Error ();
                        break;
    
                    }
                    break;
                case ParserState.InvalidValueError:
                    switch (CurrentCharacter)
                    {
                    default:
                        State = ParserState.Error; 
                            Partial_StateTransition__From_InvalidValueError ();
                            Partial_StateTransition__From_InvalidValueError__To_Error ();
                            Partial_StateTransition__To_Error ();
                        break;
    
                    }
                    break;
                case ParserState.ParseValue:
                    switch (CurrentCharacter)
                    {
                    case ' ':
                            Partial_StateTransition__From_ParseValue ();
                            Partial_StateTransition__From_ParseValue__To_ParseValue ();
                            Partial_StateTransition__To_ParseValue ();
                        break;
                    case '"':
                        State = ParserState.StringValue; 
                            Partial_StateTransition__From_ParseValue ();
                            Partial_StateTransition__From_ParseValue__To_StringValue ();
                            Partial_StateTransition__To_StringValue ();
                        break;
                    case '-':
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        State = ParserState.NumberValue; 
                            Partial_StateTransition__From_ParseValue ();
                            Partial_StateTransition__From_ParseValue__To_NumberValue ();
                            Partial_StateTransition__To_NumberValue ();
                        goto apply;
                        break;
                    case '{':
                        State = ParserState.ObjectValue; 
                            Partial_StateTransition__From_ParseValue ();
                            Partial_StateTransition__From_ParseValue__To_ObjectValue ();
                            Partial_StateTransition__To_ObjectValue ();
                        break;
                    case '[':
                        State = ParserState.ArrayValue; 
                            Partial_StateTransition__From_ParseValue ();
                            Partial_StateTransition__From_ParseValue__To_ArrayValue ();
                            Partial_StateTransition__To_ArrayValue ();
                        break;
                    case 't':
                        State = ParserState.TrueValue; 
                            Partial_StateTransition__From_ParseValue ();
                            Partial_StateTransition__From_ParseValue__To_TrueValue ();
                            Partial_StateTransition__To_TrueValue ();
                        break;
                    case 'f':
                        State = ParserState.FalseValue; 
                            Partial_StateTransition__From_ParseValue ();
                            Partial_StateTransition__From_ParseValue__To_FalseValue ();
                            Partial_StateTransition__To_FalseValue ();
                        break;
                    case 'n':
                        State = ParserState.NullValue; 
                            Partial_StateTransition__From_ParseValue ();
                            Partial_StateTransition__From_ParseValue__To_NullValue ();
                            Partial_StateTransition__To_NullValue ();
                        break;
                    default:
                        State = ParserState.InvalidValueError; 
                            Partial_StateTransition__From_ParseValue ();
                            Partial_StateTransition__From_ParseValue__To_InvalidValueError ();
                            Partial_StateTransition__To_InvalidValueError ();
                        break;
    
                    }
                    break;
                case ParserState.ObjectValue:
                    switch (CurrentCharacter)
                    {
                    case ' ':
                            Partial_StateTransition__From_ObjectValue ();
                            Partial_StateTransition__From_ObjectValue__To_ObjectValue ();
                            Partial_StateTransition__To_ObjectValue ();
                        break;
                    case '"':
                        State = ParserState.MemberName; 
                            Partial_StateTransition__From_ObjectValue ();
                            Partial_StateTransition__From_ObjectValue__To_MemberName ();
                            Partial_StateTransition__To_MemberName ();
                        break;
                    case '}':
                        State = ResumeWith.Pop (); 
                        break;
                    default:
                        State = ParserState.InvalidValueError; 
                            Partial_StateTransition__From_ObjectValue ();
                            Partial_StateTransition__From_ObjectValue__To_InvalidValueError ();
                            Partial_StateTransition__To_InvalidValueError ();
                        break;
    
                    }
                    break;
                case ParserState.MemberName:
                    ResumeWith.Push (ParserState.MemberSeparatorExpected);
                    switch (CurrentCharacter)
                    {
                    default:
                        State = ParserState.ParseString; 
                            Partial_StateTransition__From_MemberName ();
                            Partial_StateTransition__From_MemberName__To_ParseString ();
                            Partial_StateTransition__To_ParseString ();
                        goto apply;
                        break;
    
                    }
                    break;
                case ParserState.MemberSeparatorExpected:
                    switch (CurrentCharacter)
                    {
                    case ' ':
                            Partial_StateTransition__From_MemberSeparatorExpected ();
                            Partial_StateTransition__From_MemberSeparatorExpected__To_MemberSeparatorExpected ();
                            Partial_StateTransition__To_MemberSeparatorExpected ();
                        break;
                    case ',':
                        State = ParserState.MemberValueExpected; 
                            Partial_StateTransition__From_MemberSeparatorExpected ();
                            Partial_StateTransition__From_MemberSeparatorExpected__To_MemberValueExpected ();
                            Partial_StateTransition__To_MemberValueExpected ();
                        break;
                    case '}':
                        State = ResumeWith.Pop (); 
                        break;
                    default:
                        State = ParserState.InvalidValueError; 
                            Partial_StateTransition__From_MemberSeparatorExpected ();
                            Partial_StateTransition__From_MemberSeparatorExpected__To_InvalidValueError ();
                            Partial_StateTransition__To_InvalidValueError ();
                        break;
    
                    }
                    break;
                case ParserState.MemberValueExpected:
                    ResumeWith.Push (ParserState.MemberSeparatorExpected);
                    switch (CurrentCharacter)
                    {
                    default:
                        State = ParserState.ParseValue; 
                            Partial_StateTransition__From_MemberValueExpected ();
                            Partial_StateTransition__From_MemberValueExpected__To_ParseValue ();
                            Partial_StateTransition__To_ParseValue ();
                        goto apply;
                        break;
    
                    }
                    break;
                case ParserState.ArrayValue:
                    switch (CurrentCharacter)
                    {
                    case ' ':
                            Partial_StateTransition__From_ArrayValue ();
                            Partial_StateTransition__From_ArrayValue__To_ArrayValue ();
                            Partial_StateTransition__To_ArrayValue ();
                        break;
                    case ']':
                        State = ResumeWith.Pop (); 
                        break;
                    default:
                        State = ParserState.ArrayValueExpected; 
                            Partial_StateTransition__From_ArrayValue ();
                            Partial_StateTransition__From_ArrayValue__To_ArrayValueExpected ();
                            Partial_StateTransition__To_ArrayValueExpected ();
                        goto apply;
                        break;
    
                    }
                    break;
                case ParserState.ArrayValueExpected:
                    ResumeWith.Push (ParserState.ArraySeparatorExpected);
                    switch (CurrentCharacter)
                    {
                    default:
                        State = ParserState.ParseValue; 
                            Partial_StateTransition__From_ArrayValueExpected ();
                            Partial_StateTransition__From_ArrayValueExpected__To_ParseValue ();
                            Partial_StateTransition__To_ParseValue ();
                        goto apply;
                        break;
    
                    }
                    break;
                case ParserState.ArraySeparatorExpected:
                    switch (CurrentCharacter)
                    {
                    case ' ':
                            Partial_StateTransition__From_ArraySeparatorExpected ();
                            Partial_StateTransition__From_ArraySeparatorExpected__To_ArraySeparatorExpected ();
                            Partial_StateTransition__To_ArraySeparatorExpected ();
                        break;
                    case ',':
                        State = ParserState.ArrayValueExpected; 
                            Partial_StateTransition__From_ArraySeparatorExpected ();
                            Partial_StateTransition__From_ArraySeparatorExpected__To_ArrayValueExpected ();
                            Partial_StateTransition__To_ArrayValueExpected ();
                        break;
                    case ']':
                        State = ResumeWith.Pop (); 
                        break;
                    default:
                        State = ParserState.InvalidValueError; 
                            Partial_StateTransition__From_ArraySeparatorExpected ();
                            Partial_StateTransition__From_ArraySeparatorExpected__To_InvalidValueError ();
                            Partial_StateTransition__To_InvalidValueError ();
                        break;
    
                    }
                    break;
                case ParserState.ParseString:
                    switch (CurrentCharacter)
                    {
                    default:
                        State = ResumeWith.Pop (); 
                        break;
    
                    }
                    break;
                case ParserState.StringValue:
                    switch (CurrentCharacter)
                    {
                    default:
                        State = ParserState.ParseString; 
                            Partial_StateTransition__From_StringValue ();
                            Partial_StateTransition__From_StringValue__To_ParseString ();
                            Partial_StateTransition__To_ParseString ();
                        goto apply;
                        break;
    
                    }
                    break;
                case ParserState.NumberValue:
                    switch (CurrentCharacter)
                    {
                    default:
                        State = ResumeWith.Pop (); 
                        break;
    
                    }
                    break;
                case ParserState.TrueValue:
                    switch (CurrentCharacter)
                    {
                    default:
                        State = ResumeWith.Pop (); 
                        break;
    
                    }
                    break;
                case ParserState.FalseValue:
                    switch (CurrentCharacter)
                    {
                    default:
                        State = ResumeWith.Pop (); 
                        break;
    
                    }
                    break;
                case ParserState.NullValue:
                    switch (CurrentCharacter)
                    {
                    default:
                        State = ResumeWith.Pop (); 
                        break;
    
                    }
                    break;
                default:
                    Result = ParserResult.Error;
                    break;
                }
            }

            EndAcceptValue ();            

            return Result != ParserResult.Error;
        }
    }
}





