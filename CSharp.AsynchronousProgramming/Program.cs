// See https://aka.ms/new-console-template for more information
using CSharp.AsynchronousProgramming;

Console.WriteLine("Program Start");

//LifeBeforeAsync.RunExample();

await LifeAfterAsync.RunExample();

Console.WriteLine("Program End");
Console.ReadLine();