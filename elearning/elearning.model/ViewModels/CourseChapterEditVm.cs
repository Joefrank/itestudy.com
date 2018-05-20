
        /*****************************************************************
        * Code Generated at 20/05/2018 23:56:11
        * By Code MVCCodeGenerator
        *
        *
        ******************************************************************/
        
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace elearning.model.ViewModels
{
    public class CourseChapterEditVm
    {     
         public Int32 Id{get;set;}
 public Int32 CourseId{get;set;}
 public String Title{get;set;}
 public String Description{get;set;}
 public DateTime DateCreated{get;set;}
 public DateTime DateLastModified{get;set;}
 public Int32 StatusId{get;set;}
 public Int32 CreatedBy{get;set;}
 public Int32 LastModifiedBy{get;set;}
 public Int32 LessonCount{get;set;}
 public Int32 TutorialCount{get;set;}
 public ICollection lessons{get;set;}

        public bool ShowError { get; set; }
    }
}

