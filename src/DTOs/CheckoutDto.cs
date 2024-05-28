public class CheckoutCreateDto
     {
        public Guid ProductId {get ; set;}
        public int Quantity {get ; set;}
    }

     public class OrderCheckoutReadDto
    {
         public Guid Id {get ; set;}
        public DateTime CreatedAt {get ; set;} 
        public double TotalPrice {get ; set;}
        public string Address {get; set;}
    }

    