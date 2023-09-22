using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtAuction.Models.ArtworkVM;

public class ArtworkListItem {
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime BiddingFinishDate { get; set; }
}
