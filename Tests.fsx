open System
open System.IO
type Cliente = { Idade : int}
let where filtro clientes =
    seq {
        for cliente in clientes do
            if filtro cliente then
                yield cliente }

let clientes = [ {Idade = 21}; { Idade = 35 }; { Idade = 36 }]

let whereClientesTenhenMaisDe35 clientes =
    seq {
        for cliente in clientes do
            if cliente.Idade > 35 then
                yield cliente }

let maiorDe35 cliente = cliente.Idade > 35

clientes |> where maiorDe35
clientes |> where (fun cliente -> cliente.Idade > 35)

let imprimirIdadeCliente writer cliente =
    if cliente.Idade < 13 then writer "Raparig@!"
    elif cliente.Idade < 20 then writer "Adolescente"
    else writer "Adulto!"
//creamos dependency injection, neste caso de writer, co cal substituimos Console.WriteLine
//F# vai identificar writer como unha funcion que toma un string e devolve 'a -generics-
//Asi agora calquera funcion que tome un string pode usarse en lugar de Console.WriteLine

//chamando a imprimirIdadeCliente con Console.WriteLine como dependencia
imprimirIdadeCliente Console.WriteLine { Idade = 21}
//Aplicacon parcialmente imprimirIdadeCliente para crear unha version limitada do mesmo
let imprimirEnPantalla = imprimirIdadeCliente Console.WriteLine
imprimirEnPantalla { Idade = 21 }
imprimirEnPantalla { Idade = 12 }
imprimirEnPantalla { Idade = 18 }

//agora escribimos unha funcion que pode actuar como dependencia, para imprimir no sistema de arquivos. Usaremos System.IO.File.WriteAllText como a base para a dependencia
//Creamos un escritor File System que e compatible con imprimirIdadeCliente
let escribirEnArquivo texto = File.WriteAllText(@"C:\temp\saida.txt", texto)

let imprimirEnArquivo = imprimirIdadeCliente escribirEnArquivo
imprimirEnArquivo { Idade = 21 }

let contidosDoDisco = File.ReadAllText @"C:\temp\saida.txt"