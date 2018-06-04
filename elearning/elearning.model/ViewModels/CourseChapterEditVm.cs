
/*****************************************************************
* Code Generated at 31/05/2018 16:59:52
* By Code MVCCodeGenerator
*
*
******************************************************************/
        
using System;
using System.ComponentModel.DataAnnotations;

namespace elearning.model.ViewModels
{
    public class CourseChapterEditVm
    {     
         public int Id{get;set;}
         [Required(ErrorMessage="CourseId is required!" )]
         public int CourseId{get;set;}
         [Required(ErrorMessage="Title is required!" )]
         [MinLength(5, ErrorMessage = "Title is too short - min 5 characters.")]
         public string Title{get;set;}
         [Required(ErrorMessage="Description is required!" )]
         [MinLength(25, ErrorMessage = "Description is too short - at least 25 characters.")]
         public string Description{get;set;}
         public DateTime DateCreated{get;set;}
         public DateTime? DateLastModified{get;set;}
         public int StatusId{get;set;}
         public int CreatedBy{get;set;}
         public int? LastModifiedBy{get;set;}
         public int LessonCount{get;set;}
         public int TutorialCount{get;set;}
         public bool ShowError { get; set; }
    }
}

