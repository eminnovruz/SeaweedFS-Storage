using FileServer_Asp.JsonModels.Components;

namespace FileServer_Asp.JsonModels;

public class LookupJsonModel
{
    public string VolumeOrFileId { get; set; }
    public List<LocationModel> Locations { get; set; }
}
