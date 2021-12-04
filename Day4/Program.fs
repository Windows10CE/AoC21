open System
open System.Collections.Generic
open System.IO

let raw = File.ReadAllLines "../../../input"

// Part 1
let boards: int[,] array = Array.init (((raw.Length - 2) / 6)) (fun i ->
    let startIndex = (i * 6) + 2
    let board = Array2D.zeroCreate 5 5
    for rowIndex, line in Array.sub raw startIndex 5 |> Array.indexed do
        for columnIndex, num in line.Split(' ', StringSplitOptions.RemoveEmptyEntries) |> Array.map int |> Array.indexed do
            board.[rowIndex, columnIndex] <- num
    board
)
let checkForWinner (boards: IEnumerable<int[,]>) (numbers: int list) : bool * int =
    let mutable result = false
    let mutable winner = -1
    for boardIndex, board in Seq.indexed boards do
        for i = 0 to 4 do
            let mutable countInList = 0
            for j = 0 to 4 do
                if numbers |> List.contains board.[i, j] then
                    countInList <- countInList + 1
            if countInList = 5 then
                result <- true
                winner <- boardIndex
            countInList <- 0
            for j = 0 to 4 do
                if numbers |> List.contains board.[j, i] then
                    countInList <- countInList + 1
            if countInList = 5 then
                result <- true
                winner <- boardIndex
    (result, winner)

let mutable index = 0
let rolls = raw.[0].Split ',' |> Array.map int |> Array.toList
let mutable Continue = true
let mutable winnerIndex = -1
while Continue do
    index <- index + 1
    let result, winner = checkForWinner boards (rolls |> List.take index)
    Continue <- not result
    if result then
        winnerIndex <- winner

let mutable finalRolls = rolls |> List.take index
let mutable sum = 0
boards[winnerIndex] |> Array2D.iter (fun x -> if not (finalRolls |> List.contains x) then sum <- sum + x)

printfn $"Part 1:\n%A{boards[winnerIndex]}\n%A{finalRolls}\n%i{sum * List.last finalRolls}"

// Part 2
Continue <- true
index <- 1

let mutable boardsList = Array.toList boards

while Continue do
    let result, winner = checkForWinner boardsList (rolls |> List.take index)
    if result && boardsList.Length = 1 then
        Continue <- false
    else if result then
        boardsList <- boardsList |> List.removeAt winner
    else index <- index + 1

finalRolls <- rolls |> List.take index

sum <- 0
boardsList.Head |> Array2D.iter (fun x -> if not (finalRolls |> List.contains x) then sum <- sum + x)

printfn $"Part 2:\n%A{boardsList.Head}\n%A{finalRolls}\n%i{sum * List.last finalRolls}"
