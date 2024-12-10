using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace cookBook;

public class Rating { 

    public int RatingId{get;set;}
    private double _ratingValue;

    public double RatingValue
    {
        get => _ratingValue;
        set
        {
            if (value < 0 || value > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(RatingValue), "Rating value must be between 0 and 5.");
            }
            _ratingValue = value;
        }
    }

    public Rating(double rating)
    {
        RatingValue = rating; 
    }

    public Rating(){}

    }

