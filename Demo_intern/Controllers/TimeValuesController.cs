using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demo_intern.Data;
using static Demo_intern.Controllers.Helper;

namespace Demo_intern.Controllers
{
    public class TimeValuesController : Controller
    {
        private readonly DemoDbContext _context;

        public TimeValuesController(DemoDbContext context)
        {
            _context = context;
        }

        // GET: TimeValues
        public async Task<IActionResult> Index()
        {
              return View(await _context.TimeValues.ToListAsync());
        }


        // GET: TimeValues/AddOrEdit(Insert)
        // GET: TimeValues/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> CreateOrUpdate(Guid? id)
        {
            if (id == null)
                return View(new TimeValue());
            else
            {
                var timevalueModel = await _context.TimeValues.FindAsync(id);
                if (timevalueModel == null)
                {
                    return NotFound();
                }
                return View(timevalueModel);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrUpdate(Guid id, [Bind("Id, Value")] TimeValue timevalue)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == null)
                {
                    timevalue.Time = DateTime.Now;
                    _context.Add(timevalue);
                    await _context.SaveChangesAsync();

                }
                //Update
                else
                {
                    try
                    {
                        _context.Update(timevalue);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TimeValueExists(timevalue.Id))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.TimeValues.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrUpdate", timevalue) });
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timevalueModel = await _context.TimeValues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timevalueModel == null)
            {
                return NotFound();
            }

            return View(timevalueModel);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var timevalueModel = await _context.TimeValues.FindAsync(id);
            _context.TimeValues.Remove(timevalueModel);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.TimeValues.ToList()) });
        }

        //// GET: TimeValues/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null || _context.TimeValues == null)
        //    {
        //        return NotFound();
        //    }

        //    var timeValue = await _context.TimeValues
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (timeValue == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(timeValue);
        //}

        //// GET: TimeValues/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: TimeValues/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Value")] TimeValue timeValue)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        timeValue.Id = Guid.NewGuid();
        //        timeValue.Time= DateTime.Now;
        //        _context.Add(timeValue);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(timeValue);
        //}

        //// GET: TimeValues/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null || _context.TimeValues == null)
        //    {
        //        return NotFound();
        //    }

        //    var timeValue = await _context.TimeValues.FindAsync(id);
        //    if (timeValue == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(timeValue);
        //}

        //// POST: TimeValues/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Id, Value")] TimeValue timeValue)
        //{
        //    if (id != timeValue.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            timeValue.Time= DateTime.Now;
        //            _context.Update(timeValue);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TimeValueExists(timeValue.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(timeValue);
        //}

        //// GET: TimeValues/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null || _context.TimeValues == null)
        //    {
        //        return NotFound();
        //    }

        //    var timeValue = await _context.TimeValues
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (timeValue == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(timeValue);
        //}

        //// POST: TimeValues/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    if (_context.TimeValues == null)
        //    {
        //        return Problem("Entity set 'DemoDbContext.TimeValues'  is null.");
        //    }
        //    var timeValue = await _context.TimeValues.FindAsync(id);
        //    if (timeValue != null)
        //    {
        //        _context.TimeValues.Remove(timeValue);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool TimeValueExists(Guid id)
        {
          return _context.TimeValues.Any(e => e.Id == id);
        }
    }
}
