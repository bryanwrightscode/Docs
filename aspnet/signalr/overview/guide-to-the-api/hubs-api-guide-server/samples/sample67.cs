// For the complete example, go to 
// https://docs.microsoft.com/en-us/aspnet/signalr/overview/getting-started/tutorial-server-broadcast-with-signalr
// This sample only shows code related to getting and using the SignalR context.
public class StockTicker
{
    // Singleton instance
    private readonly static Lazy<StockTicker> _instance = new Lazy<StockTicker>(
        () => new StockTicker(GlobalHost.ConnectionManager.GetHubContext<StockTickerHub>()));

    private IHubContext _context;

    private StockTicker(IHubContext context)
    {
        _context = context;
    }

    // This method is invoked by a Timer object.
    private void UpdateStockPrices(object state)
    {
        foreach (var stock in _stocks.Values)
        {
            if (TryUpdateStockPrice(stock))
            {
                _context.Clients.All.updateStockPrice(stock);
            }
        }
    }
