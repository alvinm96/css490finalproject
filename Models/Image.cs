using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
  public class Image
  {
    public int ImageId { get; set; }
    public string ImageName { get; set; }
    public string GroupName { get; set; }
    public string UserName { get; set; }
    public string Description { get; set; }
    public string ImageObj { get; set; }

    public Image(int imageId, string imageName, string groupName, string userName, string description, string image)
    {
      ImageId = imageId;
      ImageName = imageName;
      GroupName = groupName;
      UserName = userName;
      Description = description;
      ImageObj = image;
    }
  }
}
