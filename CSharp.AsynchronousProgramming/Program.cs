// See https://aka.ms/new-console-template for more information
using CSharp.AsynchronousProgramming;

Console.WriteLine("Program Start");

//LifeBeforeAsync.RunExample();

//await LifeAfterAsync.RunExample();

//await ParallelProgramming.RunExample();

//await AsyncAwaitInActionExample.GetLibraries();

Console.WriteLine($"Current Thread Id: {Thread.CurrentThread.ManagedThreadId}");

CustomTask.Run(() => Console.WriteLine($"Current Thread Id: {Thread.CurrentThread.ManagedThreadId}"));

Console.WriteLine("Program End");
Console.ReadLine();