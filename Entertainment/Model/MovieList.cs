using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Entertainment.Model
{
    [DataContract(Name = "MovieList")]
    public class MovieList
    {
        [DataMember(Name = "Movies")]
        public List<Movie> Movies { get; set; }
    }
}
