namespace carApp.Models
{
    public class Repository
    {
        private static readonly List<Cars>
        _cars = new();
        static Repository(){
            _cars = new List<Cars>{
            new Cars(){Id = 1, Marka = "Renault", Model = "Twizy", Image = "twizy.jpeg", Motor = "60 bg", Yıl = 2016},
            new Cars(){Id = 2, Marka = "Renault", Model = "Symbol", Image = "symbol.jpeg", Motor = "90 bg", Yıl = 2019},
            new Cars(){Id = 3, Marka = "Tofaş", Model = "Şahin", Image = "sahin.jpeg", Motor = "75 bg", Yıl = 2016},
            new Cars(){Id = 4, Marka = "Volkswagen", Model = "Polo", Image = "polo.jpeg", Motor = "110 bg", Yıl = 2009},
            new Cars(){Id = 5, Marka = "Volkswagen", Model = "Passat", Image = "passat.jpeg", Motor = "160 bg", Yıl = 2021}
        };
        }
        public static List<Cars> Cars {
            get {
                return _cars;
            }
        }
        public static void addCar(Cars model){
            _cars.Add(model);
        }
        public static Cars? GetById(int? id){
        return _cars.FirstOrDefault(i => i.Id == id);
        }
    }
}