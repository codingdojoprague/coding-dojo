module LangtonsAnt
open NUnit.Framework
open Xunit
open Xunit.Extensions
open FsUnit.Xunit
open NUnit
 
 
//At a white square, turn 90° right, flip the color of the square, move forward one unit
//At a black square, turn 90° left, flip the color of the square, move forward one unit
 
type Color =
    | White
    | Black
 
type Direction =
    | North
    | West
    | East
    | South
 
let Flip color =
    match color with
    | White -> Black
    | Black -> White
 
let TurnLeft direction =
    match direction with
    | North -> West
    | East -> North
    | South -> East
    | West -> South
 
let TurnRight direction =
    direction |> TurnLeft |> TurnLeft |> TurnLeft
 
let MoveForwardDelta direction =
    match direction with
    | North -> (0, 1)
    | East -> (1, 0)
    | South -> (0, -1)
    | West -> (-1, 0)
 
let MoveForward direction (x, y) =
    let (dx, dy) = MoveForwardDelta direction
    (x + dx, y + dy)
 
let Turn color =
    match color with
    | White -> TurnRight 
    | Black -> TurnLeft 
 
let Step (color, direction, coordinate) =
    let newColor = Flip color
    let newDirection = Turn color direction
    let newCoordinate = MoveForward newDirection coordinate
    (newColor, newDirection, newCoordinate)
 
[<Fact>]
let ``Flips color``() = 
    Flip White |> should equal Black
    Flip Black |> should equal White
 
[<Fact>]
let ``Turns left``() =
    TurnLeft North |> should equal West
    TurnLeft East |> should equal North
    TurnLeft South |> should equal East
    TurnLeft West |> should equal South
 
[<Fact>]
let ``Turns right``() =
    TurnRight North |> should equal East
    TurnRight East |> should equal South
    TurnRight South |> should equal West
    TurnRight West |> should equal North
 
[<Fact>]
let ``moves forward``() =
    MoveForward East (1, 1) |> should equal (2, 1)
    MoveForward South (1, 1) |> should equal (1, 0)
    MoveForward West (1, 1) |> should equal (0, 1)
    MoveForward North (2, 2) |> should equal (2, 3)
 
[<Fact>]
let ``Turns on color``() =
    Turn White North |> should equal East
    Turn White East |> should equal South
    Turn White South |> should equal West
    Turn White West |> should equal North
    Turn Black North |> should equal West
    Turn Black East |> should equal North
    Turn Black South |> should equal East
    Turn Black West |> should equal South
   
[<Fact>]
let ``At a white square, turn 90° right, flip the color of the square, move forward one unit``() =
    Step(White, North, (1, 1)) |> should equal (Black, East, (2, 1))
    Step(White, West, (1, 1)) |> should equal (Black, North, (1, 2))
 
[<Fact>]
let ``At a black square, turn 90° left, flip the color of the square, move forward one unit``() =
    Step(Black, North, (1, 1)) |> should equal (White, West, (0, 1))