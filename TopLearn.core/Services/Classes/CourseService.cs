using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TopLearn.Core.Convertors;
using TopLearn.core.DTOs.Course;
using TopLearn.core.Generator;
using TopLearn.core.Security;
using TopLearn.core.Services.Intefaces;
using TopLearn.Datalayar.Context;
using TopLearn.Datalayar.Entities.Course;

namespace TopLearn.core.Services.Classes
{
   public class CourseService:ICourseService
   {
       private TopLearnContext _context;

       public CourseService(TopLearnContext context)
       {
           _context = context;
       }

        public List<GroupCourse> GetAllGroup()
        {
            return _context.GroupCourses.Include(g=>g.GroupCourses).ToList();
        }

        public List<SelectListItem> GetGroupForManageCourse()
        {
            return _context.GroupCourses.Where(g => g.ParentId == null)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetSubGroupForManageCourse(int groupId)
        {
            return _context.GroupCourses.Where(g => g.ParentId == groupId)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetTeachers()
        {
            return _context.UserRoles.Where(u => u.RoleId == 2)
                .Include(r => r.User)
                .Select(u => new SelectListItem()
                {
                    Text = u.User.UserName,
                    Value = u.UserId.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetLevels()
        {
            return _context.CourseLevels.Select(l => new SelectListItem()
            {
                Value = l.LevelId.ToString(),
                Text = l.LevelTitle
            }).ToList();
        }

        public List<SelectListItem> GetStatus()
        {
            return _context.CourseStatus.Select(l => new SelectListItem()
            {
                Value = l.StatusId.ToString(),
                Text = l.StatusTitle
            }).ToList();
        }

        public GroupCourse GetGroupById(int groupId)
        {
            return _context.GroupCourses.Include(c=>c.GroupCourses).Single(c=>c.GroupId==groupId);
        }

        public void AddGroup(GroupCourse groupCourse)
        {
            _context.GroupCourses.Add(groupCourse);
            _context.SaveChanges();
        }

        public void UpdateGroup(GroupCourse groupCourse)
        {
            _context.GroupCourses.Update(groupCourse);
            _context.SaveChanges();
        }

        public int AddCourse(Course course, IFormFile img, IFormFile demo)
        {
            course.ImageName = "no-photo.jpg";
            course.CreateDate=DateTime.Now;
            if (img != null && img.IsImage())
            {
                course.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(img.FileName);
                string ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image",
                    course.ImageName);

                using (var stream = new FileStream(ImagePath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }

                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb", course.ImageName);


                ImageConvertor imgResizer = new ImageConvertor();
                imgResizer.Image_resize(ImagePath,thumbPath,200);

            }

            if (demo != null)
            {
                course.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(demo.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/demos",
                    course.ImageName);

                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    demo.CopyTo(stream);
                }
            }

            _context.Courses.Add(course);
            _context.SaveChanges();

            return course.CourseId;
        }

        public List<ShowCourseForAdminViewModel> showCourseForAdmin()
        {
            return _context.Courses.Include(c=>c.User).Select(c => new ShowCourseForAdminViewModel()
            {
                CourseTitle = c.CourseTitle,
                CourseId = c.CourseId,
                EpisodeCount = c.CourseEpisodes.Count,
                ImageName = c.ImageName,
                TeacherName = c.User.UserName
            }).ToList();
        }

        public Course GetCourseById(int id)
        {
            return _context.Courses.Find(id);
        }

        public void UpdateCourse(Course course, IFormFile img, IFormFile demo)
        {
            course.UpdateDate=DateTime.Now;

            if (img != null && img.IsImage())
            {
                
                if (course.ImageName != "no-photo.jpg")
                {
                    string imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image", course.ImageName);
                    if (File.Exists(imagepath))
                    {
                        File.Delete(imagepath);
                    }

                    string thumbpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb", course.ImageName);
                    if (File.Exists(thumbpath))
                    {
                        File.Delete(thumbpath);
                    }

                }

                course.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(img.FileName);
               string imageNewpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image", course.ImageName);

                using (FileStream stream = new FileStream(imageNewpath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }

                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb", course.ImageName);


                ImageConvertor imgResizer = new ImageConvertor();
                imgResizer.Image_resize(imageNewpath, thumbPath, 200);
            }

            if (demo != null)
            {
                if (course.DemoFileName != null)
                {
                    string demopath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/demos", course.DemoFileName);
                    if (File.Exists(demopath))
                    {
                        File.Delete(demopath);
                    }


                }

                course.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(demo.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/demos",
                    course.ImageName);

                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    demo.CopyTo(stream);
                }
            }


            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public List<CourseEpisode> GetAllEpisodes(int CourseId)
        {
            return _context.CourseEpisodes.Where(e => e.CourseId == CourseId)
                .ToList();
        }

        public int AddEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            episode.EpisodeFileName = episodeFile.FileName;

            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Files",
                episode.EpisodeFileName);

            using (var stream = new FileStream(FilePath, FileMode.Create))
            {
                episodeFile.CopyTo(stream);
            }

            _context.CourseEpisodes.Add(episode);
            _context.SaveChanges();

            return episode.EpisodeId;
        }

        public bool IsExisTFileName(string filename)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Files",
                filename);
            
            return File.Exists(FilePath);
        }

        public CourseEpisode GetEpisodeById(int episodeId)
        {
            return _context.CourseEpisodes.Find(episodeId);
        }

        public void EditEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            if (episodeFile != null)
            {
                string DeleteFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Files",
                    episode.EpisodeFileName);

                File.Delete(DeleteFilePath);

                episode.EpisodeFileName = episodeFile.FileName;

                string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Files",
                    episode.EpisodeFileName);

                using (var stream = new FileStream(FilePath, FileMode.Create))
                {
                    episodeFile.CopyTo(stream);
                }
            }

            _context.CourseEpisodes.Update(episode);
            _context.SaveChanges();
        }

        public void DeleteEpisode(int EpisodeId)
        {
            var episode = GetEpisodeById(EpisodeId);

            string DeleteFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Files",
                episode.EpisodeFileName);

            File.Delete(DeleteFilePath);

            _context.CourseEpisodes.Remove(episode);
            _context.SaveChanges();
        }

        public Tuple<List<ShowCourseListItemViewModel>, int> GetCourseItem(int pageId = 1, int take = 6,
            string filter = "", string getType = "all", string @orderby = "date",
            int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null)
        {
            IQueryable<Course> result = _context.Courses;

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(c => c.CourseTitle.Contains(filter) || c.Tags.Contains(filter));
            }

            switch (getType)
            {
                case "all":
                    break;

                case "buy":
                {
                    result = result.Where(c => c.CoursePrice != 0);
                    break;
                }

                case "free":
                {
                    result = result.Where(c => c.CoursePrice == 0);
                    break;
                }
            }

            switch (orderby)
            {
                case "date":
                {
                    result = result.OrderByDescending(c => c.CreateDate);
                    break;
                }

                case "priceTop":
                {
                    result = result.OrderByDescending(c => c.CoursePrice);
                    break;
                    }

                case "priceLow":
                {
                    result = result.OrderBy(c => c.CoursePrice);
                    break;
                    }
            }

            if (startPrice>0)
            {
                result.Where(c => c.CoursePrice >= startPrice);
            }

            if (endPrice > 0)
            {
                result.Where(c => c.CoursePrice <= endPrice);
            }

            if (selectedGroups != null && selectedGroups.Any())
            {
                foreach (int groupId in selectedGroups)
                {
                    result = result.Where(c => c.GroupId == groupId || c.SubGroup == groupId);
                }
            }

            int skip = (pageId - 1) * take;

           

            var query= result.Include(c => c.CourseEpisodes).AsEnumerable().Select(c => new ShowCourseListItemViewModel()
            {
                CourseId = c.CourseId,
                CourseTitle = c.CourseTitle,
                ImageName = c.ImageName,
                Price = c.CoursePrice,
                TotalTime = new TimeSpan(c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
            });

            int pageCount=query.Count()/take;
            

            return Tuple.Create(query.Skip(skip).Take(take).ToList(), pageCount);
        }

        public Course GetCourseForShow(int courseId)
        {
            return _context.Courses.Include(c => c.CourseEpisodes)
                .Include(c => c.CourseStatus)
                .Include(c => c.CourseLevel)
                .Include(c => c.User)
                .Include(c=>c.UserCourses)
                .FirstOrDefault(c => c.CourseId == courseId);
        }

        public List<ShowCourseListItemViewModel> GetPopulerCourses()
        {

            return _context.Courses.Include(c => c.OrderDetails)
                .Where(c=>c.OrderDetails.Any())
                .OrderByDescending(c => c.OrderDetails.Count).Take(8).Include(c => c.CourseEpisodes)
                .AsEnumerable().Select(c => new ShowCourseListItemViewModel()
                {
                    CourseId = c.CourseId,
                    CourseTitle = c.CourseTitle,
                    ImageName = c.ImageName,
                    Price = c.CoursePrice,
                    TotalTime = new TimeSpan(c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
                }).ToList();

        }

        public bool IsCourseFree(int courseId)
        {
            return _context.Courses.Where(c => c.CourseId == courseId).Select(c => c.CoursePrice).First() == 0;
        }

        public List<CourseVote> GetVotes(int courseId)
        {
            return _context.CourseVotes.Where(v => v.CourseId == courseId).ToList();
        }

        public void AddVote(int userId, int courseId, bool vote)
        {
            var UserVote = _context.CourseVotes.SingleOrDefault(v => v.UserId == userId && v.CourseId == courseId);

            if (UserVote !=null)
            {
                UserVote.Vote = vote;
                _context.CourseVotes.Update(UserVote);
            }
            else
            {
                UserVote = new CourseVote()
                {
                    CourseId = courseId,
                    UserId = userId,
                    Vote = vote
                };
                _context.CourseVotes.Add(UserVote);
            }

            _context.SaveChanges();
        }

        public Tuple<int, int> GetCountVotes(int courseId)
        {
            var votes= _context.CourseVotes.Where(v => v.CourseId == courseId)
                .Select(v => v.Vote).ToList();

            return Tuple.Create(votes.Count(v => v), votes.Count(v => !v));
        }

        public void AddComment(CourseComments comment)
        {
            _context.CourseComments.Add(comment);
            _context.SaveChanges();
        }

        public Tuple<List<CourseComments>, int> GetAllComments(int courseId, int pageId = 1)
        {
            int take = 5;
            int skip = (pageId - 1) * take;
            int pageCount = _context.CourseComments.Count(c => c.CourseId == courseId && !c.IsDelete)/take;

            if ((pageCount%5)!=0)
            {
                pageCount += 1;
            }

            List<CourseComments> comments = _context.CourseComments.Include(c=> c.User)
                .Where(c => c.CourseId == courseId).OrderByDescending(c => c.DateTime).Skip(skip).Take(take).ToList();

            return Tuple.Create(comments, pageCount);
        }
   }
}
