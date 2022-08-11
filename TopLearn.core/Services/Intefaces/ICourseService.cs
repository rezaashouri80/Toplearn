using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.core.DTOs.Course;
using TopLearn.Datalayar.Entities.Course;

namespace TopLearn.core.Services.Intefaces
{
  public  interface ICourseService
    {
        #region Group Course

        List<GroupCourse> GetAllGroup();

        List<SelectListItem> GetGroupForManageCourse();

        List<SelectListItem> GetSubGroupForManageCourse(int groupId);

        List<SelectListItem> GetTeachers();

        List<SelectListItem> GetLevels();

        List<SelectListItem> GetStatus();

        GroupCourse GetGroupById(int groupId);

        void AddGroup(GroupCourse groupCourse);

        void UpdateGroup(GroupCourse groupCourse);



        #endregion

        #region Course

        int AddCourse(Course course, IFormFile img, IFormFile demo);

        List<ShowCourseForAdminViewModel> showCourseForAdmin();

        Course GetCourseById(int id);

        void UpdateCourse(Course course, IFormFile img, IFormFile demo);

        List<CourseEpisode> GetAllEpisodes(int CourseId);

        int AddEpisode(CourseEpisode episode, IFormFile episodeFile);

        bool IsExisTFileName(string filename);

        CourseEpisode GetEpisodeById(int episodeId);

        void EditEpisode(CourseEpisode episode, IFormFile episodeFile);

        void DeleteEpisode(int EpisodeId);

        Tuple<List<ShowCourseListItemViewModel>,int> GetCourseItem(int pageId = 1,int take=6, string filter= "", string getType ="all",
            string orderby ="date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups=null);

        Course GetCourseForShow(int courseId);

        List<ShowCourseListItemViewModel> GetPopulerCourses();

        bool IsCourseFree(int courseId);
       
        #endregion

        #region comments

        void AddComment(CourseComments comment);

        Tuple<List<CourseComments>,int> GetAllComments(int courseId,int pageId=1);

        #endregion

        #region Course Vote

        List<CourseVote> GetVotes(int courseId);

        void AddVote(int userId, int courseId, bool vote);

        Tuple<int, int> GetCountVotes(int courseId);

        #endregion
    }
}
