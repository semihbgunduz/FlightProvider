using CoreWCF;
using System.Runtime.Serialization;

namespace FlightProvider
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class SearchRequest
    {
        [DataMember(Order = 1)]
        public string Origin { get; set; } = string.Empty;

        [DataMember(Order = 2)]
        public string Destination { get; set; } = string.Empty;

        [DataMember(Order = 3)]
        public DateTime DepartureDate { get; set; }
        [DataMember(Order = 4)]
        public DateTime ReturnDate { get; set; }
    }


    [DataContract]
    public class SearchResult
    {
        [DataMember]
        public bool HasError { get; set; }
        [DataMember]
        public List<FlightOption> FlightOptions { get; set; } = new List<FlightOption>();

    }

    public class FlightOption
    {
        [DataMember]
        public string FlightNumber { get; set; } = string.Empty;
        [DataMember]
        public DateTime DepartureDateTime { get; set; }
        [DataMember]
        public DateTime ArrivalDateTime { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public decimal? ReturnCode { get; set; }

    }


    [ServiceContract]
    public interface IAirSearch
    {
        [OperationContract]
        SearchResult AvailabilitySearch(SearchRequest request);
    }


    public class AirSearch : IAirSearch
    {
        public SearchResult AvailabilitySearch(SearchRequest request)
        {
            int hour = 1;
            bool isReturn = request.ReturnDate != DateTime.MinValue ? true : false;
            SearchResult searchResult = new SearchResult
            {
                HasError = false,
                FlightOptions = new List<FlightOption>
                {
                    new FlightOption {
                        ArrivalDateTime= request.DepartureDate.AddHours(hour++),
                        DepartureDateTime =request.DepartureDate.AddHours(hour++),
                        FlightNumber = $"TK{hour}",
                        Price = 100.0M + hour*10,
                        ReturnCode = isReturn ? 1 : 0
                    },
                    new FlightOption {
                        ArrivalDateTime= request.DepartureDate.AddHours(hour++),
                        DepartureDateTime =request.DepartureDate.AddHours(hour++),
                        FlightNumber = $"TK{hour}",
                        Price = 100.0M + hour*10,
                        ReturnCode = isReturn ? 2 : 0
                    },
                    new FlightOption {
                        ArrivalDateTime= request.DepartureDate.AddHours(hour++),
                        DepartureDateTime =request.DepartureDate.AddHours(hour++),
                        FlightNumber = $"TK{hour}",
                        Price = 100.0M + hour*10,
                        ReturnCode = isReturn ? 3 : 0
                    },
                }
            };
            if (isReturn)
            {
                List<FlightOption> returnFlights = new List<FlightOption>
                {
                    new FlightOption {
                        ArrivalDateTime= request.ReturnDate.AddHours(hour++),
                        DepartureDateTime =request.ReturnDate.AddHours(hour++),
                        FlightNumber = $"TK{hour+3}",
                        Price = 100.0M + hour*10,
                        ReturnCode = 1
                    },
                    new FlightOption {
                        ArrivalDateTime= request.ReturnDate.AddHours(hour++),
                        DepartureDateTime =request.ReturnDate.AddHours(hour++),
                        FlightNumber = $"TK{hour+3}",
                        Price = 100.0M + hour*10,
                        ReturnCode = 2
                    },
                    new FlightOption {
                        ArrivalDateTime= request.ReturnDate.AddHours(hour++),
                        DepartureDateTime =request.ReturnDate.AddHours(hour++),
                        FlightNumber = $"TK{hour+3}",
                        Price = 100.0M + hour*10,
                        ReturnCode = 3
                    },
                };
                searchResult.FlightOptions.AddRange(returnFlights);
            }
            return searchResult;
        }
    }
}
