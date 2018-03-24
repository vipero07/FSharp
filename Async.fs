module Async

open System

/// <summary>
/// Chunks sequences containing async tasks and runs them in parallel leveraging every processor available
/// </summary>
/// <param name="sequence">a sequence contianing async tasks to be run in parallel</param>
let chunkAsync sequence =
    let runParallel = Async.RunSynchronously << Async.Parallel
    Seq.collect runParallel
    <| Seq.chunkBySize Environment.ProcessorCount sequence
