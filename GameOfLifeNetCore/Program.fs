open System.Threading
open GameOfLifeNetCore.Game

[<EntryPoint>]
let main argv =
    while true do
     for i in 1..22 do
      for j in 1..98 do
        isAlive(i,j)
     for x in newBoardArray do
      printfn "%s" x
     boardArray <- newBoardArray
     newBoardArray <- Array.init width (fun _ -> String.replicate height " ")
     Thread.Sleep(250)     
    0