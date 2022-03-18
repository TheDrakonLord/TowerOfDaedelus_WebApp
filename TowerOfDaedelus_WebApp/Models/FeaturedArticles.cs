using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TowerOfDaedelus_WebApp.Models
{
    public class FeaturedArticles
    {
        [Key]
        public int ArticleID { get; set; }
        public Uri? ArticleUri { get; set; }
        public bool IsFeatured { get; set; }
    }
}
