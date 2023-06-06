using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CsvColumnsExtractorToTxt
{


    public class CsvToTxtWorker
    {
        public void Run(string[] args, CancellationToken token)
        {
            if (args.Length < 3) return;

            char[] wrappers = { '\'', '"', '<', '>', '[', ']', '(', ')' };

            string csvFile = args[0];
            string separator = args[1];
            List<string> columnNumbersAsString = new List<string>(args.Skip(2));

            List<int> columnNumbersAsInt = columnNumbersAsString
                .Where(col => int.TryParse(col, out _))
                .Select(col => int.Parse(col))
                .ToList();

            using var streamReader = new StreamReader(csvFile);
            var streamWriterList = columnNumbersAsInt.Select(num => new StreamWriter($"{num}.txt")).ToList();

            string line = String.Empty;

            while ((line = streamReader.ReadLine()) != null)
            {

                string[] cols = line.Split(separator);

                for (int i = 0; i < columnNumbersAsInt.Count; i++)
                {
                    try
                    {
                        if (cols[columnNumbersAsInt[i]].Length == 0) continue;
                        streamWriterList[i].WriteLine(cols[columnNumbersAsInt[i]].TrimStart(wrappers).TrimEnd(wrappers));
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            streamWriterList.ForEach(writer => writer.Dispose());
        }
        public async Task RunAsync(string[] args, CancellationToken token)
        {
            if (args.Length < 3) return;

            char[] wrappers = { '\'', '"', '<', '>', '[', ']', '(', ')' };

            string csvFile = args[0];
            string separator = args[1];
            List<string> columnNumbersAsString = new List<string>(args.Skip(2));

            List<int> columnNumbersAsInt = columnNumbersAsString
                .Where(col => int.TryParse(col, out _))
                .Select(col => int.Parse(col))
                .ToList();

            using var streamReader = new StreamReader(csvFile);
            var streamWriterList = columnNumbersAsInt.Select(num => new StreamWriter($"{num}.txt")).ToList();

            string line = String.Empty;

            while((line = await streamReader.ReadLineAsync()) != null)
            {
                
                string[] cols = line.Split(separator);

                for(int i = 0; i < columnNumbersAsInt.Count; i++)
                {
                    try
                    {
                        if (cols[columnNumbersAsInt[i]].Length == 0) continue;
                        await streamWriterList[i].WriteLineAsync(cols[columnNumbersAsInt[i]].TrimStart(wrappers).TrimEnd(wrappers));
                    }
                    finally { }                   
                }
            }

            streamWriterList.ForEach(writer => writer.DisposeAsync());
        }
        
               
    }
}
