using DuAn1.Models;
using Microsoft.AspNetCore.Mvc;

namespace DuAn1.Controllers
{
    public class MemberController : Controller
    {
        // Danh sách thành viên
        public static readonly List<Member> members = new List<Member>()
        {
            new Member()
            {
                MemberId = Guid.NewGuid().ToString(),
                Username = "member1",
                Fullname = "Thành viên 1",
                Password = "123456",
                Email = "tv1@gmail.com"
            },

            new Member()
            {
                MemberId = Guid.NewGuid().ToString(),
                Username = "member2",
                Fullname = "Thành viên 2",
                Password = "123456",
                Email = "tv2@gmail.com"
            },

            new Member()
            {
                MemberId = Guid.NewGuid().ToString(),
                Username = "member3",
                Fullname = "Thành viên 3",
                Password = "123456",
                Email = "tv3@gmail.com"
            },

            new Member()
            {
                MemberId = Guid.NewGuid().ToString(),
                Username = "member4",
                Fullname = "Thành viên 4",
                Password = "123456",
                Email = "tv4@gmail.com"
            },

            new Member()
            {
                MemberId = Guid.NewGuid().ToString(),
                Username = "member5",
                Fullname = "Thành viên 5",
                Password = "123456",
                Email = "tv5@gmail.com"
            }
        };


        // =====================================================
        // 1. ĐƯA OBJECT MEMBER RA VIEW
        // =====================================================

        // GET: /Member
        public IActionResult Index()
        {
            // Tạo đối tượng Member
            Member member = new Member();

            // Gán dữ liệu
            member.MemberId = Guid.NewGuid().ToString();
            member.Username = "trinhvanchung";
            member.Fullname = "Trịnh Văn Chung";
            member.Password = "password";
            member.Email = "chungtrinhvan@gmail.com";

            // Đưa object member sang View
            return View(member);
        }


        // =====================================================
        // 2. ĐƯA LIST MEMBER RA VIEW
        // =====================================================

        // GET: /Member/GetMembers
        public IActionResult GetMembers()
        {
            // Đưa danh sách members sang View
            ViewBag.members = members;

            return View();
        }


        // =====================================================
        // 3. HIỂN THỊ FORM CREATE
        // =====================================================

        // GET: /Member/Create
        public IActionResult Create()
        {
            return View();
        }


        // =====================================================
        // 4. NHẬN DỮ LIỆU TỪ FORM CREATE
        // =====================================================

        [HttpPost]
        public IActionResult Create(Member member)
        {
            // Tạo mã thành viên tự động
            member.MemberId = Guid.NewGuid().ToString();

            // Thêm thành viên vào danh sách
            members.Add(member);

            // Quay lại trang danh sách
            return RedirectToAction("GetMembers");
        }
    }
}