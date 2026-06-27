using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityLostAndFound.Data;
using UniversityLostAndFound.Models;
[Authorize]
public class ItemReportsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ItemReportsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: ITEMREPORTS
    // GET: ITEMREPORTS
    public async Task<IActionResult> Index(string searchString)
    {
        // 1. نجلب كل التقارير من قاعدة البيانات أولاً
        var reports = from r in _context.ItemReports
                      select r;

        // 2. إذا كتب المستخدم كلمة في خانة البحث، نقوم بتصفية النتائج حسب العنوان (Title) أو الوصف (Description)
        if (!String.IsNullOrEmpty(searchString))
        {
            reports = reports.Where(s => s.Title.Contains(searchString) || s.Description.Contains(searchString));
        }

        // 3. نرسل النتائج المصفاة للواجهة
        return View(await reports.ToListAsync());
    }

    // GET: ITEMREPORTS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var itemreport = await _context.ItemReports
            .FirstOrDefaultAsync(m => m.ItemID == id);
        if (itemreport == null)
        {
            return NotFound();
        }

        return View(itemreport);
    }

    // GET: ITEMREPORTS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ITEMREPORTS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ItemID,Title,Description,ReportType,Location,DateReported,Status")] ItemReport itemreport)
    {
        if (ModelState.IsValid)
        {
            _context.Add(itemreport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(itemreport);
    }

    // GET: ITEMREPORTS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var itemreport = await _context.ItemReports.FindAsync(id);
        if (itemreport == null)
        {
            return NotFound();
        }
        return View(itemreport);
    }

    // POST: ITEMREPORTS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("ItemID,Title,Description,ReportType,Location,DateReported,Status")] ItemReport itemreport)
    {
        if (id != itemreport.ItemID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(itemreport);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemReportExists(itemreport.ItemID))
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
        return View(itemreport);
    }

    // GET: ITEMREPORTS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var itemreport = await _context.ItemReports
            .FirstOrDefaultAsync(m => m.ItemID == id);
        if (itemreport == null)
        {
            return NotFound();
        }

        return View(itemreport);
    }

    // POST: ITEMREPORTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var itemreport = await _context.ItemReports.FindAsync(id);
        if (itemreport != null)
        {
            _context.ItemReports.Remove(itemreport);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ItemReportExists(int? id)
    {
        return _context.ItemReports.Any(e => e.ItemID == id);
    }
}