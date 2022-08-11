using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TopLearn.core.Services.Intefaces;
using TopLearn.Datalayar.Entities.Course;
using SharpCompress.Archives;

namespace TopLearn.Web.Controllers
{
    public class CourseController : Controller
    {
        private ICourseService _courseService;
        private IUserService _userService;
        private IOrderService _orderService;

        public CourseController(ICourseService courseService, IUserService userService, IOrderService orderService)
        {
            _courseService = courseService;
            _userService = userService;
            _orderService = orderService;
        }

        public IActionResult Index(int pageId = 1, int take = 6,
            string filter = "", string getType = "all", string @orderby = "date",
            int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null)
        {
            ViewData["Groups"] = _courseService.GetAllGroup();
            ViewBag.selectedGroups = selectedGroups;
            ViewBag.pageId = pageId;
            ViewBag.orderby = orderby;
            ViewBag.getType = getType;

            return View(_courseService.GetCourseItem(pageId,9,filter,getType,orderby,
                startPrice,endPrice,selectedGroups));
        }

        [Route("ShowCourse/{id}")]
        public IActionResult ShowCourse(int id,int episodeId=0)
        {
            Course course = _courseService.GetCourseForShow(id);

            if (course == null)
                return NotFound();

            if (episodeId !=0 && User.Identity.IsAuthenticated)
            {
                if (course.CourseEpisodes.All(ep=>ep.EpisodeId!=episodeId))
                {
                    return NotFound();
                }

                if (!course.CourseEpisodes.First(ep=>ep.EpisodeId==episodeId).IsFree)
                {
                    if (!_orderService.IsUserBuyCourse(User.Identity.Name,id))
                    {
                        return NotFound();
                    }
                }

                #region Show Online

                CourseEpisode episode = course.CourseEpisodes.First(ep => ep.EpisodeId == episodeId);

                ViewBag.Episode = episode;

                string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/CourseFileOnline",
                    episode.EpisodeFileName.Replace(".rar", ".mp4"));

                if (!System.IO.File.Exists(filepath))
                {
                    string targetpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/CourseFileOnline");

                    string rarpath= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Files",
                        episode.EpisodeFileName);
                    var archive = ArchiveFactory.Open(rarpath);

                    var Entries = archive.Entries.OrderByDescending(x => x.Key.Length);

                    foreach (var item in Entries)
                    {
                        if (Path.GetExtension(item.Key)==".mp4")
                        {
                            item.WriteTo(System.IO.File.Create(Path.Combine(targetpath,episode.EpisodeFileName.Replace(".rar",".mp4"))));
                        }
                    }
                }

                ViewBag.FilePath = episode.EpisodeFileName.Replace(".rar",".mp4");

                #endregion
            }

            return View(course);
        }


        [Route("BuyCourse/{id}")]
        [Authorize]
        public IActionResult BuyCourse(int id)
        {

          int orderId=_orderService.AddOrder(User.Identity.Name, id);

          return  Redirect("/UserPanel/order/ShowOrder/" + orderId);
        }

        [Route("Download/{episodeId}")]
        public IActionResult Download(int episodeId)
        {
            var episode = _courseService.GetEpisodeById(episodeId);
          
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/Files",
                episode.EpisodeFileName);

            string fileName = episode.EpisodeFileName;

            if (episode.IsFree)
            {
                byte[] file = System.IO.File.ReadAllBytes(filePath);
                return File(file, "application/force-download", fileName);
            }

            if (User.Identity.IsAuthenticated)
            {
                if (_orderService.IsUserBuyCourse(User.Identity.Name,episode.CourseId))
                {
                    byte[] file = System.IO.File.ReadAllBytes(filePath);
                    return File(file, "application/force-download", fileName);
                }
            }

            return Forbid();
        }

        [HttpPost]
        public IActionResult Addcomment(CourseComments comment)
        {
            comment.UserId = _userService.GetUserIdByUsername(User.Identity.Name);
            comment.DateTime=DateTime.Now;
            comment.IsDelete = false;
            _courseService.AddComment(comment);

            return View("ShowComment",_courseService.GetAllComments(comment.CourseId));
        }

        public IActionResult ShowComment(int id, int pageId = 1)
        {
            //ViewBag.pageId = pageId;
            return View(_courseService.GetAllComments(id, pageId));
        }

        public IActionResult CourseVote(int id)
        {
            if (!_courseService.IsCourseFree(id))
            {
                if (!_orderService.IsUserBuyCourse(User.Identity.Name,id))
                {
                    ViewBag.NotAccess = true;
                    return PartialView(_courseService.GetCountVotes(id));
                }
            }

            return PartialView(_courseService.GetCountVotes(id));
        }

        [Authorize]
        public IActionResult AddVote(int id, bool vote)
        {
            int userId = _userService.GetUserIdByUsername(User.Identity.Name);

            _courseService.AddVote(userId,id,vote);

            return PartialView("CourseVote",_courseService.GetCountVotes(id));
        }
    }
}
