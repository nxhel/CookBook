namespace cookBook{
    public class Nutrient{


        public int NutrientId { get; set; }
        private string _name;
        private double _amount;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(_name), "Name cannot be null or empty.");
                }
                _name = value;
            }
        }

        public double Amount
        {
            get => _amount;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(_amount), "Amount cannot be negative.");
                }
                _amount = value;
            }
        }


        public Nutrient(string name, double amount)
        {
            Name = name;
            Amount = amount;
        }

        public Nutrient(){}

        


    }
}

