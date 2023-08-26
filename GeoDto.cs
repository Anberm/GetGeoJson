using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GetGeoJson;
public class GeoDto
{
    public string Type { get; set; }

    public List<GeoFeatures> Features { get; set; }


}

public class GeoFeatures
{
    public string Type { get; set; }
    public GeoProperty Properties { get; set; }
}
public class GeoProperty
{
    public string AdCode { get; set; }

    public int ChildrenNum { get; set; }
}