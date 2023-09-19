using CsvHelper;
using CsvHelper.Configuration;
using M0LTE.AdifLib;
using System.Globalization;

if (args.Length != 1)
{
    Console.WriteLine($"Expected one argument, got {args.Length}");
    return;
}

var csvFile = args[0];
if (!File.Exists(csvFile))
{
    Console.WriteLine($"File not found: {csvFile}");
    return;
}

using var fileStream = File.OpenRead(csvFile);
using var reader = new StreamReader(fileStream);
using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { MissingFieldFound = null });

csvReader.Read();
if (!csvReader.ReadHeader())
{
    Console.WriteLine("Could not read header");
    return;
}

var adifFile = new AdifFile
{
    Header = new AdifHeaderRecord { ProgramId = "ToppLog" }
};

while (csvReader.Read())
{
    AdifContactRecord record = new()
    {
        Call = csvReader["Callsign"],
        FreqMHz = csvReader["Frequency"],
        Mode = csvReader["Mode"],
        RstSent = csvReader["TX-RST"],
        RstReceived = csvReader["RX-RST"],
        Operator = csvReader["Operator"],
        StationCallsign = csvReader["Station"],
        TxPower = csvReader["Power"],
        QsoStart = DateTime.ParseExact(csvReader["Date"] + csvReader["UTC-On"], "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
        QsoEnd = DateTime.ParseExact(csvReader["Date"] + csvReader["UTC-Off"], "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
        GridSquare = csvReader["Locator"],
        MyGridSquare = csvReader["StationLocator"]
    };

    adifFile.Records.Add(record);
}

Console.WriteLine(adifFile.ToString());
