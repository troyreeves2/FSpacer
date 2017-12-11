using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FSPACERFINAL.Data;
using FSPACERFINAL.Models;
using System.Diagnostics;

namespace FSPACERFINAL.Controllers
{
    public class SpacerCardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpacerCardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SpacerCards.Include(s => s.Drive).Include(s => s.Operator);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SpacerCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spacerCard = await _context.SpacerCards
                .Include(s => s.Drive)
                .Include(s => s.Operator)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (spacerCard == null)
            {
                return NotFound();
            }

            return View(spacerCard);
        }

        // GET: SpacerCards/Create
        public IActionResult Create()
        {
            ViewData["DriveNumber"] = new SelectList(_context.DriveCards, "DriveNumber", "DriveNumber");
            ViewData["OperatorID"] = new SelectList(_context.Operators, "ID", "ID");
            return View();
        }

        // POST: SpacerCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Active,HorizontalGearCaseDeviation,HorizontalCarrierDeviation,Bearing,HMDGear,HorizontalSpacerLength,VerticalGearCaseDeviation,VerticalCarrierDeviation,GearMount,VMDGear,VerticalSpacerLength,DriveNumber,OperatorID,Date,Backlash,HorizontalSetting,IntermediateSetting,OutputSetting,HelicalGearNumber,HelicalPinionNumber")] SpacerCard spacerCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spacerCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DriveNumber"] = new SelectList(_context.DriveCards, "DriveNumber", "DriveNumber", spacerCard.DriveNumber);
            ViewData["OperatorID"] = new SelectList(_context.Operators, "ID", "ID", spacerCard.OperatorID);
            return View(spacerCard);
        }

        // GET: SpacerCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spacerCard = await _context.SpacerCards.SingleOrDefaultAsync(m => m.ID == id);
            if (spacerCard == null)
            {
                return NotFound();
            }
            ViewData["DriveNumber"] = new SelectList(_context.DriveCards, "DriveNumber", "DriveNumber", spacerCard.DriveNumber);
            ViewData["OperatorID"] = new SelectList(_context.Operators, "ID", "ID", spacerCard.OperatorID);
            return View(spacerCard);
        }

        // POST: SpacerCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Active,HorizontalGearCaseDeviation,HorizontalCarrierDeviation,Bearing,HMDGear,HorizontalSpacerLength,VerticalGearCaseDeviation,VerticalCarrierDeviation,GearMount,VMDGear,VerticalSpacerLength,DriveNumber,OperatorID,Date,Backlash,HorizontalSetting,IntermediateSetting,OutputSetting,HelicalGearNumber,HelicalPinionNumber")] SpacerCard spacerCard)
        {
            if (id != spacerCard.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spacerCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpacerCardExists(spacerCard.ID))
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
            ViewData["DriveNumber"] = new SelectList(_context.DriveCards, "DriveNumber", "DriveNumber", spacerCard.DriveNumber);
            ViewData["OperatorID"] = new SelectList(_context.Operators, "ID", "ID", spacerCard.OperatorID);
            return View(spacerCard);
        }

        // GET: SpacerCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spacerCard = await _context.SpacerCards
                .Include(s => s.Drive)
                .Include(s => s.Operator)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (spacerCard == null)
            {
                return NotFound();
            }

            return View(spacerCard);
        }

        // POST: SpacerCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spacerCard = await _context.SpacerCards.SingleOrDefaultAsync(m => m.ID == id);
            _context.SpacerCards.Remove(spacerCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpacerCardExists(int id)
        {
            return _context.SpacerCards.Any(e => e.ID == id);
        }

        public IActionResult Back()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Back(SpacerCard spacerback) {

        //   float? Backlash = spacerback.Backlash;
        //   float? HorizontalSetting = spacerback.HorizontalSetting;
        //   float? IntermediateSetting = spacerback.IntermediateSetting;
        //   float? OutputSetting = spacerback.OutputSetting;
        //   string HelicalGearNumber = spacerback.HelicalGearNumber;
        //   string HelicalPinionNumber = spacerback.HelicalPinionNumber;

        //  return View();

        //}

        public IActionResult Front()
        {
            //var list = await _context.DriveCards.Model.ToListAsync();
            //var selectList = list.OrderBy(l => l.Model);
            //ViewData["ModelList"] = selectList;

            List<string> ModelsName = new List<string> { "F85", "F110", "F135", "F155", "F175" };
            ViewData["ModelList"] = new SelectList(ModelsName);

            List<string> GearsName = new List<string> { "BL1349", "BL3051", "BL3048", "BL4593", "BL4596" };
            ViewData["GearList"] = new SelectList(GearsName);


            ViewData["DriveNumber"] = new SelectList(_context.DriveCards, "DriveNumber", "DriveNumber");
            ViewData["OperatorID"] = new SelectList(_context.Operators, "ID", "ID");
            return View();

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Front([Bind("ID,Active,HorizontalGearCaseDeviation,HorizontalCarrierDeviation,Bearing,HMDGear,HorizontalSpacerLength,VerticalGearCaseDeviation,VerticalCarrierDeviation,GearMount,VMDGear,DriveNumber,OperatorID,Date")] SpacerCard spacerCard)
        {

            if (ModelState.IsValid)
            {
                _context.Add(spacerCard);
                Debug.WriteLine("Test" + spacerCard.DriveNumber);
                Console.WriteLine("Test" + spacerCard.DriveNumber);

                //spacerCard.

                spacerCard.VerticalSpacerLength = SpacerCard.GetVerticalSpacerLength(SpacerCard.ModelToInt("F85"),
                                                                                     spacerCard.VerticalGearCaseDeviation,
                                                                                     spacerCard.VerticalCarrierDeviation,
                                                                                     spacerCard.VMDGear,
                                                                                     spacerCard.GearMount);

                spacerCard.HorizontalSpacerLength = SpacerCard.GetHorizontalSpacerLength(SpacerCard.ModelToInt("F85"),
                                                                                       spacerCard.HorizontalGearCaseDeviation,
                                                                                       spacerCard.HorizontalCarrierDeviation,
                                                                                       spacerCard.Bearing,
                                                                                       spacerCard.HMDGear);

                await _context.SaveChangesAsync();

            }

            return RedirectToAction("Length", spacerCard);

            //ViewData["DriveNumber"] = new SelectList(_context.DriveCards, "DriveNumber", "DriveNumber", spacerCard.DriveNumber);
            //ViewData["OperatorID"] = new SelectList(_context.Operators, "ID", "ID", spacerCard.OperatorID);
            //return View(spacerCard);
        }

        //public async Task<IActionResult> Back(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var spacerCard = await _context.SpacerCards.SingleOrDefaultAsync(m => m.ID == id);
        //    if (spacerCard == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["DriveNumber"] = new SelectList(_context.DriveCards, "DriveNumber", "DriveNumber", spacerCard.DriveNumber);
        //    ViewData["OperatorID"] = new SelectList(_context.Operators, "ID", "ID", spacerCard.OperatorID);
        //    return View(spacerCard);
        //}

        // POST: SpacerCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Back(int id, [Bind("ID,Backlash,HorizontalSetting,IntermediateSetting,OutputSetting,HelicalGearNumber,HelicalPinionNumber")] SpacerCard spacerCard)
        //{
        //    if (id != spacerCard.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(spacerCard);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SpacerCardExists(spacerCard.ID))
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
        //    ViewData["DriveNumber"] = new SelectList(_context.DriveCards, "DriveNumber", "DriveNumber", spacerCard.DriveNumber);
        //    ViewData["OperatorID"] = new SelectList(_context.Operators, "ID", "ID", spacerCard.OperatorID);
        //    return View(spacerCard);
        //}

        public IActionResult Length(SpacerCard spacerCard)
        {
            return View(spacerCard);
        }



        //public IActionResult Front()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult CalculateLength()
        //{
        //    return View();
        //}
    }
}
