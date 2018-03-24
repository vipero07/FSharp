open System.Xml

[<EntryPoint>]
let main argv =
    let getMatches =
        let nodeMatches (readerNode: XmlReader) = async {
                return match readerNode.NodeType, readerNode.Name with
                        | XmlNodeType.Element, "NodeName" -> Some ()
                        | _ -> None
            }
        Async.chunkAsync << XmlParser.matchOnNodes nodeMatches

    getMatches <| XmlReader.Create(@"C:\somefile.txt")

    printfn "Hello World from F#!"
    0 // return an integer exit code
