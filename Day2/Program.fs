open System.IO

let raw =
    File.ReadAllLines("../../../input")
    |> Array.toList
    
// part 1
type Instruction = { Command: string; Value: int; }
let instructions =
    raw
    |> List.map (fun x -> x.Split ' ')
    |> List.map (fun x -> { Command = x.[0]; Value = int x.[1] })
    
let mutable horizontal = 0
let mutable depth = 0

for inst in instructions do
    match inst.Command with
        | "forward" -> horizontal <- horizontal + inst.Value
        | "up" -> depth <- depth - inst.Value
        | "down" -> depth <- depth + inst.Value
        | _ -> ()

printfn $"Part 1: %i{depth * horizontal}"

// part 2
horizontal <- 0
depth <- 0
let mutable aim = 0
for inst in instructions do
    match inst.Command with
        | "forward" ->
            horizontal <- horizontal + inst.Value
            depth <- depth + aim * inst.Value
        | "up" -> aim <- aim - inst.Value
        | "down" -> aim <- aim + inst.Value
        | _ -> ()

printfn $"Part 2: %i{depth * horizontal}"