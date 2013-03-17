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

let rec write_json (output : StreamWriter) (json : JSON) =
    match json with
        |   NullValue (l,r)             ->  output.WriteLine(@"Null: ""{0}"",""{1}""", s_escape_string l, s_escape_string r)
        |   TrueValue (l,r)             ->  output.WriteLine(@"True: ""{0}"",""{1}""", s_escape_string l, s_escape_string r)
        |   FalseValue (l,r)            ->  output.WriteLine(@"False: ""{0}"",""{1}""", s_escape_string l, s_escape_string r)
        |   NumberValue (d,(l,r))       ->  output.WriteLine(@"Number: {0},""{1}"",""{2}""", d.ToString(CultureInfo.InvariantCulture), s_escape_string l, s_escape_string r)
        |   StringValue (s,(l,r))       ->  output.WriteLine(@"String: {0},""{1}"",""{2}""", s_escape_string s, s_escape_string l, s_escape_string r)
        |   ArrayValue (vs,(l,r))       ->  output.WriteLine(@"Array_Begin: ""{0}"",""{1}""", s_escape_string l, s_escape_string r)
                                            for v in vs do
                                                write_json output v
                                            output.WriteLine(@"Array_End: ""{0}"",""{1}""", s_escape_string l, s_escape_string r)
        |   ObjectValue (ms,(l,r))      ->  output.WriteLine(@"Object_Begin: ""{0}"",""{1}""", s_escape_string l, s_escape_string r)
                                            for ((n,(l',r')),v) in ms do
                                                output.WriteLine(@"Member_Begin: {0},""{1}"",""{2}""", s_escape_string n, s_escape_string l', s_escape_string r')
                                                write_json output v
                                                output.WriteLine(@"Member_End: {0},""{1}"",""{2}""", s_escape_string n, s_escape_string l', s_escape_string r')
                                            output.WriteLine(@"Object_End: ""{0}"",""{1}""", s_escape_string l, s_escape_string r)

let build_action_log (json : string) (action_log : string)= 
    let input = File.ReadAllText(json)
    use output = new StreamWriter(action_log)
    let result = parse p_json input
    match result with
        |   Success (doc, ps)       -> write_json output doc
        |   Failure (msg, _, ps)    -> failwithf "Parse failure@%d : %s" ps.pos msg

[<EntryPoint>]
let main argv = 
    let jsons = Directory.GetFiles (@"..\..\..\..\..\..\reference-data", "*.json")
                    |> Array.map Path.GetFullPath
    for json in jsons do
        let action_log = json + ".actionlog"
        printfn "Building action log for: %s" json
        try
            build_action_log json action_log
        with
            |   exc -> printfn "Failed to build action log: %s" exc.Message

    0 // return an integer exit code
