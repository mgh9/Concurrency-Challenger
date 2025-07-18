using ConcurrencyChallenger.Tester;

Console.WriteLine("Hello, World!");

// let the API be ready first
await Task.Delay(5000);

var callAddOrderWithOptimisticLockTasks = TestOptimisticLock.CallAddOrderWithOptimisticLock();

// test# 1 (testing optimistic lock): simulate multiple concurrent requests to our API
await Task.WhenAll(callAddOrderWithOptimisticLockTasks);

Console.ReadLine();
