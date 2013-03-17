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

open System
open System.IO
open System.Globalization

open M3.JSON.Reference

open Parser
open JSONParser

let rec randomizer max_level current_level max_members (r: Random) (wsg : int->Random->Whitespace) (sg : int->Random->WSString)=
    let x = if current_level = 0 then r.Next(5,7)
            else if current_level = max_level then r.Next(0,5)
            else r.Next(0,7)
    match x with 
        | 0 -> NullValue (wsg current_level r)
        | 1 -> TrueValue (wsg current_level r)
        | 2 -> FalseValue (wsg current_level r)
        | 3 -> NumberValue (r.NextDouble()*1000.0, (wsg current_level r))
        | 4 -> StringValue (sg current_level r)
        | 5 ->  
                let vc = r.Next(0,max_members)
                let vs = [| for i in 1..vc -> randomizer max_level (current_level + 1) max_members r wsg sg |]
                ArrayValue (vs, (wsg current_level r))
        | 6 ->  
                let oc = r.Next(0,max_members)
                let os = [| for i in 1..oc -> ((sg current_level r) , randomizer max_level (current_level + 1) max_members r wsg sg) |]
                ObjectValue (os, (wsg current_level r))
        | _ ->  ArrayValue ([||], (wsg current_level r))

let indenter current_level (random : Random) = ("\r\n" + new String ('\t', current_level), "")

let string_generator current_level (random : Random) =
    let c = random.Next(0,32) 
    let cs = [| for i in 1..c -> char (random.Next(int 'A', int 'Z' + 1)) |]    
    (new String (cs), indenter current_level random)

[<EntryPoint>]
let main argv =
    let random = new Random(19740531)
    let count = 10
    let rd = Path.GetFullPath(@"..\..\..\..\..\..\reference-data")
    
    for i in 0..count do
        let max_level = random.Next(1,5)
        let max_members = random.Next(5,10)
        let json = randomizer max_level 0 max_members random indenter string_generator  

        let p = sprintf @"%s\random%d.json" rd i

        let serialized = s_json json

        File.WriteAllText(p, serialized)


    0 // return an integer exit code
