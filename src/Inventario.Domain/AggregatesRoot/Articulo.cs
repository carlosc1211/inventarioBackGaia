namespace Inventario.Domain.AggregatesRoot
{
    public class Articulo
    {
        public Guid Id { get; private set; }
        public string Model { get; private set; }
        public string SalesSample { get; private set; }
        public string SourcingTrackingNumber { get; private set; }
        public string Warrant { get; private set; }
        public string OriginCountry { get; private set; }
        public string DestinationCountry { get; private set; }
        public string Warehouse { get; private set; }
        public DateTime TimeOfArrival { get; private set; }
        public string CurrentLocation { get; private set; }
        public string Incoterm { get; private set; }
        public string ContainerNumber { get; private set; }

        protected Articulo() { }

        protected Articulo(string model, string salesSample, string sourcingTrackingNumber, string warrant, string originCountry, string destinationCountry, string warehouse, DateTime timeOfArrival, string currentLocation, string incoterm, string containerNumber)
        {
            Model = model;
            SalesSample = salesSample;
            SourcingTrackingNumber = sourcingTrackingNumber;
            Warrant = warrant;
            OriginCountry = originCountry;
            DestinationCountry = destinationCountry;
            Warehouse = warehouse;
            TimeOfArrival = timeOfArrival;
            CurrentLocation = currentLocation;
            Incoterm = incoterm;
            ContainerNumber = containerNumber;
        }

        public static Articulo Crear(string model, string salesSample, string sourcingTrackingNumber, string warrant, string originCountry, string destinationCountry, string warehouse, DateTime timeOfArrival, string currentLocation, string incoterm, string containerNumber)
        {
            return new Articulo(model, salesSample, sourcingTrackingNumber, warrant, originCountry, destinationCountry, warehouse, timeOfArrival, currentLocation, incoterm, containerNumber);
        }
    }
}
