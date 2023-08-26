// See https://aka.ms/new-console-template for more information
using GetGeoJson;

Console.WriteLine("Hello, World!");

GeoJson.GetAllGeo().ConfigureAwait(true);

Console.WriteLine("获取完成");
Console.ReadKey();