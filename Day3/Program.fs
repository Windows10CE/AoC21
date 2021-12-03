open System
open System.IO

let raw = File.ReadAllLines "../../../input"

let values =
    raw
    |> Array.map (fun x -> Convert.ToUInt32(x, 2))
    
// part 1
type BitValue = { mutable Zeroes: int; mutable Ones: int; }
let initBitArray () = Array.init 32 (fun _ -> { Zeroes = 0; Ones = 0; })
let resultArray: BitValue array = initBitArray()

let computeBits arr ints = 
    for value in ints do
        for i, bits in Array.indexed arr do
            if (value &&& (1u <<< i)) <> 0u then
                bits.Ones <- bits.Ones + 1
            else
                bits.Zeroes <- bits.Zeroes + 1

computeBits resultArray values

let gamma: uint32 =
    resultArray
    |> Array.map (fun bit -> if bit.Ones > bit.Zeroes then 1u else 0u)
    |> Array.indexed
    |> Array.fold (fun acc (index, value) -> acc ||| (value <<< index)) 0u
    
let epsilon = ~~~gamma &&& uint32 (2.0 ** float raw.[0].Length) - 1u

printfn $"Part 1: %i{gamma * epsilon}"

// part 2
let funny_filter num index target =
    let masked = num &&& (1u <<< index)
    if (masked <> 0u && target = 1u) then
        true
    else if (masked = 0u && target = 0u) then
        true
    else false
        

let mutable index = 11
let mutable oxygenArray = resultArray
let mutable oxygenList = Array.toList values
while oxygenList.Length > 1 do
    let bit = oxygenArray.[index]
    let target = if bit.Ones >= bit.Zeroes then 1u else 0u
    oxygenList <- oxygenList |> List.filter (fun x -> funny_filter x index target)
    oxygenArray <- initBitArray()
    oxygenList |> List.toArray |> computeBits oxygenArray
    index <- index - 1
    
let oxygen = oxygenList.Head

index <- 11
let mutable co2Array = resultArray
let mutable co2List = Array.toList values
while co2List.Length > 1 do
    let bit = co2Array.[index]
    let target = if bit.Ones >= bit.Zeroes then 0u else 1u
    co2List <- co2List |> List.filter (fun x -> funny_filter x index target)
    co2Array <- initBitArray()
    co2List |> List.toArray |> computeBits co2Array
    index <- index - 1
    
let co2 = co2List.Head

printfn $"Part 2: %i{oxygen * co2}"
