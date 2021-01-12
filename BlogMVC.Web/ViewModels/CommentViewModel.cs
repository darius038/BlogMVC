using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMVC.Web.ViewModels
{
    public class CommentViewModel
    {
        [Required]
        public int PostId { get; set; }
        [Required]        
        public string Message { get; set; }
        [Required]
        public string CommentUserName { get; set; }
    }
}
