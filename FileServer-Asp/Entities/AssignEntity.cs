using FileServer_Asp.Entities.Common;

namespace FileServer_Asp.Entities;

public class AssignEntity : BaseEntity
{
    public string SecretName { get; set; }
    public string Fid { get; set; }
    public string PublicUrl { get; set; }
}
