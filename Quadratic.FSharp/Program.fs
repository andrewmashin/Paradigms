open System

let solveQuadraticEquation a b c =
    match a, b, c with
    | 0.0, 0.0, 0.0 -> "Бесконечное количество решений"
    | 0.0, 0.0, _ -> "Решений нет"
    | 0.0, _, _ -> sprintf "Линейное уравнение, решение: x = %f" (-c / b)
    | _, _, _ ->
        let discriminant = b * b - 4.0 * a * c
        if discriminant > 0.0 then
            let sqrtD = Math.Sqrt(discriminant)
            let x1 = (-b + sqrtD) / (2.0 * a)
            let x2 = (-b - sqrtD) / (2.0 * a)
            sprintf "Два действительных корня: x1 = %f, x2 = %f" x1 x2
        elif discriminant = 0.0 then
            let x = -b / (2.0 * a)
            sprintf "Один действительный корень: x = %f" x
        else
            "Действительных корней нет"

[<EntryPoint>]
let main argv =
    let a = 1
    let b = -3
    let c = 2

    let result = solveQuadraticEquation a b c
    printfn "%s" result

    0 // возвращаем целочисленный код завершения
