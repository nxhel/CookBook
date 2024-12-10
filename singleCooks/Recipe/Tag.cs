using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace cookBook;
  
    
public class Tag
{
    
    [Key]
    public int TagId { get; set; }

    private string _tagName;

    public string TagName
    {
        get => _tagName;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(TagName), "Tag name cannot be null or empty.");
            }
            _tagName = value;
        }
    }

    public List<Recipe> Recipes { get; set; }

    public Tag(string tagName){
        TagName = tagName;
    }

    public Tag(){}
    public string toString(){
        return TagName;
    }

    }

    


