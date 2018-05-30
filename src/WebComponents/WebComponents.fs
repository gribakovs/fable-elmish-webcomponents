namespace WebComponents

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open System
open Fable.Import
open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Elmish.Debug
open Elmish.HMR

type Template<'arg, 'model, 'msg, 'view> = {
    init : 'arg -> 'model * Cmd<'msg>
    update : 'msg -> 'model -> 'model * Cmd<'msg>
    subscribe : 'model -> Cmd<'msg>
    view : 'model -> Dispatch<'msg> -> 'view
    setState : 'model -> Dispatch<'msg> -> unit
    onError : (string*exn) -> unit
}

module Template =

    let create init update root pageParser urlUpdate nodeid =
        Program.mkProgram init update root
        |> Program.toNavigable (parseHash pageParser) urlUpdate
        #if DEBUG
        |> Program.withDebugger
        |> Program.withHMR
        #endif
        // |> Program.withReact nodeid
        |> Program.run