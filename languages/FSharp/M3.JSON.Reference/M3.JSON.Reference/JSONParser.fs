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
open System.Globalization

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

    let p_ws p          = p_whitespaces >> p >> p_whitespaces >>? (fun ((ws1, r), ws2) -> (r, (ws1, ws2)))
    let p_token token   = p_whitespaces .>> (p_string token) >> p_whitespaces
    let p_null          = p_token "null" >>? NullValue
    let p_true          = p_token "true" >>? TrueValue
    let p_false         = p_token "false" >>? FalseValue

    let s_hexdigit c    = (c <= '9' && c >= '0') || (c <= 'F' && c >= 'A')  || (c <= 'f' && c >= 'a')
    let s_digit c       = (c <= '9' && c >= '0')
    let s_digit19 c     = (c <= '9' && c >= '1')
    let p_hexdigit      = p_satisy s_hexdigit "hexdigit"
    let p_digit         = p_satisy s_digit "digit"
    let p_digit19       = p_satisy s_digit19 "digit19"
    let p_digits        = p_satisy_many (fun _ c -> s_digit c)

    let to_string o     = match o with
                            |   Some v  -> v.ToString()
                            |   _       -> ""

    let p_sign_token    = p_satisy (fun c -> '-' = c) "sign"
    let p_sign          = p_opt p_sign_token >>? to_string

    // The reference parser is about correctness, not performance
    let p_int           = p_choose  
                            [
                                p_sign >> p_digit19 >> p_digits >>? (fun ((s, d), ds) -> s + d.ToString() + ds)    ;
                                p_sign >> p_digit               >>? (fun (s, c) -> s + c.ToString())               ;
                            ]
    
    let p_exp_token     = p_choose
                            [
                                p_string "e+"   >>?? "e+"   ;
                                p_string "E+"   >>?? "e+"   ;
                                p_string "e-"   >>?? "e-"   ;
                                p_string "E-"   >>?? "e-"   ;
                                p_string "e"    >>?? "e+"   ;
                                p_string "E"    >>?? "e+"   ;
                            ]
    let p_exp           = p_exp_token >> p_digits >>? (fun (e, d) -> e + d)

    let p_frac_token    = p_char '.'
    let p_frac          = p_frac_token >>. p_digits >>? (fun d -> "." + d) 

    let p_number        = p_ws (
                                    p_int 
                                >>  (p_opt p_frac) 
                                >>  (p_opt p_exp) 
                                >>? (fun ((i, f), e) -> Double.Parse(i + (to_string f) + (to_string e), CultureInfo.InvariantCulture))
                                )   >>? NumberValue

    let p_unicodechar   =       p_string @"\u"
                            >>. p_satisy_fixed (fun _ c -> s_hexdigit c) 4 "unicodechar"
                            >>? (fun s -> char (Int16.Parse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture)))                                 
    let p_strchar       = p_choose
                            [
                                p_any_char >>! (p_satisy (fun c -> c = '"' || c = '\\') "dummy") ;
                                p_string @"\""" >>?? '"'    ;
                                p_string @"\\"  >>?? '\\'   ;
                                p_string @"\b"  >>?? '\b'   ;
                                p_string @"\f"  >>?? '\f'   ;
                                p_string @"\n"  >>?? '\n'   ;
                                p_string @"\r"  >>?? '\r'   ;
                                p_string @"\t"  >>?? '\t'   ;
                                p_unicodechar               ;
                            ]
    let p_plainstr      = p_ws (p_between (p_char '"') (p_many p_strchar) (p_char '"') >>? (fun cs -> new string(cs)))
    let p_str           = p_plainstr >>? StringValue       

    let rec p_value     = p_choose  
                            [
                                p_null          ;
                                p_true          ;
                                p_false         ;
                                p_str           ;
                                p_number        ;
                                p_array         ;
                                p_object        ;
                            ]
    and p_array ps      = (p_ws (
                            p_between (p_char '[') (p_sep p_value (p_char ',')) (p_char ']')
                            ) >>? ArrayValue) ps

                              
    and p_pair ps       = (p_plainstr .>> (p_char ':') >> p_value) ps
                          
    and p_object ps     = (p_ws (
                            p_between (p_char '{') (p_sep p_pair (p_char ',')) (p_char '}')
                            ) >>? ObjectValue ) ps
                             

    let p_json          = p_choose
                            [
                                p_array     ;
                                p_object    ;
                            ] .>> p_eos

    let s_token (sb : StringBuilder) (token : string) ((l,r) : Whitespace)    
                            =   ignore (sb.Append(l).Append(token).Append(r))
                                ()
    let rec s_json_impl (sb : StringBuilder) json 
                            =   match json with
                                |   NullValue   ws -> s_token sb "null" ws
                                |   TrueValue   ws -> s_token sb "true" ws
                                |   FalseValue  ws -> s_token sb "false" ws
                                |   NumberValue (d, ws) -> s_token sb (d.ToString(CultureInfo.InvariantCulture)) ws
                                |   StringValue (s, ws) -> s_token sb s ws  // TODO: Escape string properly
                                |   ObjectValue (ms, ws)->  let mutable is_first = true
                                                            sb.Append('{')
                                                            for ((n,ws'),v) in ms do
                                                                if is_first then
                                                                    is_first <- false
                                                                else
                                                                    ignore (sb.Append(','))
                                                                    ()
                                                                s_token sb n ws' // TODO: Escape string properly
                                                                sb.Append(':')
                                                                s_json_impl sb v
                                                                ()
                                                            ignore (sb.Append('}'))
                                                            ()
                                |   ArrayValue (vs, ws)->   let mutable is_first = true
                                                            sb.Append('[')
                                                            for v in vs do
                                                                if is_first then
                                                                    is_first <- false
                                                                else
                                                                    ignore (sb.Append(','))
                                                                    ()
                                                                s_json_impl sb v
                                                                ()
                                                            ignore (sb.Append(']'))
                                                            ()

                                                                
    let s_json json         =   let sb = new StringBuilder()
                                s_json_impl sb json
                                sb.ToString()
