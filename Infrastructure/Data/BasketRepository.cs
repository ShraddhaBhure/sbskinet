using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System.Text.Json;


namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database; 

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);
            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
           
            var serializedBasket = JsonSerializer.Serialize(basket);

           
            var created = await _database.StringSetAsync(basket.Id, serializedBasket, TimeSpan.FromMinutes(30)); // Adjust the expiration time as needed

            if (!created)
            {
                return null; 
            }

            return basket;
        }
    }
}
