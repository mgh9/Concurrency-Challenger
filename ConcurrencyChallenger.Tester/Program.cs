using ConcurrencyChallenger.Tester;

Console.WriteLine("Hello, World!");

// let the API be ready first
await Task.Delay(5000);

var callAddOrderWithOptimisticLockTasks = TestOptimisticLock.CallAddOrderWithOptimisticLock();
var callAddOrderWithPessimisticLockTasks = TestPessimisticLock.CallAddOrderWithTestPessimisticLock();

// test# 1 (testing optimistic lock): simulate multiple concurrent requests to our API
Console.WriteLine(" >>>> START TESTING Optimistic locking");
await Task.WhenAll(callAddOrderWithOptimisticLockTasks);
Console.WriteLine(" >>>> END OF TESTING Optimistic locking. Press any key to test with Pessimistic locking");
Console.ReadLine();

// test# 2 (testing Pessimistic lock): simulate multiple concurrent requests to our API
Console.WriteLine(">>>> START OF TESTING Pessimistic locking");
await Task.WhenAll(callAddOrderWithPessimisticLockTasks);
Console.WriteLine(">>>> END TESTING Pessimistic locking");

Console.ReadLine();
