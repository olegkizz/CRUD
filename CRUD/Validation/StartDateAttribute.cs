using IdentityNLayer.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IdentityNLayer.Validation
{
    public class StartDateAttribute : ValidationAttribute, IClientModelValidator
    {
        public StartDateAttribute()
        {
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            List<GroupLessonModel> groupLessons = (List<GroupLessonModel>)value;
            int i = 0;
            bool isValid = true;
            foreach(GroupLessonModel groupLesson in groupLessons)
            {
                if (groupLesson.StartDate < DateTime.Now)
                {
                    groupLesson.Error.Add($"StartDate Less Than Now.");
                    groupLesson.StartDate = DateTime.Now;
                    isValid = false;
                }
                for(int j = i; j < groupLessons.Count(); ++j)
                {
                    if(groupLesson.StartDate < groupLessons[j].StartDate?.AddMinutes(groupLessons[j].Lesson.Duration) 
                        && groupLesson.StartDate > groupLessons[j].StartDate)
                    {
                        groupLesson.Error.Add($"StartDate entry in Duration of Lesson {j + 1}.");
                        isValid = false;
                    }
                    else if(groupLessons[j].StartDate < groupLesson.StartDate?.AddMinutes(groupLessons[j].Lesson.Duration)
                        && groupLessons[j].StartDate > groupLesson.StartDate)
                    {
                        groupLessons[j].Error.Add($"StartDate entry in Duration of Lesson {i + 1}.");
                        isValid = false;
                    }
                }
                i++;
            }
            return isValid;
        }
    }
}    