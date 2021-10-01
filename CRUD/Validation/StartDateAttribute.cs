using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
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

        protected override ValidationResult IsValid(object value, ValidationContext validateContext)
        {
            if (value == null)
                return ValidationResult.Success;

            IGroupLessonService _groupLessonService = (IGroupLessonService)
                validateContext.GetService(typeof(IGroupLessonService));

            IGroupService _groupService = (IGroupService)
            validateContext.GetService(typeof(IGroupService));

            List<GroupLessonModel> groupLessons = (List<GroupLessonModel>)value;
            Group group = _groupService.GetByIdAsync(((List<GroupLessonModel>)value).FirstOrDefault().GroupId).Result;
            int i = 0;
            ValidationResult isValid = ValidationResult.Success;
            foreach (GroupLessonModel groupLesson in groupLessons)
            {
                for (int j = i; j < groupLessons.Count(); ++j)
                {
                    if (groupLesson.StartDate < groupLessons[j].StartDate?.AddMinutes(groupLessons[j].Lesson.Duration)
                        && groupLesson.StartDate > groupLessons[j].StartDate)
                    {
                        groupLesson.Error.Add($"StartDate entry in Duration of Lesson {j + 1}.");
                        isValid = null;
                    }
                    else if (groupLessons[j].StartDate < groupLesson.StartDate?.AddMinutes(groupLesson.Lesson.Duration)
                        && groupLessons[j].StartDate > groupLesson.StartDate)
                    {
                        groupLessons[j].Error.Add($"StartDate entry in Duration of Lesson {i + 1}.");
                        isValid = null;
                    }
                }
                i++;
                if (groupLesson.StartDate < DateTime.Now)
                {
                    if (group.Status == GroupStatus.Started)
                    {
                        DateTime? endCurrentLesson = _groupLessonService.GetByIdAsync(groupLesson.Id)
                                .Result.StartDate;
                        if (endCurrentLesson > DateTime.Now)
                        {
                            groupLesson.Error.Add($"StartDate Less Than Now.");
                            groupLesson.StartDate = DateTime.Now.AddDays(1);
                            isValid = null;
                        }
                    }
                    else
                    {
                        groupLesson.Error.Add($"StartDate Less Than Now.");
                        groupLesson.StartDate = DateTime.Now.AddDays(1);
                        isValid = null;
                    }
                }
            }
            return isValid;
        }
    }
}