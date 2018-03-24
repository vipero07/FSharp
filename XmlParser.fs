module XmlParser

open System.Xml

/// <summary>
/// split the provided XmlReader by the first node and all matched siblings into a sequence of XmlReaders containing only the subtree elements of the matched nodes
/// </summary>
/// <param name="nodeName">node name string to find and split on</param>
/// <param name="reader">the reader from the xml document</param>
let splitByNodeSiblings nodeName (reader: XmlReader) =
    let matchSiblings state =
        if state
        then Some (reader.ReadSubtree(), reader.ReadToNextSibling(nodeName))
        else None
    reader.ReadToFollowing(nodeName)
    |> Seq.unfold matchSiblings

/// <summary>
/// 
/// </summary>
/// <param name="reader"></param>
let getAllNodes (reader: XmlReader) =
    let readNodes state =
        if state
        then Some (reader.ReadSubtree(), reader.Read())
        else None
    reader.Read()
    |> Seq.unfold readNodes

/// <summary>
/// accepts a match function to match every node against an XmlReader
/// </summary>
/// <param name="matchFn"></param>
let matchOnNodes (matchFn: XmlReader -> _ ) =
    Seq.map matchFn << getAllNodes
