namespace ConcurrencyChallenger.Tester;

internal class TestOptimisticLock
{
    internal static IEnumerable<Task> CallAddOrderWithOptimisticLock()
    {
        var tasks = Enumerable.Range(0, 100).Select(i =>
            Task.Run(async () =>
            {
                try
                {
                    using var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7287/api/orders/1");
                    var response = await client.SendAsync(request);
                    //response.EnsureSuccessStatusCode();

                    var res = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[{i}] Status >>>>>> : {response.StatusCode} --- content: `{res}`");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERRRROR : " + ex.Message);
                }
            })
        );
        return tasks;
    }
}
