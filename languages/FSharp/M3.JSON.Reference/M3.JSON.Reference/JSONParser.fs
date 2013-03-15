// ----------------------------------------------------------------------------------------------
// Copyright (c) Mårten Rånge.
// ----------------------------------------------------------------------------------------------
// This source code is subject to terms and conditions of the Microsoft Public License. A 
// copy of the license can be found in the License.html file at the root of this distribution. 
// If you cannot locate the  Microsoft Public License, please send an email to 
// dlr@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
//  by the terms of the Microsoft Public License.
// ----------------------------------------------------------------------------------------------
// You must not remove this notice, or any other, from this software.
// ----------------------------------------------------------------------------------------------

namespace M3.JSON.Reference

open System
open System.Diagnostics
open System.Text

open Parser

type Whitespace     = string*string

type WSString       = string*Whitespace
type WSDouble       = double*Whitespace

type JSON           =
    |   StringValue of WSString
    |   NumberValue of WSDouble
    |   ObjectValue of (WSString*JSON)[]*Whitespace
    |   ArrayValue  of JSON[]*Whitespace
    |   TrueValue   of Whitespace
    |   FalseValue  of Whitespace
    |   NullValue   of Whitespace

module JSONParser =

    let p_token token   = p_whitespaces .>> (p_string token) >> p_whitespaces 
    let p_null          = p_token "null" >>? NullValue
    let p_true          = p_token "true" >>? NullValue
    let p_false         = p_token "false" >>? NullValue
