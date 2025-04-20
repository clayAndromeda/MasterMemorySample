using MasterMemory;
using MessagePack;
using MessagePack.Resolvers;
using MasterMemorySamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterMemorySamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MasterMemoryサンプルアプリケーション");
            Console.WriteLine("-----------------------------------");

            // MessagePackの初期化（ボイラープレート）
            var resolver = CompositeResolver.Create(
                MasterMemoryResolver.Instance,
                StandardResolver.Instance
            );
            var options = MessagePackSerializerOptions.Standard.WithResolver(resolver);
            MessagePackSerializer.DefaultOptions = options;

            // サンプルデータの作成
            Console.WriteLine("サンプルデータを作成しています...");
            var persons = new List<Person>
            {
                new Person
                {
                    Id = 1,
                    Name = "田中太郎",
                    Age = 30,
                    Email = "tanaka@example.com",
                    RegisterDate = DateTime.Now.AddDays(-30)
                },
                new Person
                {
                    Id = 2,
                    Name = "鈴木花子",
                    Age = 25,
                    Email = "suzuki@example.com",
                    RegisterDate = DateTime.Now.AddDays(-20)
                },
                new Person
                {
                    Id = 3,
                    Name = "佐藤次郎",
                    Age = 40,
                    Email = "sato@example.com",
                    RegisterDate = DateTime.Now.AddDays(-10)
                }
            };

            // DatabaseBuilderを使ってバイナリデータを生成する
            Console.WriteLine("バイナリデータを生成しています...");
            var databaseBuilder = new DatabaseBuilder();
            databaseBuilder.Append(persons);
            var binary = databaseBuilder.Build();
            Console.WriteLine($"バイナリデータのサイズ: {binary.Length} bytes");

            // MemoryDatabaseでバイナリから読み込む
            Console.WriteLine("\nバイナリデータを読み込んでいます...");
            var memoryDatabase = new MemoryDatabase(binary);

            // テーブルからデータを検索
            Console.WriteLine("\n全てのPersonデータを表示します:");
            var personTable = memoryDatabase.PersonTable;
            foreach (var person in personTable.All)
            {
                Console.WriteLine(person);
            }

            Console.WriteLine("\nIDによる検索:");
            var person1 = personTable.FindById(1);
            Console.WriteLine($"ID=1の人: {person1}");

            Console.WriteLine("\n範囲検索（年齢が25以上30以下）:");
            foreach (var person in personTable.All.Where(p => p.Age >= 25 && p.Age <= 30))
            {
                Console.WriteLine(person);
            }

            Console.WriteLine("\nプログラムを終了するには何かキーを押してください...");
            Console.ReadKey();
        }
    }
}
