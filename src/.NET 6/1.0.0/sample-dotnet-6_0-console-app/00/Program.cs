using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace sample_dotnet_6_0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // var summary = BenchmarkRunner.Run<SringVsTypeAsKeyDictionary>();
            new TypeAsKeyReferenceCheck().Run();
        }
    }

    internal sealed class TypeAsKeyReferenceCheck
    {
        private static readonly ConcurrentDictionary<IItem, IItemDetail> _store = new ConcurrentDictionary<IItem, IItemDetail>();

        public void Run()
        {

            var itemDetail1 = new ItemDetail("John Doe");
            var itemDetail2 = new ItemDetail("Julia Ray");
            var itemDetail3 = new ItemDetail("Jackie Maine");

            var item1 = new Item(itemDetail1.Name, itemDetail1);
            var item11 = new Item(itemDetail2.Name, itemDetail2);
            var item2 = new Item(itemDetail2.Name, itemDetail2);
            var item22 = new Item(itemDetail3.Name, itemDetail3);
            var item3 = new Item(itemDetail3.Name, itemDetail3);
            var item33 = new Item(itemDetail1.Name, itemDetail1);

            _store.TryAdd(item1, item1.Detail ?? new ItemDetail(item1.Name));
            _store.TryAdd(item11, item11.Detail ?? new ItemDetail(item11.Name));
            _store.TryAdd(item2, item2.Detail ?? new ItemDetail(item2.Name));
            _store.TryAdd(item22, item22.Detail ?? new ItemDetail(item22.Name));
            _store.TryAdd(item3, item3.Detail ?? new ItemDetail(item3.Name));
            _store.TryAdd(item33, item33.Detail ?? new ItemDetail(item33.Name));

            _store.TryGetValue(item1, out var result1);
            _store.TryGetValue(item11, out var result11);
            _store.TryGetValue(item2, out var result2);
            _store.TryGetValue(item22, out var result22);
            _store.TryGetValue(item3, out var result3);
            _store.TryGetValue(item33, out var result33);

            Console.WriteLine(result1);
            Console.WriteLine(result11);
            Console.WriteLine(result2);
            Console.WriteLine(result22);
            Console.WriteLine(result3);
            Console.WriteLine(result33);
        }

    }

    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    internal sealed class SringVsTypeAsKeyDictionary
    {
        private static readonly ConcurrentDictionary<IItem, double> _dictionary1 = new ConcurrentDictionary<IItem, double>();
        private static readonly ConcurrentDictionary<string, double> _dictionary2 = new ConcurrentDictionary<string, double>();

        [GlobalSetup]
        public void GlobalSetup()
        {
            //Write your initialization code here
        }

        [Benchmark]
        public void GetByClassKey()
        {
            double val;
            IItem _item = new Item("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.");
            _dictionary1.TryAdd(_item, 0);
            _dictionary1.TryGetValue(_item, out val);
            if(val > 0)
            {
                Console.WriteLine(val);
            }
        }

        [Benchmark]
        public void GetByStringKey()
        {
            double val;
            IItem _item = new Item("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.");
            var itemString = _item.ToString();
            _dictionary2.TryAdd(itemString, 0);
            _dictionary2.TryGetValue(itemString, out val);
            if (val > 0)
            {
                Console.WriteLine(val);
            }
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            //Write your cleanup logic here
        }
    }

    internal sealed record Item(string Name, IItemDetail? Detail = null) : IItem;

    public interface IItem
    {
        string Name { get; }

        IItemDetail? Detail { get; }
    }

    internal sealed record ItemDetail(string Name) : IItemDetail
    {
        public Guid Id => Guid.NewGuid();
    }

    public interface IItemDetail
    {
        string Name { get; }
    }
}