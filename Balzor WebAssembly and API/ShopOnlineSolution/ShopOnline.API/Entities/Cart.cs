namespace ShopOnline.API.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public List<CartItem>? CartItems { get; set; }
    }
}
