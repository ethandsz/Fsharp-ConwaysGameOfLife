module GameOfLifeNetCore.Game
let width = 24
let height = 100
let mutable boardArray = Array.init width (fun _ -> String.replicate height " ")
let mutable newBoardArray = Array.init width (fun _ -> String.replicate height " ")

let getChar (x, y) (board: string array) = board.[x].[y]

let replaceAt idx (ch : char) (s: string) =
    let len = s.Length
    let before = s.Substring(0, idx)
    let after = if idx >= len then "" else s.Substring(idx+1, len-idx-1)
    sprintf "%s%c%s" before ch after
    
let insertCell (x,y)=
    boardArray.[x] <- boardArray.[x] |> replaceAt y 'X'

let insertRandomCell (x, y) =
    let rnd = System.Random() 
    if rnd.Next(100) < 10 then 
        insertCell(x, y)

for x in 0..22 do 
    for y in 0..98 do
        insertRandomCell (x, y)

let neighborOf (x, y)=
        [
      getChar(x-1, y-1) boardArray;   getChar(x-1, y) boardArray;   getChar(x-1, y+1) boardArray;
      getChar(x, y-1) boardArray;                        getChar(x, y+1) boardArray;
      getChar(x+1, y-1) boardArray;   getChar(x+1, y) boardArray;   getChar(x+1, y+1) boardArray;
    ]
       
(*
    Rules: 
    - Any live cell with fewer than two live neighbors dies, as if by underpopulation.
    - Any live cell with two or three live neighbors lives on to the next generation.
    - Any live cell with more than three live neighbors dies, as if by overpopulation.
    - Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
*)
let isAlive (x, y) =
    let cells = neighborOf (x, y)
    let length = cells |> Seq.filter ((=) 'X') |> Seq.length
    let state  =
        match getChar(x, y) boardArray with
        | 'X' -> true
        | _ -> false
    
    match (length, state) with
    | 2, true -> newBoardArray.[x] <- newBoardArray.[x] |> replaceAt y 'X'
    | 3, true -> newBoardArray.[x] <- newBoardArray.[x] |> replaceAt y 'X'
    | 3, false -> newBoardArray.[x] <- newBoardArray.[x] |> replaceAt y 'X'
    | _, true -> newBoardArray.[x] <- newBoardArray.[x] |> replaceAt y ' '
    | _ -> newBoardArray.[x] <- newBoardArray.[x] |> replaceAt y ' '
    
let nextGen =
    newBoardArray <- Array.init width (fun _ -> String.replicate height " ")

//Glider pattern
(*
insertCell(1,1)
insertCell(1,3)
insertCell(2,2)
insertCell(2,3)
insertCell(3,2)
*)
