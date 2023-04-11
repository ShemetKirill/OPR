using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseOPR.Database;
using CourseOPR.Models;
using Microsoft.AspNetCore.Authorization;
using CourseOPR.Servises;
using static System.Formats.Asn1.AsnWriter;

namespace CourseOPR.Controllers
{
    [Authorize]
    public class ScoresController : Controller
    {
        private readonly ApplicationContext _context;
        public Parser parser= new Parser();
        string path = "D:\\C#\\text.txt";

        public ScoresController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Scores
        public async Task<IActionResult> Index()
        {
            
            var applicationContext = _context.Score.Include(s => s.Student).Include(s => s.Subject);
            return View(await applicationContext.ToListAsync());
        }


        public  IActionResult SelectStudent()
        {
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentFullName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> SelectStudent([Bind("StudentId,StudentFullName")] Student student)
        {
            var studentId = student.StudentId;
            return Redirect("ViewScoreStudent/" + studentId.ToString());
        }

        public async Task<IActionResult> ViewScoreStudent(int? id)
        {
            if (id == null || _context.Score == null)
            {
                return NotFound();
            }
            var student = await _context.Student.FirstOrDefaultAsync(c=>c.StudentId==id);
            var score = _context.Score.Where(u => u.StudentId == student.StudentId).Include(s=>s.Student)
                .Include(s=>s.Subject);
            if (score == null)
            {
                return NotFound();
            }
            return View(score);

        }


        // GET: Scores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Score == null)
            {
                return NotFound();
            }

            var score = await _context.Score
                .Include(s => s.Student)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.ScoreId == id);
            if (score == null)
            {
                return NotFound();
            }

            return View(score);
        }

        // GET: Scores/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentFullName");
            ViewData["SubjectId"] = new SelectList(_context.Subject, "SubjectId", "SubjectName");
            return View();
        }


        public IActionResult Parse()
        {
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentFullName");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Parse([Bind("ScoreId,ScoreValue,StudentId,SubjectId")] Score score) 
        {
            //var speciality = _context.Speciality(x => x.Speciality)
            //    .Include(c => c.Student)
            //    .Include(c => c.Subject).ToList();


            var student = await _context.Student.FirstOrDefaultAsync(s => s.StudentId == score.StudentId);
            var group = await _context.Group.FirstOrDefaultAsync(c=>c.GroupId==student.GroupId);
            var subject = _context.Subject.Where(c=>c.SpecialityId==group.SpecialityId).ToList();

            var parseScore = parser.ArrayParse(path);
            for (int i=0;i<subject.Count();i++)
            {
                score.ScoreId = 0;
                score.SubjectId = subject[i].SubjectId;
                score.ScoreValue = parseScore[i].ToString();
                _context.Add(score);
                await _context.SaveChangesAsync();
            }        
            score.Student= student;
            score.StudentId = student.StudentId;
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentFullName", score.StudentId);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        // POST: Scores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScoreId,ScoreValue,StudentId,SubjectId")] Score score)
        {
            if (ModelState.IsValid)
            {
                _context.Add(score);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentId", score.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subject, "SubjectId", "SubjectId", score.SubjectId);
            return View(score);
        }

        // GET: Scores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Score == null)
            {
                return NotFound();
            }

            var score = await _context.Score.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentFullName", score.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subject, "SubjectId", "SubjectName", score.SubjectId);
            return View(score);
        }

        // POST: Scores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScoreId,ScoreValue,StudentId,SubjectId")] Score score)
        {
            if (id != score.ScoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(score);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScoreExists(score.ScoreId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentId", score.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subject, "SubjectId", "SubjectId", score.SubjectId);
            return View(score);
        }

        // GET: Scores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Score == null)
            {
                return NotFound();
            }

            var score = await _context.Score
                .Include(s => s.Student)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.ScoreId == id);
            if (score == null)
            {
                return NotFound();
            }

            return View(score);
        }

        // POST: Scores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Score == null)
            {
                return Problem("Entity set 'ApplicationContext.Score'  is null.");
            }
            var score = await _context.Score.FindAsync(id);
            if (score != null)
            {
                _context.Score.Remove(score);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScoreExists(int id)
        {
          return (_context.Score?.Any(e => e.ScoreId == id)).GetValueOrDefault();
        }
    }
}
