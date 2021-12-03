open System.IO

let values =
    File.ReadAllLines("../../../input")
    |> Array.toList
    |> List.map int
   
// part 1
let mutable countIncreased = 0

values
    |> List.windowed 2
    |> List.iter (fun window -> if window.[1] > window.[0] then countIncreased <- countIncreased + 1)
    
printfn $"Part 1: %i{countIncreased}"

// part 2
countIncreased <- 0

values
    |> List.windowed 3
    |> List.map List.sum
    |> List.windowed 2
    |> List.iter (fun window -> if window.[1] > window.[0] then countIncreased <- countIncreased + 1)
    
printfn $"Part 2: %i{countIncreased}"
