﻿// See https://aka.ms/new-console-template for more information
using CsvColumnsExtractorToTxt;

try
{   
    var csvtotxt = new CsvToTxtWorker();
    await csvtotxt.RunAsync(args, default);
}
catch(AggregateException exs)
{      
    foreach(var ex in exs.InnerExceptions)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.StackTrace);
    }

    string msg = "Must fit:\ncsv_file separator column_numbers_to_parse";

    Console.WriteLine(msg);
}

