using AutoMapper;
using BlogMVC.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogMVC.Web.ViewModels.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PostViewModel, Post>();
            //.ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<Post, PostViewModel > ();

            CreateMap<CategoryViewModel, Category>();
        }
    }
}
