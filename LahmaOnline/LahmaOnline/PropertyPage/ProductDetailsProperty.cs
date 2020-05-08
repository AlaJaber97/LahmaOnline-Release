using System;
using System.Collections.Generic;
using System.Text;

namespace LahmaOnline.Property
{
    public class ProductDetailsProperty : BaseProperty
    {
        private int _position;
        public int Position
        {
            get { return _position; }
            set { _position = value; OnPropertyChanged(); }
        }

        private BLL.M.Mobile.Product _Product;

        public BLL.M.Mobile.Product Product
        {
            get { return _Product?? (_Product = new BLL.M.Mobile.Product()); }
            set { _Product = value; OnPropertyChanged(); }
        }

        private ReviewCommentUser _reviewComment;
        public ReviewCommentUser ReviewComment
        {
            get { return _reviewComment ?? (_reviewComment = new ReviewCommentUser()); }
            set { _reviewComment = value; OnPropertyChanged(); }
        }
    }
    public class ReviewCommentUser
    {
        public string Comment { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
    public class ReviewEvaluationUser
    {
        public int Rating { get; set; } = 0;
        public string Comment { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
