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
open M3.JSON.Reference

open Parser
open JSONParser

[<EntryPoint>]
let main argv = 
    let jsons = Directory.GetFiles (@"..\..\..\..\..\..\reference-data", "*.json")
                    |> Array.map Path.GetFullPath
    for json in jsons do
        let content = File.ReadAllText(json)
        let result = parse p_json content
        match result with
            |   Success (js,_)  ->  let serialize = s_json js
                                    if serialize = content then
                                        printf "SUCCESS: Parsed and compared content: %s\r\n" json
                                    else
                                        printf "FAILURE: Parsed but failed to compare content: %s\r\n" json
            |   _               ->  printf "FAILURE: Failed to parse: %s\r\n" json
    0