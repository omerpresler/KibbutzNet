using Newtonsoft.Json;

namespace Backend.Business.Utils
{
    public class House
    {
        public int HouseId;

        public string Direction;

        //will be a google map link
        public object mapsLink;

        public House(int houseId, string direction)
        {
            this.HouseId = houseId;
            this.Direction = direction;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}